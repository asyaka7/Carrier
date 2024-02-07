using Assets.Code.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    [InspectorName("Movement")]
    [SerializeField]
    MovementSettings movementSettings = new MovementSettings()
    {
        jumpSpeed = 1f,
        backSpeed = 0.5f,
        directSpeed = 3f,
        walkSpeed = 1f,
    };

    [SerializeField]
    AudioClip flapAudio;


    private Rigidbody rb;
    private Animator animator;

    bool isTransitioning = false;
    bool collisionEnabled = true;

    bool isFlying = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            MakeFlap();  
        }

        if (isFlying)
        {
            Fly();
        }
        else
        {
            Walk();
        }
    }

    private void MakeFlap()
    {
        isFlying = true;
        PlayFlySound();
        PlayFlyAnimation();
        MoveUp();
    }

    private void Fly()
    {
        // todo: remove back movement? or rotate the bird
        if (Input.GetKey(KeyCode.A))
        {
            ApplyRotation(-movementSettings.backSpeed);
        }

        if (Input.GetKey(KeyCode.D))
        {
            ApplyRotation(movementSettings.directSpeed);
        }
    }

    private void PlayFlySound()
    {
        if (!AudioPlayer.Instance.IsPlaying)
        {
            AudioPlayer.Instance.Play(flapAudio);
        }
    }

    private void PlayFlyAnimation()
    {
        animator.SetBool("isFlying", true);
    }

    private void MoveUp()
    {
        rb.freezeRotation = true;
        Vector3 upForce = Vector3.up * movementSettings.jumpSpeed * Time.deltaTime;
        rb.AddRelativeForce(upForce);
        //Debug.Log($"UP: {upForce}");
        rb.freezeRotation = false;
    }

    private void ApplyRotation(float speed)
    {
        //rb.AddRelativeForce(directSpeed * Time.deltaTime, 0, 0);
        //Debug.Log($"F: {Vector3.back}");
        Vector3 fwd = Vector3.back * speed * Time.deltaTime;
        //Debug.Log($"D: {fwd}");
        transform.Rotate(fwd);
    }

    private void Walk()
    {
        if (Input.GetKey(KeyCode.A))
        {
            transform.position -= new Vector3(movementSettings.walkSpeed*Time.deltaTime, 0, 0);
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.position += new Vector3(movementSettings.walkSpeed*Time.deltaTime, 0, 0);
        }
    }

    // collision
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
                LandBird();
                break;
        }
    }

    private void StartCrushSequence()
    {
        // todo: add notification about death
        // todo: add bird-death effect
        isTransitioning = true;
        GetComponent<PlayerMovement>().enabled = false;
        //audioSource?.PlayOneShot(gameSettings.crushAudio);
        animator.SetBool("isDead", true);
        //gameSettings.crushParticle?.Play();
        //Invoke("ReloadLevel", gameSettings.reloadDelay);
        GameManager.Instance.GameOver();
    }

    private void StartWinSequence()
    {
        // todo: add win effects
        isTransitioning = true;
        //audioSource?.PlayOneShot(gameSettings.winAudio);
        //gameSettings.winParticle?.Play();
        //Invoke("ReloadLevel", gameSettings.reloadDelay);
        GameManager.Instance.Win();
    }



    private void LandBird()
    {
        isFlying = false;
        rb.freezeRotation = false;
        transform.rotation = Quaternion.identity;
        rb.freezeRotation = true;
        animator.SetBool("isFlying", false);
    }
}
