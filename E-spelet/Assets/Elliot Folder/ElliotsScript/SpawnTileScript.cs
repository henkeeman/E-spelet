using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTileScript : MonoBehaviour
{
    public GameObject Tile;
    public int AmountOfTiles;
    Vector3 Startpos;
    float SpawnOdds;
    float camWidth;
    float timer; // in sec
    public float StartTime;
    public float DifTime; // how much the time should change for the spawning

    Vector3 scale;
    // Start is called before the first frame update
    void Start()
    {
        /* if (StartTime == null)
             StartTime = 1;
         if (DifTime == null)
             DifTime = 0.5f;*/
        scale = new Vector3(( 16.2f / AmountOfTiles),.5f,4);
        timer = StartTime;
        SpawnOdds = 1;
        camWidth = Camera.main.orthographicSize * 2.0f * Camera.main.aspect;
    }

    // Update is called once per frame
    void Update()
    {
        if(spawnTimer())
        {
            SpawnTile(AmountOfTiles);
        }
      //  print("time of timer: " + timer);
    }

    #region SpawnTiles

    void SpawnTile(int amount)
    {
        int spawnAmount = amount;

        for (int i = 0; i < amount; i++)
        {
            if (oddsOfSpawning())
            {
               GameObject g = Instantiate(Tile, posOfTile(spawnAmount,Tile), Quaternion.identity);
                g.transform.localScale = scale;
                spawnAmount--;
            }
            else
                spawnAmount--;

         }

    }

    Vector3 posOfTile(int positionX, GameObject tile)
    {
        
        float x = transform.position.x + ((positionX - .5f)  * scale.x * 1.1f);
        print("X: " + x);
        return new Vector3(x, transform.position.y, 2);
    }


    bool oddsOfSpawning()
    {
        if (SpawnOdds == 1)
        {
            SpawnOdds = SpawnOdds * 0.6f;
            return true;
        }
        else if(SpawnOdds >= Random.value)
        {
            return true;
        }
        else
        {
            return false;
        }
        
    }
    #endregion

    bool spawnTimer()
    {
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            timer = StartTime * DifTime;
            return true;
        }
        else
            return false;
    }
}
