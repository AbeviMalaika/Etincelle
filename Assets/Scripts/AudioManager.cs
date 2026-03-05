using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    // Variables pour les options par défaut
    public float volumeMusiqueDefaut;
    public float volumeSFXDefaut;
    public AudioSource pisteSFX;

    // Variables pour les options actuellement manipulées par le joueur
    public static float volumeMusique;
    public static float volumeSFX;

    // Éléments à manipuler
    public AudioMixer audioMixer; // AudioMixer pour la musique et les SFX

    // Gestion de l'apparence du UI des options
    [SerializeField] private Slider controleurVolMusique;
    [SerializeField] private Slider controleurVolSFX;

    public AudioSource mPiste1;
    public AudioSource mPiste2;
    public float vitesseTransition;

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

    // Fonction pour ajuster le volume de la musique
    public void AjusterVolumeMusique()
    {
        audioMixer.SetFloat("volMusique", controleurVolMusique.value);
        volumeMusique = controleurVolMusique.value;
    }

    // Fonction pour ajuster le volume des effets sonores
    public void AjusterVolumeSFX()
    {
        audioMixer.SetFloat("volSFX", controleurVolSFX.value);
        volumeSFX = controleurVolSFX.value;
    }

    // Fonction pour réinitialiser les options
    public void ReinitialiserOptions()
    {
        volumeMusique = volumeMusiqueDefaut;
        volumeSFX = volumeSFXDefaut;

        controleurVolMusique.value = volumeMusiqueDefaut;
        controleurVolSFX.value = volumeSFXDefaut;

        AjusterVolumeMusique();
        AjusterVolumeSFX();
    }

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

    IEnumerator TransitionMusique(AudioSource pisteChoisie, AudioSource pisteEnCours)
    {
        float temps = 0;

        pisteChoisie.volume = 0;
        pisteChoisie.Play();

        while (temps < 1)
        {
            temps += Time.deltaTime * vitesseTransition;

            float t = Mathf.SmoothStep(0f, 1f, Mathf.Clamp01(temps));

            pisteEnCours.volume = Mathf.Lerp(1, 0, t);
            pisteChoisie.volume = Mathf.Lerp(0, 1, t);

            yield return null;
        }

        pisteEnCours.Pause();
    }

    public void FadeMusiqueNormal(AudioSource musique)
    {
        ChangerVolume(musique, 1f, 1f);
    }

    public void FadeMusiqueMute(AudioSource musique)
    {
        ChangerVolume(musique, 0f, 1f);
    }

    public void ChangerVolume(AudioSource source, float volumeCible, float vitesse)
    {
        StopCoroutine("TransitionVolume");
        StartCoroutine(TransitionVolume(source, volumeCible, vitesse));
    }

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
}
