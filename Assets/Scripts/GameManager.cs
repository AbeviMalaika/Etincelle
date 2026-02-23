using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Oculus.Interaction;

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

    public static bool enPause;

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

        if (sceneActuelle.name == "SceneMenuPrincipal")
        {
            if (UIManager.Instance != null)
            {
                UIManager.Instance.ShowUI(MenuPrincipalUI);
                UIManager.Instance.ShowUI(ConteneurMenuPrincipalUI);
            }
        }

        if (sceneActuelle.name == "ScenePartie")
        {
            enPause = false;
        }
    }

    void Start()
    {
        if (sceneActuelle.name == "ScenePartie")
        {
            // On démarre la première quête
            QuestManager.Instance.DemarrerQuest("1");
            QuestManager.Instance.gameObject.GetComponent<Quest_1>().enabled = true;
        }

        //Récupérer le matériel de la sphère
        if (SphereTransition != null)
            matTransition = SphereTransition.GetComponent<MeshRenderer>().material;
    }

    void Update()
    {
        if (sceneActuelle.name == "ScenePartie")
        {
            if (OvrHand == null) return;

            OVRHand.MicrogestureType microGesture = OvrHand.GetMicrogestureType();

            if (microGesture == OVRHand.MicrogestureType.SwipeRight && !gestureDone)
            {
                enPause = !enPause;
                gestureDone = true;

                OnPauseGesture?.Invoke();
            }
        }
    }

    public void SetPause()
    {
        enPause = !enPause;
    }

    //Fonction pour démarrer la coroutine du chargement de scène
    public void ChargerScene(string nomScene)
    {
        StartCoroutine(ChargementAsyncScene(nomScene));
    }

    IEnumerator ChargementAsyncScene(string nomScene)
    {
        //On active l'animation de fade-out
        FadeOut();

        //On attend le temps que l'animation dure
        yield return new WaitForSeconds(2.5f);

        //On demande le chargement de la scène
        AsyncOperation scene = SceneManager.LoadSceneAsync(nomScene);

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

    //
    void ResetGestureDone()
    {
        gestureDone = false;
    }

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
}