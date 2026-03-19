using UnityEngine;

public abstract class QuestScript : MonoBehaviour
{
    public Quest quest;

    public bool demarrerProchaineQuete;

    void Update()
    {
        if (QuestManager.Instance.queteActuelle != quest)
        {

        }
    }

    protected void AjouterProgression(int amount = 1)
    {
        QuestManager.Instance.AjouterProgression(quest.questID, amount);
    }

    protected void CompleterQuete()
    {
        if (quest == null) return;

        int restant = quest.progressionRequise - quest.progressionActuelle;
        if (restant > 0)
        {
            QuestManager.Instance.AjouterProgression(quest.questID, restant);
            if (demarrerProchaineQuete) QuestManager.Instance.DemarrerQuest(quest.questID + 1);
            enabled = false;
        }
    }
}