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
        Func_VisualizeCategoriesPanel(false);
    }

    //Update Category Panel Visibility
    public void Func_VisualizeCategoriesPanel(bool isActive)
    {
        if (isActive == true)
        {
            _CategoriesCanvas.gameObject.SetActive(true); //Show Category Panel
        }
        else
        {
            _CategoriesCanvas.gameObject.SetActive(false); //Hide Category Panel
        }
    }
}
