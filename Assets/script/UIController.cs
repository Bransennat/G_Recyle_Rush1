using UnityEngine;
using UnityEngine.EventSystems;

public class UIController : MonoBehaviour, IPointerDownHandler
{
    public enum ButtonType { Jump }
    public ButtonType buttonType;

    public PlayerController player;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (buttonType == ButtonType.Jump)
        {
            player.Jump();
        }
    }
}