using UnityEngine;
using UnityEngine.Playables;

public class Quest_2 : MonoBehaviour
{
    public GameObject chevet;
    public GameObject tableOrdi;
    public GameObject lit;
    public GameObject commode;
    public GameObject plantes;
    public PlayableDirector director;

    Quest quest_2;

    void Start()
    {
        quest_2 = QuestManager.Instance.TrouverQuest("2");
        TimelineManager.Instance.PlayTimeline();
    }

    void Update()
    {
        // Objectif 1
        if (quest_2.progressionActuelle == 0)
        {
            if (TimelineManager.Instance.timelinePause)
            {
                chevet.GetComponent<ToucherDetection>().detecterToucher = true;
            }

            // Si la table de chevet est touchée
            if (chevet.GetComponent<ToucherDetection>().toucher)
            {
                QuestManager.Instance.AjouterProgression("2");
                chevet.GetComponent<ToucherDetection>().detecterToucher = false;
                TimelineManager.Instance.PlayTimeline();
            }
        }

        // Objectif 2
        if (quest_2.progressionActuelle == 1)
        {
            if (TimelineManager.Instance.timelinePause)
            {
                tableOrdi.GetComponent<ToucherDetection>().detecterToucher = true;
            }

            // Si la table d'ordi est touchée
            if (tableOrdi.GetComponent<ToucherDetection>().toucher)
            {
                QuestManager.Instance.AjouterProgression("2");
                tableOrdi.GetComponent<ToucherDetection>().detecterToucher = false;
                TimelineManager.Instance.PlayTimeline();
            }
        }

        // Objectif 3
        if (quest_2.progressionActuelle == 2)
        {
            if (TimelineManager.Instance.timelinePause)
            {
                lit.GetComponent<ToucherDetection>().detecterToucher = true;
            }

            // Si le lit est touché
            if (lit.GetComponent<ToucherDetection>().toucher)
            {
                QuestManager.Instance.AjouterProgression("2");
                lit.GetComponent<ToucherDetection>().detecterToucher = false;
                TimelineManager.Instance.PlayTimeline();
            }
        }

        // Objectif 4
        if (quest_2.progressionActuelle == 3)
        {
            if (TimelineManager.Instance.timelinePause)
            {
                commode.GetComponent<ToucherDetection>().detecterToucher = true;
            }

            // Si la commode est touchée
            if (commode.GetComponent<ToucherDetection>().toucher)
            {
                QuestManager.Instance.AjouterProgression("2");
                commode.GetComponent<ToucherDetection>().detecterToucher = false;
                TimelineManager.Instance.PlayTimeline();
            }
        }

        // Objectif 5 | -------------------------------------------------------
        if (quest_2.progressionActuelle == 4)
        {
            if (TimelineManager.Instance.timelinePause)
            {
                plantes.GetComponent<ToucherDetection>().detecterToucher = true;
            }

            // Si les plantes sont touchées
            if (plantes.GetComponent<ToucherDetection>().toucher)
            {
                //Compléter la quête
                QuestManager.Instance.AjouterProgression("2");
                plantes.GetComponent<ToucherDetection>().detecterToucher = false;
                TimelineManager.Instance.PlayTimeline();
            }
        }

        // Objectif 6 | -------------------------------------------------------
        if (quest_2.progressionActuelle == 5)
        {
            //Si le narrateur est entrée en scène
            if (TimelineManager.Instance.entreeLho)
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
