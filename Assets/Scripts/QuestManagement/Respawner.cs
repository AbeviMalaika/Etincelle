using UnityEngine;

public class Respawner : MonoBehaviour
{
    public float hauteurLimite = -10f;
    public Transform pointDeRespawn;

    Rigidbody rb;
    Vector3 positionInitiale;
    Quaternion rotationInitiale;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        positionInitiale = transform.position;
        rotationInitiale = transform.rotation;
    }

    void Update()
    {
        if (transform.position.y < hauteurLimite)
        {
            Respawn();
        }
    }

    void Respawn()
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