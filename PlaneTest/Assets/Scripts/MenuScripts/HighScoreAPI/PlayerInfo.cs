using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerInfo 
{
    public static HighScoreViewModel hvm { get; set; }
    public static UserViewModel uvm { get; set; }
    private static int playerScore = 0;
    private static bool isSignedIn = false;

    // session sign in info methods
    public static void signIn()
    {
        isSignedIn = true;
    }

    public static void signOut()
    {
        isSignedIn = false;
    }

    public static bool isPlayerSignedIn()
    {
        return isSignedIn;
    }

    // score methods
    public static void addPoints(int p)
    {
        playerScore += p;
    }

    public static int getScore()
    {
        return playerScore;
    }

    public static string getScoreString()
    {
        return playerScore.ToString();
    }

    public static void resetScore()
    {
        playerScore = 0;
    }
}
