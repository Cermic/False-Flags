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

    public RoundData RandomGeneration(RoundData[] allRoundData, FlagData[] flagData, string[] x, int y)
    {

        //End initialisation
        for (int i = 0; i < allRoundData[1].questions.Length; i++)
        {

            for (int j = 0; j < 4; j++)
            {
                allRoundData[1].questions[i].answers[j] = flagData[0].answers[RandomNumber(flagData[0].answers.Length)];
            }

            //Checks if answers generated are unique values
            for (int k = 0; k < 4; k++)
            {
                for (int m = 0; m < 4; m++)
                {
                    //Compares all values to each other value
                    if (k != m)
                    {
                        while (allRoundData[1].questions[i].answers[k] == allRoundData[1].questions[i].answers[m])
                        {
                            allRoundData[1].questions[i].answers[k] = flagData[0].answers[RandomNumber(flagData[0].answers.Length)];
                        }
                    }
                }
            }

            if (i < y)
            {//Sets correct answer of new round data answer to be the current wrong answer and sets it to be true 
                int random = RandomNumber(3);
                bool checking = false;

                allRoundData[1].questions[i].questionText = "Try this again";
                for (int k = 0; k < 4; k++)
                {
                    if (allRoundData[1].questions[i].answers[k].answerText == x[i])//If the name of the country is already in the list, make this the correct answer
                    {
                        checking = true;
                        allRoundData[1].questions[i].answers[k].isCorrect = true;
                        break;
                    }
                }
                if (!checking)//If the name of the country is not in the button list set it to a random button and make it correct
                {
                    allRoundData[1].questions[i].answers[random].answerText = x[i];
                    allRoundData[1].questions[i].answers[random].isCorrect = true;
                }
            }
            else if (i >= y && i < 5)//Bonus questions
            {
                allRoundData[1].questions[i].questionText = "Bonus question: " + (allRoundData[1].pointsAddedForCorrectAnswer * 3) + " points!";
                allRoundData[1].questions[i].answers[RandomNumber(3)].isCorrect = true;

            }
        }
        return allRoundData[1];
    }

    private int RandomNumber(int num)
    {
        int value = (int)(Random.Range(0.0f, (float)num));
        return value;
    }
}