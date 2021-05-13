using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SwapingRoomManager : MonoBehaviour
{
    private static SwapingRoomManager _instance;

    public static SwapingRoomManager Instance { get { return _instance; } }


    public enum direction { entrance, left, right};


    public DoorTrigger doorLeftFirstRoom;
    public DoorTrigger doorRightFirstRoom; 

    public Transform firstRoomPlayerLocation;
    public Transform firstRoomTransform;

    public GameObject endOfLevelRoom;
    public float endOfLevelRoomDimention;
    public float swappingRoomDimention = 1.9f;

    public GameObject[] prefabs;
    private int currentPrefabIndex;

    //it is a queue so we can remove rooms further back so we don't collide with them
    private Queue<SwapingRoom> rooms; 

    private bool roomLevelIsComplete;
   

    private void OnEnable()
    {
        RoomAction.spawnCopyRoom += InstantiateNewRoom;
        RoomAction.spawnNextRoom += AdvanceRoom;
        RoomAction.finishArea += FinishArea;

    }

    private void OnDisable()
    {
        RoomAction.spawnCopyRoom -= InstantiateNewRoom;
        RoomAction.spawnNextRoom -= AdvanceRoom;
        RoomAction.finishArea -= FinishArea;

    }


    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
        rooms = new Queue<SwapingRoom>();

    }

    void Start()
    {
      
        currentPrefabIndex = 0;
        roomLevelIsComplete = false;

    }

    public GameObject GetCurrentPrefab()
    {
        if (!roomLevelIsComplete)
            return prefabs[currentPrefabIndex];

        return null; 
    }

    public void AdvanceRoom(Transform doorTransform)
    {

        currentPrefabIndex += 1; 
        if (currentPrefabIndex >= prefabs.Length)
        {
            roomLevelIsComplete = true;
            FinishArea(doorTransform);
        } else
        {
            InstantiateNewRoom(doorTransform);
        }
    }





  
    public void InstantiateNewRoom(Transform doorTransform)
    {
        Debug.Log("Instantiating a room");

        if (roomLevelIsComplete) return;


        GameObject roomPrefab = prefabs[currentPrefabIndex];
        Vector3 currentDoorPosition = doorTransform.position;
        Quaternion currentDoorRotation = doorTransform.rotation; 
        GameObject spawnedRoom = Instantiate(roomPrefab) as GameObject;
        //spawnedRoom.transform.rotation = currentDoorRotation;
        spawnedRoom.transform.rotation = Quaternion.Euler(0,currentDoorRotation.eulerAngles.y, 0);
        spawnedRoom.transform.position = new Vector3(doorTransform.position.x, firstRoomTransform.position.y,doorTransform.position.z) + doorTransform.forward * swappingRoomDimention;

        //spawnedRoom.transform.Translate(doorTransform.forward * swappingRoomDimention);
        SwapingRoom room = spawnedRoom.GetComponent<SwapingRoom>();
        rooms.Enqueue(room);
        if(rooms.Count > 4)
        {
            room = rooms.Dequeue();
            Destroy(room.gameObject);
            room = rooms.Peek();

            //to prevent backtracking
            room.ActivateEntranceDoor();
        }


    }

    
   
    private void FinishArea(Transform doorTransform)
    {
        Vector3 currentDoorPosition = doorTransform.position;
        Quaternion currentDoorRotation = doorTransform.rotation;
        GameObject finalRoom = Instantiate(endOfLevelRoom) as GameObject;
        finalRoom.transform.rotation = currentDoorRotation;
        finalRoom.transform.position = doorTransform.position + doorTransform.forward * endOfLevelRoomDimention;
        SwapingRoom room; 
        while(rooms.Count > 1)
        {
            room = rooms.Dequeue();
            Destroy(room.gameObject);
        }
        room = rooms.Dequeue();
        //can't go back door is locked behind you.
        room.ActivateEntranceDoor();
    }


    public void Reset(GameObject player)
    {

        rooms.Clear();

        //or if its easier just destroy the doors and instantiate new ones
        doorLeftFirstRoom.Reset();
        doorRightFirstRoom.Reset();

        player.transform.position = firstRoomPlayerLocation.position;
        player.transform.rotation = firstRoomPlayerLocation.rotation;
        //change the rotation too?
    }

}
