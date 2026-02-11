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

    public Transform targetFin;
    public GameObject hud;
    public GameObject UIFin;
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

    private void Start()
    {
        GameManager.enPause = false;
    }

    private void Update()
    {
        //pokeInteraction.SetActive(GameManager.enPause);
        //pointableCanvas.enabled = GameManager.enPause;
        rayInteractable.enabled = GameManager.enPause;
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

    IEnumerator corou_ShowUI(GameObject UI)
    {
        yield return new WaitForSeconds(1f);
        UI.GetComponent<CanvasGroup>().alpha = 1;
        UI.SetActive(true);
        UI.GetComponent<Animator>().SetTrigger("show");
        UI.GetComponent<CanvasGroup>().interactable = true;
        yield return null;
    }

    IEnumerator corou_HideUI(GameObject UI)
    {
        UI.GetComponent<Animator>().SetTrigger("hide");
        UI.GetComponent<CanvasGroup>().interactable = false;
        yield return new WaitForSeconds(1f);
        UI.GetComponent<CanvasGroup>().alpha = 0;
        UI.SetActive(false);
        yield return null;
    }


    public void Quit() => Application.Quit();

    public void OuvrirLien(string hyperlien) => Application.OpenURL(hyperlien);
}
