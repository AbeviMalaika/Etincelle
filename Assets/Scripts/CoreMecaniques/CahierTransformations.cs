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
/// Gère les transformations du cahier pendant les quêtes.
/// Permet d'effacer ou de dessiner, de gérer l'état et l'animation de la texture.
/// </summary>
public class CahierTransformations : MonoBehaviour
{
    public bool modifCahier;
    public bool inverse;
    bool initialisation;
    public float dureeTransformation;
    float tempsEcoule;
    float etatTransformation;
    float etatFinal;
    float etatDepart;
    public bool autoriserModification;

    public Texture2D textureCroquisFinal;

    Material mat;

    /// <summary>
    /// Initialise le matériel du cahier et les variables de transformation.
    /// </summary>
    void Start()
    {
        //Initialisation du matériel
        etatFinal = !inverse ? 1.85f : 0f;
        etatDepart = !inverse ? 0f : 1.85f;
        initialisation = true;
        modifCahier = false;
        autoriserModification = false;

        mat = GetComponent<MeshRenderer>().material;
        mat.SetFloat("_Effacage", etatDepart);
    }

    /// <summary>
    /// Met à jour la transformation du cahier chaque frame si nécessaire.
    /// </summary>
    void Update()
    {
        etatFinal = !inverse ? 1.85f : 0f;
        etatDepart = !inverse ? 0f : 1.85f;

        if(autoriserModification)
        {
            if (modifCahier)
            {
                print("Le cahier a été modifié");
                if (tempsEcoule < dureeTransformation)
                {
                    float t = tempsEcoule / dureeTransformation;
                    t = Mathf.SmoothStep(0f, 1f, t);

                    etatTransformation = Mathf.Lerp(etatDepart, etatFinal, t);
                    tempsEcoule += Time.deltaTime;
                    mat.SetFloat("_Effacage", etatTransformation);
                }
                else
                {
                    etatTransformation = etatFinal;
                    tempsEcoule = 0f;
                    modifCahier = false;
                    initialisation = false;
                    inverse = !inverse;
                    autoriserModification = false;
                }
            }
            else
            {
                if (initialisation)
                {
                    mat.SetFloat("_Effacage", etatDepart);
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
        if (infoCollision.gameObject.name == "Efface" && autoriserModification && QuestManager.Instance.queteActuelle.questID == "1")
        {
            modifCahier = true;
            print("<color=green>Objet touché: " + infoCollision.gameObject.name + "</color>");
        }

        // Dessiner une étincelle à la quête 4
        if (infoCollision.gameObject.name == "Mine" && autoriserModification && QuestManager.Instance.queteActuelle.questID == "4")
        {
            modifCahier = true;
            print("<color=green>Objet touché: " + infoCollision.gameObject.name + "</color>");
        }
    }

    /// <summary>
    /// Change l'image du cahier pour afficher le croquis final.
    /// </summary>
    public void SwitchCroquisFinal()
    {
        mat.SetTexture("_Dessin", textureCroquisFinal);
    }
}