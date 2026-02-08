using UnityEngine;

public class ToucherDetection : MonoBehaviour
{
    public bool toucher;

    void Start()
    {
        toucher = false;
    }

    private void OnTriggerEnter(Collider infoCollision)
    {
        // Si l'objet se fait toucher, il se transforme
        if (infoCollision.gameObject.tag == "Doigt")
        {
            toucher = true;
            print("<color=green>Objet touché: " + gameObject.name + "</color>");
        }
    }
}
