using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class S_CategoryBtn : MonoBehaviour
{
    public Button _CurrentButton; //Initialize Category Buttons from Game Scene
    public S_CtryCanvas _CategoriesMenu; //Initialize Category Canvas from Game Scene and S_CtryCanvas
    public S_StartupCanvas _StartupCanvas; // Initialize Startup Canvas from Game Scene and S_Startup Script 
    public S_Quiz _QuizCanvas; // Initialize Quiz Canvas from Game Scene and S_Quiz Script

    // Start is called before the first frame update
    void Start()
    {
        Func_OnClickCategoryButton();
    }

    //On ANY Category Button Click
    void Func_OnClickCategoryButton()
    {
        _CurrentButton.onClick.AddListener(Func_SetupQuiz);
    }

    //Load Quiz || Back to Startup
    void Func_SetupQuiz()
    {
        int _ClickedButtonIndex = System.Array.IndexOf(_CategoriesMenu._CategoriesButtons, _CurrentButton); //Get Clicked Button Index from the CategoriesButtons Array
        string QuizQuestion = ""; //The Name of the Quiz

        //Track which button is Clicked || Set Question Text
        switch ( _ClickedButtonIndex )
        {
            case 0:
                QuizQuestion = "Guess the Group";
                _QuizCanvas.Func_VisualizeQuestionElements(true,false,false);
                break;
            case 1:
                QuizQuestion = "Guess the Song";
                _QuizCanvas.Func_VisualizeQuestionElements(false, false, true);
                break;
            case 2:
                QuizQuestion = "Guess the Idol";
                _QuizCanvas.Func_VisualizeQuestionElements(true, false, false);
                break;
            case 3:
                QuizQuestion = "Guess the Idol who Sing";
                _QuizCanvas.Func_VisualizeQuestionElements(false, false, true);
                break;
            case 4:
                QuizQuestion = "Guess song part performer";
                _QuizCanvas.Func_VisualizeQuestionElements(false, false, true);
                break;
            case 5:
                QuizQuestion = "Guess which song the dance is from";
                _QuizCanvas.Func_VisualizeQuestionElements(false, true, false);
                break;
            case 6:
                QuizQuestion = "Guess Idol Nationality";
                _QuizCanvas.Func_VisualizeQuestionElements(true, false, false);
                break;

        }

        _QuizCanvas.Func_UpdateQuestionText(QuizQuestion); //Update Question Text Content

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