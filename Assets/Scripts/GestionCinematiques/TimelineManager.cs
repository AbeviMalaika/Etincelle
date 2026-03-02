using UnityEngine;
using UnityEngine.Playables;

public class TimelineManager : MonoBehaviour
{
    public static TimelineManager Instance;
    public bool cinematiqueTerminee;
    PlayableDirector director;

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
    }

    public void PauseTimeline()
    {
        director.playableGraph.GetRootPlayable(0).SetSpeed(0);
    }

    public void PlayTimeline()
    {
        director.playableGraph.GetRootPlayable(0).SetSpeed(1);
    }

    public void TerminerCinematique()
    {
        cinematiqueTerminee = true;
        GameManager.Instance.SetDecoFin();
        PauseTimeline();
    }
}
