using UnityEngine;

public class CahierTransformations : MonoBehaviour
{
    public static bool estEfface;
    public static bool etincelleDessine;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider infoCollision)
    {
        // Si l'objet se fait toucher, il se transforme (Effacer le dessin à la quête 1)
        if (infoCollision.gameObject.name == "Efface" && QuestManager.Instance.queteActuelle.questID == "1")
        {
            estEfface = true;
            print("<color=green>Objet touché: " + infoCollision.gameObject.name + "</color>");
        }

        // Si l'objet se fait toucher, il se transforme (Dessiner une étincelle à la dessin à la quête 4)
        if (infoCollision.gameObject.name == "Mine" && QuestManager.Instance.queteActuelle.questID == "4")
        {
            etincelleDessine = true;
            print("<color=green>Objet touché: " + infoCollision.gameObject.name + "</color>");
        }
    }
}
