using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Video;

public class S_Quiz : MonoBehaviour
{   
    //Quiz Canvas Objects
    public Canvas _QuizCanvas; // Create Canvas Object to Initialize Quiz Canvas from the Scene

    public TextMeshProUGUI _QuestionTextMesh; // Initialize Question Text Mesh
    public TextMeshProUGUI _QuestionNumberTextMesh; // Initialize Question Number Text Mesh

    public Image _QuestionPicture; //Initialize Question Picture Image
    public VideoPlayer _QuestionVideoPlayer; //Initialize Question Video Player
    public AudioSource _QuestionAudioSource; //Initialize Question Audio Source

    public Button QuitButton; // Initialize QuitButton
    public Button NextButton; // Initialize NextButton

    public Button[] AnsButtons; // Initialize array with AnsButtons
    public GameObject[] ClickMarks; // Initialize array with ClickMarks

    //Other Game Objects
    public S_CtryCanvas _CtryCanvas; // Initialize Category Canvas

    //Variables
    int v_Last_ClickedButtonIndex = -1; //Initialize Last Clicked Answer Button Index
    int v_ClickedButtons_Quantity; // Check Quantity of Clicked Answer Buttons 
    int v_QuestionNumber = 0; // Track Question Number
    int v_QuestionsQuantity = 20; // Initialize Questions Quantity in the Quiz

    // Start is called before the first frame update
    void Start()
    {
        Func_VisualizeQuizPanel(false); //Visualize Quiz Panel
        Func_UpdateQuestionNumber();
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

    //Update Question TextMeshPro Content(Text)
    public void Func_UpdateQuestionText(string QuestionText)
    {
        _QuestionTextMesh.text = QuestionText; //Update Question Text to Loaded Quiz Category Type
    }

    // Hide Click Marks
    public void Func_HideClickMark()
    {
        foreach (GameObject clickMark in ClickMarks) // Get EACH Element(Click Mark) in the ClickMarks array
        {
            clickMark.gameObject.SetActive(false); // Hide CURRENT Click Mark
        }
    }

    //Next Question || On Click Next Button
    public void Func_UpdateQuestionNumber()
    {
        if (v_QuestionNumber < v_QuestionsQuantity)
        {
            v_QuestionNumber++;
            _QuestionNumberTextMesh.text = "Question " + v_QuestionNumber + " / " + v_QuestionsQuantity;
        }
        else
        {
            _QuestionNumberTextMesh.text = "All Questions are answered";
        }
    }

    //Update Visibility of Question Elements
    public void Func_VisualizeQuestionElements(bool isPictureVisible, bool isVideoVisible, bool isAudioVisible)
    {
        _QuestionPicture.gameObject.SetActive(isPictureVisible);
        _QuestionVideoPlayer.gameObject.SetActive(isVideoVisible);
        _QuestionAudioSource.gameObject.SetActive(isAudioVisible);
    }

    //On Answer Button Click
    public void Func_AnsButonClicked(Button clickedButton)
    {
        int Index_ClickedButton = System.Array.IndexOf(AnsButtons, clickedButton); // Get Clicked Anser Button Index from AnsButton array

        GameObject Mark_ClickedButton = ClickMarks[Index_ClickedButton]; // Get the parallel Click Mark (on the SAME INDEX)
        Mark_ClickedButton.SetActive(true); //Visualize the Click Mark

        v_ClickedButtons_Quantity++; //Increment Quantity of Clicked Answer Buttons

        if (v_ClickedButtons_Quantity >= 2) // If Clicked Answer Buttons is 2 at the same time
        {
            GameObject PreviousButtonMark = ClickMarks[v_Last_ClickedButtonIndex]; //Get Prevous Click Mark (on the Previous Clicked Answer Button)
            PreviousButtonMark.SetActive(false); //Hide the Previous Click Mark
            v_ClickedButtons_Quantity = 1; // Return the Quality of Clicked Buttons to 1
        }

        v_Last_ClickedButtonIndex = Index_ClickedButton; //Update the Index of Last Clicked Button
    
    }

    //Quit Quiz Function
    public void Func_Quit()
    {
        Func_VisualizeQuizPanel(false); //Hide Quiz Canvas and Objects
        _CtryCanvas.Func_VisualizeCategoriesPanel(true); //Show Category Canvas and Objects
        Func_HideClickMark();
    }

}
