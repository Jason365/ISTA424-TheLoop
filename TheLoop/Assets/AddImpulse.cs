using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddImpulse : MonoBehaviour {

    public float force;

	// Use this for initialization
	void Start () {
        gameObject.GetComponent<Rigidbody>().AddForce(0, force, 0);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
