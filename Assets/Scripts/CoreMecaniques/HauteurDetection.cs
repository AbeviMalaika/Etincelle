using Oculus.Interaction.Locomotion;
using UnityEngine;

public class HauteurDetection : MonoBehaviour
{
    public GameObject player;
    public Transform centerEyeAnchor;

    public float hauteurJoueur;
    public float hauteurAssis;
    public float hauteurActuelle;
    public float seuilHauteur;
    public bool estAssis;


    void Start()
    {
        // On fait la lecture de la hauteur initiale du joueur
        Invoke("GetJoueurHauteurDonnees", 2f);
    }

    void Update()
    {
        hauteurActuelle = centerEyeAnchor.localPosition.y;

        if (hauteurJoueur != 0 && hauteurActuelle <= hauteurAssis)
        {
            estAssis = true;
        }

        //Detection du bouton A | Ce bouton sert à calibrer l'espace du joueur
        if (OVRInput.GetDown(OVRInput.Button.One))
        {
            Debug.Log("Primary button pressed!");
        }

        //Detection du bouton B | Ce bouton sert à confirmer le calibrage du joueur
        if (OVRInput.GetDown(OVRInput.Button.Two))
        {
            Debug.Log("Secondary button pressed!");
        }
    }

    public void GetJoueurHauteurDonnees()
    {
        //On calcule selon la taille du joueur, mais si la valeur n'est pas calculé, on prend la valeur par défaut
        if(centerEyeAnchor.localPosition.y < 0)
        {
            hauteurJoueur = centerEyeAnchor.localPosition.y;
        }
        else
        {
            hauteurJoueur = 1.6f;
        }

        hauteurAssis = hauteurJoueur - seuilHauteur;
    }

    public void CalibrageJoueur()
    {
        OVRManager.display.RecenterPose();
    }
}
