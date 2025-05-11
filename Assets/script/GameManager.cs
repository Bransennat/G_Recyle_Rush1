using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    [Header("Game Stats")]
    public int score = 0;
    public int health = 3;
    public Trash.TrashType currentInstruction;
    private int collectedTrash = 0;

    [Header("UI Elements")]
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI instructionText;

    public bool isGameOver { get; private set; } = false;

    private TrashSpawner trashSpawner;

    void Start()
    {
        trashSpawner = Object.FindFirstObjectByType<TrashSpawner>();
        currentInstruction = (Random.Range(0, 2) == 0) ? Trash.TrashType.Organik : Trash.TrashType.Anorganik;
    }

    void Update()
    {
        UpdateUI();
    }

    public void GenerateInstruction()
    {
        currentInstruction = currentInstruction == Trash.TrashType.Organik
            ? Trash.TrashType.Anorganik
            : Trash.TrashType.Organik;
    }

    public void UpdateScore(int amount)
    {
        if (isGameOver) return;

        score += amount;
        collectedTrash++;

        if (score % 100 == 0 && trashSpawner != null)
        {
            trashSpawner.IncreaseSpawnRate();
            GenerateInstruction();
        }
    }

    public void UpdateHealth(int amount)
    {
        if (isGameOver) return;

        health += amount;

        if (health <= 0)
        {
            EndGame();
        }
    }

    private void EndGame()
    {
        isGameOver = true;
        SaveScore(score);

        GameOverUI gameOver = Object.FindFirstObjectByType<GameOverUI>();
        if (gameOver != null)
        {
            gameOver.ShowGameOver(score);
        }

        Time.timeScale = 0f;
    }

    private void SaveScore(int scoreToSave)
    {
        string json = PlayerPrefs.GetString("ScoreHistory", "{\"entries\":[]}");
        ScoreEntryList scoreList = JsonUtility.FromJson<ScoreEntryList>(json);

        if (scoreList == null)
        {
            scoreList = new ScoreEntryList { entries = new List<ScoreEntry>() };
        }

        ScoreEntry newEntry = new ScoreEntry
        {
            score = scoreToSave,
            date = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
        };
        scoreList.entries.Add(newEntry);

        string newJson = JsonUtility.ToJson(scoreList);
        PlayerPrefs.SetString("ScoreHistory", newJson);
        PlayerPrefs.Save();
    }

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

    void UpdateUI()
    {
        scoreText.text = "Skor: " + score;
        healthText.text = "HP: " + health;
        instructionText.text = "Ambil sampah " + currentInstruction.ToString().ToLower();
    }
}
