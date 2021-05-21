using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationController : MonoBehaviour
{

    public enum Location
    {
        hallway, 
        livingRoom, 
        livingRoomafter, 
        bathRoom, 
        bathRoomDeath, 
        bathRoomDraining, 
        bathRoom_before_after,
        corridors,
        corridors2, 
        corridors3, 
        corridors4,
        corridors5,
        endOfLevel,
    }
    public static Location previousLocation;
    public static Location currentLocation; 

}
