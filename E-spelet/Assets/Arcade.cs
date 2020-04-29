// Arcade.cs
// Version 1.01
// 2020-03-10
// Author Simon Grönberg
// LBS Kreativa Gymnasiet

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
/// <summary>
/// An enumeration of all buttons for each player in the arcade
/// </summary>
public enum ArcadeButton
{
    Left = 0, 
    Right = 1, 
    Up = 2, 
    Down = 3, 
    Red = 4, 
    Green = 5, 
    Blue = 6, 
    Yellow = 7
}

public class AxisState
{
    bool IsDown = false;
    bool WasReleased = false;
    bool WasPressed = false;
    int Sign = 0;
    DateTime PressedTime = DateTime.Now;
    DateTime ReleasedTime = DateTime.Now;
    string AxisName;

    public AxisState(string axisName, bool positive)
    {
        if (positive)
            Sign = 1;
        else
            Sign = -1;
        AxisName = axisName;
    }
    void UpdateAxis()
    {
        if (Input.GetAxis(AxisName) * Sign > 0 && (!IsDown || DateTime.Now > PressedTime + TimeSpan.FromSeconds(0.5)))
        {
            IsDown = true;
            PressedTime = DateTime.Now;
            WasPressed = true;
        }
        if (Input.GetAxis(AxisName) * Sign <= 0 && IsDown)
        {
            IsDown = false;
            WasReleased = true;
            ReleasedTime = DateTime.Now;
        }

    }
    public bool GetAxisDown()
    {
        UpdateAxis();
        if (WasPressed && DateTime.Now < PressedTime + TimeSpan.FromSeconds(0.2))
        {
            WasPressed = false;
            return true;
        }
        return false;
           
    }
    public bool GetAxisUp()
    {
        UpdateAxis();
        if (WasReleased && DateTime.Now < ReleasedTime + TimeSpan.FromSeconds(0.2))
        {
            WasReleased = false;
            return true;
        }
        return false;
    }
    public bool GetAxis()
    {
        UpdateAxis();
        return (Input.GetAxis(AxisName) * Sign > 0);
    }
}
/// <summary>
/// Contains button mapping and score for one player
/// Maps keycodes to ArcadeButtons
/// </summary>
public class ArcadePlayer
{
    KeyCode[] buttons = new KeyCode[8];
    int PlayerId;
    string[] joyButtons = new string[8];
    AxisState[] axes = new AxisState[4];
    bool UsingJoystick = false;
    bool LeftDown;
    bool RightDown;
    bool UpDown;
    bool DownDown;

    bool ingame = false;
    int score = 0;

    /// <summary>
    /// Constructor for the ArcadePlayer which sets up the button mapping to a keyboard and joysticks
    /// </summary>
    /// <param name="playerId"></param>
    /// <param name="leftButton"></param>
    /// <param name="rightButton"></param>
    /// <param name="upButton"></param>
    /// <param name="downButton"></param>
    /// <param name="redButton"></param>
    /// <param name="greenButton"></param>
    /// <param name="blueButton"></param>
    /// <param name="yellowButton"></param>
    public ArcadePlayer(int playerId, KeyCode leftButton, KeyCode rightButton, KeyCode upButton, KeyCode downButton, KeyCode redButton, KeyCode greenButton, KeyCode blueButton, KeyCode yellowButton, bool usingJoystick = false)
    {
        buttons[0] = leftButton;
        buttons[1] = rightButton;
        buttons[2] = upButton;
        buttons[3] = downButton;
        buttons[4] = redButton;
        buttons[5] = greenButton;
        buttons[6] = blueButton;
        buttons[7] = yellowButton;
        PlayerId = playerId;
        UsingJoystick = true;
        string prefix = "Joy" + (playerId+1).ToString() + "_";
        joyButtons[0] = prefix + "X";
        joyButtons[1] = prefix + "X";
        joyButtons[2] = prefix + "Y";
        joyButtons[3] = prefix + "Y";
        joyButtons[4] = prefix + "0";
        joyButtons[5] = prefix + "1";
        joyButtons[6] = prefix + "2";
        joyButtons[7] = prefix + "3";

        axes[0] = new AxisState(prefix + "X", false);
        axes[1] = new AxisState(prefix + "X", true);
        axes[2] = new AxisState(prefix + "Y", false);
        axes[3] = new AxisState(prefix + "Y", true);
    }



    /// <summary>
    /// Add this player to the game
    /// </summary>
    public void AddToGame()
    {
        ingame = true;
    }
    /// <summary>
    /// Check whether this player is in game
    /// </summary>
    /// <returns>true if ingame</returns>
    public bool IsIngame()
    {
        return ingame;
    }
    /// <summary>
    /// Check if a corresponding joystick-button or axis has been pressed
    /// </summary>
    /// <param name="button">the ArcadeButton to check</param>
    /// <returns>true if the button has been pressed</returns>
    private bool GetJoyButtonDown(ArcadeButton button)
    {
        if ((int)button > 3)
        {   
            return Input.GetButtonDown(joyButtons[(int)button]);
        }
        else
        {
            return axes[(int)button].GetAxisDown();
        }
    }
    /// <summary>
    /// Check if an ArcadeButton has been pressed since last frame
    /// </summary>
    /// <param name="button">The ArcadeButton to check</param>
    /// <returns>true if the button has been pressed</returns>
    public bool GetKeyDown(ArcadeButton button)
    {
        if (UsingJoystick)
        {
            if (GetJoyButtonDown(button))
            {
                return true;
            }
        }
        return Input.GetKeyDown(buttons[(int)button]);
    }
    /// <summary>
    /// Check if a corresponding joystick-button or axis has been released
    /// </summary>
    /// <param name="button">the ArcadeButton to check</param>
    /// <returns>true if the button has been released</returns>
    private bool GetJoyButtonUp(ArcadeButton button)
    {
        if ((int)button > 3)
        {
            return Input.GetButtonUp(joyButtons[(int)button]);
        }
        else
        {
            return axes[(int)button].GetAxisUp();
        }
    }
    /// <summary>
    /// Check if an ArcadeButton has been released since last frame
    /// </summary>
    /// <param name="button">The ArcadeButton to check</param>
    /// <returns>true if the button has been released</returns>
    public bool GetKeyUp(ArcadeButton button)
    {
        if (UsingJoystick)
        {
            if (GetJoyButtonUp(button))
            {
                return true;
            }
        }
        return Input.GetKeyUp(buttons[(int)button]);
    }
    /// <summary>
    /// Check if a corresponding joystick-button or axis is being hold down
    /// </summary>
    /// <param name="button">the ArcadeButton to check</param>
    /// <returns>true if the button is being hold down</returns>
    private bool GetJoyButton(ArcadeButton button)
    {
        if ((int)button > 3)
        {
            return Input.GetButton(joyButtons[(int)button]);
        }
        else
        {
            return axes[(int)button].GetAxis();
        }
    }
    /// <summary>
    /// Check if an ArcadeButton is being hold down
    /// </summary>
    /// <param name="button">The ArcadeButton to check</param>
    /// <returns>true if the button is being hold down</returns>
    public bool GetKey(ArcadeButton button)
    {
        if (UsingJoystick)
        {
            if (GetJoyButton(button))
            {
                return true;
            }
        }
        return Input.GetKey(buttons[(int)button]);
    }
    /// <summary>
    /// Sets the player's score to a fixed number
    /// </summary>
    /// <param name="newScore">the score the player should have</param>
    public void SetScore(int newScore)
    {
        score = newScore;
        if (score < 0)
            score = 0;
    }
    /// <summary>
    /// Adds a number to the player's score
    /// </summary>
    /// <param name="scoreToAdd">the number the score should be increased with</param>
    public void AddScore(int scoreToAdd)
    {
        score += scoreToAdd;
    }
    /// <summary>
    /// Get the player's current score
    /// </summary>
    /// <returns>The player's score</returns>
    public int GetScore()
    {
        return score;
    }
    /// <summary>
    /// Subtracts a number from the player's score
    /// </summary>
    /// <param name="scoreToSubtract">the number to subract from the score</param>
    public void SubtractScore(int scoreToSubtract)
    {
        score -= scoreToSubtract;
        if (score < 0)
            score = 0;
    }

}
/// <summary>
/// A wrapper class to work with the Arcade's controls and menu system
/// </summary>
public static class Arcade
{
    static ArcadePlayer[] player = new ArcadePlayer[4];
    static StaticDestructor Dtor = new StaticDestructor();
    const string filePath = "arcade.txt";

    private sealed class StaticDestructor
    {
        ~StaticDestructor()
        {
            //Arcade.Save();
        }
    }
    /// <summary>
    /// Constructor that sets up the control mapping for all players
    /// </summary>
    static Arcade()
    {
        player[0] = new ArcadePlayer(0, KeyCode.LeftArrow, KeyCode.RightArrow, KeyCode.UpArrow, KeyCode.DownArrow, KeyCode.Return, KeyCode.RightShift, KeyCode.RightControl, KeyCode.Backspace, true);
        player[1] = new ArcadePlayer(1, KeyCode.A, KeyCode.D, KeyCode.W, KeyCode.S, KeyCode.Q, KeyCode.E, KeyCode.Z, KeyCode.X, true);
        player[2] = new ArcadePlayer(2, KeyCode.F, KeyCode.H, KeyCode.T, KeyCode.G, KeyCode.R, KeyCode.Y, KeyCode.V, KeyCode.B, true);
        player[3] = new ArcadePlayer(3, KeyCode.J, KeyCode.L, KeyCode.I, KeyCode.K, KeyCode.U, KeyCode.O, KeyCode.M, KeyCode.N, true);
        Load();
    }
    /// <summary>
    /// Reads the transfer file from the Arcade's menu system and adds the active players
    /// </summary>
    static void Load()
    {
        StreamReader sr = new StreamReader(filePath);
        int i = 0;
        while (!sr.EndOfStream && i < 4)
        {
            string line = sr.ReadLine();
            if (line == "true")
            {
                player[i].AddToGame();
            }
            i++;
        }

        sr.Close();
    }
    /// <summary>
    /// Saves the score to the transfer file to the Arcade's menu system
    /// </summary>
    public static void Save()
    {
        StreamWriter sw = new StreamWriter(filePath);
        for (int i=0; i<4; i++)
        {
            sw.WriteLine(player[i].GetScore().ToString());
        }
        sw.Close();
    }
    /// <summary>
    /// Checks if the playerId is within the bounds
    /// </summary>
    /// <param name="playerId">the playerId to check</param>
    /// <returns>true if the playerId is within bounds</returns>
    static bool checkPlayerId(int playerId)
    {
        if (playerId >= 0 && playerId < 4)
        {
            return true;
        }
        return false;
            
    }
    /// <summary>
    /// Check if an ArcadeButton for a certain player has been pressed since last frame
    /// </summary>
    /// <param name="playerId">The player to check</param>
    /// <param name="button">The ArcadeButton to check</param>
    /// <returns>true if the button has been pressed</returns>
    /// <summary>
    public static bool GetKeyDown(int playerId, ArcadeButton button)
    {
        if (checkPlayerId(playerId))
        {
            return player[playerId].GetKeyDown(button);
        }
        else
        {
            throw new System.ArgumentException("Arcade.GetKeyDown: PlayerId out of bounds");
        }
        
    }
    /// <summary>
    /// Check if an ArcadeButton for a certain player has been released since last frame
    /// </summary>
    /// <param name="playerId">The player to check</param>
    /// <param name="button">The ArcadeButton to check</param>
    /// <returns>true if the button has been released</returns>
    /// <summary>
    public static bool GetKeyUp(int playerId, ArcadeButton button)
    {
        if (checkPlayerId(playerId))
        {
            return player[playerId].GetKeyUp(button);
        }
        else
        {
            throw new System.ArgumentException("Arcade.GetKeyUp: PlayerId out of bounds");
        }
        
    }
    /// <summary>
    /// Check if an ArcadeButton for a certain player is being hold down
    /// </summary>
    /// <param name="playerId">The player to check</param>
    /// <param name="button">The ArcadeButton to check</param>
    /// <returns>true if the button is being hold down</returns>
    /// <summary>
    public static bool GetKey(int playerId, ArcadeButton button)
    {
        if (checkPlayerId(playerId))
        {
            return player[playerId].GetKey(button);
        }
        else
        {
            throw new System.ArgumentException("Arcade.GetKey: PlayerId out of bounds");
        }
        
    }
    /// <summary>
    /// Checks whether the player is currently in the game
    /// </summary>
    /// <param name="playerId">the player to check</param>
    /// <returns></returns>
    public static bool PlayerIsIngame(int playerId)
    {
        if (playerId >= 0 && playerId < 4)
        {
            return player[playerId].IsIngame();
        }
        else
        {
            throw new System.ArgumentException("Arcade.PlayerIsIngame: PlayerId out of bounds");
        }
        
    }
    /// <summary>
    /// Gets the current score of a player
    /// </summary>
    /// <param name="playerId">The player whose score to get</param>
    /// <returns>the score</returns>
    public static int GetScore(int playerId)
    {
        if (checkPlayerId(playerId))
        {
            return player[playerId].GetScore();
        }
        else
        {
            throw new System.ArgumentException("Arcade.GetScore: PlayerId out of bounds");
        }
        
    }
    /// <summary>
    /// Sets the score of a player to a fixed number
    /// </summary>
    /// <param name="playerId">the player whose score to set</param>
    /// <param name="score">the number which the score should be set to</param>
    public static void SetScore(int playerId, int score)
    {
        if (checkPlayerId(playerId))
        {
            player[playerId].SetScore(score);
        }
        else
        {
            throw new System.ArgumentException("Arcade.SetScore: PlayerId out of bounds");
        }
        
    }
    /// <summary>
    /// Increases the score of a player by a certain number
    /// </summary>
    /// <param name="playerId">the player whose score to increase</param>
    /// <param name="score">the amount of points to add to the score</param>
    public static void AddScore(int playerId, int score)
    {
        if (checkPlayerId(playerId))
        {
            player[playerId].AddScore(score);
        }
        else
        {
            throw new System.ArgumentException("Arcade.AddScore: PlayerId out of bounds");
        }
    }

    /// <summary>
    /// Decreases the score of a player by a certain number
    /// </summary>
    /// <param name="playerId">the player whose score to decrease</param>
    /// <param name="score">the amount of points the score should be decreased with</param>
    public static void SubtractScore(int playerId, int score)
    {
        if (checkPlayerId(playerId))
        {
            player[playerId].SubtractScore(score);
        }
        else
        {
            throw new System.ArgumentException("Arcade.SubtractScore: PlayerId out of bounds");
        }
        
    }


}
