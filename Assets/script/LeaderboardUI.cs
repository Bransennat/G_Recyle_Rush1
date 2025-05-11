using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;

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
        string json = PlayerPrefs.GetString("ScoreHistory", "{\"entries\":[]}");
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

        // Sort scores in descending order (highest first)
        scoreEntries.Sort((a, b) => b.score.CompareTo(a.score));

        // Take the top 100 scores
        var topScores = scoreEntries.Take(100).ToList();

        // Display scores with rank numbers
        for (int i = 0; i < topScores.Count; i++)
        {
            ScoreEntry entry = topScores[i];
            GameObject entryGO = Instantiate(scoreEntryPrefab, contentPanel);
            TextMeshProUGUI[] texts = entryGO.GetComponentsInChildren<TextMeshProUGUI>();
            foreach (var text in texts)
            {
                if (text.gameObject.name == "RankText")
                {
                    text.text = (i + 1).ToString(); // Rank (1, 2, 3, ...)
                }
                else if (text.gameObject.name == "ScoreText")
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
