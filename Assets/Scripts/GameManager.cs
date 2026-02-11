using UnityEngine;
using static Oculus.Interaction.Context;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public static bool enPause;
    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        // On démarre la première quête
        QuestManager.Instance.DemarrerQuest("1");
        QuestManager.Instance.gameObject.GetComponent<Quest_1>().enabled = true;
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
