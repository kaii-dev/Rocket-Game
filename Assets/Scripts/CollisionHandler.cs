using System;
using System.Data.Common;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float Dely = 1f;
    [SerializeField] AudioClip finishPad;
    [SerializeField] AudioClip HittingObstacle;
    [SerializeField] ParticleSystem crashParticles;
    [SerializeField] ParticleSystem finishParticles;

    AudioSource audioSource;
    bool isControllable = true;
    bool iscollidable = true;

//=======================================
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        RespondToDebugKeys();
    }
   
//========================================
    void OnCollisionEnter(Collision other) //if you hit an obstacle
    {
        if (!isControllable|| !iscollidable){return;}

        switch (other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("everything looking good");
                break;
            case "Finish":
                FinishingPad();
                 break;
            default:
                startCrashSequence();
                break;
        }
    }

//===============================================
    void startCrashSequence() //when gets hit
    {
        isControllable = false;
        audioSource.Stop();
        audioSource.PlayOneShot(HittingObstacle);
        crashParticles.Play();

              //================
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel", Dely);

    }

    void FinishingPad()//when going to next level 
    {
        isControllable = false;
        audioSource.Stop();
        audioSource.PlayOneShot(finishPad);
        finishParticles.Play();

            //=================
        GetComponent<Movement>().enabled = false;
        Invoke("NextLevel", Dely);
    }

//===============================================
    void NextLevel() //to load the next level
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        int nextScene = currentScene + 1;

        if (nextScene == SceneManager.sceneCountInBuildSettings)
        {
            nextScene = 0;
        }
        SceneManager.LoadScene(nextScene);
    }

 void RespondToDebugKeys() //when click a key do something
    {
        if (Keyboard.current.lKey.isPressed)
        {
            Invoke("NextLevel", 1f);
            isControllable = false;
        }
        else if (Keyboard.current.cKey.isPressed)
        {
            iscollidable = !iscollidable;
            Debug.Log("collison turned off");
        }
    }

    void ReloadLevel() //respawn if you hit
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene);

    }
}