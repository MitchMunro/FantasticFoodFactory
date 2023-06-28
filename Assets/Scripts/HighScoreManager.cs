using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighScoreManager : MonoBehaviour
{
    public string[] HSKeyLevel1 { get; private set; } = {
        "Level1_HS1",
        "Level1_HS2",
        "Level1_HS3",
        "Level1_HS4"};

    public string[] HSKeyLevel2 { get; private set; } = {
        "Level2_HS1",
        "Level2_HS2",
        "Level2_HS3",
        "Level2_HS4"};

    public string[] HSKeyLevel3 { get; private set; } = {
        "Level3_HS1",
        "Level3_HS2",
        "Level3_HS3",
        "Level3_HS4"};

    public string[] HSKeyLevel4 { get; private set; } = {
        "Level4_HS1",
        "Level4_HS2",
        "Level4_HS3",
        "Level4_HS4"};

    public string[] HSKeyLevel5 { get; private set; } = {
        "Level5_HS1",
        "Level5_HS2",
        "Level5_HS3",
        "Level5_HS4"};

    public int[] HSLevel1 { get; private set; } = { 0, 0, 0, 0};
    public int[] HSLevel2 { get; private set; } = { 0, 0, 0, 0};
    public int[] HSLevel3 { get; private set; } = { 0, 0, 0, 0};
    public int[] HSLevel4 { get; private set; } = { 0, 0, 0, 0};
    public int[] HSLevel5 { get; private set; } = { 0, 0, 0, 0};


    private void Awake()
    {
        LoadHS();
    }


    public void LoadHS()
    {
        LoadLevelHS(HSKeyLevel1, HSLevel1);
        LoadLevelHS(HSKeyLevel2, HSLevel2);
        LoadLevelHS(HSKeyLevel3, HSLevel3);
        LoadLevelHS(HSKeyLevel4, HSLevel4);
        LoadLevelHS(HSKeyLevel5, HSLevel5);

    }

    private void LoadLevelHS(string[] keys, int[] scoreList)
    {
        for (int i = 0; i < keys.Length; i++)
        {
            scoreList[i] = PlayerPrefs.GetInt(keys[i]);
        }
    }


    public void SaveHS(int level, int newScore)
    {
        switch (level)
        {
            case 1:
                SaveLevelHS(level, HSLevel1, newScore);
                break;
            case 2:
                SaveLevelHS(level, HSLevel2, newScore);
                break;
            case 3:
                SaveLevelHS(level, HSLevel3, newScore);
                break;
            case 4:
                SaveLevelHS(level, HSLevel4, newScore);
                break;
            default:
                Debug.Log("Level number doesn't exist.");
                break;
        }
    }

    private void SaveLevelHS(int level, int[] scoreList, int newScore)
    {
        if (newScore > scoreList[scoreList.Length -1])
        {
            scoreList[scoreList.Length -1] = newScore;

            // Sort the high scores array in descending order
            Array.Sort(scoreList);
            Array.Reverse(scoreList);

            switch (level)
            {
                case 1:

                    for(int i = 0; i < HSKeyLevel1.Length; i++)
                    {
                        PlayerPrefs.SetInt(HSKeyLevel1[i], scoreList[i]);
                    }
                    break;

                case 2:
                    for (int i = 0; i < HSKeyLevel2.Length; i++)
                    {
                        PlayerPrefs.SetInt(HSKeyLevel2[i], scoreList[i]);
                    }
                    break;

                case 3:
                    for (int i = 0; i < HSKeyLevel3.Length; i++)
                    {
                        PlayerPrefs.SetInt(HSKeyLevel3[i], scoreList[i]);
                    }
                    break;

                case 4:

                    for (int i = 0; i < HSKeyLevel4.Length; i++)
                    {
                        PlayerPrefs.SetInt(HSKeyLevel4[i], scoreList[i]);
                    }
                    break;

            }

            PlayerPrefs.Save();

        }
    }

    public int[] GetLevelHighScores(int levelNumber)
    {
        switch (levelNumber)
        {
            case 1:
                return HSLevel1;

            case 2:
                return HSLevel2;

            case 3:
                return HSLevel3;

            case 4:
                return HSLevel4;

            case 5:
                return HSLevel5;

            default:
                return new int[] { -1, -1, -1, -1 };
        }
    }
    

}
