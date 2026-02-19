using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public OVRHand OvrHand;

    public static bool enPause;
    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        enPause = false;
    }

    void Start()
    {
        // On démarre la première quête
        QuestManager.Instance.DemarrerQuest("1");
        QuestManager.Instance.gameObject.GetComponent<Quest_1>().enabled = true;
    }

    void Update()
    {
        OVRHand.MicrogestureType microGesture = OvrHand.GetMicrogestureType();

        if (microGesture == OVRHand.MicrogestureType.SwipeRight) enPause = !enPause;
    }
}
