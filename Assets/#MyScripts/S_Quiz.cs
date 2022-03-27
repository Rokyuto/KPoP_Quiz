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

    public TextMeshProUGUI _QuizTaskTextMesh; // Quiz Task Text Mesh
    public TextMeshProUGUI _QuestionNumberTextMesh; // Question Number Text Mesh

    public TextMeshProUGUI _CorrectAnswersText; // Correct Answers Text Object
    public TextMeshProUGUI _WrongAnswersText; // Wrong Answers Text Object

    public TextMeshProUGUI[] _Arr_AnsButtonsText; // Array with Answer Buttons Texts
    public List<TextMeshProUGUI> _List_AnsButtonsText; // List with Answer Buttons Texts

    public Button QuitButton; // QuitButton
    public Button NextButton; // NextButton
    public Button NextQuizButton; //Next Quiz Button when the Quiz END

    public Button[] AnsButtons; // Array with AnsButtons
    public GameObject[] ClickMarks; // Array with ClickMarks

    public Image _QuestionPicture; // Question Picture Image
    public VideoPlayer _QuestionVideoPlayer; // Question Video Player
    public AudioSource _QuestionAudioSource; // Question Audio Source

    public Sprite[] _Arr_GuessGroupsImgs; // Array with Groups Images for "Guess the Group" Quiz
    public List<Sprite> _List_QuessImage; // List with Groups Images

    public AudioClip[] _Arr_GuessSongs; // Array with Songs Audios for "Guess the Song" Quiz
    public List<AudioClip> _List_QuessAudio; // List with Songs Audios

    //Other Game Objects
    public S_CtryCanvas _CtryCanvas; // Category Canvas


    //Variables
    public int v_QuizIndex; //Initilize which is the Quiz to Generate Question

    int v_Last_ClickedButtonIndex = -1; // Last Clicked Answer Button Index
    int v_ClickedButtons_Quantity; // Check Quantity of Clicked Answer Buttons 

    int v_QuestionNumber = 0; // Tracker Question Number
    int v_QuestionsQuantity = 30; // Questions Quantity in the Quiz

    int v_CorrectScore = 0; //Score for Correct Answers
    int v_WrongScore = 0; //Score for Wrong Answers

    int v_Index_QuestionAudio; // Question Audio Index
    string v_QuestionAudioName; // Question Audio Name (Correct Question Answer)

    int v_Index_QuestionImage; //Question Image Index
    string v_QuestionImageName; // Question Image Name (Correct Question Answer)

    Button LastClickedButton = null; // Tracker Last Clicked Button
    Button v_ClickedButton = null; //Tracker for Clicked Button when Next Button is Pressed to Check is the Answer Correct

    //Answers objects
    public List<string> List_Answers = new List<string>(); //List Quiz Answers

    //Quiz Answers Arrays
    public string[] Arr_GroupsNames; //Array with Groups Names
    public string[] Arr_SongsNames; //Array with Songs Names


    // Start is called before the first frame update
    void Start()
    {
        Func_VisualizeQuizPanel(false); //Visualize Quiz Panel
    }

    //Update Quiz Canvas Visibility
    public void Func_VisualizeQuizPanel(bool isActive)
    {
        if (isActive == true)
        {
            Func_UpdateQuestionNumber(); //Update Question Number
            Func_HideClickMark(); //Hide Click Marks

            _QuizCanvas.gameObject.SetActive(true); //Show Quiz Canvas and Objects     
            Func_VisualizeElements(isActive); // Show Quiz Elements
            NextQuizButton.gameObject.SetActive(false);
        }
        else
        {
            _QuizCanvas.gameObject.SetActive(false); //Hide Quiz Canvas and Objects
            Func_VisualizeElements(isActive); //Hide Quiz ELements
            v_QuestionNumber = 0;
        }
    }

    //Update Quiz Elements Visibility - MOST IMPORTANT WHEN THE QUIZ END
    void Func_VisualizeElements(bool isActive)
    {
        //Quiz Task Text
        _QuizTaskTextMesh.gameObject.SetActive(isActive);

        // Quiz Question Canvas Elenets
        _QuestionPicture.gameObject.SetActive(isActive);
        _QuestionVideoPlayer.gameObject.SetActive(isActive);
        _QuestionAudioSource.gameObject.SetActive(isActive);

        // Answer Buttons
        foreach (var Button in AnsButtons)
        {
            Button.gameObject.SetActive(isActive);
        }
        // Utility Buttons
        QuitButton.gameObject.SetActive(isActive);
        NextButton.gameObject.SetActive(isActive);

        //Hide Scores Texts
        _CorrectAnswersText.gameObject.SetActive(false);
        _WrongAnswersText.gameObject.SetActive(false);
    }

    //Update Question TextMeshPro Content(Text)
    public void Func_UpdateTaskText(string TaskText)
    {
        _QuizTaskTextMesh.text = TaskText; //Update Question Text to Loaded Quiz Category Type
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
        v_QuestionNumber++; //Increment Question Number

        if (v_QuestionNumber <= v_QuestionsQuantity - 1) // If the Question Numbers is LESS than 29 ( v_QuestionsQuantity(30) - 1 )
        {
            _QuestionNumberTextMesh.text = "Question " + v_QuestionNumber + " / " + v_QuestionsQuantity; // Update Question Number Text
            
            Func_HideClickMark(); // Reset Click Marks
            Func_UpdateLists(); // Update Quiz Lists

            switch (v_QuizIndex)
            {
                case 0:
                    _QuestionPicture.enabled = true; //ENABLE Question Picture Object 
                    _QuestionAudioSource.enabled = false; //DISABLE Question Audio Object 

                    List_Answers.AddRange(Arr_GroupsNames); // Add again the Groups Array to Answers List
                    _List_QuessImage.RemoveAt(v_Index_QuestionImage); // Remove the Question Image 

                    Func_GenQuestionImgContent(); // Generate Question Image
                    Func_GenButtonsAnswers(); // Generate Answers
                    break;

                case 1:
                    _QuestionPicture.enabled = false; //DISABLE Question Picture Object 
                    _QuestionAudioSource.enabled = true; //ENABLE Question Audio Object 

                    List_Answers.AddRange(Arr_SongsNames); // Add again the Groups Array to Answers List

                    Func_GenQuestionAudioContent(); // Generate Question Audio
                    Func_GenButtonsAnswers(); // Generate Answers
                    break;
            }

        }
        else // The Quiz Ends (Question Number = 30 (Questions Quantity)
        {
            _QuestionNumberTextMesh.text = "All Questions are answered";

            Func_VisualizeElements(false); // Hide Quiz Elements

            //Show Scores
            _CorrectAnswersText.gameObject.SetActive(true);
            _CorrectAnswersText.text = "Correct Answers: " + v_CorrectScore;
            _WrongAnswersText.gameObject.SetActive(true);
            _WrongAnswersText.text = "Wrong Answers: " + v_WrongScore;

            NextQuizButton.gameObject.SetActive(true); //Show Next Quiz Button

            // Reset the List with Guess Groups Images
            _List_QuessImage.Clear();

            List_Answers.Clear(); // Clear Answers List

        }
    }

    //Generate Random Question Image from _List_QuessImage List
    public void Func_GenQuestionImgContent()
    {
        v_Index_QuestionImage = Random.Range( 0, _List_QuessImage.Count ); // Generate Random INDEX for Question Picture
        _QuestionPicture.sprite = _List_QuessImage[v_Index_QuestionImage]; // Change Question Picture sprite to the Image located on the INDEX in Guess Group Array 
        v_QuestionImageName = _QuestionPicture.sprite.name; //GET the NAME of the IMAGE
    }

    //Generate Random Question Audio from _List_QuessAudio List
    public void Func_GenQuestionAudioContent()
    {
        v_Index_QuestionAudio = Random.Range(0, _List_QuessAudio.Count); // Generate Random INDEX for Question Picture
        _QuestionAudioSource.clip = _List_QuessAudio[v_Index_QuestionAudio]; // Change Question Picture sprite to the Image located on the INDEX in Guess Group Array 
        v_QuestionAudioName = _QuestionAudioSource.clip.name; //GET the NAME of the IMAGE
    }

    //Generate Question Answers and CHANGE the Buttons Text to them 
    public void Func_GenButtonsAnswers()
    {
        var v_CorrectButtonTextIndex = Random.Range(0, _List_AnsButtonsText.Count); // Generate Random Index of the Correct Buttons Text List which will contains the Correct Answer
        if(v_QuizIndex == 0)
        {
            _List_AnsButtonsText[v_CorrectButtonTextIndex].text = v_QuestionImageName; // Update his Text to the QUESTION IMAGE NAME - the CORRECT ANSWER
        }
        else if(v_QuizIndex == 1)
        {
            _List_AnsButtonsText[v_CorrectButtonTextIndex].text = v_QuestionAudioName; // Update his Text to the QUESTION IMAGE NAME - the CORRECT ANSWER
        }

        _List_AnsButtonsText.RemoveAt(v_CorrectButtonTextIndex); // REMOVE the INDEX of the CORRECT BUTTON TEXT
        List_Answers.Remove(v_QuestionImageName); // REMOVE the CORRECT ANSWER (STRING) from Answers List

        foreach(var ButtonsText in _List_AnsButtonsText)
        { 
            var v_WrongButtonsAnswerIndex = Random.Range(0, List_Answers.Count); // Generate Random Wrong Answer from List Answers
            ButtonsText.text = List_Answers[v_WrongButtonsAnswerIndex]; // Display the Wrong Answer to Current Button Text in the List

            List_Answers.Remove(List_Answers[v_WrongButtonsAnswerIndex]); // REMOVE the CORRECT ANSWER (STRING) from Answers List
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

        _List_AnsButtonsText.Clear(); // Clear ALL List Buttons Text
        _List_AnsButtonsText.AddRange(_Arr_AnsButtonsText); // Add again the ButtonsText Array to Buttons Text List

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
        }
    }

    //Quit Quiz Function
    public void Func_Quit()
    {
        Func_HideClickMark(); // Call the Function to Hide Click Marks

        // Call Function to Update Quiz Question Numbers
        v_QuestionNumber = 0;

        Func_VisualizeQuizPanel(false); // Hide Quiz Canvas and Objects
        _CtryCanvas.Func_VisualizeCategoriesPanel(true); // Show Category Canvas and Objects

        //Reset the List with GuessGroup Images 
        _List_QuessImage.Clear();

        List_Answers.Clear(); //Clear Answers List
        _List_AnsButtonsText.Clear(); // Clear Answer Buttons Text List
        _List_AnsButtonsText.AddRange(_Arr_AnsButtonsText); // Fill Again Answer Buttons Text List
    }

    public void Func_NextQuiz()
    {
        v_QuestionNumber = 0; // Reset to 0 Question Number
        Func_VisualizeQuizPanel(false); // Hide Quiz Canvas and Objects
        _CtryCanvas.Func_VisualizeCategoriesPanel(true); // Show Category Canvas and Objects

        //Reset Score
        v_CorrectScore = 0;
        v_WrongScore = 0;
    }

}
