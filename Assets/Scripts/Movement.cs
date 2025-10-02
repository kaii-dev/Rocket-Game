using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [SerializeField] InputAction thrust;
    [SerializeField] InputAction rotation;
    [SerializeField] float thrustStreagth = 100f;
    [SerializeField] float rotationStreagth = 100f;
    [SerializeField] AudioClip MainEngine;
    [SerializeField] ParticleSystem mainBooster;
    [SerializeField] ParticleSystem rightBooster;
    [SerializeField] ParticleSystem leftBooster;


    Rigidbody rb;
    AudioSource audioSource;

//=======================================
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        thrust.Enable();
        rotation.Enable();
    }
//============================================

    private void FixedUpdate()
    {
        ProcessThrust();
        ProcessRotation();
    }

    private void ProcessThrust()
    {
        if (thrust.IsPressed())
        {
            startThrusting();
        }
        else
        {
            audioSource.Stop();
            mainBooster.Stop();
        }
    }

    private void ProcessRotation()
    {
        startRotation();
    }
//==========================================

    private void startThrusting() //move the rocket up
    {
        rb.AddRelativeForce(Vector3.up * thrustStreagth * Time.fixedDeltaTime);
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(MainEngine);
        }
        if (!mainBooster.isPlaying)
        {
            mainBooster.Play();
        }
    }
    


    private void startRotation() //move the rocket right and left
    {
        float rotationInput = rotation.ReadValue<float>();
        if (rotationInput < 0)
        {
            ApplyRotation(rotationStreagth);

            if (!rightBooster.isPlaying)
            {
                leftBooster.Stop();
                rightBooster.Play();
            }
        }

        else if (rotationInput > 0)
        {
            ApplyRotation(-rotationStreagth);
            if (!leftBooster.isPlaying)
            {
                rightBooster.Stop();
                leftBooster.Play();
            }
        }
        else
        {
            rightBooster.Stop();
            leftBooster.Stop();
        }
    }



    private void ApplyRotation(float rotationThisFrame) //Still dont know exactly
    {
        rb.freezeRotation = true;
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.fixedDeltaTime);
        rb.freezeRotation = false;

    }
}
