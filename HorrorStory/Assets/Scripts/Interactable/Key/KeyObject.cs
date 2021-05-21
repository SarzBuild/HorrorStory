using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityCore.Audio;

public class KeyObject : Interactable
{
    //We get the player references to update some values
    private Sc_PlayerReferences playerReferences;
    [SerializeField] private bool invisible;
    [SerializeField] private Door doorToOpen;
    private void Start()
    {
        playerReferences = Sc_PlayerReferences.Instance;
    }

    void GetKey()
    {
        Destroy(gameObject);
        AudioAction.PlaySound(UnityCore.Audio.AudioType.PickUp);
        doorToOpen.canBeOpened = true;
    }

    //We return the string that's used when the player's vision is on the game object
    public override string GetDescription()
    {
        return "Hold [E] to take the key.";
    }
    
    //If there's an interaction, we call our functions
    public override void Interact()
    {
        GetKey();
    }

    public override bool Invisible()
    {
        return invisible;
    }
}
