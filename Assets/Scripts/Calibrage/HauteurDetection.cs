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
/// Détecte si le joueur est assis ou debout en fonction de la hauteur de son centre de caméra (centerEyeAnchor).
/// </summary>
public class HauteurDetection : MonoBehaviour
{
    public Transform centerEyeAnchor;

    float hauteurJoueur;
    float hauteurAssis;
    float hauteurActuelle;
    public bool estAssis;

    /// <summary>
    /// Initialise les hauteurs du joueur et de sa position assise à partir des données de session,
    /// ou utilise des valeurs par défaut pour le débogage.
    /// </summary>
    void Start()
    {
        //if (SessionData.hauteurJoueur != 0 && SessionData.hauteurAssis != 0)
        //{
        //    hauteurJoueur = SessionData.hauteurJoueur;
        //    hauteurAssis = SessionData.hauteurAssis;
        //}
        //else
        //{
        //    // Pour débogage seulement, à supprimer par la suite
        //    hauteurJoueur = 1.6f;
        //    hauteurAssis = 1.4f;
        //}
    }

    /// <summary>
    /// Vérifie chaque frame la position verticale de la caméra pour déterminer si le joueur est assis.
    /// </summary>
    void Update()
    {
        hauteurActuelle = centerEyeAnchor.position.y;

        if (hauteurJoueur != 0 && hauteurActuelle <= hauteurAssis)
        {
            estAssis = true;
        }
    }
}