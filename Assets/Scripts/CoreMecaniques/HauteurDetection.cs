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

    // Update is called once per frame
    void Update()
    {
        hauteurActuelle = centerEyeAnchor.localPosition.y;

        if (hauteurJoueur != 0 && hauteurActuelle <= hauteurAssis)
        {
            estAssis = true;
            //print("Le joueur est assis");
        }


        //Detection du bouton A | Ce bouton sert à calibrer l'espace du joueur
        if (OVRInput.GetDown(OVRInput.Button.One)) // OVRInput.Button.One maps to the primary button
        {
            Debug.Log("Primary button pressed!");
        }

        //Detection du bouton B | Ce bouton sert à confirmer le calibrage du joueur
        if (OVRInput.GetDown(OVRInput.Button.Two)) // OVRInput.Button.One maps to the primary button
        {
            Debug.Log("Secondary button pressed!");
        }
    }

    public void GetJoueurHauteurDonnees()
    {
        hauteurJoueur = centerEyeAnchor.localPosition.y;
        hauteurAssis = hauteurJoueur - seuilHauteur;
    }

    public void CalibrageJoueur()
    {
        OVRManager.display.RecenterPose();
    }
}
