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

[CreateAssetMenu(fileName = "NewQuest", menuName = "Etincelle/Quest")]
public class Quest : ScriptableObject
{
    public int questID;
    public string titre;
    public List<Objectif> listeObjectif = new List<Objectif>();

    [HideInInspector] public QuestState etat = QuestState.NonDemarree;
    [HideInInspector] public int progressionActuelle;

    public int progressionRequise;

    /// <summary>
    /// Réinitialise la quête à zéro.
    /// </summary>
    public void ResetQuest()
    {
        etat = QuestState.NonDemarree;
        progressionActuelle = 0;
    }

    /// <summary>
    /// Initialise la quête et la met en état "EnProgression".
    /// </summary>
    public void DemarrerQuest()
    {
        etat = QuestState.EnProgression;
        progressionActuelle = 0;
    }

    /// <summary>
    /// Ajoute une progression à la quête. Si la progression atteint le maximum requis, la quête est complétée.
    /// </summary>
    /// <param name="amount">Quantité de progression à ajouter (1 par défaut).</param>
    public void AjouterProgression(int amount = 1)
    {
        if (etat != QuestState.EnProgression) return;

        progressionActuelle += amount;

        if (progressionActuelle >= progressionRequise)
        {
            CompleterQuest();
        }
    }

    /// <summary>
    /// Marque la quête comme complétée.
    /// </summary>
    void CompleterQuest()
    {
        etat = QuestState.Completee;
    }
}