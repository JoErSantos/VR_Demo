using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    private Camera cam;
    private bool isInView;
    [SerializeField]
    private float viewDistance = 3f;
    [SerializeField]
    private LayerMask mask;
    private PlayerUI playerUI;

    private PlayerPickUpObj pickUp;

    private bool isHoldingItem;

    private InputManager inputManager;
    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<PlayerLook>().cam;
        playerUI = GetComponent<PlayerUI>();
        inputManager = GetComponent<InputManager>();
        pickUp = GetComponent<PlayerPickUpObj>();
        isHoldingItem = false;
    }

    // Update is called once per frame
    void Update()
    {

        if(isHoldingItem)
        {   
            if(inputManager.getWalking().Interact.triggered)
            {
                toggleIsHoldingItem();
                pickUp.DropObject();
            }
            if(inputManager.getWalking().Throw.triggered){
                toggleIsHoldingItem();
                pickUp.ThrowObject();
            }
        }
        else
        {
            playerUI.UpdateText(string.Empty);
            Ray ray = new Ray(cam.transform.position, cam.transform.forward);
            Debug.DrawRay(ray.origin,ray.direction* viewDistance);
            RaycastHit hitInfo;
            isInView = Physics.Raycast(ray, out hitInfo, viewDistance,mask);
            if(isInView)
            {
                if(hitInfo.collider.GetComponent<Interactable>() != null)
                {
                    Interactable objectInteracted = hitInfo.collider.GetComponent<Interactable>();
                    playerUI.UpdateText(objectInteracted.promptMessage);
                    if(inputManager.getWalking().Interact.triggered) 
                    {
                        objectInteracted.BaseInteract();
                    }
                }
            }
        }
    }

    public void toggleIsHoldingItem()
    {
        isHoldingItem = !isHoldingItem;
    }
}
