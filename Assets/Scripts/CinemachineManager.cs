using Unity.Cinemachine;
using UnityEngine;

public class CinemachineManager : MonoBehaviour
{
    public CinemachineCamera cinemachineCam;

    public void TargetSwitch(Transform target)
    {
        cinemachineCam.Target.TrackingTarget = target;
    }
}
