using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

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

        if (sceneActuelle.buildIndex == 2)
        {
            enPause = false;
        }
    }

    void Start()
    {
        if (sceneActuelle.buildIndex == 2)
        {
            // On démarre la première quête
            QuestManager.Instance.DemarrerQuest("1");
            QuestManager.Instance.gameObject.GetComponent<Quest_1>().enabled = true;

            if (SessionData.calibrage != null)
            {
                transform.position += SessionData.calibrage.positionOffset;
                transform.rotation = SessionData.calibrage.rotationOffset * transform.rotation;
            }
        }

        //Récupérer le matériel de la sphère
        if (SphereTransition != null)
            matTransition = SphereTransition.GetComponent<MeshRenderer>().material;
    }

    void Update()
    {
        if (!finPartie)
        {
            if (sceneActuelle.buildIndex == 2)
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

    public void SetPause()
    {
        enPause = !enPause;
    }

    //Fonction pour démarrer la coroutine du chargement de scène
    public void ChargerScene(int indexScene)
    {
        StartCoroutine(ChargementAsyncScene(indexScene));
    }

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

    public void ChangerCouleurFade() { matTransition.SetColor("_Color", couleurFade); }

    //Fonction pour faire un fondu d'entrée
    public void FadeIn()
    {
        StartCoroutine(corou_FadeIn());
    }

    //Fonction pour faire un fondu de sortie
    public void FadeOut()
    {
        StartCoroutine(corou_FadeOut());
    }

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

    //Mettre tout en place pour la scène finale
    public void SetDecoFin()
    {
        portail.runtimeAnimatorController = animatorPortailFin;

        cristauxChemin.SetTrigger("formation");
        Invoke("ActiverPortail", 5f);
    }

    public void ActiverPortail()
    {
        portail.SetTrigger("flip");
    }

    public void GestionFinPartie()
    {
        StartCoroutine(corou_GestionFinPartie());
    }

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