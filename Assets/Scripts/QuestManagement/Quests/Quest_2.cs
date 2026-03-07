/***
 * 
 * ÉTINCELLE
 * 
 * Par Malaïka Abevi
 * Dernière modification : 06/03/2026 
 * 
 */

using UnityEngine;
using UnityEngine.Playables;

/// <summary>
/// Gère les objectifs et la progression de la quête 2 dans le jeu.
/// Active les interactions sur les objets à toucher et contrôle la timeline.
/// </summary>
public class Quest_2 : MonoBehaviour
{
    public GameObject chevet;
    public GameObject tableOrdi;
    public GameObject lit;
    public GameObject commode;
    public GameObject plantes;
    public PlayableDirector director;

    Quest quest_2;

    /// <summary>
    /// Initialise la quête 2 et lance la timeline au démarrage.
    /// </summary>
    void Start()
    {
        quest_2 = QuestManager.Instance.TrouverQuest("2");
        TimelineManager.Instance.PlayTimeline();
    }

    /// <summary>
    /// Vérifie à chaque frame l’état de chaque objectif de la quête 2
    /// et met à jour la progression en conséquence.
    /// Active les objets à toucher seulement lorsque la timeline est en pause.
    /// </summary>
    void Update()
    {
        // Objectif 1
        if (quest_2.progressionActuelle == 0)
        {
            if (TimelineManager.Instance.timelinePause)
            {
                chevet.GetComponent<ToucherDetection>().detecterToucher = true;
            }

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

            if (commode.GetComponent<ToucherDetection>().toucher)
            {
                QuestManager.Instance.AjouterProgression("2");
                commode.GetComponent<ToucherDetection>().detecterToucher = false;
                TimelineManager.Instance.PlayTimeline();
            }
        }

        // Objectif 5
        if (quest_2.progressionActuelle == 4)
        {
            if (TimelineManager.Instance.timelinePause)
            {
                plantes.GetComponent<ToucherDetection>().detecterToucher = true;
            }

            if (plantes.GetComponent<ToucherDetection>().toucher)
            {
                QuestManager.Instance.AjouterProgression("2");
                plantes.GetComponent<ToucherDetection>().detecterToucher = false;
                TimelineManager.Instance.PlayTimeline();
            }
        }

        // Objectif 6
        if (quest_2.progressionActuelle == 5)
        {
            if (TimelineManager.Instance.entreeLho)
            {
                QuestManager.Instance.AjouterProgression("2");
                QuestManager.Instance.DemarrerQuest("3");
                gameObject.GetComponent<Quest_3>().enabled = true;
            }
        }

        // Désactive le script si la quête n'est plus active
        if (quest_2 != QuestManager.Instance.queteActuelle)
        {
            print("<color=green>Quête " + quest_2.questID + "complétée!</color>");
            enabled = false;
        }
    }
}