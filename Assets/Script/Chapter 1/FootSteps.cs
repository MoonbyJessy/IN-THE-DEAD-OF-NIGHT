using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootSteps : MonoBehaviour
{
    public AudioSource walkAudioSource;
    public AudioSource sprintAudioSource;

    private void Update()
    {
        bool isMoving = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D);
        bool isSprinting = Input.GetKey(KeyCode.LeftShift);

        if (isMoving && isSprinting)
        {
           
            if (!sprintAudioSource.isPlaying)
                sprintAudioSource.Play();

            if (walkAudioSource.isPlaying)
                walkAudioSource.Stop();
        }
        else if (isMoving)
        {
            
            if (!walkAudioSource.isPlaying)
                walkAudioSource.Play();

            if (sprintAudioSource.isPlaying)
                sprintAudioSource.Stop();
        }
        else
        {
            
            if (walkAudioSource.isPlaying)
                walkAudioSource.Stop();

            if (sprintAudioSource.isPlaying)
                sprintAudioSource.Stop();
        }
    }

}
