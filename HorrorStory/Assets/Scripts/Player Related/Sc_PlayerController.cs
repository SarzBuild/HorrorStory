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
            HandleInteract();
            HandleLens();
            CheckIfDead();
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
        if (playerInputs.cursorVisibility)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    //This function will be dealt with the SendMessage method
    void HandleDamage(int damage)
    {
        //currentHealth - damage;
    }
    void HandleInteract()
    {
        if (playerInputs.GetInteraction())
        {
            //What we'd want to with this is making sure that the message can only be received if the player is in the precise trigger zone
            gameObject.SendMessageUpwards("Interaction",SendMessageOptions.DontRequireReceiver);
        }
    }

    void HandleLens()
    {
        
    }
    
    void CheckIfDead()
    {
        if (currentHealth <= 0)
        {
            //We completely lock the player and its actions in this statement, more locking to be added
            playerReferences.isDead = true;
            playerInputs.SetLockPlayer();
            playerInputs.SetLockPlayerCursorVisibility();
            playerReferences.isInCutscene = true;
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
