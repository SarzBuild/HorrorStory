using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Room : MonoBehaviour
{

    public GameObject northDoor;
    public GameObject eastDoor;
    public GameObject southDoor;
    public GameObject westDoor;

    public List<NavMeshSurface> navMeshSurfaces;
    

    //ensure all doors are active to start
    public void initializeClone()
    {
        northDoor.SetActive(true);
        eastDoor.SetActive(true);
        southDoor.SetActive(true);
        westDoor.SetActive(true);

    }

    public List <NavMeshSurface> GetSurfaces()
    {
        return navMeshSurfaces;
    }
    public void RemoveNorthDoor()
    {
        northDoor.SetActive(false);

    }
    public void RemoveSouthDoor()
    {
        southDoor.SetActive(false);

    }

    public void RemoveEastDoor()
    {
        eastDoor.SetActive(false);
    }

    public void RemoveWestDoor()
    {
        westDoor.SetActive(false);
    }

    public void RemoveSpecificDoor(RoomManager.direction dir)
    {
        switch (dir)
        {
            case (RoomManager.direction.north):
                northDoor.SetActive(false);
                break;
            case (RoomManager.direction.east):
                eastDoor.SetActive(false);
                break;
            case (RoomManager.direction.south):
                southDoor.SetActive(false);
                break;
            case (RoomManager.direction.west):
                westDoor.SetActive(false);
                break;

        }
    }
}
