using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    private static RoomManager _instance;

    public static RoomManager Instance { get { return _instance; } }

    public enum direction {north, south, east, west};
    public Room startingRoom; 
    public GameObject[] prefabs;
    public int totalRooms = 1;
    public float roomDimention = 41.838f; 

    private int currentPrefabIndex;

    private bool [,] roomLocatedAt = new bool [100,100];
    private Room [,] rooms = new Room[100, 100];
    private int currentRoomX = 50; 
    private int currentRoomZ = 50;
    private Room currentRoom;
    private bool roomLevelIsComplete;

    private void OnEnable()
    {
        RoomAction.spawnRoom += InstantiateNewRoom; 

    }

    private void OnDisable()
    {
        RoomAction.spawnRoom -= InstantiateNewRoom;
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
    }

    void Start()
    {

        //inititialze the two arrays with default values
        for (int i = 0; i < 100; i++)
        {
            for (int j = 0; j < 100; j++)
            {
                if(i == 50 && j == 50)
                {
                    roomLocatedAt[i, j] = true;
                    rooms[i, j] = startingRoom;
                    currentRoom = startingRoom;
                } else
                {
                    roomLocatedAt[i, j] = false;
                    rooms[i, j] = null;
                }

            }
        }
        currentRoomX = 50;
        currentRoomZ = 50; 
        currentPrefabIndex = 0;
        roomLevelIsComplete = false; 

    }

    public GameObject GetCurrentPrefab()
    {
        if (!roomLevelIsComplete)
            return prefabs[currentPrefabIndex];

        return null; 
    }

    public bool AdvanceRoom()
    {
        currentPrefabIndex += 1; 
        if (currentPrefabIndex >= totalRooms)
        {
            roomLevelIsComplete = true;
            return false;
        }

        return true;
    }



    public direction ReverseTheDirection(direction dir)
    {
        switch(dir)
        {
            case (direction.north): return direction.south;
            case (direction.south): return direction.north;
            case (direction.east): return direction.west;
            case (direction.west): return direction.east;
            default: return direction.north;
        }

    }


    public void InstantiateNewRoom(direction directionOfSpawnedRoom)
    {
        Debug.Log("Instantiating a room");

        if (roomLevelIsComplete) return;

        direction reverseDirectionOfSpawnedRoom = ReverseTheDirection(directionOfSpawnedRoom);

        GameObject roomPrefab = prefabs[currentPrefabIndex];
        Vector3 currentRoomPosition = currentRoom.transform.position;
        Vector3 spawnedRoomPosititon; 
        GameObject spawnedRoom = Instantiate(roomPrefab) as GameObject;

        int xOffsetOfSpawnedRoom = 0;
        int zOffsetOfSpawnedRoom = 0;

        switch (directionOfSpawnedRoom)
        {
            case (direction.north): 
                zOffsetOfSpawnedRoom = 1;
                break;
            case (direction.south):
                zOffsetOfSpawnedRoom = -1;
                break;
            case (direction.east):
                xOffsetOfSpawnedRoom = 1;
                break;
            case (direction.west):
                xOffsetOfSpawnedRoom = -1;
                break;
        }
        spawnedRoomPosititon = currentRoomPosition + new Vector3(xOffsetOfSpawnedRoom * roomDimention, 0, zOffsetOfSpawnedRoom * roomDimention);
        spawnedRoom.transform.position = spawnedRoomPosititon;

        Room room = spawnedRoom.GetComponent<Room>();

        room.initializeClone();
        room.RemoveSpecificDoor(reverseDirectionOfSpawnedRoom);



        currentRoom = room;


        //create the new room
        AssignNewRoom(xOffsetOfSpawnedRoom, zOffsetOfSpawnedRoom, room);

    }

    public void AssignNewRoom(int xOffset, int zOffset, Room room)
    {
        currentRoomX += xOffset;
        currentRoomZ += zOffset;


        //update the arrays and indexes with the new room added
        roomLocatedAt[currentRoomX, currentRoomZ] = true;
        rooms[currentRoomX, currentRoomZ] = room;

        RemoveConnectingDoors();
    }


    private void RemoveConnectingDoors()
    {

        Room connectingRoom;


        if (HasRoomToNorth()) //has another connecting room to the north 
        {
            connectingRoom = GetRoomToNorth();
            connectingRoom.RemoveSouthDoor();
            currentRoom.RemoveNorthDoor();
        }
        if (HasRoomToEast()) //has another connecting room to the east 
        {
            connectingRoom = GetRoomToEast();
            connectingRoom.RemoveWestDoor();
            currentRoom.RemoveEastDoor();
        }
        if (HasRoomToSouth()) //has another connecting room to the south 
        {
            connectingRoom = GetRoomToSouth();
            connectingRoom.RemoveNorthDoor();
            currentRoom.RemoveSouthDoor();

        }
        if (HasRoomToWest()) //has another connecting room to the west 
        {
            connectingRoom = GetRoomToWest();
            connectingRoom.RemoveEastDoor();
            currentRoom.RemoveWestDoor();
        }
    }

    private bool HasRoomToNorth()
    {
        if (roomLocatedAt[currentRoomX, currentRoomZ - 1]) return true;

        return false;
    }

    private bool HasRoomToSouth()
    {
        if (roomLocatedAt[currentRoomX, currentRoomZ + 1]) return true;

        return false;
    }

    private bool HasRoomToEast()
    {
        if (roomLocatedAt[currentRoomX + 1, currentRoomZ]) return true;

        return false;
    }

    private bool HasRoomToWest()
    {
        if (roomLocatedAt[currentRoomX - 1, currentRoomZ]) return true;

        return false;
    }

    private Room GetRoomToNorth()
    {
        return rooms[currentRoomX, currentRoomZ - 1];
    }

    private Room GetRoomToSouth()
    {
        return rooms[currentRoomX, currentRoomZ + 1];

    }
    private Room GetRoomToEast()
    {
        return rooms[currentRoomX + 1, currentRoomZ];

    }
    private Room GetRoomToWest()
    {
        return rooms[currentRoomX - 1, currentRoomZ];

    }

}