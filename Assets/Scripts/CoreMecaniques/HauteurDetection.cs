using UnityEngine;

public class HauteurDetection : MonoBehaviour
{
    public Transform centerEyeAnchor;
    public float hauteurJoueur;
    public float hauteurAssis;
    public float hauteurActuelle;
    public float seuilHauteur;
    static public bool estAssis;

    void Start()
    {
        // On fait la lecture de la hauteur initiale du joueur
        Invoke("GetJoueurHauteurDonnees", 2f);
    }

    // Update is called once per frame
    void Update()
    {
        hauteurActuelle = centerEyeAnchor.localPosition.y;

        if (hauteurJoueur != 0 && hauteurActuelle <= hauteurAssis)
        {
            estAssis = true;
            print("Le joueur est assis");
        }
    }

    public void GetJoueurHauteurDonnees()
    {
        hauteurJoueur = centerEyeAnchor.localPosition.y;
        hauteurAssis = hauteurJoueur - seuilHauteur;
    }
}
