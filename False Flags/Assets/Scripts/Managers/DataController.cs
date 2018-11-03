﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class DataController : MonoBehaviour {

    public RoundData[] allRoundData;

	// Use this for initialization
	void Start () {
        DontDestroyOnLoad(gameObject);

        SceneManager.LoadScene("MenuScreen");
	}

    public RoundData GetCurrentRoundData()
    {
        return allRoundData[0];
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
