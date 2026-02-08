using UnityEngine;

public class Quest_3 : MonoBehaviour
{
    Quest quest_3;
    static public bool cinematiqueTerminee;

    void Start()
    {
        //QuestManager.Instance.DemarrerQuest("1");
        quest_3 = QuestManager.Instance.TrouverQuest("3");
        cinematiqueTerminee = false;
    }

    void Update()
    {
        // Objectif 1
        if (quest_3.progressionActuelle == 0)
        {
            if (CollisionChaise.contactChaise && HauteurDetection.estAssis)
            {
                QuestManager.Instance.AjouterProgression("3");
            }
        }

        // Objectif 2 | -------------------------------------------------------
        if (quest_3.progressionActuelle == 1)
        {
            if (cinematiqueTerminee)
            {
                //Compléter la quête
                QuestManager.Instance.AjouterProgression("3");

                //Démarrer la nouvelle quête
                QuestManager.Instance.DemarrerQuest("4");
                gameObject.GetComponent<Quest_4>().enabled = true;
            }
        }

        // Si la quête actuelle n'est pas la quête 1, alors désactiver le script
        if (quest_3 != QuestManager.Instance.queteActuelle)
        {
            print("<color=green>Quête " + quest_3.questID + "complétée!</color>");
            enabled = false;
        }
    }
}