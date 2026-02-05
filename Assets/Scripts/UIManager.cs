using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI titreQuete;
    public TextMeshProUGUI titreObjectif;
    Quest questActuelle;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        QuestManager.Instance.DemarrerQuest("1");
        questActuelle = QuestManager.Instance.TrouverQuestActuelle();
        AfficherQueteUI();
    }

    // Update is called once per frame
    void Update()
    {
        questActuelle = QuestManager.Instance.TrouverQuestActuelle();
        AfficherQueteUI();
    }

    void AfficherQueteUI()
    {
        titreQuete.text = questActuelle.titre;
        titreObjectif.text = questActuelle.listeObjectif[questActuelle.listeObjectif.Find(q => q.objectifID == questActuelle.progressionActuelle).objectifID].titre;
    }
}
