using UnityEngine;

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
