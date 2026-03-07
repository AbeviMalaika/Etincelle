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
using UnityEngine.UI;

/// <summary>
/// Gère la logique et la progression de la quête 4.
/// Cette quête inclut plusieurs objectifs interactifs tels que l'utilisation du portail,
/// l'interaction avec l'ordinateur et le clavier, la manipulation du crayon et du téléphone,
/// et la fin de la partie.
/// </summary>
public class Quest_4 : MonoBehaviour
{
    public GameObject crayon;
    public GameObject cahier;
    public GameObject telephone;
    public GameObject clavier;
    public OrdinateurTexteInput ordi;
    public GameObject joueur;
    public CollisionChaise collisionChaise;
    public ZonePortail zonePortail;

    bool devoilement;

    //Les effets sur les mains
    public List<GameObject> effetsMains;

    Quest quest_4;

    /// <summary>
    /// Initialise la référence à la quête 4 depuis le QuestManager.
    /// </summary>
    void Start()
    {
        quest_4 = QuestManager.Instance.TrouverQuest("4");
    }

    /// <summary>
    /// Vérifie en continu les conditions pour chacun des objectifs de la quête 4
    /// et déclenche les événements associés (texte, son, téléportation, fin de partie...).
    /// </summary>
    void Update()
    {
        // Objectif 1
        if (quest_4.progressionActuelle == 0)
        {
            zonePortail.detecterToucher = true;

            // Si le joueur est dans la zone de portail et que le zoneMiroir est touché
            if (zonePortail.toucher)
            {
                zonePortail.detecterToucher = false;

                QuestManager.Instance.AjouterProgression("4");

                //On retourne dans la chambre
                zonePortail.RetourChambre();

                //On ajuste le texte à l'écran
                ordi.ChangerTexte();

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
            clavier.GetComponent<ToucherDetection>().detecterToucher = true;
            // Si le joueur est assis à l'ordinateur
            if (clavier.GetComponent<ToucherDetection>().toucher && !devoilement)
            {
                clavier.GetComponent<ToucherDetection>().detecterToucher = false;

                //On dévoile le texte final
                ordi.DevoilerTexteFinal();
                devoilement = true;
            }

            //Si le texte est enfin dévoilé, alors on passe à la quête suivante
            if (ordi.texteDevoile)
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

        // Si la quête actuelle n'est pas la quête 4, alors désactiver le script
        if (quest_4 != QuestManager.Instance.queteActuelle)
        {
            print("<color=green>Quête " + quest_4.questID + "complétée!</color>");
            enabled = false;
        }
    }
}