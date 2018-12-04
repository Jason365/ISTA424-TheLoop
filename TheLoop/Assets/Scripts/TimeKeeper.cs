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
    public GameObject tutorialObj;

    int beat = 0;
    bool click = true;
    bool tut = true;
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
        if (tut)
        {
            endTut();
            tut = false;
        }
    }

    public void KickHit ()
    {
        sound.PlayOneShot(kick);
        kicks[(beat) % 8] = true;
        if (tut)
        {
            endTut();
            tut = false;
        }
    }

    public void SnareHit()
    {
        sound.PlayOneShot(snare);
        snares[(beat) % 8] = true;
        if (tut)
        {
            endTut();
            tut = false;
        }
    }

    public void endTut()
    {
        Destroy(tutorialObj);
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
        if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger) || Input.GetKeyDown(KeyCode.F))
        {
            //hats = new bool[8];
            //kicks = new bool[8];
            //snares = new bool[8];
            if (click)
            {
                click = false;
            }
            else
            {
                click = true;
            }
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
        
        if (beat == 0 && click)
        {
            sound.PlayOneShot(highClick);
        }
        else if (beat % 2 == 0 && click)
        {
            sound.PlayOneShot(lowClick);
        }

        if (hats[(beat + 1) % 8])
        {
            Instantiate(note, hatSpawn.transform);
            hats[(beat + 1) % 8] = false;
        }
        if (kicks[(beat + 1) % 8])
        {
            Instantiate(note, kickSpawn.transform);
            kicks[(beat + 1) % 8] = false;
        }
        if (snares[(beat + 1) % 8])
        {
            Instantiate(note, snareSpawn.transform);
            snares[(beat + 1) % 8] = false;
        }
    }
}
