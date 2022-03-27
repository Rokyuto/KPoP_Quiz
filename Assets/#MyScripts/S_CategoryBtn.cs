using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class S_CategoryBtn : MonoBehaviour
{
    public Button _CurrentButton; //Initialize Category Buttons from Game Scene
    public S_CtryCanvas _CategoriesMenu; //Initialize Category Canvas from Game Scene and S_CtryCanvas
    public S_StartupCanvas _StartupCanvas; // Initialize Startup Canvas from Game Scene and S_Startup Script 
    public S_Quiz _QuizCanvas; // Initialize Quiz Canvas from Game Scene and S_Quiz Script

    public string QuizTask = ""; //The Name of the Quiz

    //Load Quiz || Back to Startup
    public void Func_SetupQuiz()
    {
        int _ClickedButtonIndex = System.Array.IndexOf(_CategoriesMenu._CategoriesButtons, _CurrentButton); //Get Clicked Button Index from the CategoriesButtons Array

        //Track which button is Clicked || Set Question Text
        switch ( _ClickedButtonIndex )
        {
            case 0:
                // If Quiz Task is "Guess the Group" :
                QuizTask = "Guess the Group";
                _QuizCanvas.List_Answers.AddRange(_QuizCanvas.Arr_GroupsNames); // Insert in the List Answers Guess Group Array
                _QuizCanvas._List_QuessImage.AddRange(_QuizCanvas._Arr_GuessGroupsImgs);
                _QuizCanvas.v_QuizIndex = 0;
                break;
            case 1:
                QuizTask = "Guess the Song";
                _QuizCanvas.List_Answers.AddRange(_QuizCanvas.Arr_SongsNames); // Insert in the List Answers Guess Group Array
                _QuizCanvas._List_QuessAudio.AddRange(_QuizCanvas._Arr_GuessSongs);
                _QuizCanvas.v_QuizIndex = 1;
                break;
            case 2:
                QuizTask = "Guess the Idol";
                break;
            case 3:
                QuizTask = "Guess the Idol who Sing";
                break;
            case 4:
                QuizTask = "Guess song part performer";
                break;
            case 5:
                QuizTask = "Guess which song the dance is from";
                break;
            case 6:
                QuizTask = "Guess Idol Nationality";
                break;

        }

        _QuizCanvas.Func_UpdateTaskText(QuizTask); //Update Question Text Content

        //Load Next Canvas on Button Clicked
        if (_ClickedButtonIndex < _CategoriesMenu._CategoriesButtons.Length - 1) //If Clicked Button is EVERYONE EXCEPT BACK BUTTON
        {
            LoadQuizCanvas(); //Load Quiz Canvas
        }
        else //If Clicked Button is BACK BUTTON
        {
            Func_Back(); //Go one step back - to Startup Canvas
        }
    }

    //Load Quiz Canvas
    void LoadQuizCanvas()
    {
        _CategoriesMenu.Func_VisualizeCategoriesPanel(false); //Hide Category Canvas
        _QuizCanvas.Func_VisualizeQuizPanel(true); //Show Quiz Canvas
    }

    //Go Back to Category Canvas
    void Func_Back()
    {
        _CategoriesMenu.Func_VisualizeCategoriesPanel(false); //Hide Category Canvas
        _StartupCanvas.Func_ShowStartupCanvas(); //Show Startup Canvas
    }
}

//Debug.Log(); //DEBUG :: Print