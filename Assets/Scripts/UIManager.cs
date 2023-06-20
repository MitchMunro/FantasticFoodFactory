using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public GameObject playPauseButtonGameObj;
    private Button playPauseButton;

    public Sprite playButtonSprite;
    public Sprite pauseButtonSprite;
    public Sprite replayButtonSprite;
    public Sprite crossButtonSprite;

    public GameObject moneyTextGameObj;
    public GameObject moneyGoalTextGameObj;
    public GameObject timeTextGameObj;

    private TextMeshProUGUI moneyText;
    private TextMeshProUGUI moneyGoalText;
    private TextMeshProUGUI timeText;

    private Image image;

    public GameObject statusPanel;

    public GameObject TutorialUI;
    public GameObject tutorialMainPanelParent;
    public GameObject tutorialCompletePanelObj;

    public GameObject speedSliderGameObj;
    private Slider speedSlider;

    private void Awake()

    {
        speedSlider = speedSliderGameObj.GetComponent<Slider>();
        playPauseButton = playPauseButtonGameObj.GetComponent<Button>();
        playPauseButton.onClick.AddListener(ButtonClicked);
        image = playPauseButtonGameObj.GetComponent<Image>();

        var canvas = GameObject.Find("Canvas").transform.Find("ScoringPanel");

        moneyText = moneyTextGameObj.GetComponent<TextMeshProUGUI>();
        moneyGoalText = moneyGoalTextGameObj.GetComponent<TextMeshProUGUI>();
        timeText = timeTextGameObj.GetComponent<TextMeshProUGUI>();

        startFontSize = countdownText.fontSize;
    }

    private void Start()

    {
        if (TutorialUI != null &&
            !GameManager.Instance.isMainMenu) TutorialUI.SetActive(true);
    }


    public void SetTextMoney(string newText)
    {
        moneyText.text = newText;
    }

    public void SetTextMoneyGoal(string newText)
    {
        moneyGoalText.text = newText;
    }

    public void SetTextTimer(string newText)
    {
        timeText.text = newText;
    }


    public void ButtonClicked()
    {
        statusPanel.SetActive(false);


        if (GameManager.Instance.buttonState == ButtonState.Pause)
        {
            ButtonChangedToPlay();
            GameManager.Instance.StopFacrotyAndReset();
        }
        else if (GameManager.Instance.buttonState == ButtonState.Play)
        {
            ButtonChangedToPause();
            GameManager.Instance.PlayFactory();
        }
        else if(GameManager.Instance.buttonState == ButtonState.Replay)
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

    public void ButtonChangedToCross()
    {
        image.sprite = crossButtonSprite;
    }

    public void UpdateStatusPanelText(string text)
    {
        var textGameObj = statusPanel.transform.Find("Status Text");
        var statusPanelText = textGameObj.GetComponent<TextMeshProUGUI>();

        statusPanelText.text = text;
    }

    public float SpeedSliderValue()
    {
        return speedSlider.value;
    }

    public void CompleteTutorial()
    {
        if (tutorialMainPanelParent != null)
        tutorialMainPanelParent.SetActive(false);

        if (tutorialCompletePanelObj != null)
            tutorialCompletePanelObj.SetActive(true);
    }


    public TextMeshProUGUI countdownText;
    public float fadeDuration = 1f;
    public float scaleSpeed = 1f;
    private float startFontSize;

    private Coroutine numberFadeCoroutine;

    public void DisplayCountdownNumber(int num)
    {
        if (numberFadeCoroutine != null) StopCoroutine(numberFadeCoroutine);

        numberFadeCoroutine = StartCoroutine(NumberFadeCoroutine(num));
    }

    private IEnumerator NumberFadeCoroutine(int num)
    {
        //If num == -1 then Coroutine ends
        if (num == -1)
        {
            StopCoroutine(numberFadeCoroutine);
        }

        countdownText.text = num.ToString();

        countdownText.fontSize = startFontSize;
        countdownText.color = new Color(countdownText.color.r, countdownText.color.g, countdownText.color.b, 1f);

        while (countdownText.color.a > 0)
        {
            countdownText.fontSize += Time.deltaTime * scaleSpeed;
            countdownText.color = new Color(countdownText.color.r, countdownText.color.g, countdownText.color.b, countdownText.color.a - (Time.deltaTime / fadeDuration));

            yield return null;
        }

        if (num == 0)
        {
            countdownText.text = "";
        }
    }


}
