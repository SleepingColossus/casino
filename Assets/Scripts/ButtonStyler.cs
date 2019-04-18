using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonStyler : MonoBehaviour
{
    public Button wager10Button;
    public Button wager100Button;
    public Button wager1000Button;

    public Button muteMusicButton;
    public Button muteSoundButton;

    private readonly Color _activeColor = Color.yellow;
    private readonly Color _inactiveColor = Color.white;
    
    
    void Awake()
    {
        wager100Button.image.color = _activeColor;
    }

    public void AdjustColor(Button b)
    {
        var allButtons = new Button[]
            {
                wager10Button,
                wager100Button,
                wager1000Button,
            };

        foreach (var button in allButtons)
        {
            button.image.color = _inactiveColor;
        }

        b.image.color = _activeColor;
    }

    public void ToggleColor(Button b)
    {
        b.image.color = b.image.color == _inactiveColor ? _activeColor : _inactiveColor;
    }
}
