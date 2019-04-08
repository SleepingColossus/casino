using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonStyler : MonoBehaviour
{
    public Button wager10Button;
    public Button wager100Button;
    public Button wager1000Button;

    void Awake()
    {
        wager100Button.image.color = Color.yellow;
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
            button.image.color = Color.white;
        }

        b.image.color = Color.yellow;
    }
}
