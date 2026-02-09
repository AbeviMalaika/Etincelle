using UnityEngine;
using static Oculus.Interaction.Context;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
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
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
