/***
 * 
 * ÉTINCELLE
 * 
 * Par Malaïka Abevi
 * Dernière modification : 06/03/2026 
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
    public OVRHand OvrHand;
    public bool gestureDone;

    public Animator animTransition;
    public Scene sceneActuelle;

    public GameObject SphereTransition;
    Material matTransition;

    public GameObject MenuPrincipalUI;
    public GameObject ConteneurMenuPrincipalUI;

    public event Action OnPauseGesture;

    public bool enPause;

    public bool finPartie;

    public bool gestionFinFait;

    public Color couleurFade;

    public GameObject UIFin;
    public GameObject BtnUIFin;

    OVRHand.MicrogestureType microGesture;

    //Pour la fin
    public Animator portail;
    public RuntimeAnimatorController animatorPortailFin;

    public Animator cristauxChemin;

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
            QuestManager.Instance.gameObject.GetComponent<Quest_1>().enabled = true;

            //if (SessionData.calibrage != null)
            //{
            //    transform.position += SessionData.calibrage.positionOffset;
            //    transform.rotation = SessionData.calibrage.rotationOffset * transform.rotation;
            //}
        }

        //Récupérer le matériel de la sphère
        if (SphereTransition != null)
            matTransition = SphereTransition.GetComponent<MeshRenderer>().material;
    }

    /// <summary>
    /// Vérifie les gestes de la main pour gérer la pause du jeu
    /// et déclenche la gestion de fin de partie si nécessaire.
    /// </summary>
    void Update()
    {
        if (!finPartie)
        {
            if (sceneActuelle.buildIndex == 1)
            {
                if (OvrHand == null) return;

                microGesture = OvrHand.GetMicrogestureType();

                if (microGesture == OVRHand.MicrogestureType.SwipeRight && !gestureDone)
                {
                    enPause = !enPause;
                    gestureDone = true;

                    OnPauseGesture?.Invoke();
                }
            }
        }
        else
        {
            if (!gestionFinFait)
            {
                gestionFinFait = true;
                GestionFinPartie();
            }
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
        //On active l'animation de fade-out
        FadeOut();

        //On attend le temps que l'animation dure
        yield return new WaitForSeconds(2.5f);

        //On demande le chargement de la scène
        AsyncOperation scene = SceneManager.LoadSceneAsync(indexScene);

        //On ne veut pas que la prochaine scène s'affiche tant qu'elle n'est pas entièrement chargée
        scene.allowSceneActivation = false;

        do
        {
            Debug.Log("Chargement... : " + scene.progress);

            if (scene.progress >= 0.9f)
            {
                //On attend le temps que l'animation dure
                yield return new WaitForSeconds(2f);
                scene.allowSceneActivation = true;
            }

            yield return null;

        } while (!scene.isDone);

        Debug.Log("La scène est chargée");

        yield return null;
    }

    /// <summary>
    /// Change la couleur utilisée pour l'effet de fondu sur la sphère de transition.
    /// </summary>
    public void ChangerCouleurFade() { matTransition.SetColor("_Color", couleurFade); }

    /// <summary>
    /// Lance un fondu d'entrée (apparition de la scène).
    /// </summary>
    public void FadeIn()
    {
        StartCoroutine(corou_FadeIn());
    }

    /// <summary>
    /// Lance un fondu de sortie (disparition de la scène).
    /// </summary>
    public void FadeOut()
    {
        StartCoroutine(corou_FadeOut());
    }

    /// <summary>
    /// Coroutine qui gère l'animation de fondu d'entrée.
    /// </summary>
    IEnumerator corou_FadeIn()
    {
        animTransition.enabled = true;
        animTransition.SetTrigger("fadeIn");

        yield return new WaitForSeconds(2.5f);

        //Désactiver l'animator car il controle l'opacité et empèche de set par la suite
        animTransition.enabled = false;

        if (matTransition != null)
            matTransition.SetFloat("_Opacity", 0f);

        yield return null;
    }

    /// <summary>
    /// Coroutine qui gère l'animation de fondu de sortie.
    /// </summary>
    IEnumerator corou_FadeOut()
    {
        animTransition.enabled = true;
        animTransition.SetTrigger("fadeOut");

        yield return new WaitForSeconds(2.5f);

        //Désactiver l'animator car il controle l'opacité et empèche de set par la suite
        animTransition.enabled = false;

        if (matTransition != null)
            matTransition.SetFloat("_Opacity", 1f);

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
        Invoke("ActiverPortail", 5f);
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
        yield return new WaitForSeconds(5.5f);

        FadeOut();

        yield return new WaitForSeconds(5.5f);

        UIManager.Instance.ShowUI(UIFin);

        yield return new WaitForSeconds(3f);

        UIManager.Instance.ShowUI(BtnUIFin);

        yield return null;
    }
}