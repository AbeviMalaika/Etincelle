using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using UnityEngine.Timeline;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public OVRHand OvrHand;

    public Animator animTransition;
    //public float tempsTransition;
    public Scene sceneActuelle;

    public GameObject SphereTransition;
    Material matTransition;

    public static bool enPause;
    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        sceneActuelle = SceneManager.GetActiveScene();

        if (sceneActuelle.name == "ScenePartie")
        {
            enPause = false;
        }
    }

    void Start()
    {
        if(sceneActuelle.name == "ScenePartie")
        {
            // On démarre la première quête
            QuestManager.Instance.DemarrerQuest("1");
            QuestManager.Instance.gameObject.GetComponent<Quest_1>().enabled = true;
        }

        //Récupérer le matériel de la sphère
        matTransition = SphereTransition.GetComponent<Material>();
    }

    void Update()
    {
        if (sceneActuelle.name == "ScenePartie")
        {
            OVRHand.MicrogestureType microGesture = OvrHand.GetMicrogestureType();
            if (microGesture == OVRHand.MicrogestureType.SwipeRight) enPause = !enPause;
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
        //changementSceneEnCours = true;

        //On active l'animation de fade-out
        animTransition.SetTrigger("fadeOut");

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
        animTransition.SetTrigger("fadeIn");
        yield return new WaitForSeconds(2.5f);
        matTransition.SetFloat("Alpha", 0f);

        yield return null;
    }

    IEnumerator corou_FadeOut()
    {
        animTransition.SetTrigger("fadeOut");
        yield return new WaitForSeconds(2.5f);
        matTransition.SetFloat("Alpha", 1f);

        yield return null;
    }
}
