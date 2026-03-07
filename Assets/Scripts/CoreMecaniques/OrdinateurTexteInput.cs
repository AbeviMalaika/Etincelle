/***
 * 
 * ÉTINCELLE
 * 
 * Par Malaïka Abevi
 * Dernière modification : 06/03/2026 
 * 
 */

using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Gère l'affichage et la révélation progressive du texte d'un ordinateur dans la scène.
/// Permet de détecter la suppression du texte et de lancer un fondu pour révéler le texte final.
/// </summary>
public class OrdinateurTexteInput : MonoBehaviour
{
    public bool texteSupp;
    public bool texteDevoile;

    public Text textInputed;
    public TextMeshProUGUI textOutputed;
    public string texteFinal;

    public Image fadeEcran;

    public float dureeTransformation;

    /// <summary>
    /// Initialise les variables d'état du texte au lancement.
    /// </summary>
    void Start()
    {
        texteSupp = false;
        texteDevoile = false;
    }

    /// <summary>
    /// Met à jour le texte affiché à l'écran et vérifie si le texte a été complètement supprimé.
    /// </summary>
    void Update()
    {
        textOutputed.text = textInputed.text;

        if (textOutputed.text == "")
        {
            texteSupp = true;
        }
    }

    /// <summary>
    /// Remplace le texte entré par le texte final et rend l'écran complètement blanc.
    /// </summary>
    public void ChangerTexte()
    {
        textInputed.text = texteFinal;
        fadeEcran.color = new Color(1f, 1f, 1f, 1f);
    }

    /// <summary>
    /// Lance la coroutine qui effectue le fondu pour révéler le texte final.
    /// </summary>
    public void DevoilerTexteFinal()
    {
        StartCoroutine(FadeRoutine());
    }

    /// <summary>
    /// Coroutine qui diminue progressivement l'opacité de l'écran blanc pour révéler le texte.
    /// </summary>
    IEnumerator FadeRoutine()
    {
        float tempsEcoule = 0f;

        while (tempsEcoule < dureeTransformation)
        {
            float t = tempsEcoule / dureeTransformation;
            t = Mathf.SmoothStep(0f, 1f, t);

            float alpha = Mathf.Lerp(1f, 0f, t);

            Color c = fadeEcran.color;
            c.a = alpha;
            fadeEcran.color = c;

            tempsEcoule += Time.deltaTime;
            yield return null; // Attend la frame suivante
        }

        // Assure que l'alpha final est bien à 0
        Color finalColor = fadeEcran.color;
        finalColor.a = 0f;
        fadeEcran.color = finalColor;

        texteDevoile = true;
    }
}