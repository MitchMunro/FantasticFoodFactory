using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using static Unity.Burst.Intrinsics.X86.Avx;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public bool isMainMenu = false;
    public bool isTutorialLevel1;
    public UIManager uIManager;
    public HighScoreManager highScoreManager;

    public float sliderMin = 0.5f;
    public float sliderMax = 4f;
    private float scaledSliderValue = 1;

    public float timerCount { get; private set; }
    public float extraTimeCountdown { get; private set; }
    public int extraTimeLimit = 10;
    public int extraTimeVisibleFrom = 5;
    private int extraTimeInt;
    public int money;
    private int moneyScoreAtRoundStart;

    public LevelGoal levelGoal;

    //This is used to check if the play button has been clicked, and therefore if the factory is playing or not
    public ButtonState buttonState { get; private set; } = ButtonState.Play;
    public GameState gameState { get; private set; } = GameState.Building;

    public GameObject FoodSpawnedParent;
    public GameObject ObjectsBoughtParent;

    // Food GameObjects get added to this is in Awake()
    public List<GameObject> FoodSpawned = new List<GameObject>();


    private bool timerExpiresWorkDone = false;

    public GameObject[] FoodList;
    public GameObject sandwich;


    private GameObject selectedObject;
    Vector3 selectedObjectOffset;
    private float rotateSpeed = 100f;

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

        FoodSpawnedParent = transform.Find("FoodSpawned").gameObject;

    }

    private void Start()
    {
        uIManager.SetTextMoneyGoal($"Goal: $ {levelGoal.Star1Goal}");
        uIManager.SetTextMoney("Money: $ ");
        uIManager.SetTextTimer($"0.00/{levelGoal.timeLimit}.00");
        UpdateScore(levelGoal.startingMoney);

        scaledSliderValue = uIManager.SpeedSliderValue();

        SetHSPanel();

    }

    private void SetHSPanel()
    {
        if (highScoreManager == null) return;
        if (isTutorialLevel1) return;

        //Make all level Goal stars black
        uIManager.BlackenStar(1);
        uIManager.BlackenStar(2);
        uIManager.BlackenStar(3);

        uIManager.SetStarScores(levelGoal.Star1Goal, levelGoal.Star2Goal,
            levelGoal.Star3Goal);

        //Set High scores
        RetrieveHighScores(levelGoal.LevelNumber);

        //Set Last score to 0
        uIManager.SetLastScore(0);

        CheckStars();


    }

    private void CheckStars()
    {
        int[] levelHS = highScoreManager.GetLevelHighScores(levelGoal.LevelNumber);

        if (levelHS[0] >= levelGoal.Star1Goal)
        {
            uIManager.HighlightStar(1);
        }

        if (levelHS[0] >= levelGoal.Star2Goal)
        {
            uIManager.HighlightStar(2);
        }

        if (levelHS[0] >= levelGoal.Star3Goal)
        {
            uIManager.HighlightStar(3);
        }
    }

    private void RetrieveHighScores(int levelNumber)
    {
        switch (levelNumber)
        {
            case 0:
                uIManager.SetHighScores(
                    highScoreManager.HSLevelTut[0],
                    highScoreManager.HSLevelTut[1],
                    highScoreManager.HSLevelTut[2],
                    highScoreManager.HSLevelTut[3]);
                break;
            case 1:
                uIManager.SetHighScores(
                    highScoreManager.HSLevel1[0],
                    highScoreManager.HSLevel1[1],
                    highScoreManager.HSLevel1[2],
                    highScoreManager.HSLevel1[3]);
                break;

            case 2:
                uIManager.SetHighScores(
                    highScoreManager.HSLevel2[0],
                    highScoreManager.HSLevel2[1],
                    highScoreManager.HSLevel2[2],
                    highScoreManager.HSLevel2[3]);
                break;

            case 3:
                uIManager.SetHighScores(
                    highScoreManager.HSLevel3[0],
                    highScoreManager.HSLevel3[1],
                    highScoreManager.HSLevel3[2],
                    highScoreManager.HSLevel3[3]);
                break;

            case 4:
                uIManager.SetHighScores(
                    highScoreManager.HSLevel4[0],
                    highScoreManager.HSLevel4[1],
                    highScoreManager.HSLevel4[2],
                    highScoreManager.HSLevel4[3]);
                break;

            case 5:
                uIManager.SetHighScores(
                    highScoreManager.HSLevel5[0],
                    highScoreManager.HSLevel5[1],
                    highScoreManager.HSLevel5[2],
                    highScoreManager.HSLevel5[3]);
                break;

            default:
                Debug.Log("Incorrect Level number given.");
                break;

        }
    }


    void Update()
    {
        Timer();
        SelectAndMoveItems();
    }

    private void SelectAndMoveItems()
    {
        if (isFactoryPlayingAtAll())
        {
            DeselectObject();
            return;
        }

        if (Input.GetMouseButtonDown(0)) // Left mouse button clicked
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);
            if (hit.collider != null && hit.collider.CompareTag("DraggableObject"))
            {
                // Store the selected object
                selectedObject = hit.collider.attachedRigidbody.gameObject;

            }
            else
            {
                DeselectObject();
            }
        }

        //Everything below this is only called if there is a selected object.
        if (selectedObject == null) return;

        var component = selectedObject.GetComponent<FactoryObject>();

        if (component != null)
        {
            component.HighlightActivate();
        }


        if (Input.GetMouseButtonDown(0))
        {
            // Calculate the offset from the mouse to the center of the GameObject. 
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 objectCenter = selectedObject.transform.position;

            selectedObjectOffset = mousePosition - objectCenter;

        }

        // If an object is selected, drag it with the mouse
        if (Input.GetMouseButton(0))
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            Vector3 pos = new Vector3(mousePosition.x - selectedObjectOffset.x, mousePosition.y - selectedObjectOffset.y, selectedObject.transform.position.z);

            selectedObject.transform.position = pos;
        }


        if (Input.GetKey(KeyCode.Q))
        {
            selectedObject.transform.Rotate(Vector3.forward * rotateSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.E))
        {
            selectedObject.transform.Rotate(-Vector3.forward * rotateSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.D))
        {
            if (component != null)
            {
                component.SellObject();
            }

        }

    }

    private void DeselectObject()
    {
        // Deselect the object if something else is clicked
        if (selectedObject != null)
        {
            var comp = selectedObject.GetComponent<FactoryObject>();

            if (comp != null)
            {
                comp.HighlightDeactivate();
            }
        }
        selectedObject = null;
    }

    public void Timer()
    {
        if (!timerExpiresWorkDone &&
        timerCount >= levelGoal.timeLimit)
        {
            timerExpiresWorkDone = true;

            gameState = GameState.FactoryPlayingExtraTime;

            TimerExpires();

            extraTimeInt = extraTimeLimit;
        }

        if (gameState == GameState.FactoryPlayingTimer)
        {
            timerCount += Time.deltaTime;
            uIManager.SetTextTimer($"{timerCount.ToString("F2")}/{levelGoal.timeLimit}.00");
        }
        else if (gameState == GameState.FactoryPlayingExtraTime)
        {
            extraTimeCountdown -= Time.deltaTime;
            HandleExtraTime();
        }

    }

    private void TimerExpires()
    {
        extraTimeCountdown = extraTimeLimit;
        gameState = GameState.FactoryPlayingExtraTime;
    }

    

    private void HandleExtraTime()
    {
        // 1s after the timer gets to 0
        if (extraTimeInt > -1 && FoodSpawnedParent.transform.childCount > 0)
        {
            if (extraTimeCountdown < extraTimeInt)
            {
                //Debug.Log(extraTimeInt);

                // Only displays the extra time count down if it is lower than the extraTimeVisibleFrom variable.
                if (extraTimeInt <= extraTimeVisibleFrom)
                {
                    uIManager.DisplayCountdownNumber(extraTimeInt);

                }
                extraTimeInt--;
            }
        }
        else
        {
            FinalScoring();
            StopFacrotyAndReset();
        }
    }

    private void FinalScoring()
    {
        gameState = GameState.Building;

        buttonState = ButtonState.Replay;
        uIManager.ButtonChangedToReplay();

        if (!isTutorialLevel1)
        {
            if (highScoreManager != null) highScoreManager.SaveHS(levelGoal.LevelNumber, money);
            RetrieveHighScores(levelGoal.LevelNumber);
            uIManager.SetLastScore(money);
            CheckStars();
            uIManager.OpenHighScorePanel();

        }

        if (money >= levelGoal.Star1Goal)
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

        uIManager.SetTextMoney($"Money: ${money}");
    }

    public void PlayFactory()
    {
        buttonState = ButtonState.Pause;

        //// Saves money score at round start so that it can be reset to this value later.
        //moneyScoreAtRoundStart = moneyScore;

        timerExpiresWorkDone = false;

        foreach (Transform childTransform in FoodSpawnedParent.transform)
        {
            Destroy(childTransform.gameObject);
        }

        gameState = GameState.FactoryPlayingTimer;
        timerCount = 0f;

    }

    public void StopFacrotyAndReset()
    {
        buttonState = ButtonState.Play;
        uIManager.ButtonChangedToPlay();

        gameState = GameState.Building;

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
            var component = transform.gameObject.GetComponent<FactoryObject>();
            moneySpent += component.buyPrice;
        }

        money = levelGoal.startingMoney - moneySpent;

        UpdateScore();
    }

    public float SpeedSliderMultiplier()
    {
        scaledSliderValue = Mathf.Lerp(sliderMin, sliderMax, uIManager.SpeedSliderValue());
        return scaledSliderValue;
    }

    public void NoBuildZoneViolated()
    {
        buttonState = ButtonState.Cross;
        uIManager.ButtonChangedToCross();
    }

    public void NoBuildZoneUnViolated()
    {
        buttonState = ButtonState.Play;
        uIManager.ButtonChangedToPlay();

    }
    /// <summary>
    /// Returns true if Gamestate == FactoryPlayingTimer, or FactoryPlayingExtraTime.
    /// </summary>
    /// <returns></returns>
    public bool isFactoryPlayingAtAll()
    {
        if (gameState == GameState.FactoryPlayingTimer ||
            gameState == GameState.FactoryPlayingExtraTime)
            return true;
        else return false;
    }

}

public enum ButtonState
{
    Play,
    Pause,
    Replay,
    Cross
}

public enum GameState
{
    FactoryPlayingTimer,
    FactoryPlayingExtraTime,
    Building,

}
