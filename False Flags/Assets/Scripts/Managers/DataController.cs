using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;


public class DataController : MonoBehaviour {

    private Randomiser randomiser;
    private RoundData[] allRoundData;
    private FlagData[] flagData;
    private TransitionFactData[] transitionFactData;
    private PlayerProgress playerProgress;
    private string gameDataFile = "data.json";  //Initialised here but if game develops more modes this would need to be set elsewhere
    private string flagDataFile = "flag.json";
    private string transitionFactDataFile = "transitionFacts.json";

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
    private int RandomNumber(int num)
    {
        int value = (int)(Random.Range(0.0f, (float)num));
        return value;
    }
    public string GetInfoData() //public RoundData GetCurrentRoundData(int index)/////////////////////////////////////////////////////////
    {
        return transitionFactData[RandomNumber(transitionFactData.Length)].fact;                     //
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
        string filePath1;
        string filePath2;
        string filePath3;
        #if UNITY_EDITOR
        
            filePath1 = Path.Combine(Application.streamingAssetsPath, gameDataFile);
            filePath2 = Path.Combine(Application.streamingAssetsPath, flagDataFile);
            filePath3 = Path.Combine(Application.streamingAssetsPath, transitionFactDataFile);

        #elif UNITY_ANDROID
            filePath1 = Path.Combine("jar:file://" + Application.dataPath + "!/streamingAssets/",  gameDataFile);
            filePath2 = Path.Combine("jar:file://" + Application.dataPath + "!/streamingAssets/",  flagDataFile);
            filePath3 = Path.Combine("jar:file://" + Application.dataPath + "!/streamingAssets/",  transitionFactDataFile);
        #elif UNITY_IOS
            filePath1 = Path.Combine(Application.dataPath + "/Raw", gameDataFile);
            filePath2 = Path.Combine(Application.dataPath + "/Raw", flagDataFile);
            filePath3 = Path.Combine(Application.dataPath + "/Raw", transitionFactDataFile);
        #endif


        //deserialisation
        if (File.Exists(filePath1))
        {
            string dataAsJson;
            #if UNITY_EDITOR || UNITY_IOS
                dataAsJson = File.ReadAllText(filePath1);

            #elif UNITY_ANDROID
                dataAsJson = File.ReadAllText(filePath1);
            #endif
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
        if (File.Exists(filePath3))
        {
            string dataAsJson = File.ReadAllText(filePath3);
            GameData loadedData3 = JsonUtility.FromJson<GameData>(dataAsJson);

            transitionFactData = loadedData3.transitionFactData;
        }
        else
        {
            Debug.LogError("Cannot load transition fact data.");
        }
    }
}
