using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TutorialPanelComplete : MonoBehaviour
{
    public List<GameObject> associatedGameObjects;

    private Button buttonContinue;
    private Button buttonReplay;
    public string nextSceneName;

    private void Start()
    {
        buttonReplay = transform.Find("ButtonReplay")?.GetComponent<Button>();
        buttonContinue = transform.Find("ButtonContinue")?.GetComponent<Button>();

        if (buttonReplay != null) buttonReplay.onClick.AddListener(ButtonReplay);
        if (buttonContinue != null) buttonContinue.onClick.AddListener(ButtonContinue);

    }

    private void OnEnable()
    {
        ActivateAssociatedGameObjects();
    }

    private void ActivateAssociatedGameObjects()
    {
        foreach (var obj in associatedGameObjects)
        {
            obj.SetActive(true);
        }
    }

    private void DeactivateAssociatedGameObjects()
    {
        foreach (var obj in associatedGameObjects)
        {
            obj.SetActive(false);
        }
    }

    public void ButtonReplay()
    {

        Debug.Log("Replay!");
        // Get the current scene
        Scene currentScene = SceneManager.GetActiveScene();

        // Reload the current scene
        SceneManager.LoadScene(currentScene.name);
    }

    public void ButtonContinue()
    {
        Debug.Log("Continue!");

        // Reload the current scene
        SceneManager.LoadScene("Tutorial Level 2_New_Redone");

    }



}
