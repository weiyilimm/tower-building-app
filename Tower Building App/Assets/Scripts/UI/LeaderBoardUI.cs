using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LeaderBoardUI : MonoBehaviour
{
    public TMP_InputField name;
    public TMP_InputField score;

    public LeaderBoard leaderBoard;

    void Start()
    {
        leaderBoard.Load();
        name.text = leaderBoard.data.name;
        score.text = leaderBoard.data.score.ToString();
    }



    public void Submit(string name, int score){
        leaderBoard.data.name = name;
        leaderBoard.data.score = score;
        leaderBoard.Save();
    }
}
