using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_ScoreManager : MonoBehaviour
{

    public int v_BestScore; // Player HighScore variable

    // Start is called before the first frame update
    void Start()
    {
        v_BestScore = PlayerPrefs.GetInt("HighScore", v_BestScore); // Get Highscore Data from PlayerPrefs and update v_BestScore
    }

    //Function to Update Player BestScore
    public void Func_UpadePlayerHighScore(int v_PlayerScore)
    {
        if (v_PlayerScore > v_BestScore) // Check if the Player Score > BEST SCORE -> If true
        {
            v_BestScore = v_PlayerScore; // Update BestScore variable
            PlayerPrefs.SetInt("HighScore", v_BestScore); // Update the Information || Data
            PlayerPrefs.Save(); // Save the new Information || Data
        }
    }
}
