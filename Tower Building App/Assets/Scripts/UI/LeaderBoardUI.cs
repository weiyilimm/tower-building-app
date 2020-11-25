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


    public void changeName(string text){
        leaderBoard.data.name = text;
    }

    public void changeScore(int number){
        leaderBoard.data.score = number;
    }



    public void Submit(){
        leaderBoard.Save();
    }
}
