using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CharacterPickerScript : MonoBehaviour
{
    //lägg in timer
    public float timer = 0;
    const int maxchars = 5;
    public int playerId;
    public GameObject[] spelare = new GameObject[maxchars];
    GameObject[] charicons = new GameObject[maxchars];
    int characterId = 0;
    // Start is called before the first frame update
    void Start()
    {
        if (!Arcade.PlayerIsIngame(playerId))
        {
            gameObject.SetActive(false);
            
        }
        else
        {
            for (int i = 0; i < spelare.Length; i++)
            {
                charicons[i] = Instantiate(spelare[i], new Vector3 (-2 + 1.3f*playerId, 0.6f, 0), Quaternion.Euler(0,-90,0)); //fixa fucking rotationshelvetet jag vill dö
                charicons[i].SetActive(false);
            }
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Arcade.GetKeyDown(playerId, ArcadeButton.Right))
        {
            GameData.Playerdatas[playerId].Character++;
            Text charText = GetComponentInChildren<Text>(); //byt till gameobj
            charText.text = GameData.Playerdatas[playerId].Character.ToString();
            print("FAAAAAAAAAAAAAAANNNNNNNNNNNNN");
            characterId++;
            if(characterId >= maxchars)
            {
                characterId = 0;
            }

        }
        if (Arcade.GetKeyDown(playerId, ArcadeButton.Left))
        {
            GameData.Playerdatas[playerId].Character++;
            Text charText = GetComponentInChildren<Text>(); //byt till gameobj
            charText.text = GameData.Playerdatas[playerId].Character.ToString();

            characterId++;
            if (characterId >= maxchars)
            {
                characterId = 0;
            }

        }
        for (int i = 0; i < charicons.Length; i++)
        {
            if(characterId == i)
            {
                charicons[i].SetActive(true);
            }
            else
            {
                charicons[i].SetActive(false);
            }
        }
        timer -= Time.deltaTime;
        Text timerText = GetComponentInChildren<Text>();
        timerText.text = Mathf.Round(timer).ToString();
        // fixa för fan
        //okejmenassådåva gör en lista som kopplar till spelarna som byter när man byter LOGIK ELLER HUR
        if (timer <= 0)
        {
            GameData.Playerdatas[playerId].Character = characterId;
            SceneManager.LoadScene("uwu"); //detta oxå
            
        }
        
    }
    
}
