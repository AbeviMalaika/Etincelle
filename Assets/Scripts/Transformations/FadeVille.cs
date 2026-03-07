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
/// Gère le fondu (fade) de la ville en modifiant l'alpha du matériau au fil du temps.
/// </summary>
public class FadeVille : MonoBehaviour
{
    public Material mat;
    public float dureeTransformation;
    public float etatFinal;
    public bool transformer;
    float tempsEcoule;
    float etatTransformation;

    /// <summary>
    /// Initialise le script et définit l'alpha initial de la ville.
    /// </summary>
    private void Start()
    {
        transformer = false;
        SetVilleAlpha();
    }

    /// <summary>
    /// Met à jour la transformation de l'alpha si le fade est activé.
    /// </summary>
    void Update()
    {
        if (transformer)
        {
            if (tempsEcoule < dureeTransformation)
            {
                // Pourcentage du temps écoulé par rapport au temps défini
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

    /// <summary>
    /// Définit l'alpha du matériau de la ville à sa valeur maximale (1.0).
    /// </summary>
    public void SetVilleAlpha()
    {
        mat.SetFloat("_Alpha", 1.0f);
    }

    /// <summary>
    /// Active le processus de transformation pour faire disparaître la ville progressivement.
    /// </summary>
    public void TransformerVille()
    {
        transformer = true;
    }
}