using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;

public class ZonePortail : MonoBehaviour
{
    public bool entreeZone;

    private Coroutine timerCoroutine;

    public CinemachineManager targetSwitcher;
    public Transform target;

    public GameObject decoPlateau;

    public List<DynamisationShaderMeuble> transfoMeubles;
    public TextMeshProUGUI textOrdi;

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
        yield return new WaitForSeconds(3f);

        entreeZone = true;
        Debug.Log("Le joueur est resté 5 secondes dans la zone");
    }


    public void RetourChambre()
    {
        StartCoroutine(corou_RetourChambre());
    }

    IEnumerator corou_RetourChambre()
    {
        GameManager.Instance.ChangerCouleurFade();

        yield return new WaitForSeconds(0.5f);

        GameManager.Instance.FadeOut();

        yield return new WaitForSeconds(2.5f);

        targetSwitcher.TargetSwitch(target);

        yield return new WaitForSeconds(0.5f);

        decoPlateau.SetActive(false);

        GameManager.Instance.FadeIn();

        yield return new WaitForSeconds(0.5f);

        foreach (DynamisationShaderMeuble transfoM in transfoMeubles)
        {
            transfoM.transformation = true;
        }

        yield return null;
    }
}
