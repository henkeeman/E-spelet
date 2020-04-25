using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDieSript : MonoBehaviour
{
    public ParticleSystem DeathEffeckt;
    // Update is called once per frame
    void Update()
    {
        if(GetComponent<PlayerClockSript>().Endgame)
        {
            Instantiate(DeathEffeckt, transform.position, Quaternion.identity);
            Arcade.SetScore(GetComponent<PlayerMovementPrototype>().Id, Mathf.RoundToInt(ScoreCounterScript.Sec));
            Destroy(this.gameObject);
        }
    }
}
