using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    public void OnStartButton()
    {
        SceneManager.LoadScene("Gameplay");
    }

    public void OnLeaderboardButton()
    {
        SceneManager.LoadScene("Leaderboard");
    }

    public void LoadSceneTrashInfo()
    {
        SceneManager.LoadScene("TrashInfo");
    }
}