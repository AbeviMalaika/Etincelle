using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Quest_4 : MonoBehaviour
{
    public GameObject miroir;
    public GameObject crayon;
    public GameObject cahier;
    public GameObject telephone;
    public GameObject clavier;
    public GameObject joueur;

    //Les effets sur les mains
    public List<GameObject> effetsMains;

    public CollisionChaise collisionChaise;
    public ZonePortail zonePortail;

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
            miroir.GetComponent<ToucherDetection>().detecterToucher = true;

            // Si le joueur est dans la zone de portail et que le miroir est touché
            if (miroir.GetComponent<ToucherDetection>().toucher)
            {
                miroir.GetComponent<ToucherDetection>().detecterToucher = false;

                QuestManager.Instance.AjouterProgression("4");

                //On retourne dans la chambre
                zonePortail.RetourChambre();

                //On ajuste le texte à l'écran
                clavier.GetComponent<OrdinateurTexteInput>().ChangerTexte();

                crayon.GetComponent<Respawner>().Respawn();

                //On désactive les effets sur les mains
                foreach (GameObject eff in effetsMains)
                {
                    eff.SetActive(false);
                }
            }
        }

        // Objectif 2
        if (quest_4.progressionActuelle == 1)
        {
            // Si le joueur est assis à l'ordinateur
            if (collisionChaise.contactChaise && joueur.GetComponent<HauteurDetection>().estAssis)
            {
                QuestManager.Instance.AjouterProgression("4");
            }
        }

        // Objectif 3
        if (quest_4.progressionActuelle == 2)
        {
            //À FAIRE - Si le crayon est pris et que la mine touche le cahier
            if (crayon.GetComponent<GrabDetection>().isGrabbed && cahier.GetComponent<CahierTransformations>().modifCahier)
            {
                //Faire sonner le téléphone
                telephone.GetComponent<AudioSource>().Play();

                QuestManager.Instance.AjouterProgression("4");
            }
        }

        // Objectif 4
        if (quest_4.progressionActuelle == 3)
        {
            // Le joueur doit prendre le téléphone et répondre à un appel
            if (telephone.GetComponent<GrabDetection>().isGrabbed)
            {
                //Arrêter la sonnerie
                telephone.GetComponent<AudioSource>().Stop();

                //Compléter la quête
                QuestManager.Instance.AjouterProgression("4");

                //Terminer la partie
                GameManager.Instance.finPartie = true;
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
