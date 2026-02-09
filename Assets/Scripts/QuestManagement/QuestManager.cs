using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public static QuestManager Instance;

    public List<Quest> quests = new List<Quest>();

    public Quest queteActuelle;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void DemarrerQuest(string questID)
    {
        Quest quest = TrouverQuest(questID);
        if (quest != null && quest.etat == QuestState.NonDemarree)
        {
            quest.DemarrerQuest();
            queteActuelle = TrouverQuestActuelle();
            UIManager.Instance.AfficherQueteUI(quest);
            Debug.Log($"Quête commencée : {quest.titre}");
        }
    }

    public void AjouterProgression(string questID, int amount = 1)
    {
        Quest quest = TrouverQuest(questID);
        if (quest != null)
        {
            quest.AjouterProgression(amount);
        }
    }

    public Quest TrouverQuest(string id)
    {
        return quests.Find(q => q.questID == id);
    }

    public Quest TrouverQuestActuelle()
    {
        return quests.Find(q => q.etat == QuestState.EnProgression);
    }
}
