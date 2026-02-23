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

                    DesactiverDetectionToucher();
                    Invoke("ResetToucher", 0.5f);
                }
            }

        }
    }

    //Pour réinitialiser l'état de toucher
    void ResetToucher() { toucher = false; }

    //Pour activer la détection du toucher
    public void ActiverDetectionToucher() { 
        detecterToucher = true;
        toucher = false;
    }
    public void DesactiverDetectionToucher() { detecterToucher = false; }
}
