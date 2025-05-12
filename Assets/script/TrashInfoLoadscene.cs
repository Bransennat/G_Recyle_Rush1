using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadMainMenu : MonoBehaviour
{
    // Fungsi untuk memuat scene MainMenu
    public void LoadSceneMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
