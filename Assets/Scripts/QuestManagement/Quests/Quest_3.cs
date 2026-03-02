using UnityEngine;
using UnityEngine.Playables;

public class Quest_3 : MonoBehaviour
{
    Quest quest_3;
    public GameObject joueur;
    public PlayableDirector director;
    public CollisionChaise collisionChaise;

    void Start()
    {
        //QuestManager.Instance.DemarrerQuest("1");
        quest_3 = QuestManager.Instance.TrouverQuest("3");
    }

    void Update()
    {
        // Objectif 1
        if (quest_3.progressionActuelle == 0)
        {
            // Si le joueur est assis à l'ordinateur
            if (collisionChaise.contactChaise && joueur.GetComponent<HauteurDetection>().estAssis)
            {
                QuestManager.Instance.AjouterProgression("3");
                TimelineManager.Instance.PlayTimeline();
            }
        }

        // Objectif 2 | -------------------------------------------------------
        if (quest_3.progressionActuelle == 1)
        {
            // Si la cinématique est terminée
            if (TimelineManager.Instance.cinematiqueTerminee)
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