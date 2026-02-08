using UnityEngine;

public class Quest_2 : MonoBehaviour
{
    public GameObject chevet;
    public GameObject tableOrdi;
    public GameObject cahier;
    public GameObject lit;
    public GameObject commode;
    public GameObject plantes;

    Quest quest_2;

    void Start()
    {
        //QuestManager.Instance.DemarrerQuest("1");
        quest_2 = QuestManager.Instance.TrouverQuest("2");
    }

    void Update()
    {
        // Objectif 1
        if (quest_2.progressionActuelle == 0)
        {
            if (chevet.GetComponent<ToucherDetection>().toucher)
            {
                QuestManager.Instance.AjouterProgression("2");
            }
        }

        // Objectif 2
        if (quest_2.progressionActuelle == 1)
        {
            if (tableOrdi.GetComponent<ToucherDetection>().toucher)
            {
                QuestManager.Instance.AjouterProgression("2");
            }
        }

        // Objectif 3
        if (quest_2.progressionActuelle == 2)
        {
            if (cahier.GetComponent<ToucherDetection>().toucher)
            {
                QuestManager.Instance.AjouterProgression("2");
            }
        }

        // Objectif 4
        if (quest_2.progressionActuelle == 3)
        {
            if (lit.GetComponent<ToucherDetection>().toucher)
            {
                QuestManager.Instance.AjouterProgression("2");
            }
        }

        // Objectif 5
        if (quest_2.progressionActuelle == 4)
        {
            if (commode.GetComponent<ToucherDetection>().toucher)
            {
                QuestManager.Instance.AjouterProgression("2");
            }
        }

        // Objectif 6 | -------------------------------------------------------
        if (quest_2.progressionActuelle == 5)
        {
            if (plantes.GetComponent<ToucherDetection>().toucher)
            {
                //Compléter la quête
                QuestManager.Instance.AjouterProgression("2");

                //Démarrer la nouvelle quête
                QuestManager.Instance.DemarrerQuest("3");
                gameObject.GetComponent<Quest_3>().enabled = true;
            }
        }

        // Si la quête actuelle n'est pas la quête 1, alors désactiver le script
        if (quest_2 != QuestManager.Instance.queteActuelle)
        {
            print("<color=green>Quête " + quest_2.questID + "complétée!</color>");
            enabled = false;
        }
    }
}
