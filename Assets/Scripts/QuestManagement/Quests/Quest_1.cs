using UnityEngine;

public class Quest1 : MonoBehaviour
{
    public GameObject joueur;
    Quest quest_1;

    void Start()
    {
        QuestManager.Instance.DemarrerQuest("1");
    }

    void Update()
    {
        // Objectif 1
        if (QuestManager.Instance.queteActuelle.progressionActuelle == 0)
        {
            if (CollisionChaise.contactChaise && HauteurDetection.estAssis)
            {
                QuestManager.Instance.AjouterProgression("1");
            }
        }

        // Objectif 2
        if (QuestManager.Instance.queteActuelle.progressionActuelle == 1)
        {
            if (CollisionChaise.contactChaise && HauteurDetection.estAssis)
            {
                QuestManager.Instance.AjouterProgression("1");
            }
        }

        // Objectif 3 | -------------------------------------------------------
        if (QuestManager.Instance.queteActuelle.progressionActuelle == 2)
        {
            if (CollisionChaise.contactChaise && HauteurDetection.estAssis)
            {
                //Compléter la quête
                QuestManager.Instance.AjouterProgression("1");

                //Démarrer la nouvelle quête
                QuestManager.Instance.DemarrerQuest("2");
            }
        }

        if (QuestManager.Instance.queteActuelle.progressionActuelle == QuestManager.Instance.queteActuelle.progressionRequise)
        {
            print("<color=green>ALLOOOOOOOOO</color>");
            enabled = false; 
        }
        print("<color=green>" + CollisionChaise.contactChaise + "</color>");
        print("<color=blue>" + HauteurDetection.estAssis + "</color>");
    }
}
