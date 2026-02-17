using System;
using UnityEngine;

public class ToucherDetection : MonoBehaviour
{
    public bool toucher;
    public bool detecterToucher;

    void Start()
    {
        toucher = false;
    }

    private void OnTriggerEnter(Collider infoCollision)
    {
        // Si l'objet se fait toucher, il se transforme
        if (infoCollision.gameObject.tag == "Doigt")
        {
            if (detecterToucher)
            {
                if (!toucher)
                {
                    toucher = true;
                    print("<color=green>Objet touché: " + gameObject.name + "</color>");
                    Invoke("ResetToucher", 1f);
                }
            }

        }
    }

    //Pour réinitialiser l'état de toucher
    void ResetToucher() { toucher = false; }
}
