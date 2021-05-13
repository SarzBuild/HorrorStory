using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{

    public enum TriggerTypes { copy, next, finish, locked }
    public TriggerTypes triggerType;
    private Transform initialTransform; 
    private bool playerHasEntered; 


    private void Awake()
    {
        playerHasEntered = false;
        initialTransform = transform; 
    }


    public void Reset()
    {
        playerHasEntered = false;
        transform.position = initialTransform.position;
        transform.rotation = initialTransform.rotation;
    }

    private void OnCollisionEnter(Collision collision)
    {
        
        if (collision.collider.transform.tag != "Player" || playerHasEntered)
        {
            return;
        }
        playerHasEntered = true;

        //Action to spawn room

        Debug.Log(initialTransform.position);
        Debug.Log(initialTransform.rotation);

        switch (triggerType)
        {
            case (TriggerTypes.copy):
                RoomAction.spawnCopyRoom(initialTransform);
                break;
            case (TriggerTypes.next):
                RoomAction.spawnNextRoom(initialTransform);
                break;
            case (TriggerTypes.finish):
                RoomAction.finishArea(initialTransform);
                break;
            case (TriggerTypes.locked):
                break;
            default:
                break;
        }

        Destroy(gameObject);
    }

    

    //open the door or let the hinge / rigidbody handle it

}
