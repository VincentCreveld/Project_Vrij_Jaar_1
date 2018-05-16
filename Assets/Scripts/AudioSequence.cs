using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSequence : MonoBehaviour {

    AudioSource audiosource;
    public int EnableControlClip;
    public AudioClip[] audioClips;
    public float[] audioDelays;
    PlayerController playControl;
    bool audioDone = false;
    public AudioClip elevatorClip1;
    public AudioClip elevatorClip2;
    public AudioClip elevatorClip3;
    public bool elevator;

    // Use this for initialization spaghet
    void Start()
    {
        GameManager.canPlayClip1Elevator = true;
        audiosource = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioSource>();
        playControl = GetComponent<PlayerController>();
        playControl.enabled = false;
        if (GameManager.canPlayClip1Elevator == true && elevator == true && GameManager.Clip1ElevatorPlayed == false)
        {
            audiosource.clip = elevatorClip1;
            audiosource.Play();
            GameManager.Clip1ElevatorPlayed = true;
        }
        else if (GameManager.canPlayClip2Elevator == true && elevator == true && GameManager.Clip2ElevatorPlayed == false)
        {
            audiosource.clip = elevatorClip2;
            audiosource.Play();
            GameManager.Clip2ElevatorPlayed = true;
        }
        else if (GameManager.canPlayClip3Elevator == true && elevator == true && GameManager.Clip3ElevatorPlayed == false)
        {
            audiosource.clip = elevatorClip3;
            audiosource.Play();
            GameManager.Clip3ElevatorPlayed = true;
        }
        else 
        {
            StartCoroutine(PlayAudio());
        }
        if(audioClips.Length == 0)
        {
            playControl.enabled = true;
        }
    }

    IEnumerator PlayAudio()
    {
        
        int i = 0;
        float delay;
        while(i < audioClips.Length)
        {
            audiosource.clip = audioClips[i];
            audiosource.Play();
            delay = audioClips[i].length + audioDelays[i];
            yield return new WaitForSeconds(delay);
            i++;
        }
        if(i == audioClips.Length || i == EnableControlClip)
        {
            playControl.enabled = true;
        }
    }
}
