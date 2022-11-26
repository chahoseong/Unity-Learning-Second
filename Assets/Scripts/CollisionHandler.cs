using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] private float delayTime;
    [SerializeField] private AudioClip successAudioClip;
    [SerializeField] private AudioClip crashAudioClip;

    private Movement movement;
    private AudioSource audioSource;

    private bool transitioning = false;

    private void Start()
    {
        movement = GetComponent<Movement>();
        audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (transitioning)
        {
            return;
        }

        switch (collision.gameObject.tag)
        {
            case "Friendly":
                break;
            case "Finish":
                StartNextScene();
                break;
            default:
                StartReloadScene();
                break;
        }
    }

    private void StartNextScene()
    {
        transitioning = true;
        movement.enabled = false;
        audioSource.PlayOneShot(successAudioClip);
        Invoke(nameof(LoadNextScene), delayTime);
    }

    private void LoadNextScene()
    {
        int currentIndex = SceneManager.GetActiveScene().buildIndex;
        int nextIndex = (currentIndex + 1) % SceneManager.sceneCountInBuildSettings;
        SceneManager.LoadScene(nextIndex);
    }

    private void StartReloadScene()
    {
        transitioning = true;
        movement.enabled = false;
        audioSource.PlayOneShot(crashAudioClip);
        Invoke(nameof(ReloadScene), delayTime);
    }

    private void ReloadScene()
    {
        int currentIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentIndex);
    }
}
