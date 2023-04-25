using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public GameObject playPauseButtonGameObject;
    private Button playPauseButton;

    public Sprite playButtonSprite;
    public Sprite pauseButtonSprite;

    private Image image;

    public GameObject statusPanel;

    public GameObject tutorialMainPanelParent;
    public GameObject tutorialCompletePanelObject;

    public GameManager game;

    private void Start()
    {
        playPauseButton = playPauseButtonGameObject.GetComponent<Button>();
        playPauseButton.onClick.AddListener(ButtonClicked);
        image = playPauseButtonGameObject.GetComponent<Image>();

    }

    public void ButtonClicked()
    {
        statusPanel.SetActive(false);


        if (GameManager.Instance.isFactoryPlaying == true)
        {
            ButtonChangedToPlay();
            GameManager.Instance.StopFacrotyAndReset();
        }
        else if (GameManager.Instance.isFactoryPlaying == false)
        {
            ButtonChangedToPause();
            GameManager.Instance.PlayFactory();
        }
    }

    public void ButtonChangedToPause()
    {
        image.sprite = pauseButtonSprite;
    }

    public void ButtonChangedToPlay()
    {
        image.sprite = playButtonSprite;
    }

    public void UpdateStatusPanelText(string text)
    {
        var textGameObj = statusPanel.transform.Find("Status Text");
        var statusPanelText = textGameObj.GetComponent<TextMeshProUGUI>();

        statusPanelText.text = text;
    }

    public void CompleteTutorial()
    {
        tutorialMainPanelParent.SetActive(false);
        tutorialCompletePanelObject.SetActive(true);
    }

}
