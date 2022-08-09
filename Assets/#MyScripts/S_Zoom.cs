using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class S_Zoom : MonoBehaviour
{
    public S_Quiz _Quiz;
    public Image _QuestionImage;
    public Image _TransparentBGD;
    public Button _ExitZoomModeButton;

    [SerializeField] private Vector3 defaultTransform;
    [SerializeField] private Vector3 zoomTransform;

    private void Start()
    {
        S_Quiz _Quiz = GetComponent<S_Quiz>(); // Initialize Quiz
    }

    private void Update()
    {
        if (_Quiz.v_isZoom) // If View Image Button is Pressed
        {
            _QuestionImage.rectTransform.localScale = zoomTransform; // Zoom Question Image
        }
    }

    // Exite Zoom Mode when ExitZoomModeButton is Clicked
    public void Func_ExitZoomMode()
    {
        _Quiz.v_isZoom = false;
        _QuestionImage.rectTransform.localScale = defaultTransform; // Return Question Image Default Scale / Transform
        _TransparentBGD.gameObject.SetActive(false); // Hide TransparentBGD
        _ExitZoomModeButton.gameObject.SetActive(false); // Hide ExitZoomModeButton
    }

}
