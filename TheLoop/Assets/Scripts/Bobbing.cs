using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bobbing : MonoBehaviour {

    public float height = 0.0f;
    public float rotSpd = 0.2f;
    private float rand;

	// Use this for initialization
	void Start () {
        rand = Random.Range(-2.0f, 4.0f);
    }
	
	// Update is called once per frame
	void Update () {
        transform.position = new Vector3(transform.position.x, Mathf.Sin(Time.time) + height + rand, transform.position.z);
        transform.Rotate(new Vector3(0.0f, 0.0f, rotSpd));
    }
}
