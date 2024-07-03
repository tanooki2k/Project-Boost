using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class Movement : MonoBehaviour
{
    [SerializeField] float mainThrust = 100f;
    [SerializeField] float rotationThrust = 50f;
    [SerializeField] AudioClip mainEngine;

    [SerializeField] ParticleSystem mainEngineParticles;
    [SerializeField] ParticleSystem leftThrusterParticles;
    [SerializeField] ParticleSystem rightThrusterParticles;

    Rigidbody _rb;
    AudioSource _audioSource;

    public bool _isAlive;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            StartThrusting();
        }
        else
        {
            StopThrusting();
        }
    }


    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            RotateLeft();
        }
        else if (Input.GetKey(KeyCode.D))
        {
            RotateRight();
        }
        else
        {
            StopRotating();
        }
    }

    void ApplyRotation(float rotationThisFrame)
    {
        _rb.freezeRotation = true; // freezing rotation so we can manually rotate
        transform.Rotate(Vector3.forward * (rotationThisFrame * Time.deltaTime));
        _rb.freezeRotation = false; // unfreezing rotation so the physics system can take over
    }

    void StopThrusting()
    {
        _audioSource.Stop();
        mainEngineParticles.Stop();
    }

    void StartThrusting()
    {
        _rb.AddRelativeForce(Vector3.up * (mainThrust * Time.deltaTime));
        PlaySound(mainEngine);
        PlayParticle(mainEngineParticles);
    }

    void RotateRight()
    {
        PlayParticle(rightThrusterParticles);
        ApplyRotation(-rotationThrust);
    }


    void RotateLeft()
    {
        PlayParticle(leftThrusterParticles);
        ApplyRotation(rotationThrust);
    }

    void StopRotating()
    {
        leftThrusterParticles.Stop();
        rightThrusterParticles.Stop();
    }

    void PlayParticle(ParticleSystem particle)
    {
        if (!particle.isPlaying)
        {
            particle.Play();
        }
    }

    void PlaySound(AudioClip sound)
    {
        if (!_audioSource.isPlaying)
        {
            _audioSource.PlayOneShot(sound);
        }
    }
}