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
/// Script Pour la gestion du suivi du UI par rapport à la position et rotation du joueur
/// </summary>
public class UIFollowHead : MonoBehaviour
{
    [Header("Target")]
    public Transform cameraTransform;

    [Header("Position")]
    public float distance = 2.5f;
    public float heightOffset = -0.2f;
    public float positionSmoothTime = 0.15f;

    [Header("Rotation")]
    public float rotationSmoothSpeed = 8f;

    private Vector3 velocity;

    /// <summary>
    /// Positionne le UI devant la caméra du joueur avec un mouvement fluide
    /// et oriente l'interface vers la caméra pour qu'elle reste toujours lisible.
    /// </summary>
    void LateUpdate()
    {
        if (!cameraTransform) return;

        //Position cible devant la caméra
        Vector3 targetPos =
            cameraTransform.position +
            cameraTransform.forward * distance +
            Vector3.up * heightOffset;

        //Smooth position (Meta-like)
        transform.position = Vector3.SmoothDamp(
            transform.position,
            targetPos,
            ref velocity,
            positionSmoothTime
        );

        //Rotation douce vers la caméra
        Quaternion targetRot = Quaternion.LookRotation(
            transform.position - cameraTransform.position
        );

        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            targetRot,
            rotationSmoothSpeed * Time.deltaTime
        );
    }
}