using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void TutorialButton()
    {
        SceneManager.LoadScene("Tutorial Level_New");
    }

    public void Level1Button()
    {
        SceneManager.LoadScene("");

    }

    public void Level2Button()
    {
        SceneManager.LoadScene("");

    }

    public void Level3Button()
    {
        SceneManager.LoadScene("Level 3 - Meatball Pinball - Mitch");

    }

    public void Level4Button()
    {
        SceneManager.LoadScene("");

    }

    public void Level5Button()
    {
        SceneManager.LoadScene("Level5_New Art");

    }
}
