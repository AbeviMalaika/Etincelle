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
public class Quest_3 : MonoBehaviour
{
    Quest quest_3;
    public GameObject joueur;
    public PlayableDirector director;
    public CollisionChaise collisionChaise;

    //Les effets sur les mains
    public List<GameObject> effetsMains;

    /// <summary>
    /// Récupère la référence de la quête 3 depuis le QuestManager au démarrage.
    /// </summary>
    void Start()
    {
        quest_3 = QuestManager.Instance.TrouverQuest("3");
    }

    /// <summary>
    /// Vérifie en continu les conditions permettant de faire progresser
    /// les différents objectifs de la quête.
    /// Gère également l'activation d'effets visuels et le passage à la quête suivante.
    /// </summary>
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
            if (collisionChaise.joueurAssis && TimelineManager.Instance.timelinePause)
            {
                //On désactive le UI pour ne pas qu'il soit une source de problème pendant la quête
                GameManager.Instance.desactivationUI = true;
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
                //On réactive le UI
                GameManager.Instance.desactivationUI = false;

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

    /// <summary>
    /// Lance la timeline via le TimelineManager.
    /// Appelé après un délai lorsque le joueur s'assoit à l'ordinateur.
    /// </summary>
    void DemarrerTimeline()
    {
        TimelineManager.Instance.PlayTimeline();
    }
}