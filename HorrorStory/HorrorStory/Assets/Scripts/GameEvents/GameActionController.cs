using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameActionController : MonoBehaviour
{
    public Sprite startCutscene1; 
    public Sprite startCutscene2;
    public Sprite startCutscene3;

    public Water water;

    private Door bathroomDoor;

    private void Awake()
    {
        bathroomDoor = null;
    }
    private void OnEnable()
    {
        GameActions.RunStartCutscene += RunStartCutscene;
        GameActions.SafeUnlocked += SafeUnlocked; //turn off tv ect 
        GameActions.enterBathroom += EnterBathroom; //lock door start water rising
        GameActions.playerDrown += PlayerDrown;
        GameActions.waterDrains += WaterDrains; // after some time unlock the bathroom door
        GameActions.endCutscene += RunLastCutScene;
        GameActions.WrongMirror += WrongMirrorBreak;
        GameActions.BathroomFinished += BathroomFinished;
    }

    private void OnDisable()
    {
        GameActions.RunStartCutscene -= RunStartCutscene;
        GameActions.SafeUnlocked -= SafeUnlocked; //turn off tv ect 
        GameActions.enterBathroom -= EnterBathroom; //lock door start water rising
        GameActions.playerDrown -= PlayerDrown;
        GameActions.waterDrains -= WaterDrains; // after some time unlock the bathroom door
        GameActions.endCutscene -= RunLastCutScene;
        GameActions.WrongMirror += WrongMirrorBreak;
        GameActions.BathroomFinished -= BathroomFinished;


    }
    public void RunLastCutScene()
    {
        UnityCore.Audio.AudioAction.PlaySound(UnityCore.Audio.AudioType.EndCutscene);

        // fade into darkness

        StartCoroutine(delayCutsceneScreen(2.0f, startCutscene1)); //pass in image
        StartCoroutine(delayCutsceneScreen(7.0f, startCutscene2)); //pass in image
        StartCoroutine(delayCutsceneScreen(12.0f, startCutscene3)); //pass in image

    }
    public void RunStartCutscene()
    {
        UnityCore.Audio.AudioAction.PlaySound(UnityCore.Audio.AudioType.StartCutscene);
        StartCoroutine(delayCutsceneScreen(0.0f, startCutscene1)); //pass in image
        StartCoroutine(delayCutsceneScreen(5.0f, startCutscene2)); //pass in image
        StartCoroutine(delayCutsceneScreen(10.0f, startCutscene3)); //pass in image
        StartCoroutine(delayFadeFromDark(15.0f, 2.0f));

    }
    public void SafeUnlocked()
    {
        UnityCore.Audio.AudioAction.PlaySound(UnityCore.Audio.AudioType.UI_Code_Sucess);
        StartCoroutine(delaySound(0.5f, UnityCore.Audio.AudioType.PickUp));
        StartCoroutine(delaySound(1.0f, UnityCore.Audio.AudioType.TVTurnOff));

    }


    public void PlayerDrown()
    {
        //gameover
        UnityCore.Audio.AudioAction.PlaySound(UnityCore.Audio.AudioType.WaterAbovePlayer);

    }
    public void BathroomFinished()
    {
        bathroomDoor.UnlockDoor();
        bathroomDoor.Interact();
        UnityCore.Audio.AudioAction.StopSound(UnityCore.Audio.AudioType.WaterDrainingOut);


    }
    public void WaterDrains()
    {
        UnityCore.Audio.AudioAction.PlaySound(UnityCore.Audio.AudioType.PortraitSmash);
        UnityCore.Audio.AudioAction.PlaySound(UnityCore.Audio.AudioType.WaterDrainingOut);
        water.Drain();

        
    }

    public void WrongMirrorBreak()
    {
        UnityCore.Audio.AudioAction.PlaySound(UnityCore.Audio.AudioType.PortraitSmash);
    }

    public void EnterBathroom(Door brDoor)
    {
        bathroomDoor = brDoor;
        //bathroomDoor.CloseDoor();
        bathroomDoor.canBeOpened = false;
        water.Rise();
        Debug.Log("start");

    }
    IEnumerator delayCutsceneScreen(float time, Sprite spr)
    {
        yield return new WaitForSeconds(time);
        //Send Sprite to UI Image
    }

    IEnumerator delayFadeFromDark(float time, float time2)
    {
        yield return new WaitForSeconds(time);
        //fade from dark
        yield return new WaitForSeconds(time2);
        UnityCore.Audio.AudioAction.LivingRoomStart();


    }

    IEnumerator delayUnlock(float time)
    {
        yield return new WaitForSeconds(time);
    }

    IEnumerator delaySound(float time, UnityCore.Audio.AudioType sound )
    {
        yield return new WaitForSeconds(time);
        UnityCore.Audio.AudioAction.PlaySound(sound);

    }

    IEnumerator delayStopAudio(float time, UnityCore.Audio.AudioType sound)
    {
        yield return new WaitForSeconds(time);
        UnityCore.Audio.AudioAction.StopSound(sound);

    }
}
