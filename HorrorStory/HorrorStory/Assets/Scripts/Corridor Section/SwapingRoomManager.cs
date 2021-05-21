using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SwapingRoomManager : MonoBehaviour
{
    private static SwapingRoomManager _instance;

    public static SwapingRoomManager Instance { get { return _instance; } }


    public enum direction { entrance, left, right};



    public Transform firstRoomTransform;

    public GameObject endOfLevelRoom;
    public float endOfLevelRoomDimention;
    public float swappingRoomDimention = 4.3f;
    public GameObject[] prefabs;
    private int currentPrefabIndex;

    //it is a queue so we can remove rooms further back so we don't collide with them
    private Queue<GameObject> rooms; 

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
        rooms = new Queue<GameObject>();

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
        Quaternion currentDoorRotation = doorTransform.rotation; //.eulerAngles + Quaternion.EulerRotation(0.0f, 180.0f, 0.0f); 
        GameObject spawnedRoom = Instantiate(roomPrefab) as GameObject;
        //spawnedRoom.transform.rotation = currentDoorRotation;
        spawnedRoom.transform.rotation = Quaternion.Euler(0,currentDoorRotation.eulerAngles.y + 180.0f, 0);
        spawnedRoom.transform.position = new Vector3(doorTransform.position.x, firstRoomTransform.position.y, doorTransform.position.z) - doorTransform.forward * swappingRoomDimention;

        //spawnedRoom.transform.Translate(doorTransform.forward * swappingRoomDimention);
        rooms.Enqueue(spawnedRoom);
        if(rooms.Count > 4)
        {
            spawnedRoom = rooms.Dequeue();
            Destroy(spawnedRoom.gameObject);
        }


    }

    
   
    private void FinishArea(Transform doorTransform)
    {
        Vector3 currentDoorPosition = doorTransform.position;
        Quaternion currentDoorRotation = doorTransform.rotation;
        GameObject finalRoom = Instantiate(endOfLevelRoom) as GameObject;
        finalRoom.transform.rotation = currentDoorRotation;
        finalRoom.transform.position = doorTransform.position + doorTransform.forward * endOfLevelRoomDimention;
        rooms.Enqueue(finalRoom);
        if (rooms.Count > 4)
        {
            finalRoom = rooms.Dequeue();
            Destroy(finalRoom.gameObject);
        }
    }



}
