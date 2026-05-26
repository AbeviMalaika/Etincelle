using UnityEngine;
using UnityEngine.UI;

public class TelephoneFonctionnement : MonoBehaviour
{
    public Image imageEcran;
    public Image ecran;
    public bool switching;
    public bool manualSwitch;
    public bool switchOn;
    public float fadeTemps;

    float tempsEcoule;
    Color etatFinal;
    Color etatDepart;

    Color transparent;


    /// <summary>
    /// Initialise le matériel du cahier et les variables de transformation.
    /// </summary>
    void Start()
    {
        transparent = new Color(1f, 1f, 1f, 0f);

        //Initialisation de l'image
        etatFinal = transparent;
        etatDepart = Color.black;
        switching = false;
        switchOn = true;
    }

    /// <summary>
    /// Met à jour la transformation du cahier chaque frame si nécessaire.
    /// </summary>
    void Update()
    {
        if (gameObject.GetComponent<GrabDetection>().stateChanged && !switching && manualSwitch)
        {
            switching = true;
        }

        if (switching)
        {
            //Si l'allumage de l'écran est géré par l'utilisateur
            if (gameObject.GetComponent<GrabDetection>().stateChanged && manualSwitch)
            {
                tempsEcoule = 0;

                //Si l'état change, on inverse la transition et reset le temps
                if (gameObject.GetComponent<GrabDetection>().wasDropped)
                {
                    etatFinal = Color.black;
                    etatDepart = transparent;
                }
                else if (gameObject.GetComponent<GrabDetection>().isGrabbed)
                {
                    etatFinal = transparent;
                    etatDepart = Color.black;
                }
            }

            //Si l'allumage de l'écran n'est pas géré par l'utilisateur 
            else if (!manualSwitch)
            {
                if (switchOn)
                {
                    etatFinal = transparent;
                    etatDepart = Color.black;
                }

                if (!switchOn)
                {
                    etatFinal = Color.black;
                    etatDepart = transparent;
                }
            }

            if (tempsEcoule < fadeTemps)
            {
                float t = tempsEcoule / fadeTemps;
                t = Mathf.SmoothStep(0f, 1f, t);

                Color color = Color.Lerp(etatDepart, etatFinal, t);

                tempsEcoule += Time.deltaTime;

                ecran.color = color;
            }
            else
            {
                ecran.color = etatFinal;
                tempsEcoule = 0f;
                switching = false;
            }
        }
    }
}
