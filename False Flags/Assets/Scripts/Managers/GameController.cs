﻿using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class GameController : MonoBehaviour {

    public Text questionDisplayText;
    public Text scoreDisplayText;
    public Text timeRemaningDisplayText;
    public Text highScoreDisplay;
    public SimpleObjectPool answerButtonObjectPool;
    public Transform answerButtonParent;
    public GameObject questionDisplay, roundEndDisplay;

    private DataController dataController;
    private RoundData currentRoundData;
    private TransitionFactData[] transitionFactData;
    private QuestionData[] questionPool;

    //wrong answer variables
    private string[] wrongAnswers;
    private string[] passAnswers;
    private int num_wrong_answers;
    private string correct_answer;
    private int round;

    private bool isRoundActive;
    private float timeRemaining;
    private int questionIndex;
    private int playerScore;

    private List<GameObject> answerButtonGameObjects = new List<GameObject>();

    //variable for the progress bar
    private Scrollbar progressBar;

    private InfoCardScript ics;

    //variable for the flag image
    private Image questionFlag;

    private GameObject infoT;
    private Text infoText;

    // Use this for initialization
    void Start() {
        dataController = FindObjectOfType<DataController>();
        currentRoundData = dataController.GetCurrentRoundData();
        questionPool = currentRoundData.questions;
        timeRemaining = currentRoundData.timeLimitInSeconds;
        UpdateTimeRemaningDisplay();

        //Set wrong answer variables
        num_wrong_answers = 0;
        wrongAnswers = new string[5];
        round = 1;

        playerScore = 0;
        initialise();

    }
    void initialise()
    {
        //finds the game object with the "flag" tag and sets the questionflag variable to be its image component for flag image setting.
        questionFlag = GameObject.FindGameObjectWithTag("flag").GetComponent<Image>();

        //finds the Scrollbar component in the scene and initialises progressBar to it
        progressBar = FindObjectOfType<Scrollbar>();

        ics = FindObjectOfType<InfoCardScript>(); // This will work because there actually IS only ONE animtor in the scene.
                                                  // If more are to be added tags will need to be used.
        infoT = GameObject.Find("Canvas/TransitionPanel/InfoText"); // Find the InfoText Game Object
        infoText = infoT.GetComponent<UnityEngine.UI.Text>(); // Assigns the actual Text Component to the infoText variable.

        questionIndex = 0;

        ShowQuestion();
        isRoundActive = true;
    }

    /// <summary>
    /// ShowQuestion and RemoveAnswerButtons are parts of the non canvas setup for the question buttons
    /// This works by creating a button prefab that forms into a vertical list of buttons
    /// May need to switch to non-canvas version used in video or look into modifying this section to change pre-defined buttons in the canvas.
    /// </summary>
    private void ShowQuestion()
    {
        RemoveAnswerButtons();
        QuestionData questionData = questionPool[questionIndex];
        questionDisplayText.text = questionData.questionText;

        //changes the image on the flag display to be the one appropriate to the question by 
        //looking in the images folder for the flag named the same as in the questions flag text field
        //if ()
        //{
        string flagFilePath = " ";

        for(int i = 0; i < 4; i++)
        {
            if (questionData.answers[i].isCorrect==true)
            {
                flagFilePath = questionData.answers[i].answerText;
                correct_answer = questionData.answers[i].answerText;                            //used while saving wrong answers
                break;
            }
        }

        questionFlag.sprite = Resources.Load<Sprite>("Images/" + flagFilePath);
            

        for (int i = 0; i < questionData.answers.Length; i++)
        {

            GameObject answerButtonGameObject = answerButtonObjectPool.GetObject();
            answerButtonGameObjects.Add(answerButtonGameObject);
            answerButtonGameObject.transform.SetParent(answerButtonParent);

            AnswerButton answerButton = answerButtonGameObject.GetComponent<AnswerButton>();
            answerButton.Setup(questionData.answers[i]);

        }
    }

    private void RemoveAnswerButtons() {
        while (answerButtonGameObjects.Count > 0)
        {
            answerButtonObjectPool.ReturnObject(answerButtonGameObjects[0]);
            answerButtonGameObjects.RemoveAt(0);
        }
    }

    public void AnswerButtonClicked (bool isCorrect)
    {
        if (isCorrect)
        {
            if(currentRoundData.name == "Wrong Answers")
            {            
                if(currentRoundData.questions[questionIndex].questionText == "Bonus question: 15 points!")          //hard coded = bad
                {
                    playerScore += (currentRoundData.pointsAddedForCorrectAnswer) * 3;
                }
                else
                {
                    playerScore += currentRoundData.pointsAddedForCorrectAnswer;
                }
            }
            else
            {
                playerScore += currentRoundData.pointsAddedForCorrectAnswer;
            }
            scoreDisplayText.text = "Score: " + playerScore.ToString();
        }else if(!isCorrect && num_wrong_answers < 5 && currentRoundData.name != "Wrong Answers")                                 //Populates an array with up to 5 mistakes
        {
            wrongAnswers[num_wrong_answers] = correct_answer;
            num_wrong_answers++;
        }

        if (questionPool.Length > questionIndex + 1)
        {
            questionIndex++;
            ShowQuestion();
        }
        else if(round ==2)
        {
            questionIndex++;
            EndRound();
        }
        else
        {
            passAnswers = new string[num_wrong_answers];
            for (int i = 0; i < num_wrong_answers; i++)
            {
                passAnswers[i] = wrongAnswers[i];
            }
            currentRoundData = dataController.GetCurrentRoundData(passAnswers, num_wrong_answers);
            questionPool = currentRoundData.questions;
            timeRemaining = currentRoundData.timeLimitInSeconds;
            UpdateTimeRemaningDisplay();
            initialise();
            round++;
        }
        if (questionIndex % 5 == 0 && questionIndex != 0)
        {
            infoText.text = dataController.GetInfoData();
            ics.SlideIn();
        }
    }

    public void EndRound()
    {

            isRoundActive = false;
            dataController.SubmitNewPlayerScore(playerScore);
            highScoreDisplay.text = "High score: " + dataController.GetHighestPlayerScore().ToString();

            questionDisplay.SetActive(false);
            roundEndDisplay.SetActive(true);
    }

    public void ReturnToMenu()
    {
        FindObjectOfType<AudioManager>().Stop("Werq"); // Stop Game Music
        FindObjectOfType<AudioManager>().Play("Button_Click"); // Play button click sound
        FindObjectOfType<AudioManager>().Play("Getting_it_Done"); // Play menu music
        SceneManager.LoadScene("MenuScreen");
    }

    private void UpdateTimeRemaningDisplay()
    {
        string temp = (Mathf.Round(timeRemaining)).ToString();
        timeRemaningDisplayText.text = "Time: " + temp;
    }

	// Update is called once per frame
	void Update () {
        if (isRoundActive)
        {
            //sets the amount of steps in the progress bar to be the same as the total number of questions.
            progressBar.numberOfSteps = questionPool.Length;
            //changes the value (position) of the progress bars highlighted element to indicate the current question using its index
            progressBar.value = 1f - (((float)questionPool.Length - 1f) - (float)questionIndex) / ((float)questionPool.Length - 1f);
            //scales the size of the highlighted progress bar element to be in relation to the total number of questions, i.e. if there are 2 questions it will take up 50%
            progressBar.size = 1f / questionPool.Length;

            timeRemaining -= Time.deltaTime;
            UpdateTimeRemaningDisplay();

            if (timeRemaining <= 0.0f)
            {
                EndRound();
            }
        }
	}
}
