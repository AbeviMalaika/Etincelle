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
        //QuestManager.Instance.DemarrerQuest("1");
        quest_2 = QuestManager.Instance.TrouverQuest("2");
        director.gameObject.SetActive(true);

        TimelineManager.Instance.PlayTimeline(director);
    }

    void Update()
    {
        // Objectif 1
        if (quest_2.progressionActuelle == 0)
        {
            if (director.playableGraph.GetRootPlayable(0).GetSpeed() == 0)
            {
                chevet.GetComponent<ToucherDetection>().detecterToucher = true;
            }

            // Si la table de chevet est touchée
            if (chevet.GetComponent<ToucherDetection>().toucher)
            {
                QuestManager.Instance.AjouterProgression("2");
                chevet.GetComponent<ToucherDetection>().detecterToucher = false;
                TimelineManager.Instance.PlayTimeline(director);
            }
        }

        // Objectif 2
        if (quest_2.progressionActuelle == 1)
        {
            if (director.playableGraph.GetRootPlayable(0).GetSpeed() == 0)
            {
                tableOrdi.GetComponent<ToucherDetection>().detecterToucher = true;
            }

            // Si la table d'ordi est touchée
            if (tableOrdi.GetComponent<ToucherDetection>().toucher)
            {
                QuestManager.Instance.AjouterProgression("2");
                tableOrdi.GetComponent<ToucherDetection>().detecterToucher = false;
                TimelineManager.Instance.PlayTimeline(director);
            }
        }

        // Objectif 4
        if (quest_2.progressionActuelle == 2)
        {
            if (director.playableGraph.GetRootPlayable(0).GetSpeed() == 0)
            {
                lit.GetComponent<ToucherDetection>().detecterToucher = true;
            }

            // Si le lit est touché
            if (lit.GetComponent<ToucherDetection>().toucher)
            {
                QuestManager.Instance.AjouterProgression("2");
                lit.GetComponent<ToucherDetection>().detecterToucher = false;
                TimelineManager.Instance.PlayTimeline(director);
            }
        }

        // Objectif 5
        if (quest_2.progressionActuelle == 3)
        {
            if (director.playableGraph.GetRootPlayable(0).GetSpeed() == 0)
            {
                commode.GetComponent<ToucherDetection>().detecterToucher = true;
            }

            // Si la commode est touchée
            if (commode.GetComponent<ToucherDetection>().toucher)
            {
                QuestManager.Instance.AjouterProgression("2");
                commode.GetComponent<ToucherDetection>().detecterToucher = false;
                TimelineManager.Instance.PlayTimeline(director);
            }
        }

        // Objectif 6 | -------------------------------------------------------
        if (quest_2.progressionActuelle == 4)
        {
            if (director.playableGraph.GetRootPlayable(0).GetSpeed() == 0)
            {
                plantes.GetComponent<ToucherDetection>().detecterToucher = true;
            }

            // Si les plantes sont touchées
            if (plantes.GetComponent<ToucherDetection>().toucher)
            {
                //Compléter la quête
                QuestManager.Instance.AjouterProgression("2");
                plantes.GetComponent<ToucherDetection>().detecterToucher = false;
                TimelineManager.Instance.PlayTimeline(director);

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
