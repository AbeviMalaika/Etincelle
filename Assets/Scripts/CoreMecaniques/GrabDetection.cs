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
    public bool enableTriggerSwitch;
    bool lastState;
    public bool wasDropped;
    public bool stateChanged;

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

        //Si l'option de switch le trigger est true, alors appeler la fonction qui gère le changement
        if (enableTriggerSwitch)
        {
            ColliderToTrigger();
        }

        if (isGrabbed != lastState)
        {
            stateChanged = true;
            // State has changed since last frame
            if (isGrabbed) { wasDropped = false; }
            else { wasDropped = true; }
        }
        else
        {
            stateChanged = false;
        }
        // Update the previous state at the end of the frame
        lastState = isGrabbed;
    }

    /// <summary>
    /// Change le statut trigger du collider selon si l'objet est attrapé ou non.
    /// </summary>
    void ColliderToTrigger()
    {
         GetComponent<Collider>().isTrigger = isGrabbed;
    }
}