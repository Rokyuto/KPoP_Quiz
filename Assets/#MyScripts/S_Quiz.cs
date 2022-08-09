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
    public Canvas _QuestionCanvas; // Question Canvas

    public TextMeshProUGUI _QuizTaskTextMesh; // Quiz Task Text Mesh
    public TextMeshProUGUI _QuestionNumberTextMesh; // Question Number Text Mesh

    public TextMeshProUGUI _CorrectAnswersText; // Correct Answers Text Object
    public TextMeshProUGUI _WrongAnswersText; // Wrong Answers Text Object

    public TextMeshProUGUI[] _Arr_AnsButtonsText; // Array with Answer Buttons Texts
    public List<TextMeshProUGUI> _List_AnsButtonsText; // List with Answer Buttons Texts

    // Utility Buttons
    public Button QuitButton; // QuitButton
    public Button NextButton; // NextButton
    public Button NextQuizButton; //Next Quiz Button when the Quiz END
    public Button SpecialButton; // Special Button about Question Content
    public Button ExitZoomModeButton; // Button to Exit View Image Mode

    public Button[] AnsButtons; // Array with AnsButtons
    public GameObject[] ClickMarks; // Array with ClickMarks

    public Image _QuestionPicture; // Question Picture Image
    public VideoPlayer _QuestionVideoPlayer; // Question Video Player
    public AudioSource _QuestionAudioSource; // Question Audio Source

    public GameObject TransparentBGD;

    // Question Lists and Arrays

    // Lists
    public List<Sprite> _List_GuessImage; // List with Images
    public List<AudioClip> _List_GuessAudio; // List with Audios
    public List<VideoClip> _List_GuessVideo; // List with Videos

    // Arrays
    public Sprite[] _Arr_GuessGroupsImgs; // Array with Groups Images for "Guess the Group" Quiz
    public Sprite[] _Arr_GuessIdolsImgs; // Array with Idols Images
    public AudioClip[] _Arr_GuessSongs; // Array with Songs Audios for "Guess the Song" Quiz
    public AudioClip[] _Arr_GuessPerformer; // Array with Song Performer for "Guess the Idol who Sing" Quiz
    public VideoClip[] _Arr_GuessSongDance; // Array with Dances from Songs

    // TODO:
    public AudioClip[] _Arr_GuessSongAlbum; // Array with Songs from Albums
    public Sprite[] _Arr_GuessIdolNationality; // Array with Idols to Guess Their Nationality

    //Answers List
    public List<string> List_Answers = new List<string>(); //List Quiz Answers

    //Quiz Answers Arrays
    public string[] Arr_GroupsNames; // Array with Question Groups Names
    public string[] Arr_SongsNames; // Array with Question Songs Names
    public string[] Arr_IdolsNames; // Array with Question Idols Names
    public string[] Arr_PerformersNames; // Array with Question Song Performers
    public string[] Arr_SongAlbums; // Array with Question Songs Albums
    public string[] Arr_SongDances; // Array with Question Songs Dances
    public string[] Arr_IdolNationality; // Array with Question Idols Nationality

    //Other Objects
    public S_CtryCanvas _CtryCanvas; // Category Canvas
    S_ScoreManager _ScoreManager; // Create Score Manager Object

    //Variables
    public int v_QuizIndex; //Initilize which is the Quiz to Generate Question

    int v_Last_ClickedButtonIndex = -1; // Last Clicked Answer Button Index
    int v_ClickedButtons_Quantity; // Check Quantity of Clicked Answer Buttons 

    int v_QuestionNumber = 0; // Tracker Question Number
    public int v_QuestionsQuantity; // Questions Quantity in the Quiz

    public int v_CorrectScore = 0; //Score for Correct Answers
    public int v_WrongScore = 0; //Score for Wrong Answers

    int v_Index_QuestionImage; //Question Image Index
    string v_QuestionImageName; // Question Image Name (Correct Question Answer)

    int v_Index_QuestionAudio; // Question Audio Index
    string v_QuestionAudioName; // Question Audio Name (Correct Question Answer)

    int v_Index_QuestionVideo; // Question Video Index
    string v_QuestionVideoName; // Question Video Name (Correct Question Answer)

    string v_CorrectAnswer; // Correct Answer Variable

    Button LastClickedButton = null; // Tracker Last Clicked Button
    Button v_ClickedButton = null; //Tracker for Clicked Button when Next Button is Pressed to Check is the Answer Correct

    int v_CorrectAnswerIndex; // Correct Answer Index in the Answer List
    public int v_GenderBorder = 23; // Border between FEMALE and MALE Images

    public bool v_isZoom = false;
    public int v_CategoryIndex;

    // Start is called before the first frame update
    void Start()
    {
        TransparentBGD.gameObject.SetActive(false);
        ExitZoomModeButton.gameObject.SetActive(false);

        _ScoreManager = GameObject.FindGameObjectWithTag("Manager").GetComponent<S_ScoreManager>(); // Initialize the S_ScoreManager Script from the BestScoreText (this Object has "Manager" tag )
        //_ScoreManager = new S_ScoreManager();
        Func_VisualizeQuizPanel(false); //Visualize Quiz Panel

    }

    //Update Quiz Canvas Visibility
    public void Func_VisualizeQuizPanel(bool isActive)
    {
        if (isActive == true)
        {
            _QuizCanvas.gameObject.SetActive(true); //Show Quiz Canvas and Objects     
            Func_VisualizeElements(isActive); // Show Quiz Elements

            Func_UpdateQuestionNumber(); //Update Question Number
            Func_HideClickMark(); //Hide Click Marks

            NextQuizButton.gameObject.SetActive(false);
        }
        else
        {
            _QuizCanvas.gameObject.SetActive(false); //Hide Quiz Canvas and Objects
            Func_VisualizeElements(isActive); //Hide Quiz ELements
            v_QuestionNumber = 0; // Reset Question Number
        }
    }

    //Update Quiz Elements Visibility - MOST IMPORTANT WHEN THE QUIZ END
    void Func_VisualizeElements(bool isActive)
    {
        //Quiz Task Text
        _QuizTaskTextMesh.gameObject.SetActive(isActive);

        // Quiz Question Canvas Elenets
        _QuestionCanvas.gameObject.SetActive(isActive);

        // Answer Buttons
        foreach (var Button in AnsButtons)
        {
            Button.gameObject.SetActive(isActive);
        }
        // Utility Buttons
        QuitButton.gameObject.SetActive(isActive);
        NextButton.gameObject.SetActive(isActive);
        SpecialButton.gameObject.SetActive(isActive);

        //Hide Scores Elements
        _CorrectAnswersText.gameObject.SetActive(false);
        _WrongAnswersText.gameObject.SetActive(false);
        _ScoreManager._PlayerSuccessRateSlider.gameObject.SetActive(false); // Hide Player SuccessRate ProgresBar
        _ScoreManager._BestSuccessRateSlider.gameObject.SetActive(false); // Hide BestSuccessRate ProgresBar
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

        if (v_QuestionNumber <= v_QuestionsQuantity) // If the Question Numbers <= v_QuestionsQuantity(30) )
        {
            _QuestionNumberTextMesh.text = "Question " + v_QuestionNumber + " / " + v_QuestionsQuantity; // Update Question Number Text
            
            Func_HideClickMark(); // Reset Click Marks
            Func_UpdateLists(); // Update Quiz Lists

            NextButton.gameObject.SetActive(false); // Disable Button 'Next' until the Question Content and Buttons Answers are Generated

            switch (v_QuizIndex)
            {
                case 0: // Guess Group
                    v_CategoryIndex = 0;
                    List_Answers.AddRange(Arr_GroupsNames); // Add again the Groups Array to Answers List

                    Func_GenQuestionImgContent(); // Generate Question Image
                    Func_GenButtonsAnswers(); // Generate Answers

                    SpecialButton.onClick.RemoveAllListeners(); // Remove All Listeners and Events untill now for the Button

                    SpecialButton.onClick.AddListener(Func_Zoom); // Add new Listener

                    break;

                case 1: // Guess Song
                    v_CategoryIndex = 1;
                    _QuestionAudioSource.enabled = true; // Enable the Question Audio Source

                    List_Answers.AddRange(Arr_SongsNames); // Add again the Groups Array to Answers List

                    // Clear Buttons' Texts
                    foreach(var AnsButtons in _Arr_AnsButtonsText)
                    {
                        AnsButtons.GetComponentInChildren<TextMeshProUGUI>().text = null;
                    }
                    _QuestionAudioSource.Stop(); // Stop Playing Question Audio 

                    SpecialButton.onClick.RemoveAllListeners();

                    // Play Question Music On Special Button Click || After that Generate Answers || In the End Remove Listener
                    SpecialButton.onClick.AddListener(Func_GenQuestionAudioContent);

                    break;

                case 2: // Guess Idol
                    v_CategoryIndex = 2;
                    List_Answers.AddRange(Arr_IdolsNames); // Add again the Idols Array to Answers List

                    Func_GenQuestionImgContent(); // Generate Question Image
                    Func_GenButtonsAnswers(); // Generate Answers

                    SpecialButton.onClick.RemoveAllListeners(); // Remove All Listeners and Events untill now for the Button
                    SpecialButton.onClick.AddListener(Func_Zoom); // Add new Listener

                    break;

                case 3: // Guess the Idol who Sing
                    v_CategoryIndex = 3;
                    _QuestionAudioSource.enabled = true; // Enable the Question Audio Source
                    //NextButton.gameObject.SetActive(false);

                    List_Answers.AddRange(Arr_PerformersNames); // Add again the Idol who Sing Array to Answers List

                    // Clear Buttons' Texts
                    foreach (var AnsButtons in _Arr_AnsButtonsText)
                    {
                        AnsButtons.GetComponentInChildren<TextMeshProUGUI>().text = null;
                    }
                    _QuestionAudioSource.Stop(); // Stop Playing Question Audio 

                    SpecialButton.onClick.RemoveAllListeners();

                    // Play Question Music On Special Button Click || After that Generate Answers || In the End Remove Listener
                    SpecialButton.onClick.AddListener(Func_GenQuestionAudioContent);

                    break;

                case 4: // Guess Song Album
                    v_CategoryIndex = 4;
                    _QuestionAudioSource.enabled = true; // Enable the Question Audio Source

                    List_Answers.AddRange(Arr_SongAlbums); // Add again the Groups Array to Answers List

                    // Clear Buttons' Texts
                    foreach (var AnsButtons in _Arr_AnsButtonsText)
                    {
                        AnsButtons.GetComponentInChildren<TextMeshProUGUI>().text = null;
                    }
                    _QuestionAudioSource.Stop(); // Stop Playing Question Audio 

                    SpecialButton.onClick.RemoveAllListeners();

                    // Play Question Music On Special Button Click || After that Generate Answers || In the End Remove Listener
                    SpecialButton.onClick.AddListener(Func_GenQuestionAudioContent);

                    break;

                case 5: // Guess Which Song the Dance is From
                    v_CategoryIndex = 5;
                    //_QuestionVideoPlayer.enabled = true; // Enable the Question Video Player

                    List_Answers.AddRange(Arr_SongDances); // Add again the Song Dances Arr to Answers List

                    // Clear Buttons' Texts
                    foreach (var AnsButtons in _Arr_AnsButtonsText)
                    {
                        AnsButtons.GetComponentInChildren<TextMeshProUGUI>().text = null;
                    }
                    _QuestionVideoPlayer.Stop(); // Stop Playing Question Video 
                    _QuestionVideoPlayer.GetComponentInChildren<RawImage>().enabled = false;

                    SpecialButton.onClick.RemoveAllListeners(); // Remove All Events with the Special Button

                    // Play Question Video On Special Button Click || After that Generate Answers || In the End Remove Listener
                    SpecialButton.onClick.AddListener(Func_GenQuestionVideoContent);

                    break;

                case 6: // Guess Idol Nationality
                    v_CategoryIndex = 6;
                    List_Answers.AddRange(Arr_IdolNationality); // Add again the Idols Nationality Array to Answers List

                    Func_GenQuestionImgContent(); // Generate Question Image
                    Func_GenQuiz6Answers(); // Generate Answers -- NEW MECHANIC must be created

                    SpecialButton.onClick.RemoveAllListeners(); // Remove All Listeners and Events untill now for the Button

                    SpecialButton.onClick.AddListener(Func_Zoom); // Add new Listener

                    break;

            }

        }
        else // The Quiz Ends (Question Number = 30 (Questions Quantity)
        {
            _QuestionNumberTextMesh.text = "All " + v_QuestionsQuantity + " Questions are answered";

            Func_VisualizeElements(false); // Hide Quiz Elements

            //Show Scores
            _CorrectAnswersText.gameObject.SetActive(true);
            _CorrectAnswersText.text = "Correct Answers: " + v_CorrectScore;

            _WrongAnswersText.gameObject.SetActive(true);
            _WrongAnswersText.text = "Wrong Answers: " + v_WrongScore;

            _List_GuessImage.Clear(); // Reset the List with Guess Image
            _List_GuessAudio.Clear(); // Reset the List with Guess Audio
            List_Answers.Clear(); // Reset Answers List

            NextQuizButton.gameObject.SetActive(true);

            //Success Rate Functionality
            _ScoreManager._PlayerSuccessRateSlider.gameObject.SetActive(true); // Show Player SuccessRate ProgresBar
            _ScoreManager._BestSuccessRateSlider.gameObject.SetActive(true); // Show Best SuccessRate ProgresBar

            _ScoreManager.Func_CalculateSuccessRate(v_CorrectScore, v_QuestionsQuantity); // Call Function to Check Player Score and Update the Hidhscore Data if the CURRENT Player Score is BIGGER than the HIGHSCORE

        }
    }

    // Enter Question Image Zoom Mode
    void Func_Zoom()
    {
        TransparentBGD.gameObject.SetActive(true);
        ExitZoomModeButton.gameObject.SetActive(true);
        v_isZoom = true;
        //SpecialButton.onClick.RemoveListener(Func_Zoom);

    }

    //Generate Random Question Image from _List_GuessImage List
    public void Func_GenQuestionImgContent()
    {
        v_Index_QuestionImage = Random.Range( 0, _List_GuessImage.Count ); // Generate Random INDEX for Question Picture
        _QuestionPicture.sprite = _List_GuessImage[v_Index_QuestionImage]; // Change Question Picture sprite to the Image located on the INDEX in Guess Image Array 
        v_QuestionImageName = _QuestionPicture.sprite.name; //GET the NAME of the IMAGE

        _List_GuessImage.RemoveAt(v_Index_QuestionImage); // Remove the Question Image 
    }

    //Generate Random Question Audio from _List_GuessAudio List
    public void Func_GenQuestionAudioContent()
    {
        v_Index_QuestionAudio = Random.Range(0, _List_GuessAudio.Count); // Generate Random INDEX for Question Audio
        _QuestionAudioSource.clip = _List_GuessAudio[v_Index_QuestionAudio]; // Change Question Audio clip to the Audio located on the INDEX in Guess Audio Array 
        v_QuestionAudioName = _QuestionAudioSource.clip.name; //GET the NAME of the Audio

        _List_GuessAudio.RemoveAt(v_Index_QuestionAudio); // Remove from GuessAudio List the Choosen Audio / Song

        _QuestionAudioSource.Play(); // Play the Audio Source ( Choosen Audio/Song )

        Func_GenButtonsAnswers(); // Generate Answers
        SpecialButton.onClick.RemoveListener(Func_GenQuestionAudioContent); // Remove All Special Button Listeners

    }

    //Generate Random Question Video from _List_GuessVideo List
    public void Func_GenQuestionVideoContent()
    {
        _QuestionVideoPlayer.gameObject.SetActive(true); // ENABLE Question Video Object
        _QuestionVideoPlayer.GetComponentInChildren<RawImage>().enabled = true;

        v_Index_QuestionVideo = Random.Range(0, _List_GuessVideo.Count); // Generate Random INDEX for Question Video
        _QuestionVideoPlayer.clip = _List_GuessVideo[v_Index_QuestionVideo]; // Change Question Video clip to the Video located on the INDEX in Guess Video Array 
        v_QuestionVideoName = _QuestionVideoPlayer.clip.name; //GET the NAME of the Video

        _List_GuessVideo.RemoveAt(v_Index_QuestionVideo); // Remove from GuessVideo List the Choosen Video / Dance

        _QuestionVideoPlayer.Play(); // Play the Video Source ( Choosen Video/Dance )
        _QuestionVideoPlayer.SetDirectAudioMute(0, true); // Mute the Audio of the VideoClip 

        SpecialButton.onClick.RemoveListener(Func_GenQuestionVideoContent); // Remove All Special Button Listeners

        Func_GenButtonsAnswers(); // Generate Answers

    }

    //Generate Question Answers and CHANGE the Buttons Text to them 
    public void Func_GenButtonsAnswers()
    {
        var v_CorrectButtonTextIndex = Random.Range(0, _List_AnsButtonsText.Count); // Generate Random Index of the Correct Buttons Text List which will contains the Correct Answer

        if (v_QuizIndex == 0)
        {
            _List_AnsButtonsText[v_CorrectButtonTextIndex].text = v_QuestionImageName; // Update his Text to the QUESTION IMAGE NAME - the CORRECT ANSWER
            v_CorrectAnswer = v_QuestionImageName; // Update the Corect Answer to the Name of the Question Image
        }
        else if(v_QuizIndex == 1)
        {
            _List_AnsButtonsText[v_CorrectButtonTextIndex].text = v_QuestionAudioName; // Update his Text to the QUESTION IMAGE NAME - the CORRECT ANSWER
            v_CorrectAnswer = v_QuestionAudioName; // Update the Corect Answer to the Name of the Question Audio
        }
        else if(v_QuizIndex == 2)
        {
            _List_AnsButtonsText[v_CorrectButtonTextIndex].text = v_QuestionImageName; // Update his Text to the QUESTION IMAGE NAME - the CORRECT ANSWER
            v_CorrectAnswer = v_QuestionImageName; // Update the Corect Answer to the Name of the Question Image
        }
        else if (v_QuizIndex == 3)
        {
            _List_AnsButtonsText[v_CorrectButtonTextIndex].text = v_QuestionAudioName; // Update his Text to the QUESTION AUDIO NAME - the CORRECT ANSWER
            v_CorrectAnswer = v_QuestionAudioName; // Update the Corect Answer to the Name of the Question Audio
        }
        else if(v_QuizIndex == 4)
        {
            _List_AnsButtonsText[v_CorrectButtonTextIndex].text = v_QuestionAudioName; // Update his Text to the QUESTION AUDIO NAME - the CORRECT ANSWER
            v_CorrectAnswer = v_QuestionAudioName; // Update the Corect Answer to the Name of the Question Audio
        }
        else if (v_QuizIndex == 5)
        {
            _List_AnsButtonsText[v_CorrectButtonTextIndex].text = v_QuestionVideoName; // Update his Text to the QUESTION VIDEO NAME - the CORRECT ANSWER
            v_CorrectAnswer = v_QuestionVideoName; // Update the Corect Answer to the Name of the Question Video
        }

        v_CorrectAnswerIndex = List_Answers.IndexOf(v_CorrectAnswer); // Find Index of the Correct Answer in the Answers List

        _List_AnsButtonsText.RemoveAt(v_CorrectButtonTextIndex); // REMOVE the INDEX of the CORRECT BUTTON TEXT
        List_Answers.Remove(v_CorrectAnswer); // REMOVE the CORRECT ANSWER (STRING) from Answers List

        if (v_CorrectAnswerIndex <= v_GenderBorder)
        {
            v_GenderBorder -= 1;
        }

        foreach (var ButtonsText in _List_AnsButtonsText)
        {
            var v_WrongButtonsAnswerIndex = 0;
            if (v_CorrectAnswerIndex <= v_GenderBorder) // Guess Image is FEMALE 
            {
                v_WrongButtonsAnswerIndex = Random.Range(0, v_GenderBorder); // Generate Random Wrong FEMALE Image Answer from List Answers
                v_GenderBorder -= 1;
            }
            else if(v_CorrectAnswerIndex > v_GenderBorder) // Guess Image is MALE
            {
                v_WrongButtonsAnswerIndex = Random.Range(v_GenderBorder + 1, List_Answers.Count); // Generate Random Wrong MALE Image Answer from List Answers
            }

            //var v_WrongButtonsAnswerIndex = Random.Range(0, List_Answers.Count); // Generate Random Wrong Answer from List Answers
            ButtonsText.text = List_Answers[v_WrongButtonsAnswerIndex]; // Display the Wrong Answer to Current Button Text in the List

            List_Answers.RemoveAt(v_WrongButtonsAnswerIndex); // REMOVE the Generated Wrong ANSWER (Index) from Answers List
        }

        NextButton.gameObject.SetActive(true); // Enable Button 'Next' after the Question Content and Buttons Answers are Generated

    }

    public void Func_GenQuiz6Answers()
    {
        var v_CorrectButtonTextIndex = Random.Range(0, _List_AnsButtonsText.Count); // Generate Random Index of the Correct Buttons Text List which will contains the Correct Answer

        _List_AnsButtonsText[v_CorrectButtonTextIndex].text = v_QuestionImageName; // Update his Text to the QUESTION IMAGE NAME - the CORRECT ANSWER
        v_CorrectAnswer = v_QuestionImageName; // Update the Corect Answer to the Name of the Question Image

        v_CorrectAnswerIndex = List_Answers.IndexOf(v_CorrectAnswer); // Find Index of the Correct Answer in the Answers List

        _List_AnsButtonsText.RemoveAt(v_CorrectButtonTextIndex); // REMOVE the INDEX of the CORRECT BUTTON TEXT
        List_Answers.Remove(v_CorrectAnswer); // REMOVE the CORRECT ANSWER (STRING) from Answers List

        foreach (var ButtonsText in _List_AnsButtonsText)
        {
            var v_WrongButtonsNatAnswerIndex = 0;
            v_WrongButtonsNatAnswerIndex = Random.Range(0, List_Answers.Count); // Generate Random Wrong Answer from List Answers

            ButtonsText.text = List_Answers[v_WrongButtonsNatAnswerIndex]; // Display the Wrong Answer to Current Button Text in the List

            List_Answers.RemoveAt(v_WrongButtonsNatAnswerIndex); // REMOVE the Generated Wrong ANSWER (Index) from Answers List
        }

        NextButton.gameObject.SetActive(true); // Enable Button 'Next' after the Question Content and Buttons Answers are Generated

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

        if (v_ClickedButtonText.text == v_CorrectAnswer) // Check is the Text of the Marked Button is EQUAL to the Correct Answer of the Question
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
            v_GenderBorder = 23;

            v_ClickedButtons_Quantity = 0; // Reset to NONE Clicked Answer Button
            LastClickedButton = null; // Reset to NONE the Last Clicked Button

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
        v_GenderBorder = 23;

        Func_VisualizeQuizPanel(false); // Hide Quiz Canvas and Objects
        _CtryCanvas.Func_VisualizeCategoriesPanel(true); // Show Category Canvas

        _List_GuessImage.Clear(); //Clear Quess Image List 
        _List_GuessAudio.Clear(); //Clear Quess Audio List
        _List_GuessVideo.Clear(); //Clear Quess Video List
        _QuestionAudioSource.clip = null;
        _QuestionVideoPlayer.clip = null;

        List_Answers.Clear(); //Clear Answers List
        _List_AnsButtonsText.Clear(); // Clear Answer Buttons Text List
        _List_AnsButtonsText.AddRange(_Arr_AnsButtonsText); // Fill Again Answer Buttons Text List

        //Reset Score
        v_CorrectScore = 0;
        v_WrongScore = 0;
        _QuestionPicture.sprite = null; // Reset Question Picture Actor Sprite 
        _QuestionVideoPlayer.GetComponentInChildren<RawImage>().enabled = false;
        _QuestionVideoPlayer.gameObject.SetActive(false); // DISABLE Question Video Object
    }

    public void Func_NextQuiz()
    {
        v_QuestionNumber = 0; // Reset to 0 Question Number
        Func_VisualizeQuizPanel(false); // Hide Quiz Canvas and Objects
        _CtryCanvas.Func_VisualizeCategoriesPanel(true); // Show Category Canvas and Objects

        //Reset Score
        v_CorrectScore = 0;
        v_WrongScore = 0;
        v_GenderBorder = 23;

        //Reset SuccessRate ProgressBar Capacity (value) to 0
        _ScoreManager.v_InitialProgressBarCapacity = 0;
        _ScoreManager._PlayerSuccessRateSlider.value = _ScoreManager.v_InitialProgressBarCapacity;

    }

}
