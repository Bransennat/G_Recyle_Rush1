using System.Collections;
using UnityEngine;

public class TrashSpawner : MonoBehaviour
{
    [Header("Trash Folder Paths")]
    public string organikFolder = "Trash/Organik";
    public string anorganikFolder = "Trash/Anorganik";

    private GameObject[] organikPrefabs;
    private GameObject[] anorganikPrefabs;

    [Header("Spawn Settings")]
    public float spawnInterval = 2f;
    public float spawnIntervalDecreaseAmount = 0.1f;
    public float minSpawnInterval = 0.5f;

    [Header("Spawn Area")]
    public Collider2D spawnArea;

    private float currentInterval;
    private int trashCount = 0;
    private GameManager gameManager;

    void Start()
    {
        gameManager = Object.FindFirstObjectByType<GameManager>();
        currentInterval = spawnInterval;

        // Load prefabs from respective folders
        organikPrefabs = Resources.LoadAll<GameObject>(organikFolder);
        anorganikPrefabs = Resources.LoadAll<GameObject>(anorganikFolder);

        if (organikPrefabs.Length == 0 || anorganikPrefabs.Length == 0)
        {
            Debug.LogError("Organik or Anorganik prefabs not found! Please check the folders.");
        }

        StartCoroutine(SpawnTrashRoutine());
    }

    IEnumerator SpawnTrashRoutine()
    {
        while (true)
        {
            SpawnTrash();
            yield return new WaitForSeconds(currentInterval);
        }
    }

    void SpawnTrash()
    {
        if (organikPrefabs.Length == 0 || anorganikPrefabs.Length == 0) return;

        // Randomly choose category
        bool spawnOrganik = Random.value > 0.5f;

        GameObject[] selectedArray = spawnOrganik ? organikPrefabs : anorganikPrefabs;

        int index = Random.Range(0, selectedArray.Length);

        Bounds bounds = spawnArea.bounds;
        float spawnX = Random.Range(bounds.min.x, bounds.max.x);
        float spawnY = bounds.max.y;

        Vector2 spawnPosition = new Vector2(spawnX, spawnY);

        Instantiate(selectedArray[index], spawnPosition, Quaternion.identity);

        trashCount++;
    }

    public void IncreaseSpawnRate()
    {
        currentInterval = Mathf.Max(minSpawnInterval, currentInterval - spawnIntervalDecreaseAmount);
    }
}
