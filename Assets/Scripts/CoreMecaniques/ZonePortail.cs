/***
 * 
 * ÉTINCELLE
 * 
 * Par Malaïka Abevi
 * Dernière modification : 06/03/2026 
 * 
 */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Gère la zone de portail qui détecte lorsque le joueur l'atteint.
/// Permet de déclencher le retour dans la chambre et de réinitialiser l'environnement.
/// </summary>
public class ZonePortail : MonoBehaviour
{
    [Header("Environnement")]
    public List<DynamisationShaderMeuble> transfoMeubles;
    public GameObject decoPlateau;
    public GameObject chambreDummy;
    public bool retourChambre;

    [Header("Gestion de la caméra du joueur")]
    public CinemachineManager targetSwitcher;
    public Transform target;

    [Header("Son")]
    public AudioClip sonPortail;

    /// <summary>
    /// Déclenche le retour dans la chambre avec les effets de fade et réinitialisation des objets.
    /// </summary>
    public void RetourChambre()
    {
        StartCoroutine(corou_RetourChambre());
    }

    /// <summary>
    /// Coroutine qui gère le retour dans la chambre : fade out, reset de l'environnement,
    /// repositionnement des meubles et fade in.
    /// </summary>
    /// <returns>IEnumerator pour la coroutine</returns>
    IEnumerator corou_RetourChambre()
    {
        VRFade.Instance.ChangerCouleurFade(Color.white);

        //Jouer le son du portail
        AudioManager.Instance.JouerSFX(sonPortail);

        //Fade la musique du plateau
        AudioManager.Instance.FadeMusiqueMute(AudioManager.Instance.mPiste2);

        yield return new WaitForSeconds(0.5f);

        //Moment pour le fade out
        VRFade.Instance.FadeOut(2.5f);

        yield return new WaitForSeconds(2.5f);

        // Resetting de l'environnement
        targetSwitcher.TargetSwitch(target);

        TimelineManager.Instance.StopResetDirector();
        chambreDummy.SetActive(false);

        //Petite pause par sécurité
        yield return new WaitForSeconds(2f);

        // Puis un fade in vers la chambre 
        VRFade.Instance.FadeIn(2.5f);

        yield return new WaitForSeconds(0.5f);

        //Puis on retransforme les meubles de la chambre vers des meubles normaux
        foreach (DynamisationShaderMeuble transfoM in transfoMeubles)
        {
            transfoM.transformation = true;
        }

        decoPlateau.SetActive(false);

        yield return new WaitForSeconds(3f);

        retourChambre = true;

        yield return null;
    }
}