using UnityEngine;
using UnityEngine.EventSystems;

public class UIController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public enum ButtonType { Left, Right, Jump }
    public ButtonType buttonType;

    public PlayerController player;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (buttonType == ButtonType.Left) player.MoveLeft();
        else if (buttonType == ButtonType.Right) player.MoveRight();
        else if (buttonType == ButtonType.Jump) player.Jump();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (buttonType == ButtonType.Left || buttonType == ButtonType.Right)
            player.StopMoving();
    }
}
