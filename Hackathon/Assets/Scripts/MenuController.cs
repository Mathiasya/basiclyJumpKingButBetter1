using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [Header("Levels To Load")]
    public string _newGameLevel;
    private string levelToLoad;
    

    public void NewGameDialogYes()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("LevelDesign");       
    }


    public void ExitButton()
    {
        Application.Quit();
    }
}
