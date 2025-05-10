using UnityEngine;

public class PlayerTrigger : MonoBehaviour
{
    public PlayerController playerController;

    void OnTriggerEnter2D(Collider2D other)
    {
        Trash trash = other.GetComponent<Trash>();
        if (trash != null)
        {
            if (trash.trashType == playerController.gameManager.currentInstruction)
            {
                playerController.gameManager.UpdateScore(10);
                // Removed instruction change here to fix user issue
                // playerController.gameManager.GenerateInstruction(); // Opsional: Ganti instruksi setelah benar
            }
            else
            {
                playerController.gameManager.UpdateHealth(-1);
            }

            Destroy(other.gameObject);
        }
    }
}
