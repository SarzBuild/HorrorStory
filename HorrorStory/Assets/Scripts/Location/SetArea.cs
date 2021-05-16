using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetArea : MonoBehaviour
{

    public LocationController.Location location;

    // Start is called before the first frame update
    void Start()
    {
        LocationController.currentLocation = LocationController.Location.livingRoom;
        LocationController.previousLocation = LocationController.Location.livingRoom;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        ////if player
        if (other.gameObject.tag != "Player") return; 

        if (location != LocationController.currentLocation)
        {
            LocationController.previousLocation = LocationController.currentLocation;
            LocationController.currentLocation = location;
        }
    }
}
