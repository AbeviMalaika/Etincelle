/***
 * 
 * ÉTINCELLE
 * 
 * Par Malaïka Abevi
 * Dernière modification : 06/03/2026 
 * 
 */

using Oculus.Interaction;
using Oculus.Interaction.HandGrab;
using UnityEngine;

/// <summary>
/// Détecte si un objet possédant un HandGrabInteractable
/// est actuellement saisi par la main du joueur.
/// Met à jour la variable publique isGrabbed en conséquence.
/// </summary>
public class GrabDetection : MonoBehaviour
{
    [SerializeField] private HandGrabInteractable handGrab;

    public bool isGrabbed;

    /// <summary>
    /// Vérifie à chaque frame si l'objet est actuellement saisi (grabbed)
    /// en regardant l'état du HandGrabInteractable.
    /// Si l'état est "Select", l'objet est considéré comme saisi.
    /// </summary>
    void Update()
    {
        // On detecte si l'objet est grabbed
        if (handGrab.State == InteractableState.Select)
        {
            isGrabbed = true;
            Debug.Log(handGrab.State);
        }
        else
        {
            isGrabbed = false;
        }
    }
}