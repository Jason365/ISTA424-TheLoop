﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BNote : MonoBehaviour {

    public GameObject time;
    private TimeKeeper tk;

    // Use this for initialization
    void Start()
    {
        tk = time.GetComponent<TimeKeeper>();
    }

    void OnTriggerEnter(Collider other)
    {
        tk.BHit();
        if (other.tag == "Note")
        {
            Destroy(other.gameObject);
        }
    }
}
