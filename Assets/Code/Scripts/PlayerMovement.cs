using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    public float JumpSpeed = 1f;
    [SerializeField]
    public float backSpeed = 0.5f;
    [SerializeField]
    public float directSpeed = 3f;

    private Rigidbody rb;
    private AudioSource audioSource;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
        audioSource = rb.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            PlayFlySound();
            PlayFlyAnimation();
            MoveUp();
        }

        // todo: remove back movement? or rotate the bird
        if (Input.GetKey(KeyCode.A))
        {
            ApplyRotation(-backSpeed);
        }

        if (Input.GetKey(KeyCode.D))
        {
            ApplyRotation(directSpeed);
        }
    }

    private void PlayFlySound()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }

    private void PlayFlyAnimation()
    {
        animator.SetBool("isFlying", true);
    }

    private void MoveUp()
    {
        rb.freezeRotation = true;
        Vector3 upForce = Vector3.up * JumpSpeed * Time.deltaTime;
        rb.AddRelativeForce(upForce);
        Debug.Log($"UP: {upForce}");
        rb.freezeRotation = false;
    }

    private void ApplyRotation(float speed)
    {
        //rb.AddRelativeForce(directSpeed * Time.deltaTime, 0, 0);
        Debug.Log($"F: {Vector3.back}");
        Vector3 fwd = Vector3.back * speed * Time.deltaTime;
        Debug.Log($"D: {fwd}");
        transform.Rotate(fwd);
    }
}
