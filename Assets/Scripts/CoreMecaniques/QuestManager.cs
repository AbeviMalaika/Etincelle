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

/// <summary>
/// Gère l'ensemble des quêtes du jeu.
/// Permet de démarrer une quête, ajouter de la progression, et récupérer les quêtes par ID ou en cours.
/// </summary>
public class QuestManager : MonoBehaviour
{
    public static QuestManager Instance;

    public List<Quest> quests = new List<Quest>();

    public Quest queteActuelle;

    /// <summary>
    /// Initialise le singleton ou détruit l'objet en double.
    /// </summary>
    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        foreach (Quest quest in quests)
        {
            quest.ResetQuest();
        }
    }

    /// <summary>
    /// Démarre une quête à partir de son ID si elle n’a pas encore été commencée.
    /// </summary>
    /// <param name="questID">Identifiant de la quête à démarrer.</param>
    public void DemarrerQuest(int questID)
    {
        Quest quest = TrouverQuest(questID);

        if (quest == null || quest.etat != QuestState.NonDemarree)
            return;

        quest.DemarrerQuest();

        //Enregistrer la quête actuelle
        queteActuelle = quest;

        //Activer le script lié à cette quête
        QuestScript[] scripts = FindObjectsByType<QuestScript>(FindObjectsSortMode.None);

        foreach (QuestScript script in scripts)
        {
            if (script.quest == quest)
            {
                script.enabled = true;
            }
            else
            {
                script.enabled = false;
            }
        }

        UIManager.Instance.AfficherQueteUI(quest);

        Debug.Log($"Quête commencée : {quest.titre}");
    }

    /// <summary>
    /// Ajoute de la progression à une quête donnée.
    /// </summary>
    /// <param name="questID">Identifiant de la quête.</param>
    /// <param name="amount">Quantité de progression à ajouter (1 par défaut).</param>
    public void AjouterProgression(int questID, int amount = 1)
    {
        Quest quest = TrouverQuest(questID);
        if (quest == null) return;

        quest.AjouterProgression(amount);

        UIManager.Instance.AfficherQueteUI(quest);

        if (quest.etat == QuestState.Completee)
        {
            Debug.Log($"Quête complétée : {quest.titre}");
        }
    }

    /// <summary>
    /// Cherche et retourne une quête à partir de son ID.
    /// </summary>
    /// <param name="id">Identifiant de la quête.</param>
    /// <returns>La quête correspondante, ou null si elle n’existe pas.</returns>
    public Quest TrouverQuest(int id)
    {
        return quests.Find(q => q.questID == id);
    }

    /// <summary>
    /// Retourne la première quête actuellement en progression.
    /// </summary>
    /// <returns>La quête en cours, ou null si aucune n’est en progression.</returns>
    public Quest TrouverQuestActuelle()
    {
        return quests.Find(q => q.etat == QuestState.EnProgression);
    }
}