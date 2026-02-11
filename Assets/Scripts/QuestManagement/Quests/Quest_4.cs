using UnityEngine;

public class Quest_4 : MonoBehaviour
{
    public GameObject miroir;
    public GameObject positionPortailMiroir;
    public GameObject crayon;

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
            // Si le joueur est dans la zone de portail et que le miroir est touché
            if (ZonePortail.entreeZone && miroir.GetComponent<ToucherDetection>().toucher)
            {
                QuestManager.Instance.AjouterProgression("4");
            }
        }

        // Objectif 2
        if (quest_4.progressionActuelle == 1)
        {
            // Si le joueur est assis à l'ordinateur
            if (CollisionChaise.contactChaise && HauteurDetection.estAssis)
            {
                QuestManager.Instance.AjouterProgression("4");
            }
        }

        // Objectif 3
        if (quest_4.progressionActuelle == 2)
        {
            //À FAIRE - Si le joueur ouvre l'ordi (en appuyant la barre d'espace sur le clavier pour le sortir du mode veille)
            if (true)
            {
                QuestManager.Instance.AjouterProgression("4");
            }
        }

        // Objectif 4 | -------------------------------------------------------
        if (quest_4.progressionActuelle == 3)
        {
            //À FAIRE - Si le crayon est pris et que la mine touche le cahier
            if (crayon.GetComponent<GrabDetection>().isGrabbed && CahierTransformations.modifCahier)
            {
                //Compléter la quête
                QuestManager.Instance.AjouterProgression("4");

                //Terminer la partie
                //Code pour la gestion de fin de partie. Possiblement faire un script à part pour ça et le déclencher avec enabled et/ou boolean
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
