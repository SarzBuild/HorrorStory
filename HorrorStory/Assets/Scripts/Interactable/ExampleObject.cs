using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExampleObject : Interactable
{
    //We get the player references to update some values
    [SerializeField] private bool invisible;

    void EventFunction()
    { 
        //We put everything that's related to what we want to do in here.
    }

    //We return a string containing the description of the object. If you want to create multiple description for the object, you can use if and else.
    public override string GetDescription()
    {
        return "Left Click to destroy the mirror";
    }
    
    //If there's the input from the player, we call our function
    public override void Interact()
    {
        EventFunction();
    }
    //We return if the object is invisible or not, if it is, the player will only be able to interact with it if their lens are up.
    public override bool Invisible()
    {
        return invisible;
    }
}
