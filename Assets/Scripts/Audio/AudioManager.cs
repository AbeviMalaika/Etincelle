/***
 * 
 * ÉTINCELLE
 * 
 * Par Malaïka Abevi
 * Dernière modification : 06/03/2026 
 * 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

/// <summary>
/// Gère le son global du jeu : musique, SFX, monologues et transitions.
/// Fournit des sliders pour contrôler les volumes et des méthodes pour changer la musique ou les monologues.
/// </summary>
public class AudioManager : MonoBehaviour
{
    /// <summary>
    /// Instance singleton du AudioManager.
    /// </summary>
    public static AudioManager Instance;

    public AudioMixer audioMixer;

    [SerializeField] private Slider controleurVolMusique;
    [SerializeField] private Slider controleurVolSFX;

    public float volumeMusiqueDefaut;
    public float volumeSFXDefaut;

    public static float volumeMusique;
    public static float volumeSFX;

    public AudioSource pisteSFX;

    public AudioSource mPiste1;
    public AudioSource mPiste2;
    public float vitesseTransition;

    public AudioSource pisteMonologue;
    int compteurMonologue;
    public List<AudioClip> monologueListe;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        if (volumeMusique != 0) controleurVolMusique.value = volumeMusique;
        if (volumeSFX != 0) controleurVolSFX.value = volumeSFX;

        AjusterVolumeMusique();
        AjusterVolumeSFX();
    }

    /// <summary>
    /// Ajuste le volume de la musique selon la valeur du slider.
    /// </summary>
    public void AjusterVolumeMusique()
    {
        audioMixer.SetFloat("volMusique", controleurVolMusique.value);
        volumeMusique = controleurVolMusique.value;
    }

    /// <summary>
    /// Ajuste le volume des effets sonores selon la valeur du slider.
    /// </summary>
    public void AjusterVolumeSFX()
    {
        audioMixer.SetFloat("volSFX", controleurVolSFX.value);
        volumeSFX = controleurVolSFX.value;
    }

    /// <summary>
    /// Réinitialise les volumes de musique et SFX aux valeurs par défaut.
    /// </summary>
    public void ReinitialiserOptions()
    {
        volumeMusique = volumeMusiqueDefaut;
        volumeSFX = volumeSFXDefaut;

        controleurVolMusique.value = volumeMusiqueDefaut;
        controleurVolSFX.value = volumeSFXDefaut;

        AjusterVolumeMusique();
        AjusterVolumeSFX();
    }

    /// <summary>
    /// Change la musique actuelle et lance la transition vers le clip choisi.
    /// </summary>
    /// <param name="musiqueChoisie">Clip audio à jouer.</param>
    public void ChangementMusique(AudioClip musiqueChoisie)
    {
        AudioSource pisteEnCours = mPiste1;
        AudioSource pisteChoisie = mPiste2;

        if (!pisteEnCours.isPlaying)
        {
            pisteEnCours = mPiste2;
            pisteChoisie = mPiste1;
        }

        pisteChoisie.clip = musiqueChoisie;

        StopAllCoroutines();
        StartCoroutine(TransitionMusique(pisteChoisie, pisteEnCours));
    }

    /// <summary>
    /// Coroutine qui effectue une transition douce entre deux pistes musicales.
    /// </summary>
    /// <param name="pisteChoisie">AudioSource de la piste à faire monter.</param>
    /// <param name="pisteEnCours">AudioSource de la piste à faire descendre.</param>
    /// <returns></returns>
    IEnumerator TransitionMusique(AudioSource pisteChoisie, AudioSource pisteEnCours)
    {
        float temps = 0;

        pisteChoisie.volume = 0;
        pisteChoisie.Play();

        while (temps < 1)
        {
            temps += Time.deltaTime * vitesseTransition;

            float t = Mathf.SmoothStep(0f, 1f, Mathf.Clamp01(temps));

            pisteEnCours.volume = Mathf.Lerp(1f, 0f, t);
            pisteChoisie.volume = Mathf.Lerp(0f, 0.7f, t);

            yield return null;
        }

        pisteEnCours.volume = 0f;
        pisteChoisie.volume = 0.7f;

        pisteEnCours.Pause();
    }

    /// <summary>
    /// Fait remonter le volume de la piste musicale à son volume normal.
    /// </summary>
    /// <param name="musique">AudioSource à modifier.</param>
    public void FadeMusiqueNormal(AudioSource musique)
    {
        ChangerVolume(musique, 1f, 1f);
    }

    /// <summary>
    /// Fait baisser le volume de la piste musicale jusqu’au silence.
    /// </summary>
    /// <param name="musique">AudioSource à modifier.</param>
    public void FadeMusiqueMute(AudioSource musique)
    {
        ChangerVolume(musique, 0f, 1f);
    }

    /// <summary>
    /// Change progressivement le volume d’une AudioSource vers une valeur cible.
    /// </summary>
    /// <param name="source">AudioSource à modifier.</param>
    /// <param name="volumeCible">Volume cible.</param>
    /// <param name="vitesse">Vitesse de transition.</param>
    public void ChangerVolume(AudioSource source, float volumeCible, float vitesse)
    {
        StopCoroutine("TransitionVolume");
        StartCoroutine(TransitionVolume(source, volumeCible, vitesse));
    }

    /// <summary>
    /// Coroutine qui effectue la transition du volume d’une AudioSource.
    /// </summary>
    /// <param name="source">AudioSource à modifier.</param>
    /// <param name="volumeCible">Volume final souhaité.</param>
    /// <param name="vitesse">Vitesse de transition.</param>
    /// <returns></returns>
    IEnumerator TransitionVolume(AudioSource source, float volumeCible, float vitesse)
    {
        float volumeInitial = source.volume;
        float temps = 0;

        while (temps < 1)
        {
            temps += Time.deltaTime * vitesse;

            float t = Mathf.SmoothStep(0f, 1f, temps);
            source.volume = Mathf.Lerp(volumeInitial, volumeCible, t);

            yield return null;
        }

        source.volume = volumeCible;
    }

    /// <summary>
    /// Joue un monologue depuis la liste et passe au suivant pour le prochain appel.
    /// </summary>
    public void JouerSFX(AudioClip sonSFX)
    {
        pisteSFX.PlayOneShot(sonSFX);
    }

    /// <summary>
    /// Joue un monologue depuis la liste et passe au suivant pour le prochain appel.
    /// </summary>
    public void ChangerMonologue()
    {
        pisteMonologue.PlayOneShot(monologueListe[compteurMonologue]);

        if (compteurMonologue < monologueListe.Count)
        {
            compteurMonologue++;
        }
    }
}