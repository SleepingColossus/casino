using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    public Button wager10Button;
    public Button wager100Button;
    public Button wager1000Button;

    private readonly Color _activeColor = Color.yellow;
    private readonly Color _inactiveColor = Color.white;

    public void ToggleColor(Button b)
    {
        b.image.color = b.image.color == _inactiveColor ? _activeColor : _inactiveColor;
    }

    public void SetButtonState(int remainingBalance)
    {
        wager1000Button.interactable = remainingBalance >= 1000;
        wager100Button.interactable = remainingBalance >= 100;
        wager10Button.interactable = remainingBalance >= 10;
    }
}
