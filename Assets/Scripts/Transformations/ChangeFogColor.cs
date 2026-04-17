using System.Collections;
using UnityEngine;

public class ChangeFogColor : MonoBehaviour
{
    public Color couleurActuelle;
    public float duration;

    // Initialisation
    void Start()
    {
        couleurActuelle = RenderSettings.fogColor;
    }

    public void ChangeColor(Color couleur)
    {
         StartCoroutine(Corou_ChangeColor(couleur));
    }

    IEnumerator Corou_ChangeColor(Color couleur)
    {
        float time = 0f;
        Color startColor = couleurActuelle;

        while (time < duration)
        {
            time += Time.deltaTime;

            float t = Mathf.Clamp01(time / duration);
            RenderSettings.fogColor = Color.Lerp(startColor, couleur, t);

            yield return null;
        }

        //Force la couleur finale exacte
        RenderSettings.fogColor = couleur;
        couleurActuelle = couleur;
    }
}

