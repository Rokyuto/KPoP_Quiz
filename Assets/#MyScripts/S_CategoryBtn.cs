using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    public int _ClickedButtonIndex;

    //Load Quiz || Back to Startup
    public void Func_SetupQuiz()
    {
        _ClickedButtonIndex = System.Array.IndexOf(_CategoriesMenu._CategoriesButtons, _CurrentButton); //Get Clicked Button Index from the CategoriesButtons Array

        //Track which button is Clicked || Set Question Text
        switch ( _ClickedButtonIndex )
        {
            case 0:
                // If Quiz Task is "Guess the Group" :
                QuizTask = "Guess the Group";
                _QuizCanvas.v_QuizIndex = 0;

                // Insert in the Lists Groups' Images and Names
                _QuizCanvas.List_Answers.AddRange(_QuizCanvas.Arr_GroupsNames); // Insert in the List Answers Guess Group Array
                _QuizCanvas._List_GuessImage.AddRange(_QuizCanvas._Arr_GuessGroupsImgs);

                _QuizCanvas._QuestionPicture.enabled = true; //ENABLE Question Picture Object 
                _QuizCanvas._QuestionAudioSource.enabled = false; //DISABLE Question Audio Object 
                _QuizCanvas._QuestionVideoPlayer.gameObject.SetActive(false); // DISABLE Question Video Object 
                _QuizCanvas.SpecialButton.GetComponentInChildren<TextMeshProUGUI>().text = "View Image";
                break;

            case 1:
                QuizTask = "Guess the Song";
                _QuizCanvas.v_QuizIndex = 1;

                // Insert in the Lists Songs' Audio and Names
                _QuizCanvas.List_Answers.AddRange(_QuizCanvas.Arr_SongsNames);
                _QuizCanvas._List_GuessAudio.AddRange(_QuizCanvas._Arr_GuessSongs);

                _QuizCanvas._QuestionPicture.enabled = false; //DISABLE Question Picture Object 
                //_QuizCanvas._QuestionAudioSource.enabled = true; //ENABLE Question Audio Object 
                _QuizCanvas._QuestionVideoPlayer.gameObject.SetActive(false); // DISABLE Question Video Object 
                _QuizCanvas.SpecialButton.GetComponentInChildren<TextMeshProUGUI>().text = "Play Audio";
                break;

            case 2:
                QuizTask = "Guess the Idol";
                _QuizCanvas.v_QuizIndex = 2;

                // Insert in the Lists Idols' Images and Names
                _QuizCanvas.List_Answers.AddRange(_QuizCanvas.Arr_IdolsNames);
                _QuizCanvas._List_GuessImage.AddRange(_QuizCanvas._Arr_GuessIdolsImgs);

                _QuizCanvas._QuestionPicture.enabled = true; //ENABLE Question Picture Object 
                _QuizCanvas._QuestionAudioSource.enabled = false; //DISABLE Question Audio Object 
                _QuizCanvas._QuestionVideoPlayer.gameObject.SetActive(false); // DISABLE Question Video Object 
                _QuizCanvas.SpecialButton.GetComponentInChildren<TextMeshProUGUI>().text = "View Image";
                break;

            case 3:
                QuizTask = "Guess the Idol who Sing";
                _QuizCanvas.v_QuizIndex = 3;

                // Fill Lists with Idols who Sing Answers and Questions
                _QuizCanvas.List_Answers.AddRange(_QuizCanvas.Arr_PerformersNames);
                _QuizCanvas._List_GuessAudio.AddRange(_QuizCanvas._Arr_GuessPerformer);

                _QuizCanvas._QuestionPicture.enabled = false; //DISABLE Question Picture Object
                //_QuizCanvas._QuestionAudioSource.enabled = true; //ENABLE Question Audio Object
                _QuizCanvas._QuestionVideoPlayer.gameObject.SetActive(false); // DISABLE Question Video Object 
                _QuizCanvas.SpecialButton.GetComponentInChildren<TextMeshProUGUI>().text = "Play Audio";
                break;

            case 4:
                QuizTask = "Guess Song Album";
                _QuizCanvas.v_QuizIndex = 4;

                // Fill Lists with Idols who Sing Answers and Questions
                _QuizCanvas.List_Answers.AddRange(_QuizCanvas.Arr_SongAlbums);
                _QuizCanvas._List_GuessAudio.AddRange(_QuizCanvas._Arr_GuessSongAlbum);

                _QuizCanvas._QuestionPicture.enabled = false; //DISABLE Question Picture Object
                //_QuizCanvas._QuestionAudioSource.enabled = true; //ENABLE Question Audio Object
                _QuizCanvas._QuestionVideoPlayer.gameObject.SetActive(false); // DISABLE Question Video Object 
                _QuizCanvas.SpecialButton.GetComponentInChildren<TextMeshProUGUI>().text = "Play Audio";
                break;

            case 5:
                QuizTask = "Guess which song the dance is from";
                _QuizCanvas.v_QuizIndex = 5;

                // Fill Lists with Song Dances Videos and Answers
                _QuizCanvas.List_Answers.AddRange(_QuizCanvas.Arr_SongDances);
                _QuizCanvas._List_GuessVideo.AddRange(_QuizCanvas._Arr_GuessSongDance);

                _QuizCanvas._QuestionPicture.enabled = false; // DISABLE Question Picture Object 
                _QuizCanvas._QuestionAudioSource.enabled = false; // DISABLE Question Audio Object 
                                                                  //_QuizCanvas._QuestionVideoPlayer.gameObject.SetActive(true); // ENABLE Question Video Object
                _QuizCanvas._QuestionVideoPlayer.GetComponentInChildren<RawImage>().enabled = false;
                _QuizCanvas._QuestionVideoPlayer.gameObject.SetActive(true); // ENABLE Question Video Object

                _QuizCanvas.SpecialButton.GetComponentInChildren<TextMeshProUGUI>().text = "Play Video";
                break;

            case 6:
                QuizTask = "Guess Idol Nationality";
                _QuizCanvas.v_QuizIndex = 6;

                // Fill Lists with Idols Nationality and Answers
                _QuizCanvas.List_Answers.AddRange(_QuizCanvas.Arr_IdolNationality);
                _QuizCanvas._List_GuessImage.AddRange(_QuizCanvas._Arr_GuessIdolNationality);

                _QuizCanvas._QuestionPicture.enabled = true; //ENABLE Question Picture Object 
                _QuizCanvas._QuestionAudioSource.enabled = false; //DISABLE Question Audio Object 
                _QuizCanvas._QuestionVideoPlayer.gameObject.SetActive(false); // DISABLE Question Video Object 
                _QuizCanvas.SpecialButton.GetComponentInChildren<TextMeshProUGUI>().text = "View Image";
                break;

        }

        _QuizCanvas.Func_UpdateTaskText(QuizTask); //Update Question Text Content

        //Load Next Canvas on Button Clicked
        LoadQuizCanvas();
    }

    //Load Quiz Canvas
    void LoadQuizCanvas()
    {
        _CategoriesMenu.Func_VisualizeCategoriesPanel(false); //Hide Category Canvas
        _QuizCanvas.Func_VisualizeQuizPanel(true); //Show Quiz Canvas
    }

    //Go Back to Category Canvas
    public void Func_Back()
    {
        _CategoriesMenu.Func_VisualizeCategoriesPanel(false); //Hide Category Canvas
        _StartupCanvas.Func_ShowStartupCanvas(); //Show Startup Canvas
    }

}