using UnityEngine;
using UnityEngine.UI;

public class TelephoneFonctionnement : MonoBehaviour
{
    public Image imageEcran;
    public Image ecran;
    public bool switchPhone;
    public bool inverse;
    public float fadeTemps;

    float tempsEcoule;
    Color etatFinal;
    Color etatDepart;


    /// <summary>
    /// Initialise le matériel du cahier et les variables de transformation.
    /// </summary>
    void Start()
    {
        //Initialisation de l'image
        etatFinal = !inverse ? Color.black : Color.white;
        etatDepart = !inverse ? Color.white : Color.black;
        switchPhone = false;
    }

    /// <summary>
    /// Met à jour la transformation du cahier chaque frame si nécessaire.
    /// </summary>
    void Update()
    {
        etatFinal = !inverse ? Color.black : Color.white;
        etatDepart = !inverse ? Color.white : Color.black;

        if (gameObject.GetComponent<GrabDetection>().isGrabbed)
        {
            switchPhone = true;
        }

        if (gameObject.GetComponent<GrabDetection>().wasDropped)
        {
            switchPhone = true;
        }

        if (switchPhone)
        {
            //Si l'état change, on inverse la transition et reset le temps
            if(gameObject.GetComponent<GrabDetection>().stateChanged)
            {
                inverse = !inverse;
                tempsEcoule = 0;
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
                switchPhone = false;
                inverse = !inverse;
            }
        }
    }
}
