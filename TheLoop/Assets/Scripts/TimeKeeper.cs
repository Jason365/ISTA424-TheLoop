using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeKeeper : MonoBehaviour {

    public AudioSource sound;
    public AudioClip hat;
    public AudioClip kick;
    public AudioClip snare;

    int beat = 0;
    bool[] hats = new bool[8];

	// Use this for initialization
	void Start () {
        InvokeRepeating("GetBeat", 0.25f, 0.25f);
	}

    void Update ()
    {
        if (Input.GetButtonDown("Jump"))
        {
            sound.PlayOneShot(hat);
            if(beat == 0)
            {
                hats[7] = true;
            }
            else
            {
                hats[beat-1] = true;
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
        
        if(beat%2 == 0)
        {
            sound.PlayOneShot(kick);
        }
        if(beat%4 == 0)
        {
            sound.PlayOneShot(snare);
        }
        if (beat >= 7){
            beat = 0;
        }
        else
        {
            beat += 1;
        }
        
    }
}
