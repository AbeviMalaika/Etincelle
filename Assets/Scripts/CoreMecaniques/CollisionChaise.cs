/***
 * 
 * ÉTINCELLE
 * 
 * Par Malaïka Abevi
 * Dernière modification : 06/03/2026 
 * 
 */

using System.Collections;
using UnityEngine;

/// <summary>
/// Détecte si le joueur est en contact avec la chaise à l'aide d'un trigger.
/// </summary>
public class CollisionChaise : MonoBehaviour
{
    public bool joueurAssis; //si le joueur est actuellement en contact avec la chaise.

    Coroutine timerCoroutine;  //Référence à la coroutine en cours

    bool coroutineLancee;  //Pour voir si la coroutine à déjà été démarrée. On veut éviter de la lancer en boucle par le trigger

    /// <summary>
    /// Initialise l'état de contact à false au démarrage.
    /// </summary>
    void Start()
    {
        joueurAssis = false;
        coroutineLancee = false;
    }

    /// <summary>
    /// Détecte l'entrée du joueur dans le collider de la chaise et met à jour l'état de contact.
    /// </summary>
    /// <param name="infoCollider">Collider qui entre en contact.</param>
    private void OnTriggerEnter(Collider infoCollider)
    {
        if (infoCollider.gameObject.name == "PlayerController")
        {
            if(!coroutineLancee)
            {
                // Démarre le timer quand le joueur entre
                timerCoroutine = StartCoroutine(TempsAssis());
                coroutineLancee = true;
            }
        }
    }

    /// <summary>
    /// Détecte la sortie du joueur du collider de la chaise et met à jour l'état de contact.
    /// </summary>
    /// <param name="infoCollider">Collider qui sort du trigger.</param>
    private void OnTriggerExit(Collider infoCollider)
    {
        if (infoCollider.gameObject.name == "PlayerController")
        {
            // Annule le timer si le joueur sort avant 5 secondes
            if (timerCoroutine != null)
            {
                StopCoroutine(timerCoroutine);
                timerCoroutine = null;
            }

            joueurAssis = false;
            coroutineLancee = false;
            Debug.Log("Le joueur a quitté la zone");
        }
    }

    IEnumerator TempsAssis()
    {
        yield return new WaitForSeconds(4.5f);

        joueurAssis = true;
        Debug.Log("Le joueur est bien à la chaise");
    }



}