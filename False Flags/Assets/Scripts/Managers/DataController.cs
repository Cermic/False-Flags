using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;


public class DataController : MonoBehaviour {

    private RoundData[] allRoundData;
    private PlayerProgress playerProgress;
    private string gameDataFile = "flag.json";  //Initialised here but if game develops more modes this would need to be set elsewhere

	// Use this for initialization
	void Start () {
        DontDestroyOnLoad(gameObject);
        LoadGameData();
        LoadPlayerProgress();
        SceneManager.LoadScene("MenuScreen");
	}

    public RoundData GetCurrentRoundData() //public RoundData GetCurrentRoundData(int index)
    {
        return allRoundData[0];                     //return allRoundData[index];
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SubmitNewPlayerScore(int newScore)
    {
        if(newScore > playerProgress.highestScore)
        {
            playerProgress.highestScore = newScore;
            SavePlayerProgress();

        }
    }

    public int GetHighestPlayerScore()
    {
        return playerProgress.highestScore;
    }

    private void LoadPlayerProgress()
    {
        playerProgress = new PlayerProgress();

        if (PlayerPrefs.HasKey("highestScore"))
        {
            playerProgress.highestScore = PlayerPrefs.GetInt("highestScore");
        }
    }

    private void SavePlayerProgress()
    {
        PlayerPrefs.SetInt("highestScore", playerProgress.highestScore);
    }

    private void LoadGameData()
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, gameDataFile);


        //deserialisation
        if (File.Exists(filePath))
        {
            string dataAsJson = File.ReadAllText(filePath);
            GameData loadedData = JsonUtility.FromJson<GameData>(dataAsJson);

            allRoundData = loadedData.allRoundData;
        }
        else
        {
            Debug.LogError("Cannot load game data.");
        }
    }
}
