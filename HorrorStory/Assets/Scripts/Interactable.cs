using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    //Setting the enum for different type of interaction
    public enum InteractionType
    {
        Click,
        Hold,
        Special
    }

    private float holdTime;
    //Each script that derives from this has three different values, the type of interaction, the description and what to do when there's an interaction
    public InteractionType interactionType;
    public abstract string GetDescription();
    public abstract void Interact();

    public abstract bool Invisible();
    
    //There's also those variables, but that's only for the hold type
    public void IncreaseHoldTime() => holdTime += Time.deltaTime;
    public void ResetHoldTime() => holdTime = 0f;
    public float GetHoldTime() => holdTime;
}
