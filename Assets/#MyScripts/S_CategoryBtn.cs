using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class S_CategoryBtn : MonoBehaviour
{
    public Button _CurrentButton; //Initialize Category Buttons from Game Scene
    public S_CtryCanvas _CategoriesMenu; //Initialize Category Canvas from Game Scene and S_CtryCanvas
    public S_Quiz _QuizCanvas; // Initialize Quiz Canvas from Game Scene and S_Quiz Script
    public S_StartupCanvas _StartupCanvas; // Initialize Startup Canvas from Game Scene and S_Startup Script 

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
        Debug.Log("Clicked Button: " + _CurrentButton); //DEBUG :: Print Clicked Button

        int ClickedButtonIndex = System.Array.IndexOf(_CategoriesMenu._CategoriesButtons, _CurrentButton);

        Debug.Log("Clicked Button Index: " + ClickedButtonIndex); //DEBUG :: Print Clicked Button Index

        switch (ClickedButtonIndex)
        {
            //Load Guess the Group Quiz
            case 0:
                Debug.Log("Loaded Quiz: Guess the Group"); //DEBUG :: Print Clicked Button Index
                _CategoriesMenu.Func_HideCanvas(); //Hide Category Canvas
                _QuizCanvas.Func_ShowQuizPanel(); //Show Quiz Canvas
                break;

            case 7:
                Debug.Log("Loaded Startup"); //DEBUG :: Print Clicked Button Index
                Func_Back();
                break;
        }
    }

    void Func_Back()
    {
        _CategoriesMenu.Func_HideCanvas(); //Hide Category Canvas
        _StartupCanvas.Func_ShowStartupCanvas(); //Show Startup Canvas
    }
}
