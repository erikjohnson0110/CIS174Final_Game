using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreRowScript : MonoBehaviour
{
    public Text UserName;
    public Text Score;
    public Text Date;

    public ScoreRowScript(string name, string score, string date)
    {
        UserName.text = name;
        Score.text = score;
        Date.text = date;
    }
}
