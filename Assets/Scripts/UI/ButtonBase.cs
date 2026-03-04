using Oculus.Interaction;
using UnityEngine;

public class ButtonBase : MonoBehaviour
{
    Transform positionBaseButton;

    void Start()
    {
        GameObject baseObj = new GameObject("ButtonBaseTransform");
        positionBaseButton = baseObj.transform;

        positionBaseButton.position = transform.localPosition - new Vector3(0, 1, 0);

        GetComponent<PokeInteractableVisual>().InjectButtonBaseTransform(positionBaseButton);
    }
}
