using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class GameActions 
{
    public static Action RunStartCutscene;
    public static Action SafeUnlocked; //turn off tv ect 
    public static Action <Door> enterBathroom; //lock door start water rising
    public static Action playerDrown;
    public static Action waterDrains; // after some time unlock the bathroom door
    public static Action endCutscene;
    public static Action WrongMirror;
    public static Action BathroomFinished;


}
