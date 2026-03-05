using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class Quest_3 : MonoBehaviour
{
    Quest quest_3;
    public GameObject joueur;
    public PlayableDirector director;
    public CollisionChaise collisionChaise;

    //Les effets sur les mains
    public List<GameObject> effetsMains;

    void Start()
    {
        quest_3 = QuestManager.Instance.TrouverQuest("3");
    }

    void Update()
    {
        // Objectif 1
        if (quest_3.progressionActuelle == 0)
        {
            // Le joueur doit écouter le narrateur et attendre
            if (TimelineManager.Instance.introTerminee)
            {
                QuestManager.Instance.AjouterProgression("3");

                //On désactive les effets sur les mains
                foreach (GameObject eff in effetsMains)
                {
                    eff.SetActive(true);
                }
            }
        }

        // Objectif 2
        if (quest_3.progressionActuelle == 1)
        {
            // Si le joueur est assis à l'ordinateur
            if (collisionChaise.contactChaise && joueur.GetComponent<HauteurDetection>().estAssis && TimelineManager.Instance.timelinePause)
            {
                QuestManager.Instance.AjouterProgression("3");
                Invoke("DemarrerTimeline", 5f);

                //On désactive les effets sur les mains
                foreach (GameObject eff in effetsMains)
                {
                    eff.SetActive(true);
                }
            }
        }

        // Objectif 3 | -------------------------------------------------------
        if (quest_3.progressionActuelle == 2)
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

        // Si la quête actuelle n'est pas la quête 3, alors désactiver le script
        if (quest_3 != QuestManager.Instance.queteActuelle)
        {
            print("<color=green>Quête " + quest_3.questID + "complétée!</color>");
            enabled = false;
        }
    }

    void DemarrerTimeline()
    {
        TimelineManager.Instance.PlayTimeline();
    }
}