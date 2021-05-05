using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public static class RoomAction
{
    public static Action <RoomManager.direction >spawnCopyRoom;
    public static Action<RoomManager.direction> spawnNextRoom;
    public static Action<RoomManager.direction> finishArea;

}
