using UnityEngine;

[ExecuteAlways]
public class AutoStretchBackground : MonoBehaviour
{
    public Camera targetCamera;
    public SpriteRenderer spriteRenderer;

    void Start()
    {
        if (targetCamera == null)
            targetCamera = Camera.main;

        if (spriteRenderer == null)
            spriteRenderer = GetComponent<SpriteRenderer>();

        FitToScreen();
    }

    void Update()
    {
#if UNITY_EDITOR
        FitToScreen(); // Supaya kelihatan di editor juga
#endif
    }

    void FitToScreen()
    {
        if (targetCamera == null || spriteRenderer == null)
            return;

        float screenHeight = targetCamera.orthographicSize * 2f;
        float screenWidth = screenHeight * targetCamera.aspect;

        Vector2 spriteSize = spriteRenderer.sprite.bounds.size;

        Vector3 scale = transform.localScale;
        scale.x = screenWidth / spriteSize.x;
        scale.y = screenHeight / spriteSize.y;

        transform.localScale = scale;
    }
}
