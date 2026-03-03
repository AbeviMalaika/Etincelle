using UnityEngine;
using UnityEngine.Playables;

public class TimelineManager : MonoBehaviour
{
    public static TimelineManager Instance;
    public bool cinematiqueTerminee;
    PlayableDirector director;
    public bool timelinePause;  //Booléenne pour m'assurer si le Timeline est en pause ou non

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        cinematiqueTerminee = false;
        director = GetComponent<PlayableDirector>();

        //Je veux que le Timeline soit à 0 et en pause au début du jeu
        director.playOnAwake = false;
        director.Stop();

        timelinePause = true;
    }

    public void PauseTimeline()
    {
        director.Pause();
        timelinePause = true;
    }

    public void PlayTimeline()
    {
        director.Play();
        timelinePause = false;
    }

    public void TerminerCinematique()
    {
        cinematiqueTerminee = true;
        GameManager.Instance.SetDecoFin();
        PauseTimeline();
    }
}
