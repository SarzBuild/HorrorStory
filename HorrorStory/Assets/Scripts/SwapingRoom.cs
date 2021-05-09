using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class SwapingRoom : MonoBehaviour
{

    public GameObject rightDoor;
    public GameObject leftDoor;
    public GameObject entranceDoorLocked;

    private void Awake()
    {
        entranceDoorLocked.SetActive(false);
    }


    public void ActivateEntranceDoor()
    {
        entranceDoorLocked.SetActive(true);
    }


}
