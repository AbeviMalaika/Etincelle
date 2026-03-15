/***
 * 
 * ÉTINCELLE
 * 
 * Par Malaïka Abevi
 * Dernière modification : 15/03/2026 
 * 
 */

using UnityEngine;

/// <summary>
/// Gère les transitions en fondu de la caméra VR.
/// </summary>
public class VRFade : MonoBehaviour
{
    public static VRFade Instance;
    public OVRScreenFade screenFade;

    void Awake()
    {
        Instance = this;
    }

    /// <summary>
    /// Lance un fondu de sortie (disparition de la scène).
    /// </summary>
    /// <param name="duration">Durée en seconde du fondu.</param>
    public void FadeOut(float duration)
    {
        screenFade.fadeTime = duration;
        screenFade.FadeOut();
    }

    /// <summary>
    /// Lance un fondu d'entrée (apparition de la scène).
    /// </summary>     
    /// <param name="duration">Durée en seconde du fondu.</param>
    public void FadeIn(float duration)
    {
        screenFade.fadeTime = duration;
        screenFade.FadeIn();
    }

    /// <summary>
    /// Change la couleur utilisée pour l'effet de fondu sur la sphère de transition.
    /// </summary>
    /// <param name="couleurFade">Couleur du fondu.</param>
    public void ChangerCouleurFade(Color couleurFade) { screenFade.fadeColor = couleurFade;}
}