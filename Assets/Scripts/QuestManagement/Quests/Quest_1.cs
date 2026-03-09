/***
 * 
 * ÉTINCELLE
 * 
 * Par Malaïka Abevi
 * Dernière modification : 06/03/2026 
 * 
 */

using UnityEngine;

/// <summary>
/// Gère la logique de la première quête du jeu.
/// Cette quête suit plusieurs objectifs : s'asseoir à l'ordinateur,
/// supprimer le texte entré, puis interagir avec le crayon et le cahier.
/// Lorsque tous les objectifs sont complétés, la quête suivante est démarrée.
/// </summary>
public class Quest_1 : MonoBehaviour
{
    public GameObject crayon;
    public GameObject cahier;
    public GameObject joueur;
    public OrdinateurTexteInput ordi;
    public CollisionChaise collisionChaise;

    Quest quest_1;

    /// <summary>
    /// Initialise la quête 1 au début de la scène
    /// et récupère sa référence depuis le QuestManager.
    /// </summary>
    void Start()
    {
        QuestManager.Instance.DemarrerQuest("1");
        quest_1 = QuestManager.Instance.TrouverQuest("1");
    }

    /// <summary>
    /// Vérifie en continu la progression des objectifs de la quête
    /// et déclenche l'avancement lorsque les conditions sont remplies.
    /// Désactive le script lorsque la quête n'est plus la quête active.
    /// </summary>
    void Update()
    {
        // Objectif 2
        if (quest_1.progressionActuelle == 0)
        {
            // À FAIRE - Si la value du input field est égale à "" (en appuyant le backspace pour supprimer les inputs)
            if (ordi.texteSupp)
            {
                QuestManager.Instance.AjouterProgression("1");
            }
        }

        // Objectif 3 | -------------------------------------------------------
        if (quest_1.progressionActuelle == 1)
        {   
            //On autorise la modification du cahier
            cahier.GetComponent<CahierTransformations>().autoriserModification = true;

            // Si le crayon est pris et que l'efface touche le cahier
            if (crayon.GetComponent<GrabDetection>().isGrabbed && cahier.GetComponent<CahierTransformations>().modifCahier)
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