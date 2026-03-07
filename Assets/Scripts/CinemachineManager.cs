/***
 * 
 * ÉTINCELLE
 * 
 * Par Malaïka Abevi
 * Dernière modification : 06/03/2026 
 * 
 */

using Unity.Cinemachine;
using UnityEngine;

/// <summary>
/// Gère les interactions avec la caméra Cinemachine,
/// notamment le changement de cible que la caméra doit suivre.
/// </summary>
public class CinemachineManager : MonoBehaviour
{
    public CinemachineCamera cinemachineCam;

    /// <summary>
    /// Change la cible suivie par la caméra Cinemachine.
    /// </summary>
    /// <param name="target">Transform que la caméra doit suivre.</param>
    public void TargetSwitch(Transform target)
    {
        cinemachineCam.Target.TrackingTarget = target;
    }
}