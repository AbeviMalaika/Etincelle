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
public class Quest_4 : QuestScript
{
    [Header("Paramètres de base")]
    public GameObject joueur;

    [Header("Références pour l'objectif 1")]
    public GameObject portail;
    public ZonePortail zonePortail;
    public AudioSource pisteSFX;
    public AudioClip sonPortail;
    public List<GameObject> effetsMains;   //Les effets sur les mains
    bool transitionPortail;

    [Header("Références pour l'objectif 2")]
    public GameObject clavier;
    public OrdinateurTexteInput ordi;
    bool devoilement;

    [Header("Références pour l'objectif 3")]
    public GameObject crayon;
    public GameObject cahier;

    [Header("Références pour l'objectif 4")]
    public GameObject telephone;
    public AudioSource pisteScenario;
    public Image imgAppelTelephone;
    public AudioClip audioAppel;

    /// <summary>
    /// Initialise de l'apparence du croquis final
    /// </summary>
    void Start()
    {
        cahier.GetComponent<CahierTransformations>().SwitchCroquisFinal();
    }

    /// <summary>
    /// Vérifie en continu les conditions pour chacun des objectifs de la quête 4
    /// et déclenche les événements associés (texte, son, téléportation, fin de partie...).
    /// </summary>
    void Update()
    {
        // Objectif 1
        if (quest.progressionActuelle == 0)
        {
            portail.GetComponent<ToucherDetection>().detecterToucher = true;

            // Si le joueur est dans la zone de portail et que le zoneMiroir est touché
            if (portail.GetComponent<ToucherDetection>().toucher && !transitionPortail)
            {
                portail.GetComponent<ToucherDetection>().detecterToucher = false;

                pisteSFX.PlayOneShot(sonPortail);

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

                transitionPortail = true;
            }

            if (transitionPortail && zonePortail.retourChambre)
            {
                //Ajout de progression  -------------------------------------
                AjouterProgression();
            }
        }

        // Objectif 2
        if (quest.progressionActuelle == 1)
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
                //Ajout de progression  -------------------------------------
                AjouterProgression();
            }
        }

        // Objectif 3
        if (quest.progressionActuelle == 2)
        {
            //On autorise la modification du cahier
            cahier.GetComponent<CahierTransformations>().autoriserModification = true;

            //À FAIRE - Si le crayon est pris et que la mine touche le cahier
            if (crayon.GetComponent<GrabDetection>().isGrabbed && cahier.GetComponent<CahierTransformations>().modifCahier)
            {
                //Faire sonner le téléphone
                telephone.GetComponent<AudioSource>().Play();

                //Pour qu'on voit l'image d'appel entrant
                imgAppelTelephone.color = Color.white;

                //Ajout de progression  -------------------------------------
                AjouterProgression();
            }
        }

        // Objectif 4
        if (quest.progressionActuelle == 3)
        {
            // Le joueur doit prendre le téléphone et répondre à un appel
            if (telephone.GetComponent<GrabDetection>().isGrabbed)
            {
                //Arrêter la sonnerie
                telephone.GetComponent<AudioSource>().Stop();

                //Pour qu'on voit l'image d'appel entrant

                //imgAppelTelephone.color = Color.black;

                //Puis, on entend l'appel entre le personnage principal et son ami
                pisteScenario.PlayOneShot(audioAppel);

                //Terminer la partie ----------------------------------------------------
                GameManager.Instance.finPartie = true;

                //Compléter la quête ----------------------------------------------------
                CompleterQuete();
            }
        }
    }
}