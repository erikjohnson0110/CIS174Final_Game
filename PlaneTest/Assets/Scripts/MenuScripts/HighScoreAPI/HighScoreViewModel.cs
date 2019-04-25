using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighScoreViewModel 
{
    public string userName { get; set; }
    public int score { get; set; }
    public DateTime dateAttained { get; set; }
    public Guid userID { get; set; }

    public string getDateString()
    {
        return dateAttained.ToShortDateString();
    }

    public string getScoreString()
    {
        return score.ToString();
    }
}
