using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorObject : Interactable
{
    //We get the player references to update some values
    private Sc_PlayerReferences playerReferences;
    [SerializeField] private bool invisible;
    private void Start()
    {
        playerReferences = Sc_PlayerReferences.Instance;
    }

    void BreakMirror()
    {
        Destroy(gameObject);
    }

    //We return the string that's used when the player's vision is on the game object
    public override string GetDescription()
    {
        return "Left Click to destroy the mirror";
    }
    
    //If there's an interaction, we call our functions
    public override void Interact()
    {
        BreakMirror();
    }

    public override bool Invisible()
    {
        return invisible;
    }
}
