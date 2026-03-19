using UnityEngine;
using UnityEngine.UI;

public class TelephoneFonctionnement : MonoBehaviour
{
    public Image imageEcran;
    public bool allumerTelephone;
    public bool inverse;
    public float fadeTemps;
    public bool autorisationFade;

    float tempsEcoule;
    Color etatFinal;
    Color etatDepart;

    /// <summary>
    /// Initialise le matériel du cahier et les variables de transformation.
    /// </summary>
    void Start()
    {
        //Initialisation de l'image
        etatFinal = !inverse ? Color.black : Color.white;
        etatDepart = !inverse ? Color.white : Color.black;
        allumerTelephone = false;
        autorisationFade = false;
    }

    /// <summary>
    /// Met à jour la transformation du cahier chaque frame si nécessaire.
    /// </summary>
    void Update()
    {
        etatFinal = !inverse ? Color.black : Color.white;
        etatDepart = !inverse ? Color.white : Color.black;

        if (autorisationFade)
        {
            if (allumerTelephone)
            {
                print("Le cahier a été modifié");
                if (tempsEcoule < fadeTemps)
                {
                    float t = tempsEcoule / fadeTemps;
                    t = Mathf.SmoothStep(0f, 1f, t);

                    Color color = Color.Lerp(etatDepart, etatFinal, t);

                    tempsEcoule += Time.deltaTime;

                    imageEcran.color = color;
                }
                else
                {
                    imageEcran.color = etatFinal;
                    tempsEcoule = 0f;
                    allumerTelephone = false;
                    inverse = !inverse;
                    autorisationFade = false;
                }
            }
        }
    }

    /// <summary>
    /// Détecte les collisions avec l'efface ou la mine et active la modification du cahier selon la quête actuelle.
    /// </summary>
    /// <param name="infoCollision">Collider de l'objet entrant en contact.</param>
    private void OnTriggerEnter(Collider infoCollision)
    {
        // Effacer le dessin à la quête 1
        if (infoCollision.gameObject.name == "Efface" && autorisationFade && QuestManager.Instance.queteActuelle.questID == 1)
        {
            allumerTelephone = true;
            print("<color=green>Objet touché: " + infoCollision.gameObject.name + "</color>");
        }

        // Dessiner une étincelle à la quête 4
        if (infoCollision.gameObject.name == "Mine" && autorisationFade && QuestManager.Instance.queteActuelle.questID == 4)
        {
            allumerTelephone = true;
            print("<color=green>Objet touché: " + infoCollision.gameObject.name + "</color>");
        }
    }
}
