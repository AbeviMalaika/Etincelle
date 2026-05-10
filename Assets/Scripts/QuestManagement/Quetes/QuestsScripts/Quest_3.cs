/***
 * 
 * ÉTINCELLE
 * 
 * Par Malaïka Abevi
 * Dernière modification : 06/03/2026 
 * 
 */

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

/// <summary>
/// Gère la logique et la progression de la quête 3.
/// Cette quête surveille plusieurs conditions de jeu
/// (fin de l'introduction, position du joueur, cinématique)
/// afin de faire avancer les objectifs et déclencher les événements associés.
/// </summary>
public class Quest_3 : QuestScript
{
    [Header("Paramètres de base")]
    public GameObject joueur;
    public PlayableDirector director;

    [Header("Références pour l'objectif 1")]
    //Les effets sur les mains
    public CollisionChaise collisionChaise;
    public List<GameObject> effetsMains;
    //public List<GameObject> glovesMains;

    [Header("Références pour l'objectif 2")]
    public AudioClip sonPouce;

    /// <summary>
    /// Vérifie en continu les conditions permettant de faire progresser
    /// les différents objectifs de la quête.
    /// Gère également l'activation d'effets visuels et le passage à la quête suivante.
    /// </summary>
    void Update()
    {
        // Objectif 1
        if (quest.progressionActuelle == 0)
        {
            // Le joueur doit écouter le narrateur et attendre
            if (TimelineManager.Instance.introTerminee)
            {
                //Ajout de progression  -------------------------------------
                AjouterProgression();

                //On désactive les effets sur les mains
                foreach (GameObject eff in effetsMains)
                {
                    eff.SetActive(true);
                }
            }
        }

        // Objectif 2
        if (quest.progressionActuelle == 1)
        {
            // Si le joueur est assis à l'ordinateur
            if (collisionChaise.joueurAssis && TimelineManager.Instance.timelinePause && GameManager.Instance.posePouce)
            {
                //On désactive le UI pour ne pas qu'il soit une source de problème pendant la quête
                GameManager.Instance.desactivationUI = true;
                Invoke("DemarrerTimeline", 5f);

                //Ajout de progression  -------------------------------------
                AjouterProgression();

                //On désactive les effets sur les mains
                foreach (GameObject eff in effetsMains)
                {
                    eff.SetActive(true);
                }

                //foreach (GameObject glove in glovesMains)
                //{
                //    glove.GetComponent<DynamisationShaderMeuble>().transformation = true;
                //}

                AudioManager.Instance.JouerSFX(sonPouce);
            }
        }

        // Objectif 3 | -------------------------------------------------------
        if (quest.progressionActuelle == 2)
        {
            // Si la cinématique est terminée
            if (TimelineManager.Instance.cinematiqueTerminee)
            {
                //On réactive le UI
                GameManager.Instance.desactivationUI = false;

                //Compléter la quête ------------------------
                CompleterQuete();
            }
        }
    }

    /// <summary>
    /// Lance la timeline via le TimelineManager.
    /// Appelé après un délai lorsque le joueur s'assoit à l'ordinateur.
    /// </summary>
    void DemarrerTimeline()
    {
        TimelineManager.Instance.PlayTimeline();
    }
}