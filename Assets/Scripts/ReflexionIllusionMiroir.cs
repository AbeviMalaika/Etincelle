using UnityEngine;

public class ReflexionIllusionMiroir : MonoBehaviour
{
    public Transform target;
    public float offset;
    public float angleMultiplier;

    void LateUpdate()
    {
        if (target == null) return;

        Vector3 dir = transform.position - target.position;

        Quaternion look = Quaternion.LookRotation(dir, Vector3.up);

        Vector3 angles = look.eulerAngles;

        // Inversion pour effet miroir
        float yaw = Mathf.DeltaAngle(0, angles.y);
        yaw += offset;
        yaw *= angleMultiplier;
        angles.y = yaw;

        transform.rotation = Quaternion.Euler(angles);
    }
}

