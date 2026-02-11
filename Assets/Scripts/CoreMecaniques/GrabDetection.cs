using Oculus.Interaction;
using Oculus.Interaction.HandGrab;
using UnityEngine;

public class GrabDetection : MonoBehaviour
{
    [SerializeField] private HandGrabInteractable handGrab;

    public bool isGrabbed;

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