/***
 * 
 * ÉTINCELLE
 * 
 * Par Malaïka Abevi
 * Dernière modification : 15/03/2026 
 * 
 */

using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Gestionnaire principal du jeu (Singleton) qui centralise :
/// - Le contrôle des scènes et transitions avec fondu (fade in/out)
/// - La détection des gestes pour la pause
/// - La gestion de la fin de partie et de l'activation du portail final
/// - L'initialisation des quêtes et de l'interface utilisateur
/// - Le contrôle visuel de la sphère de transition
/// </summary>
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] Quest_1 quest1;

    [Header("Paramètres de base")]
    public bool enPause;
    public Scene sceneActuelle;

    [Header("Gestion du UI")]
    OVRHand.MicrogestureType microGesture;
    public OVRHand OvrHand;
    public event Action OnPauseGesture;
    public bool gestureDone;
    public GameObject MenuPrincipalUI;
    public GameObject ConteneurMenuPrincipalUI;

    [Header("Gestion de la fin de partie")]
    public Animator portail;
    public RuntimeAnimatorController animatorPortailFin;
    public Animator cristauxChemin;
    public GameObject chambreDummy; //Représente la chambre fictive pour le reflet du portail
    public AudioClip musiqueFin;

    public float tempsDelaiFin; //Délai avant la gestion complète de la fin
    public GameObject UIFin;
    public GameObject BtnUIFin;
    
    public bool finPartie;
    public bool desactivationUI; 
    public bool gestionFinFait;

    /// <summary>
    /// Initialise le GameManager en tant que Singleton.
    /// Configure certaines valeurs selon la scène actuelle
    /// et affiche l'interface du menu principal si nécessaire.
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

        sceneActuelle = SceneManager.GetActiveScene();

        if (sceneActuelle.buildIndex == 0)
        {
            if (UIManager.Instance != null)
            {
                UIManager.Instance.ShowUI(MenuPrincipalUI);
                UIManager.Instance.ShowUI(ConteneurMenuPrincipalUI);
            }
        }

        if (sceneActuelle.buildIndex == 1)
        {
            enPause = false;
        }
    }

    /// <summary>
    /// Initialise les éléments dépendants de la scène.
    /// Lance la première quête et applique le calibrage de session
    /// si celui-ci est disponible.
    /// </summary>
    void Start()
    {
        if (sceneActuelle.buildIndex == 1)
        {
            // On démarre la première quête
            QuestManager.Instance.DemarrerQuest("1");
            quest1.enabled = true;

            //if (SessionData.calibrage != null)
            //{
            //    transform.position += SessionData.calibrage.positionOffset;
            //    transform.rotation = SessionData.calibrage.rotationOffset * transform.rotation;
            //}
        }

        //Fade In avec le script de fade
        VRFade.Instance.ChangerCouleurFade(Color.black);
        VRFade.Instance.FadeIn(2.5f);
    }

    /// <summary>
    /// Vérifie les gestes de la main pour gérer la pause du jeu
    /// et déclenche la gestion de fin de partie si nécessaire.
    /// </summary>
    void Update()
    {
        if (sceneActuelle.buildIndex == 1)
        {
            if (OvrHand != null && !finPartie && !desactivationUI)
            {
                microGesture = OvrHand.GetMicrogestureType();

                if (microGesture == OVRHand.MicrogestureType.SwipeRight && !gestureDone)
                {
                    enPause = !enPause;
                    gestureDone = true;

                    OnPauseGesture?.Invoke();
                }
            }
        }

        if (finPartie && !gestionFinFait)
        {
            gestionFinFait = true;
            GestionFinPartie();
        }
    }

    /// <summary>
    /// Inverse l'état de pause du jeu.
    /// </summary>
    public void SetPause()
    {
        enPause = !enPause;
    }

    /// <summary>
    /// Lance le chargement asynchrone d'une scène via une coroutine.
    /// </summary>
    /// <param name="indexScene">Index de la scène à charger dans le Build Settings.</param>
    public void ChargerScene(int indexScene)
    {
        StartCoroutine(ChargementAsyncScene(indexScene));
    }

    /// <summary>
    /// Gère le chargement asynchrone d'une scène avec un fondu de transition.
    /// Empêche l'activation de la scène tant que le chargement n'est pas terminé.
    /// </summary>
    /// <param name="indexScene">Index de la scène à charger.</param>
    IEnumerator ChargementAsyncScene(int indexScene)
    {
        //Fade Out avec le script de fade
        VRFade.Instance.FadeOut(2.5f);

        //On attend le temps que l'animation dure
        yield return new WaitForSeconds(2.5f);

        //On demande le chargement de la scène
        AsyncOperation scene = SceneManager.LoadSceneAsync(indexScene);

        //On ne veut pas que la prochaine scène s'affiche tant qu'elle n'est pas entièrement chargée
        scene.allowSceneActivation = false;

        // On attend que la scène soit chargée à 90%
        while (scene.progress < 0.9f)
        {
            Debug.Log("Chargement... : " + scene.progress);
            yield return null;
        }

        Debug.Log("La scène est chargée");

        //Activation de la scène
        scene.allowSceneActivation = true;

        yield return new WaitForEndOfFrame();
        yield return null;
    }

    /// <summary>
    /// Configure les éléments visuels de la scène finale
    /// et déclenche l'animation de formation du chemin.
    /// </summary>
    public void SetDecoFin()
    {
        portail.runtimeAnimatorController = animatorPortailFin;

        cristauxChemin.SetTrigger("formation");

        chambreDummy.SetActive(true);

        Invoke("ActiverPortail", 3f);
    }

    /// <summary>
    /// Active l'animation d'ouverture du portail final.
    /// </summary>
    public void ActiverPortail()
    {
        portail.SetTrigger("flip");
    }

    /// <summary>
    /// Lance la séquence de fin de partie.
    /// </summary>
    public void GestionFinPartie()
    {
        StartCoroutine(corou_GestionFinPartie());
    }

    /// <summary>
    /// Coroutine qui gère la séquence finale :
    /// fondu, affichage de l'interface de fin et activation du bouton final.
    /// </summary>
    IEnumerator corou_GestionFinPartie()
    {
        finPartie = true;
        //On veut pouvoir laisser un peu de temps pour le dialogue du personnage avec son ami
        yield return new WaitForSeconds(tempsDelaiFin);

        enPause = true;

        //Puis on commence la transition vers la fin

        //Démarrer la musique de fin
        AudioManager.Instance.ChangementMusique(musiqueFin);

        yield return new WaitForSeconds(5.5f);

        //On remet le fade en noir
        VRFade.Instance.ChangerCouleurFade(Color.black);
        //Fade out avec le script de fade
        VRFade.Instance.FadeOut(2.5f);

        yield return new WaitForSeconds(5.5f);

        UIManager.Instance.ShowUI(UIFin);

        yield return new WaitForSeconds(3f);

        UIManager.Instance.ShowUI(BtnUIFin);

        yield return null;
    }

    public void SwitchUI()
    {
        desactivationUI = !desactivationUI;
    }
}