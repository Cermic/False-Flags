using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RoundData {

    public string name;
    public int timeLimitInSeconds;          //Difficulty modifier?
    public int pointsAddedForCorrectAnswer; 
    public QuestionData[] questions;        //Testing purposes
}
