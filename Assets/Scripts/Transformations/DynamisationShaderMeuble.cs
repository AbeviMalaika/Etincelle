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
/// Script pour l'initialisation et la transformation des matériaux dynamiques (matériaux à partir de shaders personnalisés)
/// </summary>
public class DynamisationShaderMeuble : MonoBehaviour
{
    public bool transformation;
    public bool inverse;
    //bool initialisation;
    public float dureeTransformation;
    float tempsEcoule;
    float etatTransformation;
    float etatFinal;
    float etatDepart;

    Material mat;

    /// <summary>
    /// Initialise le matériel et configure l'état de transformation initial du shader.
    /// </summary>
    void Start()
    {
        mat = GetComponent<MeshRenderer>().materials[0];

        //Initialisation du matériel
        etatDepart = !inverse ? 1f : 0f;
        etatFinal = !inverse ? 0f : 1f;
        mat.SetFloat("_Degre_Transformation", etatDepart);
    }

    /// <summary>
    /// Gère la détection du toucher et applique la transformation du shader dans le temps.
    /// </summary>
    void Update()
    {
        if (gameObject.GetComponent<ToucherDetection>().toucher)
        {
            transformation = true;
        }

        etatFinal = !inverse ? 0f : 1f;
        etatDepart = !inverse ? 1f : 0f;

        if (transformation)
        {
            if (tempsEcoule < dureeTransformation)
            {
                // Pourcentage du temps écoulé par rapport au temps de transformation défini
                float t = tempsEcoule / dureeTransformation;

                // Transition smooth
                t = Mathf.SmoothStep(0f, 1f, t);

                // Application de la transformation
                etatTransformation = Mathf.Lerp(etatDepart, etatFinal, t);
                tempsEcoule += Time.deltaTime;
                mat.SetFloat("_Degre_Transformation", etatTransformation);
            }

            else
            {
                // Set l'état final
                etatTransformation = etatFinal;
                tempsEcoule = 0f;
                transformation = false;
                //initialisation = false;
                inverse = !inverse;
            }
        }
    }
}