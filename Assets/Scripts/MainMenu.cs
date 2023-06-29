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
        SceneManager.LoadScene("Level 1 - Intro");

    }

    public void Level2Button()
    {
        SceneManager.LoadScene("Level 2 - Burger Time");

    }

    public void Level3Button()
    {
        SceneManager.LoadScene("Level 3 - Meatball Pinball - Mitch");

    }

    public void Level4Button()
    {
        SceneManager.LoadScene("Level 4 - Definitely Not Burger Time");

    }

    public void Level5Button()
    {
        SceneManager.LoadScene("Level5_New Art");

    }

    public void ResetButton()
    {

    }
}
