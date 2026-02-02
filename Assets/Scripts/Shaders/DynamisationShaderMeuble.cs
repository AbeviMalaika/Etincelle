using UnityEngine;

public class DynamisationShaderMeuble : MonoBehaviour
{
    //public float timeDessolve;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public bool transformation;
    public bool inverse;
    bool initialisation;
    public float dureeTransformation;
    float tempsEcoule;
    float etatTransformation;
    float etatFinal;
    float etatDepart;

    Material mat;
    void Start()
    {
        //Initialisation du matériel
        etatFinal = !inverse ? 0f : 1f;
        etatDepart = !inverse ? 1f : 0f;
        initialisation = true;

        mat = GetComponent<MeshRenderer>().material;
        mat.SetFloat("_Degre_Transformation", etatDepart);
    }

    // Update is called once per frame
    void Update()
    {
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

                print(etatTransformation);

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
                initialisation = false;
            }
        }
        else
        {
            if(initialisation)
            {
                mat.SetFloat("_Degre_Transformation", etatDepart);
            }
        }
    }

    private void OnCollisionEnter(Collision infoCollision)
    {
        if(infoCollision.gameObject.tag == "main")
        {
            print("Objet touché");
            transformation = true;
        }
    }
}