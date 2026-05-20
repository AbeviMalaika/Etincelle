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
public class Quest_2 : QuestScript
{
    [Header("Paramètres de base")]
    public PlayableDirector director;
    public AudioClip sonLuciole;

    [Header("Références pour l'objectif 1")]
    public GameObject chevet;

    [Header("Références pour l'objectif 2")]
    public GameObject tableOrdi;

    [Header("Références pour l'objectif 3")]
    public GameObject lit;

    [Header("Références pour l'objectif 4")]
    public GameObject commode;

    [Header("Références pour l'objectif 5")]
    public GameObject plantes;

    /// <summary>
    /// Lance la timeline au démarrage.
    /// </summary>
    void Start()
    {
        Invoke("DemarrerTimeline", 2.7f);
    }


    /// <summary>
    /// Vérifie à chaque frame l’état de chaque objectif de la quête 2
    /// et met à jour la progression en conséquence.
    /// Active les objets à toucher seulement lorsque la timeline est en pause.
    /// </summary>
    void Update()
    {
        // Objectif 1
        if (quest.progressionActuelle == 0)
        {
            if (TimelineManager.Instance.timelinePause)
            {
                chevet.GetComponent<ToucherDetection>().detecterToucher = true;
            }

            if (chevet.GetComponent<ToucherDetection>().toucher)
            {
                //Son de la luciole
                AudioManager.Instance.JouerSFX(sonLuciole);

                chevet.GetComponent<ToucherDetection>().detecterToucher = false;
                TimelineManager.Instance.PlayTimeline();

                //Ajout de progression  -------------------------------------
                AjouterProgression();
            }
        }

        // Objectif 2
        if (quest.progressionActuelle == 1)
        {
            if (TimelineManager.Instance.timelinePause)
            {
                tableOrdi.GetComponent<ToucherDetection>().detecterToucher = true;
            }

            if (tableOrdi.GetComponent<ToucherDetection>().toucher)
            {
                //Son de la luciole
                AudioManager.Instance.JouerSFX(sonLuciole);

                tableOrdi.GetComponent<ToucherDetection>().detecterToucher = false;
                TimelineManager.Instance.PlayTimeline();

                //Monologue 04 du personnage
                AudioManager.Instance.ChangerMonologue();

                //Ajout de progression  -------------------------------------
                AjouterProgression();
            }
        }

        // Objectif 3
        if (quest.progressionActuelle == 2)
        {
            if (TimelineManager.Instance.timelinePause)
            {
                lit.GetComponent<ToucherDetection>().detecterToucher = true;
            }

            if (lit.GetComponent<ToucherDetection>().toucher)
            {
                //Son de la luciole
                AudioManager.Instance.JouerSFX(sonLuciole);

                lit.GetComponent<ToucherDetection>().detecterToucher = false;
                TimelineManager.Instance.PlayTimeline();

                //Ajout de progression  -------------------------------------
                AjouterProgression();
            }
        }

        // Objectif 4
        if (quest.progressionActuelle == 3)
        {
            if (TimelineManager.Instance.timelinePause)
            {
                commode.GetComponent<ToucherDetection>().detecterToucher = true;
            }

            if (commode.GetComponent<ToucherDetection>().toucher)
            {
                //Son de la luciole
                AudioManager.Instance.JouerSFX(sonLuciole);

                commode.GetComponent<ToucherDetection>().detecterToucher = false;
                TimelineManager.Instance.PlayTimeline();

                //Ajout de progression  -------------------------------------
                AjouterProgression();
            }
        }

        // Objectif 5
        if (quest.progressionActuelle == 4)
        {
            if (TimelineManager.Instance.timelinePause)
            {
                plantes.GetComponent<ToucherDetection>().detecterToucher = true;
            }

            if (plantes.GetComponent<ToucherDetection>().toucher)
            {
                //Son de la luciole
                AudioManager.Instance.JouerSFX(sonLuciole);

                plantes.GetComponent<ToucherDetection>().detecterToucher = false;
                TimelineManager.Instance.PlayTimeline();

                //Monologue 05 du personnage
                AudioManager.Instance.ChangerMonologue();

                //Ajout de progression  -------------------------------------
                AjouterProgression();
            }
        }

        // Objectif 6
        if (quest.progressionActuelle == 5)
        {
            if (TimelineManager.Instance.entreeLho)
            {
                //Compléter la quête ------------------------
                CompleterQuete();
            }
        }
    }

    void DemarrerTimeline()
    {
        TimelineManager.Instance.PlayTimeline();
    }
}