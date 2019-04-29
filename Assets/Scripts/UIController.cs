using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public Button wager10Button;
    public Button wager100Button;
    public Button wager1000Button;

    public Button toggleMusicButton;
    public Image toggleMusicIcon;
    public Button toggleSoundButton;
    public Image toggleSoundIcon;

    public Sprite musicOnSprite;
    public Sprite musicOffSprite;
    public Sprite soundOnSprite;
    public Sprite soundOffSprite;

    private readonly Color _activeColor = Color.yellow;
    private readonly Color _inactiveColor = Color.white;

    private readonly Color _winColor = Color.green;
    private readonly Color _loseColor = Color.red;

    public Text balanceText;
    public Text balanceChangeIndicatorText;

    private const string BalanceTextLabel = "Balance:";

    private void Update()
    {
        var color = balanceChangeIndicatorText.color;

        if (color.a > 0)
        {
            balanceChangeIndicatorText.color = new Color(color.r, color.g, color.b, color.a -0.01f);
        }
    }

    public void ToggleColor(Button b)
    {
        b.image.color = b.image.color == _inactiveColor ? _activeColor : _inactiveColor;
        ToggleIcon(b, b.image.color == _activeColor);
    }

    private void ToggleIcon(Button b, bool isOn)
    {
        if (b == toggleMusicButton)
        {
            toggleMusicIcon.sprite = isOn ? musicOffSprite : musicOnSprite;
        }

        if (b == toggleSoundButton)
        {
            toggleSoundIcon.sprite = isOn ? soundOffSprite : soundOnSprite;
        }
    }

    public void UpdateUIState(int balance, int delta)
    {
        UpdateButtonState(balance);
        UpdateBalanceText(balance);
        UpdateBalanceChangeIndicatorText(delta);
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

    private void UpdateBalanceChangeIndicatorText(int delta)
    {
        if (delta > 0)
        {
            balanceChangeIndicatorText.text = $"+{delta}";
            balanceChangeIndicatorText.color = _winColor;
        }
        else
        {
            balanceChangeIndicatorText.text = delta.ToString();
            balanceChangeIndicatorText.color = _loseColor;
        }
    }
}
