using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OrdinateurTexteInput : MonoBehaviour
{
    public static bool texteSupp;
    public Text textInputed;
    public TextMeshProUGUI textOutputed;
    public string texteFinal;
    public bool enterClique;

    public Image fadeEcran;

    public float dureeTransformation;

    void Start()
    {
        texteSupp = false;
    }

    // Update is called once per frame
    void Update()
    {
        textOutputed.text = textInputed.text;

        if (textOutputed.text == "")
        {
            texteSupp = true;
        }
    }

    public void ChangerTexte()
    {
        textInputed.text = texteFinal;
        fadeEcran.color = new Color(1f, 1f, 1f, 1f);
    }

    public void DevoilerTexteFinal()
    {
        StartCoroutine(FadeRoutine());
    }

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
    }

    public void detectEnter()
    {
        enterClique = true;
        print("Enter pressed");
    }
}
