using UnityEngine;
using System.Collections;

public class ZonePortail : MonoBehaviour
{
    public static bool entreeZone;

    private Coroutine timerCoroutine;

    void Start()
    {
        entreeZone = false;
    }

    private void OnTriggerEnter(Collider infoCollider)
    {
        if (infoCollider.gameObject.name == "PlayerController")
        {
            // Démarre le timer quand le joueur entre
            timerCoroutine = StartCoroutine(TempsDansZone());
        }
    }

    private void OnTriggerExit(Collider infoCollider)
    {
        if (infoCollider.gameObject.name == "PlayerController")
        {
            // Annule le timer si le joueur sort avant 5 secondes
            if (timerCoroutine != null)
            {
                StopCoroutine(timerCoroutine);
                timerCoroutine = null;
            }

            entreeZone = false;
            Debug.Log("Le joueur a quitté la zone");
        }
    }

    IEnumerator TempsDansZone()
    {
        yield return new WaitForSeconds(5f);

        entreeZone = true;
        Debug.Log("Le joueur est resté 5 secondes dans la zone");
    }
}
