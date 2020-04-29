using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayerOnStartScript : MonoBehaviour
{
    public List<GameObject> PlayerList = new List<GameObject>();
    public List<GameObject> PlayerObj = new List<GameObject>();
   // int j;
    Vector3 tempPos;
    // Start is called before the first frame update
    void Start()
    {
        if (GameData.Playerdatas == null || GameData.Playerdatas.Length == 0)
            error();
        else
        spawnPlayers();
    }
  void error()
    {
        PlayerList.Add(Instantiate(PlayerObj[0], new Vector3(transform.position.x, transform.position.y - 2, transform.position.z + 10), Quaternion.identity));
    }
    Vector3 startPos(int possition)
    {
        tempPos = new Vector3(-8 + (possition * 3), - 2, transform.position.z + 10);
        return tempPos;
    }

    void spawnPlayers()
    {
       
        for (int i = 0; i < 4; i++)
        {
            if(Arcade.PlayerIsIngame(i))
            {
                for (int j = 0; j < 5; j++)
                {
                    if (j == GameData.Playerdatas[i].Character)
                    {
                        print("player caracternumber:" + j);
                        PlayerList.Add(Instantiate(PlayerObj[i], startPos(i), Quaternion.identity));
                        PlayerList[PlayerList.Count - 1].GetComponent<PlayerMovementPrototype>().Id = i;
                      //  j++;
                    }
                }
              
            }
        }
    }

}
