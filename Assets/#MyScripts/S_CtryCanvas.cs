using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class S_CtryCanvas : MonoBehaviour
{
    public Canvas _CategoriesCanvas; //Initialize Category Canvas
    public Button[] _CategoriesButtons; //Initialize Categories Buttons

    // On App Start
    void Start()
    {
        Func_HideCanvas();
    }

    //Hide CategoriesCanvas
    public void Func_HideCanvas()
    {
        _CategoriesCanvas.gameObject.SetActive(false);
    }

    //Show CategoriesCanvas
    public void Func_ShowCanvas()
    {
        _CategoriesCanvas.gameObject.SetActive(true);
    }
}
