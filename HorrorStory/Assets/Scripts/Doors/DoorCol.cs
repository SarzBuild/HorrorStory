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
            otherCol.GetComponent<BoxCollider>().enabled = true;
            gameObject.GetComponent<BoxCollider>().enabled = false;
            door.CloseDoor();
        }
    }
}
