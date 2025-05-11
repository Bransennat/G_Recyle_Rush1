using UnityEngine;
using UnityEngine.SceneManagement; // Untuk Scene Management

public class SceneLoader : MonoBehaviour
{
    // Fungsi ini akan dipanggil ketika tombol ditekan
    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu"); // Memuat scene dengan nama "MainMenu"
    }
}