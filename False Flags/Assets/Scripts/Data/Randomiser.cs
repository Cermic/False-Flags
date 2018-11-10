using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Randomiser : MonoBehaviour
{
    private void Start()
    {
        
    }
    public RoundData RandomGeneration(RoundData[] allRoundData, FlagData[] flagData)
    {
        for (int i = 0; i < allRoundData[0].questions.Length; i++)
        {

            for (int j = 0; j < 4; j++)
            {
                allRoundData[0].questions[i].answers[j] = flagData[0].answers[RandomNumber(flagData[0].answers.Length)];
            }

            //Checks if answers generated are unique values
            for (int k = 0; k < 4; k++)
            {
                for (int m = 0; m < 4; m++)
                {
                    //Compares all values to each other value
                    if (k != m)
                    {
                        while (allRoundData[0].questions[i].answers[k] == allRoundData[0].questions[i].answers[m])
                        {
                            allRoundData[0].questions[i].answers[k] = flagData[0].answers[RandomNumber(flagData[0].answers.Length)];
                        }
                    }
                }
            }

            allRoundData[0].questions[i].answers[RandomNumber(3)].isCorrect = true;
        }
        return allRoundData[0];
    }

    private int RandomNumber(int num)
    {
        int value = (int)(Random.Range(0.0f, (float)num));
        return value;
    }
}