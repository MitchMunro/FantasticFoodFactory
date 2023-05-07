using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance;
    public UIManager uIManager;

    private TextMeshProUGUI moneyText;
    private TextMeshProUGUI moneyGoalText;
    private TextMeshProUGUI timeText;

    public GameObject speedSliderGameObj;
    private Slider speedSlider;

    public float timerCount { get; private set; }
    public int money; //{ get; private set; }
    private int moneyScoreAtRoundStart;

    public LevelGoal levelGoal;

    //This is used to check if the play button has been clicked, and therefore if the factory is playing or not
    public bool isFactoryPlaying { get; private set; }
    public ButtonState playPauseButtonImage { get; private set; } = ButtonState.Play;

    public GameObject FoodSpawnedParent;
    public GameObject ObjectsBoughtParent;

    public GameObject GoalPipe1GameObj;
    public GameObject GoalPipe2GameObj;
    public GameObject GoalPipe3GameObj;

    private Goal GoalPipe1;
    private Goal GoalPipe2;
    private Goal GoalPipe3;

    private bool finalScoreWorkDone = false;

    public GameObject burger;


    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }

        // Get all the text components attached to the canvas so they can be updated later.
        var canvas = GameObject.Find("Canvas").transform.Find("ScoringPanel");

        var moneyTextGameObj = canvas.Find("MoneyText").gameObject;
        var moneyGoalTextGameObj = canvas.Find("MoneyGoalText").gameObject;
        var timeTextGameObj = canvas.Find("TimeText").gameObject;

        moneyText = moneyTextGameObj.GetComponent<TextMeshProUGUI>();
        moneyGoalText = moneyGoalTextGameObj.GetComponent<TextMeshProUGUI>();
        timeText = timeTextGameObj.GetComponent<TextMeshProUGUI>();

        FoodSpawnedParent = transform.Find("FoodSpawned").gameObject;

        if (GoalPipe1GameObj != null) GoalPipe1 = GoalPipe1GameObj.GetComponent<Goal>();
        if (GoalPipe2GameObj != null) GoalPipe2 = GoalPipe1GameObj.GetComponent<Goal>();
        if (GoalPipe3GameObj != null) GoalPipe3 = GoalPipe1GameObj.GetComponent<Goal>();

        speedSlider = speedSliderGameObj.GetComponent<Slider>();

    }

    private void Start()
    {
        moneyGoalText.text = $"Goal: $ {levelGoal.moneyGoal}";
        moneyText.text = "Money: $ ";
        timeText.text = $"Time: ";
        UpdateScore(levelGoal.startingMoney);

    }

    void Update()
    {
        Timer();

    }

    public void Timer()
    {
        if (isFactoryPlaying) timerCount += Time.deltaTime;

        timeText.text = $"Time: {timerCount.ToString("F2")} / {levelGoal.timeLimit}.00";


        if (!finalScoreWorkDone &&
            timerCount >= levelGoal.timeLimit)
        {
            finalScoreWorkDone = true;
            StopFactory();

            FinalScoring();
        }
    }

    private void FinalScoring()
    {
        playPauseButtonImage = ButtonState.Replay;
        uIManager.ButtonChangedToReplay();

        if (money >= levelGoal.moneyGoal)
        {
            uIManager.UpdateStatusPanelText("Success!");
            uIManager.statusPanel.SetActive(true);
            uIManager.CompleteTutorial();
        }
        else
        {
            uIManager.UpdateStatusPanelText("Try again!");
            uIManager.statusPanel.SetActive(true);
        }
    }

    public void UpdateScore(int scoreToAdd = 0)
    {
        money += scoreToAdd;

        moneyText.text = $"Money: ${money}";
    }

    public void PlayFactory()
    {
        playPauseButtonImage = ButtonState.Pause;

        //// Saves money score at round start so that it can be reset to this value later.
        //moneyScoreAtRoundStart = moneyScore;

        finalScoreWorkDone = false;

        foreach (Transform childTransform in FoodSpawnedParent.transform)
        {
            Destroy(childTransform.gameObject);
        }

        isFactoryPlaying = true;
        timerCount = 0f;

        //if (GoalPipe1 != null) GoalPipe1.OpenLid();
        //if (GoalPipe2 != null) GoalPipe2.OpenLid();
        //if (GoalPipe3 != null) GoalPipe3.OpenLid();

    }

    public void StopFactory()
    {
        playPauseButtonImage = ButtonState.Play;

        isFactoryPlaying = false;

        //if (GoalPipe1 != null) GoalPipe1.CloseLid();
        //if (GoalPipe2 != null) GoalPipe2.CloseLid();
        //if (GoalPipe3 != null) GoalPipe3.CloseLid();

    }

    public void StopFacrotyAndReset()
    {
        StopFactory();

        ResetScore();

        foreach (Transform childTransform in FoodSpawnedParent.transform)
        {
            Destroy(childTransform.gameObject);
        }

        uIManager.statusPanel.SetActive(false);

    }

    private void ResetScore()
    {
        int moneySpent = 0;

        foreach(Transform transform in ObjectsBoughtParent.transform)
        {
            var component = transform.gameObject.GetComponent<ClickAndDrag>();
            moneySpent += component.sellPrice;
        }

        money = levelGoal.startingMoney - moneySpent;

        UpdateScore();
    }

    public float SpeedSliderMultiplier()
    {
        return speedSlider.value;
    }

}

public enum ButtonState
{
    Play,
    Pause,
    Replay
}