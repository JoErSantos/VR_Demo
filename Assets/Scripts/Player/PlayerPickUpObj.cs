using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class PlayerPickUpObj : MonoBehaviour
{
    public Transform holdPos;
    
    public float throwForce = 500f; //force at which the object is thrown at
    private float rotationSensitivity = 1f; //how fast/slow the object is rotated in relation to mouse movement
    private GameObject heldObj; //object which we pick up
    private Rigidbody heldObjRb; //rigidbody of object we pick up
    private bool canDrop = true; //this is needed so we don't throw/drop object when rotating the object
    private int LayerNumber; //layer index

    void Start()
    {
        LayerNumber = LayerMask.NameToLayer("holdLayer"); //if your holdLayer is named differently make sure to change this ""

        //mouseLookScript = player.GetComponent<MouseLookScript>();
    }

    void Update()
    {
        if(heldObj != null)
        {
            MoveObject();
        }
    }

    public void PickUpObject(GameObject pickUpObj)
    {
        heldObj = pickUpObj; //assign heldObj to the object that was hit by the raycast (no longer == null)
        heldObjRb = pickUpObj.GetComponent<Rigidbody>(); //assign Rigidbody
        heldObjRb.isKinematic = true;
        heldObjRb.transform.parent = holdPos.transform; //parent object to holdposition
        heldObj.layer = LayerNumber; //change the object layer to the holdLayer
        //make sure object doesnt collide with player, it can cause weird bugs
        Physics.IgnoreCollision(heldObj.GetComponent<Collider>(), gameObject.GetComponent<Collider>(), true);
    }

    public void DropObject()
    {
        StopClipping();
        //re-enable collision with player
        Physics.IgnoreCollision(heldObj.GetComponent<Collider>(), gameObject.GetComponent<Collider>(), false);
        heldObj.layer = 6; //object assigned back to default layer
        heldObjRb.isKinematic = false;
        heldObj.transform.parent = null; //unparent object
        heldObj = null; //undefine game object
    }

    private void MoveObject()
    {
        //keep object position the same as the holdPosition position
        heldObj.transform.position = holdPos.transform.position;
    }

    private void StopClipping() //function only called when dropping/throwing
    {
        var clipRange = Vector3.Distance(heldObj.transform.position, transform.position); //distance from holdPos to the camera
        //have to use RaycastAll as object blocks raycast in center screen
        //RaycastAll returns array of all colliders hit within the cliprange
        RaycastHit[] hits;
        hits = Physics.RaycastAll(transform.position, transform.TransformDirection(Vector3.forward), clipRange);
        //if the array length is greater than 1, meaning it has hit more than just the object we are carrying
        if (hits.Length > 1)
        {
            //change object position to camera position 
            heldObj.transform.position = transform.position + new Vector3(0f, -0.5f, 0f); //offset slightly downward to stop object dropping above player 
            //if your player is small, change the -0.5f to a smaller number (in magnitude) ie: -0.1f
        }
    }

    public void ThrowObject()
    {
        StopClipping();
        //same as drop function, but add force to object before undefining it
        Physics.IgnoreCollision(heldObj.GetComponent<Collider>(), gameObject.GetComponent<Collider>(), false);
        heldObj.layer = 6;
        heldObjRb.isKinematic = false;
        heldObj.transform.parent = null;
        heldObjRb.AddForce(transform.forward * throwForce);
        heldObj = null;
    }

}
