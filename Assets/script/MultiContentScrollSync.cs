using UnityEngine;

public class MultiContentScrollSync : MonoBehaviour
{
    public RectTransform mainContent; // Content yang terhubung ke ScrollRect
    public RectTransform secondaryContent; // Content kedua yang ingin digulir bersamaan

    void Update()
    {
        if (mainContent != null && secondaryContent != null)
        {
            // Sinkronkan posisi vertikal kedua content
            secondaryContent.anchoredPosition = new Vector2(
                secondaryContent.anchoredPosition.x,
                mainContent.anchoredPosition.y
            );
        }
    }
}
