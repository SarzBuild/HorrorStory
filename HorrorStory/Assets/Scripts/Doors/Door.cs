using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityCore.Audio;

public class Door : Interactable
{
    [SerializeField] private bool invisible;
    Animator myAnim;
    public bool canBeOpened = true;
    public UnityCore.Audio.AudioType lockedDoorAudio;
    public UnityCore.Audio.AudioType openDoor;
    public UnityCore.Audio.AudioType closeDoor;
    public UnityCore.Audio.AudioType unlockDoor;
    //public float doorXoffset = 0.4f;


    public enum TriggerTypes { none, copy, next, finish, bathroomDoor }
    public TriggerTypes triggerType = TriggerTypes.none;
    private Transform initialTransform;
    private bool roomIsSpawned = false;


    private bool isOpening = false;
    public bool isOpen = false;
    private void Awake()
    {
        myAnim = GetComponentInParent<Animator>();
        roomIsSpawned = false;
        initialTransform = transform;
        //initialTransform.position = new Vector3(initialTransform.position.x + doorXoffset, initialTransform.position.y, initialTransform.position.z);
        isOpen = false;
    }



    private void OpenDoor()
    {
        if (canBeOpened && !isOpen)
        {
            GetComponent<BoxCollider>().enabled = false;
            myAnim.SetBool("isOpening", true);
            myAnim.SetBool("isClosing", false);
            canBeOpened = false;
            isOpening = true;
            isOpen = true;
            AudioAction.PlaySound(openDoor);
            switch (triggerType)
            {
                case (TriggerTypes.none):
                    break;
                case (TriggerTypes.copy):
                    if (roomIsSpawned) return;
                    RoomAction.spawnCopyRoom(initialTransform);
                    roomIsSpawned = true;
                    break;
                case (TriggerTypes.next):
                    if (roomIsSpawned) return;
                    RoomAction.spawnNextRoom(initialTransform);
                    roomIsSpawned = true;
                    break;
                case (TriggerTypes.finish):
                    if (roomIsSpawned) return;
                    RoomAction.finishArea(initialTransform);
                    roomIsSpawned = true;
                    break;
                default:
                    break;
            }
        }
        else
        {
            AudioAction.PlaySound(lockedDoorAudio);
        }
    }

    public bool GetIsOpen()
    {
        return isOpen;
    }
    public void CloseDoor()
    {
        if (!isOpen) return; 

        if (triggerType == TriggerTypes.bathroomDoor)// && LocationController.currentLocation == LocationController.Location.bathRoom)
        {
            GameActions.enterBathroom(this);
        }
        canBeOpened = true;
        AudioAction.PlaySound(closeDoor);
        myAnim.SetBool("isOpening", false);
        myAnim.SetBool("isClosing", true);
        isOpen = false;

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

    public void UnlockDoor()
    {
        AudioAction.PlaySound(unlockDoor);
        canBeOpened = true;

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
