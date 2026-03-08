/***
 * 
 * ÉTINCELLE
 * 
 * Par Malaïka Abevi
 * Dernière modification : 06/03/2026 
 * 
 */

using UnityEngine;
using UnityEngine.Playables;

/// <summary>
/// Gère le PlayableDirector pour les cinématiques et timelines du jeu.
/// Permet de démarrer, mettre en pause, arrêter et valider des étapes de la timeline.
/// </summary>
public class TimelineManager : MonoBehaviour
{
    public static TimelineManager Instance;
    public bool cinematiqueTerminee;
    PlayableDirector director;
    public bool timelinePause;  // Booléenne pour vérifier si le Timeline est en pause ou non
    public bool entreeLho;
    public bool introTerminee;

    /// <summary>
    /// Singleton : Initialise l'instance de TimelineManager
    /// </summary>
    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    /// <summary>
    /// Initialise les variables et met le PlayableDirector en pause au démarrage.
    /// </summary>
    void Start()
    {
        cinematiqueTerminee = false;
        introTerminee = false;
        director = GetComponent<PlayableDirector>();

        // Timeline à 0 et en pause au début du jeu
        director.playOnAwake = false;
        director.Stop();

        timelinePause = true;
    }

    /// <summary>
    /// Met le Timeline en pause et met à jour la variable timelinePause.
    /// </summary>
    public void PauseTimeline()
    {
        director.Pause();
        timelinePause = true;
    }

    /// <summary>
    /// Joue le Timeline et met à jour la variable timelinePause.
    /// </summary>
    public void PlayTimeline()
    {
        director.Play();
        timelinePause = false;
    }

    /// <summary>
    /// Termine la cinématique, met à jour cinematiqueTerminee et appelle la fonction de fin de décor du GameManager.
    /// </summary>
    public void TerminerCinematique()
    {
        cinematiqueTerminee = true;
        GameManager.Instance.SetDecoFin();
        director.Stop();
    }

    /// <summary>
    /// Arrête le PlayableDirector et force l'évaluation de sa position actuelle.
    /// </summary>
    public void StopResetDirector()
    {
        director.Stop();
        director.Evaluate();
    }

    /// <summary>
    /// Valide l'entrée de la narratrice.
    /// </summary>
    public void ValiderEntreeLho() { entreeLho = true; }

    /// <summary>
    /// Valide la fin de l'introduction et met introTerminee à true.
    /// </summary>
    public void ValiderIntro() { introTerminee = true; }
}