using UnityEngine;

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

    Material mat;
    void Start()
    {
        //Initialisation du matériel
        etatFinal = !inverse ? 1.85f : 0f;
        etatDepart = !inverse ? 0f : 1.85f;
        initialisation = true;
        modifCahier = false;

        mat = GetComponent<MeshRenderer>().material;
        mat.SetFloat("_Effacage", etatDepart);
    }

    void Update()
    {
        //mat.SetFloat("_Effacage", Mathf.PingPong(Time.time, 1f));
        //print(mat.name);
        etatFinal = !inverse ? 1.85f : 0f;
        etatDepart = !inverse ? 0f : 1.85f;

        if (modifCahier)
        {
            print("Le cahier a été modifié");
            if (tempsEcoule < dureeTransformation)
            {
                // Pourcentage du temps écoulé par rapport au temps de modifCahier défini
                float t = tempsEcoule / dureeTransformation;

                // Transition smooth
                t = Mathf.SmoothStep(0f, 1f, t);

                // Application de la modifCahier
                etatTransformation = Mathf.Lerp(etatDepart, etatFinal, t);
                tempsEcoule += Time.deltaTime;
                mat.SetFloat("_Effacage", etatTransformation);
            }

            else
            {
                // Set l'état final
                etatTransformation = etatFinal;
                tempsEcoule = 0f;
                modifCahier = false;
                initialisation = false;
                inverse = !inverse;
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

    private void OnTriggerEnter(Collider infoCollision)
    {
        // Si l'objet se fait toucher, il se transforme (Effacer le dessin à la quête 1)
        if (infoCollision.gameObject.name == "Efface" && QuestManager.Instance.queteActuelle.questID == "1")
        {
            modifCahier = true;
            print("<color=green>Objet touché: " + infoCollision.gameObject.name + "</color>");
        }

        // Si l'objet se fait toucher, il se transforme (Dessiner une étincelle à la dessin à la quête 4)
        if (infoCollision.gameObject.name == "Mine" && QuestManager.Instance.queteActuelle.questID == "4")
        {
            modifCahier = true;
            print("<color=green>Objet touché: " + infoCollision.gameObject.name + "</color>");
        }
    }
}


