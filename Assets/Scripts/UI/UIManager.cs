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
    //public GameObject UIFin;
    //public GameObject pokeInteraction;
    //public PointableCanvas pointableCanvas;
    public RayInteractable rayInteractable;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Update()
    {
        //Uniquement lorsque la partie est en cours
        if (GameManager.Instance.sceneActuelle.name == "ScenePartie")
        {
            //Rendre possible l'interactivité avec le UI
            rayInteractable.enabled = GameManager.enPause;

            //Désactiver l'HUD
            hud.SetActive(!GameManager.enPause);

            //Afficher le menu pause
            if (GameManager.Instance.gestureDone) ShowUI(menuPause);

            //menuPause.SetActive(GameManager.enPause);
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

        hud.GetComponent<Animator>().SetTrigger("popUp");
    }

    //Fonction pour quit la game
    public void QuitterJeu()
    {
        Invoke("Quit", 5f);
    }

    //Fonction pour changer de UI à faire disparaitre
    public void ShowUI(GameObject UI)
    {
        StartCoroutine(corou_ShowUI(UI));
    }

    //Fonction pour changer de UI à afficher
    public void HideUI(GameObject UI)
    {
        StartCoroutine(corou_HideUI(UI));
    }

    //Fonction pour changer de UI à faire disparaitre
    public void ShowUIDelay(GameObject UI)
    {
        StartCoroutine(corou_ShowUIDelay(UI));
    }

    IEnumerator corou_ShowUI(GameObject UI)
    {
        //yield return new WaitForSeconds(1f);
        UI.GetComponent<Animator>().enabled = true;
        UI.SetActive(true);

        UI.GetComponent<Animator>().SetTrigger("show");
        UI.GetComponent<CanvasGroup>().interactable = true;
        yield return new WaitForSeconds(0.5f);

        UI.GetComponent<Animator>().enabled = false;

        UI.GetComponent<CanvasGroup>().alpha = 1;
        yield return null;
    }

    // Un autre enumerator dans le cas ou je veux un délai avant l'apparition du UI
    IEnumerator corou_ShowUIDelay(GameObject UI)
    {
        yield return new WaitForSeconds(1f);
        UI.GetComponent<Animator>().enabled = true;
        UI.SetActive(true);

        UI.GetComponent<Animator>().SetTrigger("show");
        UI.GetComponent<CanvasGroup>().interactable = true;
        yield return new WaitForSeconds(0.5f);

        UI.GetComponent<Animator>().enabled = false;

        UI.GetComponent<CanvasGroup>().alpha = 1;
        yield return null;
    }

    IEnumerator corou_HideUI(GameObject UI)
    {
        UI.GetComponent<Animator>().enabled = true;

        UI.GetComponent<Animator>().SetTrigger("hide");
        UI.GetComponent<CanvasGroup>().interactable = false;
        yield return new WaitForSeconds(2.5f);

        UI.GetComponent<Animator>().enabled = false;

        UI.GetComponent<CanvasGroup>().alpha = 0;
        UI.SetActive(false);
        yield return null;
    }

    void Quit() => Application.Quit();

    public void OuvrirLien(string hyperlien) => Application.OpenURL(hyperlien);
}