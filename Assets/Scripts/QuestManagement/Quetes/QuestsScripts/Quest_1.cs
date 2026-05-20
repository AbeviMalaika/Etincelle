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
public class Quest_1 : QuestScript
{
    [Header("Paramètres de base")]
    public GameObject joueur;

    [Header("Références pour l'objectif 1")]
    public OrdinateurTexteInput ordi;

    [Header("Références pour l'objectif 2")]
    public GrabDetection crayonGrabDetection;
    public CahierTransformations cahierTransform;

    private void Start()
    {
        //Monologue 01 du personnage
        AudioManager.Instance.ChangerMonologue();
    }

    /// <summary>
    /// Vérifie en continu la progression des objectifs de la quête
    /// et déclenche l'avancement lorsque les conditions sont remplies.
    /// Désactive le script lorsque la quête n'est plus la quête active.
    /// </summary>
    void Update()
    {
        // Objectif 1 | -------------------------------------------------------
        if (quest.progressionActuelle == 0)
        {
            //Si la value du input field est égale à "" (en appuyant le backspace pour supprimer les inputs)
            if (ordi.texteSupp)
            {
                //Monologue 02 du personnage
                AudioManager.Instance.ChangerMonologue();

                //QuestManager.Instance.AjouterProgression(quest.questID);
                AjouterProgression();
            }
        }

        // Objectif 2 | -------------------------------------------------------
        if (quest.progressionActuelle == 1)
        {
            // Si le crayon est pris et que l'efface touche le cahier
            if (crayonGrabDetection.isGrabbed)
            {
                //On autorise la modification du cahier
                cahierTransform.autoriserModification = true;

                //Compléter la quête ------------------------
                if (cahierTransform.modifCahier)
                {
                    CompleterQuete();
                }
            }
        }
    }
}