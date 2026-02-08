using UnityEngine;

public class Quest1 : MonoBehaviour
{
    Quest quest_1;

    void Start()
    {
        QuestManager.Instance.DemarrerQuest("1");
        quest_1 = QuestManager.Instance.TrouverQuest("1");
    }

    void Update()
    {
        // Objectif 1
        if (quest_1.progressionActuelle == 0)
        {
            if (CollisionChaise.contactChaise && HauteurDetection.estAssis)
            {
                QuestManager.Instance.AjouterProgression("1");
            }
        }

        // Objectif 2
        if (quest_1.progressionActuelle == 1)
        {
            if (CollisionChaise.contactChaise && HauteurDetection.estAssis)
            {
                QuestManager.Instance.AjouterProgression("1");
            }
        }

        // Objectif 3 | -------------------------------------------------------
        if (quest_1.progressionActuelle == 2)
        {
            if (CollisionChaise.contactChaise && HauteurDetection.estAssis)
            {
                //Compléter la quête
                QuestManager.Instance.AjouterProgression("1");

                //Démarrer la nouvelle quête
                QuestManager.Instance.DemarrerQuest("2");
                gameObject.GetComponent<Quest_2>().enabled = true;
            }
        }

        // Si la quête actuelle n'est pas la quête 1, alors désactiver le script
        if (quest_1 != QuestManager.Instance.queteActuelle)
        {
            print("<color=green>Quête " + quest_1.questID + "complétée!</color>");
            enabled = false; 
        }
    }
}




