using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeKeeper : MonoBehaviour {

    public AudioSource sound;
    public AudioClip hat;
    public AudioClip kick;
    public AudioClip snare;
    public AudioClip highClick;
    public AudioClip lowClick;
    public float bpm;

    int beat = 0;
    bool[] hats = new bool[16];
    bool[] kicks = new bool[16];
    bool[] snares = new bool[16];

    // Use this for initialization
    void Start () {
        float convertedBpm = ((60/4)/bpm);
        print(convertedBpm);

        InvokeRepeating("GetBeat", convertedBpm, convertedBpm);
	}

    void Hit ()
    {

    }

    void Update ()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            sound.PlayOneShot(hat);
            if(beat == 0)
            {
                hats[15] = true;
            }
            else
            {
                hats[beat-1] = true;
            }
                
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            sound.PlayOneShot(kick);
            if (beat == 0)
            {
                kicks[15] = true;
            }
            else
            {
                kicks[beat - 1] = true;
            }

        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            sound.PlayOneShot(snare);
            if (beat == 0)
            {
                snares[15] = true;
            }
            else
            {
                snares[beat - 1] = true;
            }

        }
    }
	
	// Update is called once per frame
	void GetBeat()
    {
        if (hats[beat])
        {
            sound.PlayOneShot(hat);
        }
        if (kicks[beat])
        {
            sound.PlayOneShot(kick);
        }
        if (snares[beat])
        {
            sound.PlayOneShot(snare);
        }
        
        if (beat == 0)
        {
            sound.PlayOneShot(highClick);
        }
        else if (beat%4 == 0)
        {
            sound.PlayOneShot(lowClick);
        }

        if (beat >= 15){
            beat = 0;
        }
        else
        {
            beat += 1;
        }
        
    }
}
