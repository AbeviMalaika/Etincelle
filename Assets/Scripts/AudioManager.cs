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

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        if(volumeMusique != 0) controleurVolMusique.value = volumeMusique;
        if(volumeSFX != 0) controleurVolSFX.value = volumeSFX;
           
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
}
