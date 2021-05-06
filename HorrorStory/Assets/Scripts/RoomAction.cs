using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public static class RoomAction
{
    public static Action <RoomManager.direction, Transform >spawnCopyRoom;
    public static Action<RoomManager.direction, Transform> spawnNextRoom;
    public static Action<RoomManager.direction, Transform> finishArea;

}
