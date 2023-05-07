using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TutorialPanelMain : MonoBehaviour
{
    public GameObject previousPanel;
    public GameObject nextPanel;

    public List<GameObject> associatedGameObjects;

    public Button buttonNext;
    public Button buttonBack;

    private void Start()
    {
        buttonBack = transform.Find("ButtonBack")?.GetComponent<Button>();
        buttonNext = transform.Find("ButtonNext")?.GetComponent<Button>();

        if (buttonBack != null) buttonBack.onClick.AddListener(ButtonBack);
        if (buttonNext != null) buttonNext.onClick.AddListener(ButtonNext);

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

    public void ButtonBack()
    {
        if (previousPanel != null)
        {
            DeactivateAssociatedGameObjects();
            this.gameObject.SetActive(false);
            previousPanel.SetActive(true);
        }
    }

    public void ButtonNext()
    {
        if (nextPanel != null)
        {
            DeactivateAssociatedGameObjects();
            this.gameObject.SetActive(false);
            nextPanel.SetActive(true);
        }

    }



}
