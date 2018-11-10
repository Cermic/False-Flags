using UnityEngine;
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
    private QuestionData[] questionPool;

    private bool isRoundActive;
    private float timeRemaining;
    private int questionIndex;
    private int playerScore;

    private List<GameObject> answerButtonGameObjects = new List<GameObject>();

    //variable for the progress bar
    private Scrollbar progressBar;

    private InfoCardScript ics;

    // Use this for initialization
    void Start() {
        dataController = FindObjectOfType<DataController>();
        currentRoundData = dataController.GetCurrentRoundData();
        questionPool = currentRoundData.questions;
        timeRemaining = currentRoundData.timeLimitInSeconds;
        UpdateTimeRemaningDisplay();

        //finds the Scrollbar component in the scene and initialises progressBar to it
        progressBar = FindObjectOfType<Scrollbar>();

        ics = FindObjectOfType<InfoCardScript>(); // This will work because there actually IS only ONE animtor in the scene.
        // If more are to be added tags will need to be used.

        playerScore = 0;
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

        for(int i= 0;i< questionData.answers.Length; i++)
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
            playerScore += currentRoundData.pointsAddedForCorrectAnswer;
            scoreDisplayText.text = "Score: " + playerScore.ToString();
        }

        if (questionPool.Length > questionIndex + 1)
        {
            questionIndex++;
            ShowQuestion();
        }
        else
        {
            questionIndex++;
            EndRound();
        }

        if (questionIndex % 5 == 0 && questionIndex != 0)
        {
            ics.SlideIn();
        }
    }

    public void EndRound()
    {
        isRoundActive = false;
        dataController.SubmitNewPlayerScore(playerScore);
        highScoreDisplay.text = "High score: " + dataController.GetHighestPlayerScore().ToString();

        questionDisplay.SetActive (false);
        roundEndDisplay.SetActive (true);
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
            progressBar.value = 0.1f + (1f / progressBar.numberOfSteps) * questionIndex;
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
