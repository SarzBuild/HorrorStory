using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{

    
    private RoomManager roomManager;
    //public float xOffset;
    //public float zOffset;
    //public RoomManager.direction doorToRemove;
    public RoomManager.direction direction;


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
        RoomAction.spawnRoom(direction);

        //remove door player went through
        gameObject.SetActive(false);
    }

}
