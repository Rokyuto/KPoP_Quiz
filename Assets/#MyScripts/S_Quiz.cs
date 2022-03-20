using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class S_Quiz : MonoBehaviour
{   
    //Quiz Canvas Objects
    public Canvas _QuizCanvas; // Create Canvas Object to Initialize Quiz Canvas from the Scene

    public Button QuitButton; // Initialize QuitButton
    public Button NextButton; // Initialize NextButton

    public Button[] AnsButtons; // Initialize array with AnsButtons
    public GameObject[] ClickMarks; // Initialize array with ClickMarks

    //Other Game Objects
    public S_CtryCanvas _CtryCanvas; // Initialize Category Canvas

    //Variables
    int Last_ClickedButtonIndex = -1; //Initialize Last Clicked Answer Button Index
    int ClickedButtons_Quantity; // Check Quantity of Clicked Answer Buttons 

    // Start is called before the first frame update
    void Start()
    {
        Func_VisualizeQuizPanel(false); //Visualize Quiz Panel

        Func_HideClickMark(); //Hide Click Marks

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

    // Hide Click Marks
    public void Func_HideClickMark()
    {
        foreach (GameObject clickMark in ClickMarks) // Get EACH Element(Click Mark) in the ClickMarks array
        {
            clickMark.gameObject.SetActive(false); // Hide CURRENT Click Mark
        }
    }

    //Quit Quiz Function
    public void Func_Quit()
    {
        Func_VisualizeQuizPanel(false); //Hide Quiz Canvas and Objects
        _CtryCanvas.Func_VisualizeCategoriesPanel(true); //Show Category Canvas and Objects
        Func_HideClickMark();
    }

    //On Answer Button Click
    public void Func_AnsButonClicked(Button clickedButton)
    {
        int Index_ClickedButton = System.Array.IndexOf(AnsButtons, clickedButton); // Get Clicked Anser Button Index from AnsButton array

        GameObject Mark_ClickedButton = ClickMarks[Index_ClickedButton]; // Get the parallel Click Mark (on the SAME INDEX)
        Mark_ClickedButton.SetActive(true); //Visualize the Click Mark

        ClickedButtons_Quantity++; //Increment Quantity of Clicked Answer Buttons

        if (ClickedButtons_Quantity >= 2) // If Clicked Answer Buttons is 2 at the same time
        {
            GameObject PreviousButtonMark = ClickMarks[Last_ClickedButtonIndex]; //Get Prevous Click Mark (on the Previous Clicked Answer Button)
            PreviousButtonMark.SetActive(false); //Hide the Previous Click Mark
            ClickedButtons_Quantity = 1; // Return the Quality of Clicked Buttons to 1
        }

        Last_ClickedButtonIndex = Index_ClickedButton; //Update the Index of Last Clicked Button

    }

}
