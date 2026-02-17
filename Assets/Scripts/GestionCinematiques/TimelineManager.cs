using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TimelineManager : MonoBehaviour
{
    public static TimelineManager Instance;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {

    }

    public void PauseTimeline(PlayableDirector director)
    {
        director.playableGraph.GetRootPlayable(0).SetSpeed(0);
    }

    public void PlayTimeline(PlayableDirector director)
    {
        director.playableGraph.GetRootPlayable(0).SetSpeed(1);
    }
}
