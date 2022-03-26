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

    public TextMeshProUGUI _QuestionTextMesh; // Question Text Mesh
    public TextMeshProUGUI _QuestionNumberTextMesh; // Question Number Text Mesh

    public TextMeshProUGUI[] _Arr_AnsButtonsText; // Array with Answer Buttons Texts
    public List<TextMeshProUGUI> _List_AnsButtonsText; // List with Answer Buttons Texts

    public Button QuitButton; // QuitButton
    public Button NextButton; // NextButton

    public Button[] AnsButtons; // Array with AnsButtons
    public GameObject[] ClickMarks; // Array with ClickMarks

    public Image _QuestionPicture; // Question Picture Image
    public VideoPlayer _QuestionVideoPlayer; // Question Video Player
    public AudioSource _QuestionAudioSource; // Question Audio Source

    public Sprite[] _Arr_GuessGroupsImgs; // Array with Groups Images for "Guess the Group" Quiz
    public List<Sprite> _List_GuessGroupsImgs; // List with Groups Images for "Guess the Group" Quiz

    //Other Game Objects
    public S_CtryCanvas _CtryCanvas; // Category Canvas

    //Variables
    int v_Last_ClickedButtonIndex = -1; // Last Clicked Answer Button Index
    int v_ClickedButtons_Quantity; // Check Quantity of Clicked Answer Buttons 
    int v_QuestionNumber = 0; // Tracker Question Number
    int v_QuestionsQuantity = 20; // Questions Quantity in the Quiz
    int v_CorrectScore = 0; //Score for Correct Answers
    int v_WrongScore = 0; //Score for Wrong Answers

    Button LastClickedButton = null; // Tracker Last Clicked Button
    Button v_ClickedButton = null; //Tracker for Clicked Button when Next Button is Pressed to Check is the Answer Correct

    string v_QuestionImageName; // Question Image Name (Correct Question Answer)
    int v_Index_QuestionPicture;

    //Answers objects
    public List<string> List_Answers = new List<string>(); //List Quiz Answers

    //Quiz Answers Arrays
    public string[] Arr_GroupsNames = { "BLACKPINK", "TWICE", "ITZY" , "DREAMCATCHER"}; //Array Guess Group

    // Start is called before the first frame update
    void Start()
    {
        Func_VisualizeQuizPanel(false); //Visualize Quiz Panel
        Func_UpdateQuestionNumber(); //Update Question Number
        Func_HideClickMark(); //Hide Click Marks
    }

    //Update Quiz Canvas Visibility
    public void Func_VisualizeQuizPanel(bool isActive)
    {
        if(isActive == true)
        {
            _QuizCanvas.gameObject.SetActive(true); //Show Quiz Canvas and Objects     
            Func_GenQuestionImage(); //Generate Random Question Image
            Func_GenButtonsAnswers(); //Generate Buttons Answers Texts
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

    //Generate Random Question Image from _Arr_GuessGroupsImgs array
    public void Func_GenQuestionImage()
    {
        v_Index_QuestionPicture = Random.Range( 0, _List_GuessGroupsImgs.Count ); // Generate Random INDEX for Question Picture
        _QuestionPicture.sprite = _List_GuessGroupsImgs[v_Index_QuestionPicture]; // Change Question Picture sprite to the Image located on the INDEX in Guess Group Array 
        v_QuestionImageName = _QuestionPicture.sprite.name; //GET the NAME of the IMAGE
    }

    //Generate Question Answers and CHANGE the Buttons Text to them 
    public void Func_GenButtonsAnswers()
    {
        var v_CorrectButtonTextIndex = Random.Range(0, _List_AnsButtonsText.Count); // Generate Random Index of the Correct Buttons Text List which will contains the Correct Answer
        _List_AnsButtonsText[v_CorrectButtonTextIndex].text = v_QuestionImageName; // Update his Text to the QUESTION IMAGE NAME - the CORRECT ANSWER

        _List_AnsButtonsText.RemoveAt(v_CorrectButtonTextIndex); // REMOVE the INDEX of the CORRECT BUTTON TEXT
        List_Answers.Remove(v_QuestionImageName); // REMOVE the CORRECT ANSWER (STRING) from Answers List

        foreach(var ButtonText in _List_AnsButtonsText)
        { 
            var v_WrongButtonsTextIndex = Random.Range(0, _List_AnsButtonsText.Count);
            ButtonText.text = List_Answers[v_WrongButtonsTextIndex];

            List_Answers.Remove(List_Answers[v_WrongButtonsTextIndex]); // REMOVE the CORRECT ANSWER (STRING) from Answers List
        }

    }

    //On Answer Button Click
    public void Func_AnsButonClicked(Button clickedButton)
    {
        int Index_ClickedButton = System.Array.IndexOf(AnsButtons, clickedButton); // Get Clicked Answer Button Index from AnsButton array

        if (LastClickedButton != clickedButton) //Check if the NEW Clicked Button is the SAME -> if NOT -> CONTINUE
        {
            ClickMarks[Index_ClickedButton].SetActive(true); //Visualize the parallel Click Mark (on the SAME INDEX of the Buttons Array)

            v_ClickedButtons_Quantity++; //Increment Quantity of Clicked Answer Buttons

            if (v_ClickedButtons_Quantity >= 2) // If Clicked Answer Buttons is 2 at the same time
            {
                GameObject PreviousButtonMark = ClickMarks[v_Last_ClickedButtonIndex]; //Get Prevous Click Mark (on the Previous Clicked Answer Button)
                PreviousButtonMark.SetActive(false); //Hide the Previous Click Mark
                v_ClickedButtons_Quantity = 1; // Return the Quality of Clicked Buttons to 1
            }

            v_Last_ClickedButtonIndex = Index_ClickedButton; //Update the Index of Last Clicked Button
            LastClickedButton = clickedButton; //Update Last Clicked Button to the CURRENT
            v_ClickedButton = clickedButton; // Update the Tracker for Clicked Button when Next Button is Pressed to Check is the Answer Correct

        }

    }

    //Check if the TEXT of the Marked Button is CORRECT
    public void Func_CheckAnswer(Button v_ClickedButton)
    {
        var v_ClickedButtonText = v_ClickedButton.GetComponentInChildren<TextMeshProUGUI>(); // Get the Text of the Marked Button 

        if (v_ClickedButtonText.text == v_QuestionImageName) // Check is the Text of the Marked Button is EQUAL to the NAME of the QUESTION IMAGE
        {
            v_CorrectScore++; //Increment Score for the Correct Answers
        }
        else
        {
            v_WrongScore++; //Increment Score for the Wrong Answers
        }
    }

    //Update Answers and AnswerButtonsText Lists
    void Func_UpdateLists()
    {
        List_Answers.Clear(); // Clear ALL List Answers
        List_Answers.AddRange(Arr_GroupsNames); // Add again the Groups Array to Answers List

        _List_AnsButtonsText.Clear(); // Clear ALL List Buttons Text
        _List_AnsButtonsText.AddRange(_Arr_AnsButtonsText); // Add again the ButtonsText Array to Buttons Text List

        _List_GuessGroupsImgs.RemoveAt(v_Index_QuestionPicture); // Remove the Question Image 
    }

    //Next Question Function
    public void Func_Next()
    {
        if(v_ClickedButtons_Quantity == 1) // If it have 1 Clicked Answer Button
        {
            v_ClickedButtons_Quantity = 0; // Reset to NONE Clicked Answer Button
            LastClickedButton = null; // Reset to NONE the Last Clicked Button

            //Score Functionality
            Func_CheckAnswer(v_ClickedButton); //Check is the Player Answer Correct

            Func_UpdateQuestionNumber(); // Update Questions Quantity and Text
            Func_HideClickMark(); // Reset Click Marks
            Func_UpdateLists(); // Update Quiz Lists
            Func_GenQuestionImage(); // Generate New Question Image
            Func_GenButtonsAnswers(); // Generate New Answers

        }
    }

    //Quit Quiz Function
    public void Func_Quit()
    {
        Func_HideClickMark(); // Call the Function to Hide Click Marks

        // Call Function to Update Quiz Question Numbers
        v_QuestionNumber = 0;
        Func_UpdateQuestionNumber();

        Func_VisualizeQuizPanel(false); // Hide Quiz Canvas and Objects
        _CtryCanvas.Func_VisualizeCategoriesPanel(true); // Show Category Canvas and Objects

        //Reset the List with GuessGroup Images 
        _List_GuessGroupsImgs.Clear();
        _List_GuessGroupsImgs.AddRange(_Arr_GuessGroupsImgs);
    }

}
