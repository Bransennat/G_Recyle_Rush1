using System.Collections;
using UnityEngine;

public class Trash : MonoBehaviour
{
    public enum TrashType { Organik, Anorganik }
    public TrashType trashType;

    private bool hasTouchedGround = false;

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Jika sampah menyentuh tanah dan belum pernah menyentuh sebelumnya j
        if (!hasTouchedGround && collision.gameObject.CompareTag("Ground"))
        {
            hasTouchedGround = true;
            StartCoroutine(DestroyAfterDelay(2f)); // Hancurkan setelah 2 detik
        }
    }

    IEnumerator DestroyAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }
}
