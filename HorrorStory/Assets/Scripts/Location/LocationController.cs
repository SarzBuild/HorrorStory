using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationController : MonoBehaviour
{

    public enum Location
    {
        hallway, 
        livingRoom, 
        bathRoom, 
        corridors,
    }
    public static Location currentLocation; 

}
