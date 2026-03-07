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
    public bool toucher;
    public bool detecterToucher;

    public CinemachineManager targetSwitcher;
    public Transform target;

    public GameObject decoPlateau;

    public List<DynamisationShaderMeuble> transfoMeubles;

    public DisparitionVille dispa;
    public GameObject chambreDummy;

    /// <summary>
    /// Initialise l'état de toucher à false au démarrage.
    /// </summary>
    void Start()
    {
        toucher = false;
    }

    /// <summary>
    /// Détecte si le joueur entre dans la zone et active l'état de toucher si la détection est autorisée.
    /// </summary>
    /// <param name="infoCollider">Le collider entrant dans la zone</param>
    private void OnTriggerEnter(Collider infoCollider)
    {
        if (infoCollider.gameObject.name == "PlayerController" && detecterToucher)
        {
            toucher = true;
        }
    }

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
        GameManager.Instance.ChangerCouleurFade();

        yield return new WaitForSeconds(0.5f);

        //Moment pour le fade out
        GameManager.Instance.FadeOut();

        yield return new WaitForSeconds(2.5f);

        // Resetting de l'environnement
        targetSwitcher.TargetSwitch(target);

        TimelineManager.Instance.StopResetDirector();
        dispa.ResetVille();
        chambreDummy.SetActive(false);

        //Petite pause par sécurité
        yield return new WaitForSeconds(0.5f);

        // Puis un fade in vers la chambre 
        GameManager.Instance.FadeIn();

        yield return new WaitForSeconds(0.5f);

        //Puis on retransforme les meubles de la chambre vers des meubles normaux
        foreach (DynamisationShaderMeuble transfoM in transfoMeubles)
        {
            transfoM.transformation = true;
        }

        decoPlateau.SetActive(false);

        yield return null;
    }
}