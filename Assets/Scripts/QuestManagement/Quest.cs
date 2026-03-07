/***
 * 
 * ÉTINCELLE
 * 
 * Par Malaïka Abevi
 * Dernière modification : 06/03/2026 
 * 
 */

using System.Collections.Generic;

[System.Serializable]
public class Quest
{
    public string questID;
    public string titre;
    public List<Objectif> listeObjectif = new List<Objectif>();

    public QuestState etat;
    public int progressionActuelle;
    public int progressionRequise;

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