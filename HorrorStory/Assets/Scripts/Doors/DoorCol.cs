using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorCol : MonoBehaviour
{
    [SerializeField] Door door;
    [SerializeField] DoorCol otherCol;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            //only close the door if its not in the last section
            if (door.triggerType == Door.TriggerTypes.none || door.triggerType == Door.TriggerTypes.bathroomDoor)
            {
                otherCol.GetComponent<BoxCollider>().enabled = true;
                gameObject.GetComponent<BoxCollider>().enabled = false;
                door.CloseDoor();
            }
        }
    }
}
