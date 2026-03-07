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
/// Gère la transformation visuelle de la ville via un shader.
/// Permet de faire disparaître ou réinitialiser l'apparence de la ville sur une durée définie.
/// </summary>
public class DisparitionVille : MonoBehaviour
{
    public Material mat;
    public bool transformation;
    public float etatFinal;
    public float dureeTransformation;
    float tempsEcoule;
    float etatTransformation;

    /// <summary>
    /// Initialise l'état du matériel au début de la scène.
    /// </summary>
    void Start()
    {
        //Initialisation du matériel
        mat.SetFloat("_Degre_Transformation", 0);
        transformation = false;
    }

    /// <summary>
    /// Met à jour la progression de la transformation de la ville à chaque frame.
    /// </summary>
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

    /// <summary>
    /// Déclenche la transformation progressive de la ville.
    /// </summary>
    public void TransformerVille() { transformation = true; }

    /// <summary>
    /// Réinitialise l'apparence de la ville à son état initial.
    /// </summary>
    public void ResetVille() { mat.SetFloat("_Degre_Transformation", 1); }
}