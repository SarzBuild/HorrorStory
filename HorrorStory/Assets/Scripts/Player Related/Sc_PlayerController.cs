using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Cursor = UnityEngine.Cursor;

public class Sc_PlayerController : MonoBehaviour
{
    private Sc_PlayerReferences playerReferences;
    private Sc_PlayerInputs playerInputs;
    
    private float currentSpeed;
    private int currentHealth;
    
    
    void Start()
    {
        //We get the reference to the script's Instance.
        playerInputs = Sc_PlayerInputs.Instance;
        playerReferences = Sc_PlayerReferences.Instance;
        //Making sure that the current health is set to the max health when the game starts
        currentHealth = playerReferences.maxHealth;
    }
    
    void FixedUpdate()
    {
        if (!CheckIfInCutscene())
        {
            HandleMovement();
            HandleJumpAndFall();
        }
    }

    void Update()
    {
        if (!CheckIfInCutscene())
        {
            HandleCameraLock();
            HandleLens();
            CheckIfDead();
            CheckForInteraction();
        }
    }

    void HandleMovement()
    {
        //First of all, we check if the player is running
        if (playerInputs.GetRunningInput())
            currentSpeed = playerReferences.runningSpeed;
        else
            currentSpeed = playerReferences.walkingSpeed;
        
        //Then we make it so that the player movement related inputs are put inside a Vector3
        Vector3 moveDirection = new Vector3(0, 0, 0);
        if (playerInputs.GetMovingUp())
            moveDirection.z = +1;
        if (playerInputs.GetMovingDown()) 
            moveDirection.z = -1;
        if (playerInputs.GetMovingLeft()) 
            moveDirection.x = -1;
        if (playerInputs.GetMovingRight()) 
            moveDirection.x = +1;
        moveDirection.Normalize(); //We normalize the values
        if (moveDirection.magnitude > 0) UnityCore.Audio.AudioAction.WalkingAudio();

        //We create a new vector3 that'll be our main moving variable and we set the velocity accordingly
        playerReferences.moveTowardsPos = new Vector3(moveDirection.x, playerReferences.jumpAndFallVelocity, moveDirection.z);
        //We update the player transform with the camera transform, meaning that the player will go forward in the direction that the camera is facing + when the player moves sideways, it updates with the camera transform
        playerReferences.moveTowardsPos = playerReferences.mainCamera.transform.forward * playerReferences.moveTowardsPos.z + playerReferences.mainCamera.transform.right * playerReferences.moveTowardsPos.x;
        //We reset the y value to fix the previous transform with the camera
        playerReferences.moveTowardsPos.y = playerReferences.jumpAndFallVelocity;
        //We set the velocity of the player accordingly
        playerReferences.rb.velocity = playerReferences.moveTowardsPos * currentSpeed;
    }

    void HandleJumpAndFall()
    {
        if (!CheckIfGrounded())
            //If we are falling, we gradually add more velocity to mimic the acceleration when falling
            playerReferences.jumpAndFallVelocity += playerReferences.gravity * Time.deltaTime;
        
        else if (CheckIfGrounded())
            //If we are grounded, we reset the velocity to zero
            playerReferences.jumpAndFallVelocity = 0f;
    }

    void HandleCameraLock()
    {
        /*if (playerInputs.cursorVisibility)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }*/
    }

    //This function will be dealt with the SendMessage method
    void HandleDamage(int damage)
    {
        //currentHealth - damage;
    }
    void CheckForInteraction()
    {
        //We send a ray in the middle of the screen
        Ray ray = playerReferences.mainCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0f));
        RaycastHit hit;

        playerReferences.successfulHit = false;
        
        //If the ray collides with something, we check if it has the Interactable component
        if (Physics.Raycast(ray, out hit, playerReferences.interactionDistance, playerReferences.interactionLayerMask))
        {
            Interactable interactable = hit.collider.GetComponent<Interactable>();
            
            //If the interactable does not return null and the object visibility is originally false, we send the script found to HandleInteraction function and we update the values of the object description when we hover over it 
            if ((interactable != null) && (!interactable.Invisible()))
            {
                HandleInteraction(interactable);
                playerReferences.interactionText.text = interactable.GetDescription();
                playerReferences.successfulHit = true;
                //If the object has the hold type, the image is set active
                playerReferences.holdButton.SetActive(interactable.interactionType ==
                                                      Interactable.InteractionType.Hold);
            }
            //Otherwise, we check if the object is invisible, technically, those objects should only show if the lens is showing, therefore, we use the lensShowing variable
            else if ((interactable != null) && (interactable.Invisible()))
            {
                if (playerReferences.lensShowing)
                {
                    HandleInteraction(interactable);
                    playerReferences.interactionText.text = interactable.GetDescription();
                    playerReferences.successfulHit = true;
                    //If the object has the hold type, the image is set active
                    playerReferences.holdButton.SetActive(interactable.interactionType ==
                                                          Interactable.InteractionType.Hold);
                }
            }
        }
        //We update if there's nothing
        if (!playerReferences.successfulHit)
        {
            if (!playerReferences.firstUseLens)
            {
                playerReferences.interactionText.text = "";
            }

            playerReferences.holdButton.SetActive(false);
        }
    }

    void HandleInteraction(Interactable interactable)
    {
        //Since we are using a enum for different cases, we check for which type of interaction we are looking for
        switch (interactable.interactionType)
        {
            case Interactable.InteractionType.Click:
                if (playerInputs.GetLeftClick())
                {
                    interactable.Interact();
                }
                break;
            case Interactable.InteractionType.Hold:
                if (playerInputs.GetInteraction())
                {
                    interactable.IncreaseHoldTime();
                    if (interactable.GetHoldTime() > 1f) 
                    {
                        interactable.Interact();
                        interactable.ResetHoldTime();
                    }
                }
                else
                {
                    interactable.ResetHoldTime();
                }
                playerReferences.holdButtonProgress.fillAmount = interactable.GetHoldTime();
                break;
            case Interactable.InteractionType.Special:
                break;
            default:
                //That's just for if there's an error
                throw new System.Exception("Unsupported type of interactable.");
        }
    }

    void HandleLens()
    {
        //First we check if the player has the lens
        if (playerReferences.hasLens)
        {
            //If the player right clicks, it updates if the lens if showing or not
            if (playerInputs.GetRightClick())
                playerReferences.lensShowing = !playerReferences.lensShowing;
            
            //If it is supposed to show, we set the lens to active, otherwise its not active
            if (playerReferences.lensShowing)
            {
                playerReferences.lensGameObject.SetActive(true);
                playerReferences.firstUseLens = false;
            }
            else
                playerReferences.lensGameObject.SetActive(false);
        }
        else
            //Here's just for when the game starts
            playerReferences.lensGameObject.SetActive(false);

        if (playerReferences.firstUseLens)
            playerReferences.interactionText.text = "Right click to use your new object.";


    }

    void HandleDeath()
    {
        //We completely lock the player and its actions in this statement, more locking to be added
        playerReferences.isDead = true;
        playerInputs.SetLockPlayer();
        playerInputs.SetLockPlayerCursorVisibility();
        playerReferences.isInCutscene = true;
    }
    
    void CheckIfDead()
    {
        //We check if the player has less than 0 health
        if (currentHealth <= 0)
        {
            HandleDeath();
        }
        //Otherwise, we check if the current health is greater than the max health, we set the current health back to the limit
        else if (currentHealth > playerReferences.maxHealth)
        {
           currentHealth = playerReferences.maxHealth;
        }
    }
    
    bool CheckIfGrounded()
    {
        //This hard-coded float is to set a maximum length to the cast
        float extraDistanceValue = 0.65f;
        RaycastHit hit;
        Physics.BoxCast(playerReferences.collider.bounds.center, playerReferences.collider.bounds.extents / 2 , Vector3.down, out hit, Quaternion.identity, extraDistanceValue, playerReferences.groundLayerMask); //We cast a ray underneath the player to see if they collide with something that is the chosen Layer Mask.
        if (hit.collider != null)
        {
            //If it doesn't return null, it means that it collided with something therefore, the player is on the ground
            return true;
        }
        else
        {
            //Otherwise, the player is either jumping or falling, in our case, the player may be falling.
            return false;
        }
    }
    
    bool CheckIfInCutscene()
    {
        if (playerReferences.isInCutscene)
        {
            return true;
        }

        return false;
    }
    
}
