using UnityEngine;

public class HauteurDetection : MonoBehaviour
{
    public Transform centerEyeAnchor;

    float hauteurJoueur;
    float hauteurAssis;
    float hauteurActuelle;
    public bool estAssis;

    void Start()
    {
        if (SessionData.hauteurJoueur != 0 && SessionData.hauteurAssis != 0)
        {
            hauteurJoueur = SessionData.hauteurJoueur;
            hauteurAssis = SessionData.hauteurAssis;
        }
        else
        {
            //Pour débugging seulement, à supprimer par la suite
            hauteurJoueur = 1.6f;
            hauteurAssis = 1.4f;
        }
    }

    void Update()
    {
        hauteurActuelle = centerEyeAnchor.position.y;

        if (hauteurJoueur != 0 && hauteurActuelle <= hauteurAssis)
        {
            estAssis = true;
        }
    }
}
