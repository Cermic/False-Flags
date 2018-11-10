using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;


public class DataController : MonoBehaviour {

    private Randomiser randomiser;
    private RoundData[] allRoundData;
    private FlagData[] flagData;
    private PlayerProgress playerProgress;
    private string gameDataFile = "data.json";  //Initialised here but if game develops more modes this would need to be set elsewhere
    private string flagDataFile = "flag.json";

	// Use this for initialization
	void Start () {
        DontDestroyOnLoad(gameObject);
        randomiser = GetComponent<Randomiser>();
        LoadGameData();
        LoadPlayerProgress();
        SceneManager.LoadScene("MenuScreen");
	}

    public RoundData GetCurrentRoundData() //public RoundData GetCurrentRoundData(int index)
    {
        randomiser.RandomGeneration(allRoundData, flagData);
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
        string filePath1 = Path.Combine(Application.streamingAssetsPath, gameDataFile);
        string filePath2 = Path.Combine(Application.streamingAssetsPath, flagDataFile);
                //deserialisation
        if (File.Exists(filePath1))
        {
            string dataAsJson = File.ReadAllText(filePath1);
            GameData loadedData1 = JsonUtility.FromJson<GameData>(dataAsJson);

            allRoundData = loadedData1.allRoundData;
        }
        else
        {
            Debug.LogError("Cannot load game data.");
        }
        if (File.Exists(filePath2))
        {
            string dataAsJson = File.ReadAllText(filePath2);
            GameData loadedData2 = JsonUtility.FromJson<GameData>(dataAsJson);

            flagData = loadedData2.flagData;
        }
        else
        {
            Debug.LogError("Cannot load game data.");
        }
    }
}
