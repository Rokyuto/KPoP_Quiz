using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Quiz : MonoBehaviour
{
    public Canvas _QuizCanvas; // Create Canvas Object to Initialize Quiz Canvas from the Scene

    // Start is called before the first frame update
    void Start()
    {
        Func_HideQuizPanel();
    }

    public void Func_HideQuizPanel()
    {
        _QuizCanvas.gameObject.SetActive(false);
    }

    public void Func_ShowQuizPanel()
    {
        _QuizCanvas.gameObject.SetActive(true);
    }

}
