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
/// Gère le respawn d'un objet lorsqu'il tombe en dessous d'une certaine hauteur.
/// </summary>
public class Respawner : MonoBehaviour
{
    public float hauteurLimite = -10f;
    public Transform pointDeRespawn;

    Rigidbody rb;
    Vector3 positionInitiale;
    Quaternion rotationInitiale;

    /// <summary>
    /// Initialise les références et enregistre la position et rotation initiales.
    /// </summary>
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        positionInitiale = transform.position;
        rotationInitiale = transform.rotation;
    }

    /// <summary>
    /// Vérifie à chaque frame si l'objet est tombé sous la hauteur limite et le respawn si nécessaire.
    /// </summary>
    void Update()
    {
        if (transform.position.y < hauteurLimite)
        {
            Respawn();
        }
    }

    /// <summary>
    /// Replace l'objet à son point de respawn ou à sa position initiale et réinitialise sa physique.
    /// </summary>
    public void Respawn()
    {
        // On coupe toute physique avant déplacement
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        if (pointDeRespawn != null)
        {
            transform.position = pointDeRespawn.position;
            transform.rotation = pointDeRespawn.rotation;
        }
        else
        {
            transform.position = positionInitiale;
            transform.rotation = rotationInitiale;
        }

        // Sécurité supplémentaire
        rb.Sleep();
    }
}