using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class S_StartupCanvas : MonoBehaviour
{
    public Button PlayButton; //Create Button Object to Initialize PlayButton from the Scene
    public Canvas StartupCanvas; //Create Canvas Object to Initialize StartupCanvas from the Scene
    public Canvas CategoriesCanvas; //Create Canvas Object to Initialize CategoriesCanvas from the Scene

    //PlayButton Clicked
    public void Func_OnClick()
    {
        StartupCanvas.gameObject.SetActive(false); //Hide StartupCanvas
        CategoriesCanvas.gameObject.SetActive(true); //Hide CategoriesCanvas
    }

    public void Func_ShowStartupCanvas()
    {
        StartupCanvas.gameObject.SetActive(true);
    }
}
