using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivingRoomPuzzleObjects : Interactable
{    
    [SerializeField] private bool invisible;
    public bool canInteract;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void ManageInteraction()
    {
        if (canInteract)
        {
            canInteract = false;
            SendMessageUpwards("HandleInteraction", SendMessageOptions.DontRequireReceiver);
        }        
    }
    public override string GetDescription()
    {
        if (canInteract)
        {
            return "Left click to read clue.";
        }
        else
        {
            return null;
        }
    }

    //If there's an interaction, we call our functions
    public override void Interact()
    {
        ManageInteraction();
    }

    public override bool Invisible()
    {
        return invisible;
    }
}
