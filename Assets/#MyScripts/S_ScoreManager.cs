using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class S_ScoreManager : MonoBehaviour
{

    public Slider _PlayerSuccessRateSlider; // Player SuccessRate ProgressBar
    public Text _PlayerSuccessRateText; // Player SuccessRate ProgressBar Text

    public Slider _BestSuccessRateSlider; // Player SuccessRate ProgressBar
    public Text _BestSuccessRateText; // Player SuccessRate ProgressBar Text

    public float v_BestSuccessRate; // Player HighScore variable
    float v_SuccessRate;  // Success Rate
    float v_ProgresBar_Step = 0.0043f; // Step for Update ProgressBar
    public float v_InitialProgressBarCapacity = 0;

    // Start is called before the first frame update
    void Start()
    {
        v_BestSuccessRate = PlayerPrefs.GetFloat("BestSuccessRate", v_BestSuccessRate); // Get Highscore Data from PlayerPrefs and update v_BestSuccessRate
        //PlayerPrefs.DeleteAll();
        _PlayerSuccessRateSlider.value = v_InitialProgressBarCapacity;
        _BestSuccessRateSlider.value = 0;
    }

    //Function to Calculate the Player Success Rate in the Quiz
    public void Func_CalculateSuccessRate(int v_CorrectScore, int v_QuestionsQuantity)
    {
        v_SuccessRate = (float)v_CorrectScore / (float)v_QuestionsQuantity; // Calculate Success Rate in float [0.0 - 1.0] -> Correct Answers / Questions Quantity

        Update(); // Fill the Progress Bar

        float v_PlayerSuccessRate = v_SuccessRate * 100; // Calculate || Setup the Success Rate in range [0 - 100] %
        Func_UpadePlayerHighScore(v_PlayerSuccessRate); // Cal Function to Update Best Score
    }

    //Function to Update Player BestScore
    public void Func_UpadePlayerHighScore(float v_PlayerSuccessRate)
    {
        if (v_PlayerSuccessRate > v_BestSuccessRate) // Check if the Player Success Rate > BEST SCORE -> If true
        {
            v_BestSuccessRate = v_PlayerSuccessRate; // Update the Best Score value to the Current Player SuccessRate

            PlayerPrefs.SetFloat("BestSuccessRate", v_BestSuccessRate); // Update the Information || Data
            PlayerPrefs.Save(); // Save the new Information || Data
        }

        Debug.Log(v_BestSuccessRate);

        //var BestSuccessProgressBarText = "Best Success Rate: " + (v_BestSuccessRate).ToString("0.00") + " %"; // Update || Setup Best Success Progress Bar Text VARIABLE [0 - 100 ] %
        //_BestSuccessRateText.text = BestSuccessProgressBarText; // Update BestSuccess ProgressBar TEXT

    }

    // Animated ProgressBar Filling
    private void Update()
    {
        if(_PlayerSuccessRateSlider.value < v_SuccessRate) // Fill until the ProgresBar is EQUAL to the Success Rate
        {
            _PlayerSuccessRateSlider.value += v_ProgresBar_Step; // Update ProgressBar Capacity
            var ProgressBarText = "Score: " + (_PlayerSuccessRateSlider.value * 100).ToString("0.00") + " %"; // Update || Setup Progress Bar Text VARIABLE [0 - 100 ] %
            _PlayerSuccessRateText.text = ProgressBarText; // Update ProgressBar TEXT
        }

        if(_BestSuccessRateSlider.value <= v_BestSuccessRate / 100) // Fill until the ProgresBar is EQUAL to the Best Success Rate || BestSuccessRate / 100 because to make the range [ 0 - 1 ]
        {
            _BestSuccessRateSlider.value += v_ProgresBar_Step; // Update ProgressBar Capacity
            var BestSuccessProgressBarText = "Best Success Rate: " + (v_BestSuccessRate).ToString("0.00") + " %"; // Update || Setup Best Success Progress Bar Text VARIABLE [0 - 100 ] %
            _BestSuccessRateText.text = BestSuccessProgressBarText; // Update BestSuccess ProgressBar TEXT
        }

    }
}
