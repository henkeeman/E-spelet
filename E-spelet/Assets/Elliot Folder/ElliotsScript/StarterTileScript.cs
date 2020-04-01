using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarterTileScript : MonoBehaviour
{

    Vector3 Startpos;
    float camWidth;
    public float DistanceBetweenSpawns;
    public GameObject Tile;
    public int AmountOfTiles;

    Vector3 scale;
    // Start is called before the first frame update
    void Start()
    {
        scale = new Vector3((16.2f / AmountOfTiles), .5f, 4);
        camWidth = Camera.main.orthographicSize * 2 * Camera.main.aspect;
        SpawnTileBetter(AmountOfTiles);
    }

    Vector3 posOfTile(int positionX)
    {

        if (positionX == -1)
        {
            return new Vector3(100, -10, -100);
        }

        float x = transform.position.x + ((positionX - .5f) * scale.x * 1.1f);
        return new Vector3(x, transform.position.y, 0);
    }

    void SpawnTileBetter(int amountOfTiles)
    {
        amountOfTiles += 1;
        int R = UnityEngine.Random.Range(1, amountOfTiles);

        int[] arrayPos = new int[amountOfTiles];

        for (int i = 0; i < R; i++)
        {
            arrayPos[i] = UnityEngine.Random.Range(1, amountOfTiles);
            for (int j = 0; j < i; j++)
            {
                if (arrayPos[i] == arrayPos[j])
                {
                    arrayPos[i] = -1;
                }
            }
            GameObject g = Instantiate(Tile,posOfTile(arrayPos[i]), Quaternion.identity);
            g.transform.localScale = scale;
        }

    }


}
