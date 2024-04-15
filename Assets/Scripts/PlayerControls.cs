using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float throttlePower;
    [SerializeField] private AudioSource[] rocketSoundSourceArray;
    //[SerializeField] private AudioClip rocket;
    private AudioSource rocketSoundSourceOne;
     private AudioSource rocketSoundSourceTwo;
    private float rotationValue;
    
    Rigidbody playerRigidbody;
    
    SFXManager sfxManager;
    SoundManager soundManager;
    Vector3 throttleDirectory;
    

    void Start()
    {

        
        playerRigidbody = GetComponent<Rigidbody>();
        rocketSoundSourceArray = GetComponents<AudioSource>();
        rocketSoundSourceOne = rocketSoundSourceArray[0];
        rocketSoundSourceTwo = rocketSoundSourceArray[1];
        sfxManager = GameObject.FindGameObjectWithTag("SFXController").GetComponent<SFXManager>();
        soundManager = GameObject.FindGameObjectWithTag("SoundController").GetComponent<SoundManager>();
       
       
    }

  
    void Update()
    {
        Steering();
        Throttle();
        
    }

    private void FixedUpdate()
    {
        
        playerRigidbody.AddRelativeForce(throttleDirectory * throttlePower * Time.fixedDeltaTime);
    }

    private void Throttle()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            Debug.Log("sila");
            throttleDirectory = Vector3.up; //* throttlePower * Time.deltaTime;
            
            if(rocketSoundSourceOne.isPlaying == false)
            {
                rocketSoundSourceOne.time = 1;
                rocketSoundSourceOne.Play();
                sfxManager.MainEngineBoostActiveSFX();
            }
            
        }
        else
        {
            throttleDirectory = Vector3.zero;
            rocketSoundSourceOne.Stop();
            sfxManager.MainEngineBoostStopSFX();
            
        }

    }

    private void Steering()
    {

        if(Input.GetKey(KeyCode.A))
        {         

            RotationDirection(rotationSpeed);
            
            
            if(!rocketSoundSourceTwo.isPlaying)
            {
                sfxManager.AuxiliaryEngineBoostLeftStartSFX();
                soundManager.AuxEngineSoundPlay();
            }
            

            
        }
        else if (Input.GetKey(KeyCode.D))
        {
            
            
            RotationDirection(-rotationSpeed);
            if (!rocketSoundSourceTwo.isPlaying)
            {
                sfxManager.AuxiliaryEngineBoostRightStartSFX();
                soundManager.AuxEngineSoundPlay();
            }

        }
        else
        {
            soundManager.AuxEngineSoundStop();
            sfxManager.AuxiliaryEngineBoostRightStopSFX();
            sfxManager.AuxiliaryEngineBoosLeftStopSFX();
        }



        /*
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)) 
        {
            if (rocketSoundSourceTwo.isPlaying == false)
            {
                soundManager.AuxEngineSoundPlay();
            }
            
        }
        else
            {
                soundManager.AuxEngineSoundStop();
            }
       */

    }

    private void RotationDirection(float rotationSpeed)
    {
        //playerRigidbody.freezeRotation = true;
        transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
        //playerRigidbody.freezeRotation = false;
    }


}
