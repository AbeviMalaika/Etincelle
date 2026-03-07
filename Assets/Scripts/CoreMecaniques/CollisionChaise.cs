/***
 * 
 * ÉTINCELLE
 * 
 * Par Malaïka Abevi
 * Dernière modification : 06/03/2026 
 * 
 */

using UnityEngine;

/// <summary>
/// Détecte si le joueur est en contact avec la chaise à l'aide d'un trigger.
/// </summary>
public class CollisionChaise : MonoBehaviour
{
    /// <summary>
    /// Indique si le joueur est actuellement en contact avec la chaise.
    /// </summary>
    public bool contactChaise;

    /// <summary>
    /// Initialise l'état de contact à false au démarrage.
    /// </summary>
    void Start()
    {
        contactChaise = false;
    }

    /// <summary>
    /// Détecte l'entrée du joueur dans le collider de la chaise et met à jour l'état de contact.
    /// </summary>
    /// <param name="infoCollider">Collider qui entre en contact.</param>
    private void OnTriggerEnter(Collider infoCollider)
    {
        if (infoCollider.gameObject.name == "PlayerController")
        {
            contactChaise = true;
            print("Contact avec la chaise");
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
            contactChaise = false;
        }
    }
}