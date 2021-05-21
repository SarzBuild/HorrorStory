using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorObject : Interactable 
{
    //We get the player references to update some values
    private Sc_PlayerReferences playerReferences;
    [SerializeField] private bool invisible;
    public UnityCore.Audio.AudioType portraitSound;
    public bool isSpecialPortrait = false; 
    private float timeSinceAudio = 4.0f;
    private bool isInteractable; 

    private void Start()
    {
        playerReferences = Sc_PlayerReferences.Instance;
        timeSinceAudio = 4.0f;
        isInteractable = true; 
    }

    void BreakMirror()
    {
        Destroy(gameObject);
    }

    public void Update()
    {
        timeSinceAudio += Time.deltaTime;
    }

    //We return the string that's used when the player's vision is on the game object
    public override string GetDescription()
    {
        if(isInteractable)
        {
            if (timeSinceAudio >= 4.0f)
            {
                UnityCore.Audio.AudioAction.PlaySound(portraitSound);
                timeSinceAudio = 0.0f;
            }
            return "Left Click to destroy the mirror";
        }
        return "";
    }
    
    //If there's an interaction, we call our functions
    public override void Interact()
    {
        if(isInteractable)
        {
            if(isSpecialPortrait)
            {
                BreakMirror();
                GameActions.waterDrains();
                isInteractable = false;
            } else
            {
                GameActions.WrongMirror();
                BreakMirror();
            }
        }
    }

    public override bool Invisible()
    {
        return invisible;
    }
}
