using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class S_Quiz : MonoBehaviour
{
    //Quiz Canvas Objects
    public Canvas _QuizCanvas; // Create Canvas Object to Initialize Quiz Canvas from the Scene
    public Button[] AnsButtons;
    public Button QuitButton;
    public Button NextButton;

    //Other Game Objects
    public S_CtryCanvas _CtryCanvas;

    // Start is called before the first frame update
    void Start()
    {
        Func_VisualizeQuizPanel(false);
    }

    //Update Quiz Canvas Visibility
    public void Func_VisualizeQuizPanel(bool isActive)
    {
        if(isActive == true)
        {
            _QuizCanvas.gameObject.SetActive(true); //Show Quiz Canvas and Objects
        }
        else
        {
            _QuizCanvas.gameObject.SetActive(false); //Hide Quiz Canvas and Objects
        }
    }

    //Quit Quiz Function
    public void Func_Quit()
    {
        Func_VisualizeQuizPanel(false); //Hide Quiz Canvas and Objects
        _CtryCanvas.Func_VisualizeCategoriesPanel(true); //Show Category Canvas and Objects
    }

}
