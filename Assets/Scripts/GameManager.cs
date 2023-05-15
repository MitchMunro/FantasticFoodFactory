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
    public UIManager uIManager;

    public float sliderMin = 0.5f;
    public float sliderMax = 4f;
    private float scaledSliderValue = 1;

    public float timerCount { get; private set; }
    public int money;
    private int moneyScoreAtRoundStart;

    public LevelGoal levelGoal;

    //This is used to check if the play button has been clicked, and therefore if the factory is playing or not
    public bool isFactoryPlaying { get; private set; }
    public ButtonState buttonState { get; private set; } = ButtonState.Play;

    public GameObject FoodSpawnedParent;
    public GameObject ObjectsBoughtParent;


    private bool finalScoreWorkDone = false;

    public GameObject[] FoodList;
    public GameObject sandwich;


    private GameObject selectedObject;
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
        uIManager.SetTextMoneyGoal($"Goal: $ {levelGoal.moneyGoal}");
        uIManager.SetTextMoney("Money: $ ");
        uIManager.SetTextTimer($"Time: ");
        UpdateScore(levelGoal.startingMoney);

        scaledSliderValue = uIManager.SpeedSliderValue();

    }

    void Update()
    {
        Timer();
        SelectAndMoveItems();
    }

    private void SelectAndMoveItems()
    {
        if (GameManager.Instance.isFactoryPlaying) return;

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
        }

        //Everything below this is only called if there is a selected object.
        if (selectedObject == null) return;

        var component = selectedObject.GetComponent<FactoryObject>();

        if (component != null)
        {
            component.HighlightActivate();
        }

        // If an object is selected, drag it with the mouse
        if (Input.GetMouseButton(0))
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            selectedObject.transform.position = new Vector3(mousePosition.x, mousePosition.y, selectedObject.transform.position.z);
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

    public void Timer()
    {
        if (isFactoryPlaying) timerCount += Time.deltaTime;

        uIManager.SetTextTimer($"Time: {timerCount.ToString("F2")} / {levelGoal.timeLimit}.00");


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
        buttonState = ButtonState.Replay;
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

        uIManager.SetTextMoney($"Money: ${money}");
    }

    public void PlayFactory()
    {
        buttonState = ButtonState.Pause;

        //// Saves money score at round start so that it can be reset to this value later.
        //moneyScoreAtRoundStart = moneyScore;

        finalScoreWorkDone = false;

        foreach (Transform childTransform in FoodSpawnedParent.transform)
        {
            Destroy(childTransform.gameObject);
        }

        isFactoryPlaying = true;
        timerCount = 0f;

    }

    public void StopFactory()
    {
        buttonState = ButtonState.Play;

        isFactoryPlaying = false;

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

}

public enum ButtonState
{
    Play,
    Pause,
    Replay,
    Cross
}
