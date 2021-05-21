using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sc_PlayerInputs : MonoBehaviour
{
    //Variable of script
    private bool lockPlayer;
    public bool cursorVisibility;
    
    //Variables for Singleton Pattern
    private static Sc_PlayerInputs _instance;
    public static Sc_PlayerInputs Instance { get { return _instance; } }

    private void Awake()
    {
        //Making Sc_PlayerInputs.Instance return this script.
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }

        Time.timeScale = 1;
        
    }

    public bool GetMovingUp()
    {
        if (!lockPlayer)
        {
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            {
                return true;
            }
        }
        return false;
    }
    public bool GetMovingRight()
    {
        if (!lockPlayer)
        {
            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                return true;
            }
        }
        return false;
    }
    public bool GetMovingLeft()
    {
        if (!lockPlayer)
        {
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                return true;
            }
        }
        return false;
    }
    public bool GetMovingDown()
    {
        if (!lockPlayer)
        {
            if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            {
                return true;
            }
        }
        return false;
    }

    public bool GetRightClick()
    {
        if (!lockPlayer)
        {
            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                return true;
            }
        }
        return false;
    }

    public bool GetLeftClick()
    {
        if (!lockPlayer)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                return true;
            }
        }
        return false;
    }

    public bool GetInteraction()
    {
        if (!lockPlayer)
        {
            if (Input.GetKey(KeyCode.E))
            {
                return true;
            }
        }
        return false;
    }

    public bool GetRunningInput()
    {
        if (!lockPlayer)
        {
            if (Input.GetKey(KeyCode.LeftControl))
            {
                return true;
            }
        }
        return false;
    }
    
    //Locks the player making it unable to input anything
    public void SetLockPlayer()
    {
        lockPlayer = !lockPlayer;
    }
    
    //Updates the player cursor if there's need to.
    public void SetLockPlayerCursorVisibility()
    {
        cursorVisibility = !cursorVisibility;
    }

}
