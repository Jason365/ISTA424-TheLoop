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
    public GameObject hatSpawn;
    public GameObject kickSpawn;
    public GameObject snareSpawn;
    public GameObject note;
    public float bpm;

    int beat = 0;
    bool[] hats = new bool[8];
    bool[] kicks = new bool[8];
    bool[] snares = new bool[8];

    // Use this for initialization
    void Start () {
        float convertedBpm = ((60)/bpm);

        InvokeRepeating("GetBeat", convertedBpm, convertedBpm);
	}

    public void HatHit ()
    {
        sound.PlayOneShot(hat);
        hats[(beat)%8] = true;
    }

    public void KickHit ()
    {
        sound.PlayOneShot(kick);
        kicks[(beat) % 8] = true;
    }

    public void SnareHit()
    {
        sound.PlayOneShot(snare);
        snares[(beat) % 8] = true;
    }

    void Update ()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            HatHit();  
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            KickHit();
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            SnareHit();
        }
        if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger))
        {
            hats = new bool[8];
            kicks = new bool[8];
            snares = new bool[8];
        }
    }
	
	// Update is called once per frame
	void GetBeat()
    {
        if (beat >= 7)
        {
            beat = 0;
        }
        else
        {
            beat += 1;
        }

        StartCoroutine(BeatOffset());
 
    }
    IEnumerator BeatOffset()
    {
        yield return new WaitForSeconds(0.25f);
        
        if (beat == 0)
        {
            sound.PlayOneShot(highClick);
        }
        else if (beat % 2 == 0)
        {
            sound.PlayOneShot(lowClick);
        }

        if (hats[(beat + 1) % 8])
        {
            Instantiate(note, hatSpawn.transform);
            if(beat == 0)
            {
                hats[7] = false;
            }
            else
            {
                hats[beat - 1] = false;
            }
        }
        if (kicks[(beat + 1) % 8])
        {
            Instantiate(note, kickSpawn.transform);
            if (beat == 0)
            {
                kicks[7] = false;
            }
            else
            {
                kicks[beat - 1] = false;
            }
        }
        if (snares[(beat + 1) % 8])
        {
            Instantiate(note, snareSpawn.transform);
            if (beat == 0)
            {
                snares[7] = false;
            }
            else
            {
                snares[beat - 1] = false;
            }
        }
    }
}
