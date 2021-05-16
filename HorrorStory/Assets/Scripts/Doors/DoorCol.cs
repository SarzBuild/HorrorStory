using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorCol : MonoBehaviour
{
    [SerializeField] DoorCol otherCol;
    [SerializeField] Door door;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            //only close the door if its not in the last section
            if (door.triggerType == Door.TriggerTypes.none)
            {
                otherCol.GetComponent<BoxCollider>().enabled = true;
                gameObject.GetComponent<BoxCollider>().enabled = false;
                door.CloseDoor();
            }

        }
    }
}
