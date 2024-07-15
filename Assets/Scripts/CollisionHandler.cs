using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float levelLoadDelay = 1f;
    [SerializeField] AudioClip success;
    [SerializeField] AudioClip crash;

    [SerializeField] ParticleSystem successParticles;
    [SerializeField] ParticleSystem crashParticles;

    Movement _movement;
    AudioSource _audioSource;

    bool _isTransitioning = false;
    bool _collisionDisable = false;

    void Start()
    {
        _movement = GetComponent<Movement>();
        _audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        // RespondToDebugKeys();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (_isTransitioning || _collisionDisable) { return; }

        switch (other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("This thing is friendly");
                break;
            case "Finish":
                StartSuccessSequence();
                break;
            default:
                StartCrashSequence();
                break;
            
        }
    }

    void RespondToDebugKeys()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadNextLevel();
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            _collisionDisable = !_collisionDisable; // toggle collision
        }
    }


    void StopGameplay()
    {
        _movement.enabled = false;
        _isTransitioning = true;
        _audioSource.Stop();
    }

    void StartCrashSequence()
    {
        StopGameplay();
        _audioSource.PlayOneShot(crash);
        crashParticles.Play();
        Invoke("ReloadLevel", levelLoadDelay);
    }

    void StartSuccessSequence()
    {
        StopGameplay();
        _audioSource.PlayOneShot(success);
        successParticles.Play();
        Invoke("LoadNextLevel", levelLoadDelay);
    }

    void LoadNextLevel()
    {
        int nextSceneIndex = (SceneManager.GetActiveScene().buildIndex + 1) % SceneManager.sceneCountInBuildSettings;
        if (nextSceneIndex == 0)
        {   
            nextSceneIndex++;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }

    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}