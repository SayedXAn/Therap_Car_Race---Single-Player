using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;
using System.Collections.Generic;
using System.Collections;

public class LeaderboardUI : MonoBehaviour
{

    /*//This script is for local leaderboard. ***********************************************************************************************************************
    public GameObject entryPrefab; // Prefab for leaderboard entry (assign in Inspector)
    public Transform scrollViewContent; // Content GameObject of the Scroll View (assign in Inspector)
    public GameObject LBUI;
    public GameObject loadingScreen;
    public GameObject mainLB;
    public float loadingTime = 2f;


    void Start()
    {
        LeaderboardManager.CreateLeaderboard("Therap_Car_Race", LeaderboardType.Score);        
    }

    public void AddScoreToLeaderboard(string name, int score)
    {
        LeaderboardManager.AddEntryToBoard("Therap_Car_Race", LeaderboardType.Score, new LeaderboardEntryScore(name, score));
        LeaderboardManager.Save();
    }
    public void ShowLeaderboard()
    {
        GenerateLeaderboard("Therap_Car_Race");
        StartCoroutine(EktuTimeDen());        
    }
    public void HideLeaderboard()
    {
        mainLB.SetActive(false);
        loadingScreen.SetActive(true);
        LBUI.SetActive(false);
    }

    IEnumerator EktuTimeDen()
    {
        LBUI.SetActive(true);
        loadingScreen.SetActive(true);
        yield return new WaitForSeconds(loadingTime);
        loadingScreen.SetActive(false);
        mainLB.SetActive(true);
    }
    public void GenerateLeaderboard(string boardId)
    {
        // Clear existing entries
        foreach (Transform child in scrollViewContent)
        {
            Destroy(child.gameObject);
        }

        // Get the leaderboard
        var leaderboard = LeaderboardManager.GetLeaderboard(boardId);

        if (leaderboard == null || leaderboard.BoardData.Count == 0)
        {
            Debug.LogWarning($"No data found for leaderboard {boardId}");
            return;
        }

        // Sort the entries by score in descending order
        var sortedEntries = leaderboard.BoardData.OrderByDescending(entry => entry.EntryValue).ToList();

        // Create UI entries
        foreach (var entry in sortedEntries)
        {
            var entryUI = Instantiate(entryPrefab, scrollViewContent);
            //var entryText = entryUI.GetComponent<Text>();
            var nameText = entryUI.transform.GetChild(0).GetComponent<TMP_Text>();
            var scoreText = entryUI.transform.GetChild(1).GetComponent<TMP_Text>();

            if (nameText != null)
            {
                nameText.text = entry.EntryName;
                scoreText.text = entry.EntryValue.ToString();
            }
        }
        
    }
    */


}
