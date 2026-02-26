using UnityEngine;

public class DisparitionVille : MonoBehaviour
{
    public Material mat;
    public bool transformation;
    public float etatFinal;
    public float dureeTransformation;
    float tempsEcoule;
    float etatTransformation;

    void Start()
    {
        //Initialisation du matériel
        mat.SetFloat("_Degre_Transformation", 0);
        transformation = false;
}

    void Update()
    {
        if (transformation)
        {
            if (tempsEcoule < dureeTransformation)
            {
                // Pourcentage du temps écoulé par rapport au temps de transformation défini
                float t = tempsEcoule / dureeTransformation;

                // Transition smooth
                t = Mathf.SmoothStep(0f, 1f, t);

                // Application de la transformation
                etatTransformation = Mathf.Lerp(0, etatFinal, t);
                tempsEcoule += Time.deltaTime;
                mat.SetFloat("_Degre_Transformation", etatTransformation);
            }

            else
            {
                // Set l'état final
                etatTransformation = etatFinal;
                transformation = false;
            }
        }
    }

    public void TransformerVille() { transformation = true; }
}
