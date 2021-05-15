using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Interactable
{
    [SerializeField] private bool invisible;
    [SerializeField] DoorCol exitCol;
    [SerializeField] DoorCol enterCol;
    Animator myAnim;
    public bool canBeOpened = true;
    private bool isOpening = false;
    private void Awake()
    {
        myAnim = GetComponentInParent<Animator>();
    }
    private void OpenDoor()
    {
        if (canBeOpened)
        {
            GetComponent<BoxCollider>().enabled = false;
            myAnim.SetBool("isOpening", true);
            myAnim.SetBool("isClosing", false);
            canBeOpened = false;
            isOpening = true;
        }
    }
    public void CloseDoor()
    {        
        myAnim.SetBool("isOpening", false);
        myAnim.SetBool("isClosing", true);            
        canBeOpened = true;
        
    }
    public override string GetDescription()
    {
        if (canBeOpened)
        {
            return "Left click to open door.";
        }
        else
        {
            return null;
        }
        
    }

    //If there's an interaction, we call our functions
    public override void Interact()
    {
        OpenDoor();
    }

    public override bool Invisible()
    {
        return invisible;
    }
}
