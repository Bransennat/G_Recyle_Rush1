using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LeaderboardUI : MonoBehaviour
{
    [System.Serializable]
    private class ScoreEntry
    {
        public int score;
        public string date;
    }

    [System.Serializable]
    private class ScoreEntryList
    {
        public List<ScoreEntry> entries;
    }

    public RectTransform contentPanel;
    public GameObject scoreEntryPrefab;
    public ScrollRect scrollRect;

    private List<ScoreEntry> scoreEntries = new List<ScoreEntry>();

    void Start()
    {
        LoadScores();
        PopulateLeaderboard();
    }

    void LoadScores()
    {
        string json = PlayerPrefs.GetString("ScoreHistory", "[]");
        ScoreEntryList scoreList = JsonUtility.FromJson<ScoreEntryList>(json);

        if (scoreList != null && scoreList.entries != null)
        {
            scoreEntries = scoreList.entries;
        }
        else
        {
            scoreEntries = new List<ScoreEntry>();
        }
    }

    void PopulateLeaderboard()
    {
        foreach (Transform child in contentPanel)
        {
            Destroy(child.gameObject);
        }

        // Display scores in reverse order (most recent first)
        for (int i = scoreEntries.Count - 1; i >= 0; i--)
        {
            ScoreEntry entry = scoreEntries[i];
            GameObject entryGO = Instantiate(scoreEntryPrefab, contentPanel);
            TextMeshProUGUI[] texts = entryGO.GetComponentsInChildren<TextMeshProUGUI>();
            foreach (var text in texts)
            {
                if (text.gameObject.name == "ScoreText")
                {
                    text.text = entry.score.ToString();
                }
                else if (text.gameObject.name == "DateText")
                {
                    text.text = entry.date;
                }
            }
        }
    }
}
