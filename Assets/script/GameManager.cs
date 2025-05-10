using UnityEngine;
using TMPro;

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
        // Initial instruction chosen randomly
        currentInstruction = (Random.Range(0, 2) == 0) ? Trash.TrashType.Organik : Trash.TrashType.Anorganik;
    }

    void Update()
    {
        UpdateUI();
    }

    public void GenerateInstruction()
    {
        // Alternate instruction between Organik and Anorganik
        if (currentInstruction == Trash.TrashType.Organik)
            currentInstruction = Trash.TrashType.Anorganik;
        else
            currentInstruction = Trash.TrashType.Organik;
    }

    public void UpdateScore(int amount)
    {
        if (isGameOver) return;

        int oldScore = score;
        score += amount;
        collectedTrash++;

        // Check if score is exactly a multiple of 100
        if (score % 100 == 0)
        {
            if (trashSpawner != null)
            {
                trashSpawner.IncreaseSpawnRate();
                GenerateInstruction();
            }
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
        Debug.Log("GameManager: EndGame called. Health reached zero.");
        isGameOver = true;

        SaveScore(score);

        GameOverUI gameOver = Object.FindFirstObjectByType<GameOverUI>();
        if (gameOver != null)
        {
            Debug.Log("GameManager: Found GameOverUI, calling ShowGameOver.");
            gameOver.ShowGameOver(score);
        }
        else
        {
            Debug.LogWarning("GameManager: GameOverUI not found in scene.");
        }

        Time.timeScale = 0f;
    }

    private void SaveScore(int scoreToSave)
    {
        // Load existing scores JSON string
        string json = PlayerPrefs.GetString("ScoreHistory", "[]");
        ScoreEntryList scoreList = JsonUtility.FromJson<ScoreEntryList>(json);

        if (scoreList == null)
        {
            scoreList = new ScoreEntryList();
            scoreList.entries = new System.Collections.Generic.List<ScoreEntry>();
        }

        // Add new score entry with current date
        ScoreEntry newEntry = new ScoreEntry();
        newEntry.score = scoreToSave;
        newEntry.date = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        scoreList.entries.Add(newEntry);

        // Limit to last 100 entries
        if (scoreList.entries.Count > 100)
        {
            scoreList.entries.RemoveAt(0);
        }

        // Save back to PlayerPrefs
        string newJson = JsonUtility.ToJson(scoreList);
        PlayerPrefs.SetString("ScoreHistory", newJson);
        PlayerPrefs.Save();

        Debug.Log("GameManager: Score saved to PlayerPrefs.");
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
        public System.Collections.Generic.List<ScoreEntry> entries;
    }

    void UpdateUI()
    {
        scoreText.text = "Skor: " + score;
        healthText.text = "HP: " + health;
        instructionText.text = "Ambil sampah " + currentInstruction.ToString().ToLower();
    }
}
