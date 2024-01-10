using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCollisionBehavior : MonoBehaviour
{
    [SerializeField]
    public float reloadDelay = 2f;
    [SerializeField]
    public AudioClip crushAudio;
    [SerializeField]
    public AudioClip winAudio;

    [SerializeField]
    ParticleSystem winParticle;
    [SerializeField]
    ParticleSystem crushParticle;

    AudioSource audioSource;
    bool isTransitioning = false;
    bool collisionEnabled = true;

    private Animator animator;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        RespondToDebugKey();
    }

    private void RespondToDebugKey()
    {
        if(Input.GetKeyDown(KeyCode.C))
        {
            collisionEnabled = !collisionEnabled; // turn on / turn off
            Debug.Log($"Collision enabled: {collisionEnabled}");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (isTransitioning || !collisionEnabled) { return; }

        switch (collision.gameObject.tag)
        {
            case "Deadly":
                StartCrushSequence();
                break;
            case "Finish":
                StartWinSequence();
                break;
            case "Ground":
                CorrectBirdBehavior();
                break;
        }
    }

    private void StartCrushSequence()
    {
        // todo: add notification about death
        // todo: add bird-death effect
        isTransitioning = true;
        GetComponent<PlayerMovement>().enabled = false;
        audioSource?.PlayOneShot(crushAudio);
        animator.SetBool("isDead", true);
        crushParticle?.Play();
        Invoke("ReloadLevel", reloadDelay);
    }

    private void StartWinSequence()
    {
        // todo: add win effects
        isTransitioning = true;
        audioSource?.PlayOneShot(winAudio);
        winParticle?.Play();
        Invoke("ReloadLevel", reloadDelay);
    }

    public void ReloadLevel()
    {
        string activeSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(activeSceneName);
    }

    private void CorrectBirdBehavior()
    {
        animator.SetBool("isFlying", false);
    }
}
