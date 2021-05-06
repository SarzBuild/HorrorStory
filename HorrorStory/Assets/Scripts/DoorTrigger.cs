using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{

    public enum TriggerTypes { copy, next, finish }
    private RoomManager roomManager;
    //public float xOffset;
    //public float zOffset;
    //public RoomManager.direction doorToRemove;
    public RoomManager.direction direction;
    public TriggerTypes triggerType; 


    private void Awake()
    {
        roomManager = GameObject.FindObjectOfType<RoomManager>();
    }




    private void OnTriggerEnter(Collider other)
    {
        
        if (other.transform.tag != "Player")
        {
            return;
        }

        //Action to spawn room

        switch (triggerType)
        {
            case (TriggerTypes.copy):
                RoomAction.spawnCopyRoom(direction);
                break;
            case (TriggerTypes.next):
                RoomAction.spawnNextRoom(direction);
                break;
            case (TriggerTypes.finish):
                RoomAction.finishArea(direction);
                break;
            default:
                RoomAction.spawnCopyRoom(direction);
                break;
        }

        //remove door player went through
        gameObject.SetActive(false);
    }

}