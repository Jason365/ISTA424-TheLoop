using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabController : MonoBehaviour
{
    private bool isGrabbing = false;
    private Transform grabbedTransform;
    public float zSpeed = 4.0f;

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        //We are creating a variable to hold the active controller
        OVRInput.Controller activeController = OVRInput.GetActiveController();

        //We are setting the position of the ray to the calculated position of the
        //active controller (it's not tracked but an estimate is hold)
        transform.localPosition = OVRInput.GetLocalControllerPosition(activeController);

        //We are setting the rotation of the ray to the rotation of the active controller
        transform.rotation = OVRInput.GetLocalControllerRotation(activeController);      

        if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger))
        {
            RaycastHit hitInfo;
            if (Physics.Raycast(new Ray(transform.position, transform.forward), out hitInfo))
            {
                //We are using a Tag named Grabbable (set on the Inspector View for the Cube
                //and the Sphere) to not take into account hits with other objects (table etc.)
                if (hitInfo.transform.tag == "Grabbable")                             
                {
                    isGrabbing = true;

                    //We are getting the transform value of the hit object
                    grabbedTransform = hitInfo.transform;

                    //We are setting isKinematic as true and useGravity as falseso that we can
                    //control the object via controller, as if it was stuck to it
                    grabbedTransform.GetComponent<Rigidbody>().isKinematic = true;    
                    grabbedTransform.GetComponent<Rigidbody>().useGravity = false;

                    //We are declaring that the Hand object (to which this script is attached)
                    //is the parent of the hit object (Cube or Sphere in this case)
                    //So that we can control its movement
                    grabbedTransform.parent = transform;                             
                }                                                                    
            }
        }

        if (OVRInput.GetUp(OVRInput.Button.PrimaryIndexTrigger))
        {
            //We are reversing the isKinematic and useGravity settings so that
            //the object can move independent of the Hand
            grabbedTransform.GetComponent<Rigidbody>().isKinematic = false;         
            grabbedTransform.GetComponent<Rigidbody>().useGravity = true;

            //We are setting the parent as none so that the Hand object
            //no longer controls the movement for this object
            grabbedTransform.parent = null;                                         
            isGrabbing = false;
        }


        if (isGrabbing)
        {
            //We are moving the grabbed object in its local z-axis to cater for
            //the lack of position tracking in Oculus Go controller
            float distance = OVRInput.Get(OVRInput.Axis2D.PrimaryTouchpad).y;

            //We can adjust the speed with the zSpeed variable
            grabbedTransform.position += distance * Time.deltaTime * zSpeed * transform.forward;       
        }


    }
}
