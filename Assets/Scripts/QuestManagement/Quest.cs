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

    public void DemarrerQuest()
    {
        etat = QuestState.EnProgression;
        progressionActuelle = 0;
    }

    public void AjouterProgression(int amount = 1)
    {
        if (etat != QuestState.EnProgression) return;

        progressionActuelle += amount;

        if (progressionActuelle >= progressionRequise)
        {
            CompleterQuest();
        }
    }

    void CompleterQuest()
    {
        etat = QuestState.Completee;
        //Debug.Log($"Quête terminée : {titre}");
    }
}