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
        int _ClickedButtonIndex = System.Array.IndexOf(_CategoriesMenu._CategoriesButtons, _CurrentButton);

        switch( _ClickedButtonIndex )
        {
            case 0:
                break;
        }

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

    void LoadQuizCanvas()
    {
        _CategoriesMenu.Func_VisualizeCategoriesPanel(false); //Hide Category Canvas
        _QuizCanvas.Func_VisualizeQuizPanel(true); //Show Quiz Canvas
    }

    void Func_Back()
    {
        _CategoriesMenu.Func_VisualizeCategoriesPanel(false); //Hide Category Canvas
        _StartupCanvas.Func_ShowStartupCanvas(); //Show Startup Canvas
    }
}

//Debug.Log(); //DEBUG :: Print