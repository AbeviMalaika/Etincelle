using UnityEngine;

public class Quest_4 : MonoBehaviour
{
    public GameObject miroir;
    public GameObject positionPortailMiroir;

    Quest quest_4;

    void Start()
    {
        //QuestManager.Instance.DemarrerQuest("1");
        quest_4 = QuestManager.Instance.TrouverQuest("4");
    }

    void Update()
    {
        // Objectif 1
        if (quest_4.progressionActuelle == 0)
        {
            if (ZonePortail.entreeZone && miroir.GetComponent<ToucherDetection>().toucher)
            {
                QuestManager.Instance.AjouterProgression("4");
            }
        }

        // Objectif 2
        if (quest_4.progressionActuelle == 1)
        {
            if (true)
            {
                QuestManager.Instance.AjouterProgression("4");
            }
        }

        // Objectif 3
        if (quest_4.progressionActuelle == 2)
        {
            if (true)
            {
                QuestManager.Instance.AjouterProgression("4");
            }
        }

        // Objectif 4 | -------------------------------------------------------
        if (quest_4.progressionActuelle == 3)
        {
            if (true)
            {
                //Compléter la quête
                QuestManager.Instance.AjouterProgression("4");

                //Terminer la partie
                //Code pour la gestion de fin de partie. Possiblementy faire un script à part pour ça et le déclencher avec enabled et/ou boolean
            }
        }

        // Si la quête actuelle n'est pas la quête 1, alors désactiver le script
        if (quest_4 != QuestManager.Instance.queteActuelle)
        {
            print("<color=green>Quête " + quest_4.questID + "complétée!</color>");
            enabled = false;
        }
    }
}
