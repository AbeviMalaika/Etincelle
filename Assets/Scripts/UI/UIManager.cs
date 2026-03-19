/***
 * 
 * ÉTINCELLE
 * 
 * Par Malaïka Abevi
 * Dernière modification : 06/03/2026 
 * 
 */


using Oculus.Interaction;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// Gestionnaire pour le UI. S'occupe des tâches principales pour l'affichage des quêtes et des menus
/// </summary>
public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public List<TextMeshProUGUI> titresQuetes = new List<TextMeshProUGUI>();
    public List<TextMeshProUGUI> titresObjectif = new List<TextMeshProUGUI>();

    public GameObject hud;
    public GameObject menuPause;

    public GameObject options;
    public GameObject pause;
    public RayInteractable rayInteractable;

    Dictionary<GameObject, Coroutine> runningUI = new Dictionary<GameObject, Coroutine>();
    Coroutine popupHUDCoroutine;

    /// <summary>
    /// Initialise le singleton du UIManager.
    /// </summary>
    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    /// <summary>
    /// Gère l'état du HUD, du menu pause et de l'interaction avec le UI selon l'état du jeu.
    /// </summary>
    private void Update()
    {
        //Uniquement lorsque la partie est en cours
        if (GameManager.Instance.sceneActuelle.name == "ScenePartie")
        {
            if (GameManager.Instance.enPause)
            {
                //Rendre possible l'interactivité avec le UI
                rayInteractable.enabled = true;

                //Désactiver l'HUD
                if (hud.activeSelf)
                    hud.SetActive(false);
            }
            else
            {
                //Rendre possible l'interactivité avec le UI
                rayInteractable.enabled = false;

                //Activer l'HUD
                if (!hud.activeSelf)
                    hud.SetActive(true);
            }

            if (GameManager.Instance.gestureDone)
            {
                GestionAffichagePause();

                GameManager.Instance.gestureDone = false; // empêche plusieurs déclenchements
            }
        }
    }

    /// <summary>
    /// Gère l'affichage ou la fermeture du menu pause et des options.
    /// </summary>
    public void GestionAffichagePause()
    {
        if (!GameManager.Instance.enPause)
        {
            HideUI(menuPause);

            if (options.activeSelf) HideUI(options);
            if (pause.activeSelf) HideUI(pause);

            Invoke("PopUpHUD", 1f);
        }
        else if (GameManager.Instance.enPause)
        {
            ShowUI(menuPause);
            ShowUI(pause);
        }
    }

    /// <summary>
    /// Met à jour le texte des quêtes et objectifs dans le UI.
    /// </summary>
    public void AfficherQueteUI(Quest quete)
    {
        foreach (var t in titresQuetes)
        {
            t.text = quete.titre;
        }

        if (quete.progressionActuelle < quete.listeObjectif.Count)
        {
            foreach (var t in titresObjectif)
            {
                t.text = quete.listeObjectif[quete.progressionActuelle].titre;
            }
        }

        PopUpHUD();
    }

    /// <summary>
    /// Lance la coroutine qui affiche temporairement le HUD.
    /// </summary>
    public void PopUpHUD()
    {
        if (popupHUDCoroutine != null)
            StopCoroutine(popupHUDCoroutine);

        popupHUDCoroutine = StartCoroutine(corou_PopUpHUD());
    }

    /// <summary>
    /// Coroutine qui fait apparaître puis disparaître le HUD avec un effet de fondu.
    /// </summary>
    IEnumerator corou_PopUpHUD()
    {
        hud.SetActive(true);

        CanvasGroup cg = hud.GetComponent<CanvasGroup>();
        cg.interactable = true;

        float duration = 0.5f;

        float time = 0f;
        while (time < duration)
        {
            time += Time.deltaTime;
            cg.alpha = Mathf.Lerp(0f, 1f, time / duration);
            yield return null;
        }

        cg.alpha = 1f;

        yield return new WaitForSeconds(5f);

        time = 0f;
        while (time < duration)
        {
            time += Time.deltaTime;
            cg.alpha = Mathf.Lerp(1f, 0f, time / duration);
            yield return null;
        }

        cg.alpha = 0f;

        yield return null;
    }

    /// <summary>
    /// Lance l'animation d'apparition d'un élément UI.
    /// </summary>
    public void ShowUI(GameObject UI)
    {
        if (UI == null) return;

        if (runningUI.ContainsKey(UI) && runningUI[UI] != null)
            StopCoroutine(runningUI[UI]);

        runningUI[UI] = StartCoroutine(corou_ShowUI(UI));
    }

    /// <summary>
    /// Lance l'animation de disparition d'un élément UI.
    /// </summary>
    public void HideUI(GameObject UI)
    {
        if (UI == null) return;

        if (runningUI.ContainsKey(UI) && runningUI[UI] != null)
            StopCoroutine(runningUI[UI]);

        runningUI[UI] = StartCoroutine(corou_HideUI(UI));
    }

    /// <summary>
    /// Lance l'apparition d'un élément UI après un délai.
    /// </summary>
    public void ShowUIDelay(GameObject UI)
    {
        StartCoroutine(corou_ShowUIDelay(UI));
    }

    /// <summary>
    /// Coroutine qui fait apparaître un élément UI avec un fondu.
    /// </summary>
    IEnumerator corou_ShowUI(GameObject UI)
    {
        UI.SetActive(true);

        CanvasGroup cg = UI.GetComponent<CanvasGroup>();
        cg.interactable = true;

        float duration = 0.5f;
        float time = 0f;

        float startAlpha = cg.alpha;
        float targetAlpha = 1f;

        while (time < duration)
        {
            time += Time.deltaTime;
            cg.alpha = Mathf.Lerp(startAlpha, targetAlpha, time / duration);
            yield return null;
        }

        cg.alpha = 1f;

        runningUI.Remove(UI);

        yield return null;
    }

    /// <summary>
    /// Coroutine qui affiche un élément UI après un délai avec un effet de fondu.
    /// </summary>
    IEnumerator corou_ShowUIDelay(GameObject UI)
    {
        yield return new WaitForSeconds(1f);

        UI.SetActive(true);

        CanvasGroup cg = UI.GetComponent<CanvasGroup>();
        cg.interactable = true;

        float duration = 0.5f;
        float time = 0f;

        float startAlpha = cg.alpha;
        float targetAlpha = 1f;

        while (time < duration)
        {
            time += Time.deltaTime;
            cg.alpha = Mathf.Lerp(startAlpha, targetAlpha, time / duration);
            yield return null;
        }

        cg.alpha = 1f;

        yield return null;
    }

    /// <summary>
    /// Coroutine qui fait disparaître un élément UI avec un fondu.
    /// </summary>
    IEnumerator corou_HideUI(GameObject UI)
    {
        CanvasGroup cg = UI.GetComponent<CanvasGroup>();
        cg.interactable = false;

        float duration = 0.5f;
        float time = 0f;

        float startAlpha = cg.alpha;
        float targetAlpha = 0f;

        while (time < duration)
        {
            time += Time.deltaTime;
            cg.alpha = Mathf.Lerp(startAlpha, targetAlpha, time / duration);
            yield return null;
        }

        cg.alpha = 0f;

        UI.SetActive(false);

        runningUI.Remove(UI);

        yield return null;
    }

    /// <summary>
    /// Lance la fermeture du jeu après un délai.
    /// </summary>
    public void QuitterJeu()
    {
        Invoke("Quit", 5f);
    }

    /// <summary>
    /// Ferme l'application.
    /// </summary>
    void Quit() => Application.Quit();

    /// <summary>
    /// Ouvre un lien web dans le navigateur.
    /// </summary>
    public void OuvrirLien(string hyperlien) => Application.OpenURL(hyperlien);
}