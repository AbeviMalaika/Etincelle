using UnityEngine;

public class DynamisationShaderMeuble : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public bool transformation;
    public bool inverse;
    //bool initialisation;
    public float dureeTransformation;
    float tempsEcoule;
    float etatTransformation;
    float etatFinal;
    float etatDepart;

    Material mat;

    void Start()
    {
        mat = GetComponent<MeshRenderer>().materials[0];

        //Initialisation du matériel
        etatDepart = !inverse ? 1f : 0f;
        etatFinal = !inverse ? 0f : 1f;
        mat.SetFloat("_Degre_Transformation", etatDepart);
    }

    // Update is called once per frame
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