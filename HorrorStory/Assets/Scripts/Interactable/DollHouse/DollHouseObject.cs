using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DollHouseObject : Interactable
{
    //We get the player references to update some values
    private Sc_PlayerReferences playerReferences;
    private Sc_PlayerInputs playerInputs;
    private Animator dollhouseAnimator;
    private Collider dollhouseCollider;
    private CameraFade _cameraFade;
    [SerializeField] private bool invisible;
    [SerializeField] private GameObject lockGUI;
    private bool isOpen;
    
    private void Start()
    {
        _cameraFade = CameraFade.CameraInstance;
        playerReferences = Sc_PlayerReferences.Instance;
        playerInputs = Sc_PlayerInputs.Instance;
        dollhouseAnimator = GetComponent<Animator>();
        dollhouseCollider = GetComponent<Collider>();
        lockGUI.SetActive(false);
        isOpen = false;
    }

    public void OpenDollHouse()
    {
        dollhouseAnimator.SetBool("mustOpen", true);
        dollhouseCollider.enabled = false;
        CloseLockGUI();
    }

    void OpenLockGUI()
    {
        isOpen = true;
        playerInputs.SetLockPlayer();
        playerInputs.SetLockPlayerCursorVisibility();
        _cameraFade.FadeOut(0.1f,false);
        lockGUI.SetActive(true);
    }

    public void CloseLockGUI()
    {
        isOpen = false;
        playerInputs.SetLockPlayer();
        playerInputs.SetLockPlayerCursorVisibility();
        _cameraFade.FadeIn(0.1f,false);
        lockGUI.SetActive(false);
    }

    //We return the string that's used when the player's vision is on the game object
    public override string GetDescription()
    {
        if (isOpen) return "";
        return "Click Left Mouse to open the lock.";
    }
    
    //If there's an interaction, we call our functions
    public override void Interact()
    {
        OpenLockGUI();
    }

    public override bool Invisible()
    {
        return invisible;
    }
}
