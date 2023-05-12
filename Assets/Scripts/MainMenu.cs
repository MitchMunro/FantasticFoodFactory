using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void TutorialButton()
    {
        SceneManager.LoadScene("Tutorial Level_New Art");
    }
}
