using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public Button wager10Button;
    public Button wager100Button;
    public Button wager1000Button;

    private readonly Color _activeColor = Color.yellow;
    private readonly Color _inactiveColor = Color.white;

    public Text balanceText;
    private const string BalanceTextLabel = "Balance:";

    public void ToggleColor(Button b)
    {
        b.image.color = b.image.color == _inactiveColor ? _activeColor : _inactiveColor;
    }

    public void UpdateUIState(int balance)
    {
        UpdateButtonState(balance);
        UpdateBalanceText(balance);
    }

    private void UpdateButtonState(int balance)
    {
        wager1000Button.interactable = balance >= 1000;
        wager100Button.interactable = balance >= 100;
        wager10Button.interactable = balance >= 10;
    }

    private void UpdateBalanceText(int balance)
    {
        balanceText.text = $"{BalanceTextLabel} {balance}";
    }
}
