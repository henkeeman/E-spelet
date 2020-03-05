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
        scale = new Vector3(( 16.2f / AmountOfTiles),.5f,4);
        timer = StartTime;
        SpawnOdds = .6f;
        camWidth = Camera.main.orthographicSize * 2.0f * Camera.main.aspect;
    }

    // Update is called once per frame
    void Update()
    {
        if(spawnTimer())
        {
            SpawnTileBetter(AmountOfTiles);
        }
    }

    Vector3 posOfTile(int positionX)
    {

        if (positionX == -1)
        {
            return new Vector3(100, -10, -100);
        }

        float x = transform.position.x + ((positionX - .5f) * scale.x * 1.1f);
        return new Vector3(x, transform.position.y, 2);
    }

    void SpawnTileBetter(int amountOfTiles)
    {
        amountOfTiles += 1;
        int R = Random.Range(1, amountOfTiles);

        int[] arrayPos = new int[amountOfTiles];
        for (int i = 0; i < R; i++)
        {
            arrayPos[i] = Random.Range(1, amountOfTiles);
            for (int j = 0; j < i; j++)
            {
                if (arrayPos[i] == arrayPos[j])
                {
                    arrayPos[i] = -1;
                }
            }
            GameObject g = Instantiate(Tile, posOfTile(arrayPos[i]), Quaternion.identity);
            g.transform.localScale = scale;
        }

    }

    #region OldSpawnTiles

    void SpawnTile(int amount)
    {
        int spawnAmount = amount;

        for (int i = 0; i < amount; i++)
        {
            if (oddsOfSpawning())
            {
               GameObject g = Instantiate(Tile, posOfTile(spawnAmount), Quaternion.identity);
                g.transform.localScale = scale;
                spawnAmount--;
            }
            else
                spawnAmount--;

         }

    }

    


    bool oddsOfSpawning()
    {
        if(SpawnOdds >= Random.value)
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
