using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameData
{
    public static Playerdata[] Playerdatas = new Playerdata[4];

    static GameData()
    {
        for (int i = 0; i < 4; i++)
        {
            Playerdatas[i] = new Playerdata();
        }
    }
}
