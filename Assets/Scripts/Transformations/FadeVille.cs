using UnityEngine;
using UnityEngine.PlayerLoop;

public class FadeVille : MonoBehaviour
{
    public Material mat;
    public float dureeTransformation;
    public float etatFinal;
    public bool transformer;
    float tempsEcoule;
    float etatTransformation;

    private void Start()
    {
        transformer = false;
        SetVilleAlpha();
    }
    void Update()
    {
        if (transformer)
        {
            if (tempsEcoule < dureeTransformation)
            {
                // Pourcentage du temps écoulé par rapport au temps de  défini
                float t = tempsEcoule / dureeTransformation;

                // Transition smooth
                t = Mathf.SmoothStep(0f, 1f, t);

                // Application de la transformation
                etatTransformation = Mathf.Lerp(1, etatFinal, t);
                tempsEcoule += Time.deltaTime;
                mat.SetFloat("_Alpha", etatTransformation);
            }
        }
    }

    public void SetVilleAlpha()
    {
        mat.SetFloat("_Alpha", 1.0f);
    }

    public void TransformerVille()
    {
        transformer = true;
    }
}
