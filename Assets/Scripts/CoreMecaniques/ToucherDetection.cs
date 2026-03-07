/***
 * 
 * ÉTINCELLE
 * 
 * Par Malaïka Abevi
 * Dernière modification : 06/03/2026 
 * 
 */

using UnityEngine;

/// <summary>
/// Gère la détection du toucher d'un objet à l'aide d'un collider.
/// Permet d'activer ou de désactiver la détection et déclenche un état de toucher temporaire.
/// </summary>
public class ToucherDetection : MonoBehaviour
{
    public bool toucher;
    public bool detecterToucher;

    /// <summary>
    /// Initialise l'état de toucher à false au démarrage.
    /// </summary>
    void Start()
    {
        toucher = false;
    }

    /// <summary>
    /// Détecte lorsqu'un objet avec le tag "Doigt" entre dans le collider
    /// et active l'état de toucher si la détection est permise.
    /// </summary>
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

    /// <summary>
    /// Réinitialise l'état de toucher après un court délai.
    /// </summary>
    void ResetToucher() { toucher = false; }

    /// <summary>
    /// Active la détection du toucher et réinitialise l'état de toucher.
    /// </summary>
    public void ActiverDetectionToucher()
    {
        detecterToucher = true;
        toucher = false;
    }

    /// <summary>
    /// Désactive la détection du toucher.
    /// </summary>
    public void DesactiverDetectionToucher() { detecterToucher = false; }
}