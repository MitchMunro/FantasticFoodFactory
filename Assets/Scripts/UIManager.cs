using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static UnityEngine.GraphicsBuffer;

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

    public GameObject HighScorePanel;
    private RawImage Star1;
    private RawImage Star2;
    private RawImage Star3;
    private TextMeshProUGUI StarScore1Text;
    private TextMeshProUGUI StarScore2Text;
    private TextMeshProUGUI StarScore3Text;
    private TextMeshProUGUI LastScoreText;
    private TextMeshProUGUI HighScoresText;

    private RectTransform HSPanelRect;
    public float HSPanelMoveDuration = 0.5f;
    private Vector2 HSPanelInitialPosition;
    private bool isMoving = false;
    private float HSPanelYAxisExtended = -95.8f;
    private float HSPanelYAxisHidden = 2.7f;
    private bool IsHSPanelExtended = false;

    public ParticleSystem scoreBurst1;

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

        if (HighScorePanel != null)
        {
            HSPanelRect = HighScorePanel.GetComponent<RectTransform>();

            Star1 = HighScorePanel.transform.Find("StarPanel")
            .gameObject.transform.Find("Star1").GetComponent<RawImage>();
            Star2 = HighScorePanel.transform.Find("StarPanel")
                .gameObject.transform.Find("Star2").GetComponent<RawImage>();
            Star3 = HighScorePanel.transform.Find("StarPanel")
                .gameObject.transform.Find("Star3").GetComponent<RawImage>();

            StarScore1Text = HighScorePanel.transform.Find("StarPanel")
                .gameObject.transform.Find("StarScore1").GetComponent<TextMeshProUGUI>();
            StarScore2Text = HighScorePanel.transform.Find("StarPanel")
                .gameObject.transform.Find("StarScore2").GetComponent<TextMeshProUGUI>();
            StarScore3Text = HighScorePanel.transform.Find("StarPanel")
                .gameObject.transform.Find("StarScore3").GetComponent<TextMeshProUGUI>();

            HighScoresText = HighScorePanel.transform.Find("HighScoresText")
            .GetComponent<TextMeshProUGUI>();

            LastScoreText = HighScorePanel.transform.Find("LastScoreText")
            .GetComponent<TextMeshProUGUI>();
        }

        if (countdownText != null) startFontSize = countdownText.fontSize;


            

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

        CloseHighScorePanel();

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


    public void HighlightStar(int starNumber)
    {
        switch (starNumber)
        {
            case 1:
                Star1.color = Color.white;
                break;
            case 2:
                Star2.color = Color.white;
                break;
            case 3:
                Star3.color = Color.white;
                break;
            default:
                Debug.Log("Invalid star number chosen.");
                break;
        }
    }

    public void BlackenStar(int starNumber)
    {
        if (Star1 == null ||
            Star2 == null ||
            Star3 == null)
            return;

        switch (starNumber)
        {
            case 1:
                Star1.color = Color.black;
                break;
            case 2:
                Star2.color = Color.black;
                break;
            case 3:
                Star3.color = Color.black;
                break;
            default:
                Debug.Log("Invalid star number chosen.");
                break;
        }
    }

    public void SetHighScores(int score1, int score2, int score3, int score4)
    {
        if (HighScoresText == null) return;

        HighScoresText.text = $"1.${score1}     2.${score2}     3.${score3}     4.${score4}";
    }

    public void SetLastScore(int score)
    {
        if (LastScoreText == null) return;

        LastScoreText.text = $"Last Score: ${score}";
    }

    public void SetStarScores(int star1Score, int star2Score, int star3Score)
    {
        if (StarScore1Text == null ||
            StarScore2Text == null ||
            StarScore3Text == null)
            return;

        StarScore1Text.text = $"${star1Score}";
        StarScore2Text.text = $"${star2Score}";
        StarScore3Text.text = $"${star3Score}";
    }

    public void ToggleHighScorePanel()
    {
        if (isMoving) return;

        if (IsHSPanelExtended)
        {
            HSPanelInitialPosition = HSPanelRect.anchoredPosition;
            StartCoroutine(MovePanelCoroutine(HSPanelYAxisHidden));
            IsHSPanelExtended = false;
        }
        else
        {
            HSPanelInitialPosition = HSPanelRect.anchoredPosition;
            StartCoroutine(MovePanelCoroutine(HSPanelYAxisExtended));
            IsHSPanelExtended = true;
        }
    }

    public void OpenHighScorePanel()
    {
        if (!IsHSPanelExtended)
        {
            HSPanelInitialPosition = HSPanelRect.anchoredPosition;
            StartCoroutine(MovePanelCoroutine(HSPanelYAxisExtended));
            IsHSPanelExtended = true;
        }
    }

    public void CloseHighScorePanel()
    {
        if (IsHSPanelExtended)
        {
            HSPanelInitialPosition = HSPanelRect.anchoredPosition;
            StartCoroutine(MovePanelCoroutine(HSPanelYAxisHidden));
            IsHSPanelExtended = false;
        }
    }

    private IEnumerator MovePanelCoroutine(float targetY)
    {
        isMoving = true;

        float elapsedTime = 0f;

        while (elapsedTime < HSPanelMoveDuration)
        {
            float t = elapsedTime / HSPanelMoveDuration;
            float easedT = Mathf.SmoothStep(0f, 1f, t); // Apply easing function

            Vector2 newPosition = new Vector2(HSPanelInitialPosition.x, Mathf.Lerp(HSPanelInitialPosition.y, targetY, easedT));
            HSPanelRect.anchoredPosition = newPosition;

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        HSPanelRect.anchoredPosition = new Vector2(HSPanelInitialPosition.x, targetY);
        isMoving = false;
    }

    public void PlayScoreBurst1()
    {
        if (scoreBurst1 == null) return;

        scoreBurst1.Play();

    }
}
