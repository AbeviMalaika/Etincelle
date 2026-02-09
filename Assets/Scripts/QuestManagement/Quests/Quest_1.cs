using UnityEngine;

public class Quest1 : MonoBehaviour
{
    public GameObject crayon;
    public GameObject cahier;

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
            // Si le joueur est assis à l'ordinateur
            if (CollisionChaise.contactChaise && HauteurDetection.estAssis)
            {
                QuestManager.Instance.AjouterProgression("1");
            }
        }

        // Objectif 2
        if (quest_1.progressionActuelle == 1)
        {
            // À FAIRE - Si la value du input field est égale à "" (en appuyant le backspace pour supprimer les inputs)
            if (true)
            {
                QuestManager.Instance.AjouterProgression("1");
            }
        }

        // Objectif 3 | -------------------------------------------------------
        if (quest_1.progressionActuelle == 2)
        {
            // Si le crayon est pris et que l'efface touche le cahier
            if (crayon.GetComponent<GrabDetection>().isGrabbed && CahierTransformations.estEfface)
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




