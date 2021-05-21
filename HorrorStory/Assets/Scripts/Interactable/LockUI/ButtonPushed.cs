using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityCore.Audio;
public class ButtonPushed : MonoBehaviour
{
    public static event Action<string> ButtonPressed = delegate { };

    private int deviderPosition;
    private string buttonName, buttonValue;

    void Start()
    {
        buttonName = gameObject.name;
        deviderPosition = buttonName.IndexOf("_");
        buttonValue = buttonName.Substring(0, deviderPosition);
        
        gameObject.GetComponent<Button>().onClick.AddListener(ButtonClicked);
    }

    private void ButtonClicked()
    {
        AudioAction.PlaySound(UnityCore.Audio.AudioType.buttonPress);
        ButtonPressed(buttonValue);
    }
}
