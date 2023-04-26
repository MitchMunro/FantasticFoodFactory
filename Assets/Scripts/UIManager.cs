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
    public Sprite replayButtonSprite;

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


        if (GameManager.Instance.playPauseButtonImage == ButtonState.Pause)
        {
            ButtonChangedToPlay();
            GameManager.Instance.StopFacrotyAndReset();
        }
        else if (GameManager.Instance.playPauseButtonImage == ButtonState.Play)
        {
            ButtonChangedToPause();
            GameManager.Instance.PlayFactory();
        }
        else if(GameManager.Instance.playPauseButtonImage == ButtonState.Replay)
        {
            ButtonChangedToPlay();
            GameManager.Instance.StopFacrotyAndReset();
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

    public void ButtonChangedToReplay()
    {
        image.sprite = replayButtonSprite;
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
