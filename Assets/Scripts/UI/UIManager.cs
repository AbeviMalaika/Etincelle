using Oculus.Interaction;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

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

    private void Update()
    {
        //Uniquement lorsque la partie est en cours
        if (GameManager.Instance.sceneActuelle.name == "ScenePartie")
        {
            if (GameManager.enPause)
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

    public void GestionAffichagePause()
    {
        if (!GameManager.enPause)
        {
            HideUI(menuPause);

            if (options.activeSelf) HideUI(options);
            if (pause.activeSelf) HideUI(pause);

            Invoke("PopUpHUD", 1f);
        }
        else if (GameManager.enPause)
        {
            ShowUI(menuPause);
            ShowUI(pause);
        }
    }

    public void AfficherQueteUI(Quest quete)
    {
        // Pour chaque titre de quête qu'on veut modifier, on passe au travers d'un tableau de titre (HUD et pause)
        foreach (var t in titresQuetes)
        {
            t.text = quete.titre;
        }

        // Pour chaque description d'objectif qu'on veut modifier, on passe au travers d'un tableau de titre (HUD et pause)
        foreach (var t in titresObjectif)
        {
            t.text = quete.listeObjectif[quete.listeObjectif.Find(q => q.objectifID == quete.progressionActuelle).objectifID].titre;
        }

        //hud.GetComponent<Animator>().SetTrigger("popUp");
        PopUpHUD();
    }

    public void PopUpHUD()
    {
        if (popupHUDCoroutine != null)
            StopCoroutine(popupHUDCoroutine);

        popupHUDCoroutine = StartCoroutine(corou_PopUpHUD());
    }

    //Coroutine pour le pop up du HUD
    IEnumerator corou_PopUpHUD()
    {
        hud.SetActive(true);

        CanvasGroup cg = hud.GetComponent<CanvasGroup>();
        cg.interactable = true;

        float duration = 0.5f;

        // FADE IN
        float time = 0f;
        while (time < duration)
        {
            time += Time.deltaTime;
            cg.alpha = Mathf.Lerp(0f, 1f, time / duration);
            yield return null;
        }

        cg.alpha = 1f;

        yield return new WaitForSeconds(5f);

        // FADE OUT
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

    //Fonction pour changer de UI à faire disparaitre
    public void ShowUI(GameObject UI)
    {
        if (UI == null) return;

        if (runningUI.ContainsKey(UI) && runningUI[UI] != null)
            StopCoroutine(runningUI[UI]);

        runningUI[UI] = StartCoroutine(corou_ShowUI(UI));
    }

    //Fonction pour changer de UI à afficher
    public void HideUI(GameObject UI)
    {
        if (UI == null) return;

        if (runningUI.ContainsKey(UI) && runningUI[UI] != null)
            StopCoroutine(runningUI[UI]);

        runningUI[UI] = StartCoroutine(corou_HideUI(UI));
    }

    //Fonction pour changer de UI à faire disparaitre
    public void ShowUIDelay(GameObject UI)
    {
        StartCoroutine(corou_ShowUIDelay(UI));
    }

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

    // Un autre enumerator dans le cas ou je veux un délai avant l'apparition du UI
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

    //Fonction pour quit la game
    public void QuitterJeu()
    {
        Invoke("Quit", 5f);
    }

    void Quit() => Application.Quit();

    public void OuvrirLien(string hyperlien) => Application.OpenURL(hyperlien);
}