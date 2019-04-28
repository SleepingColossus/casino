using System.Linq;
using UnityEngine;

public class MotionController : MonoBehaviour
{
    public RotateLever lever;
    public SpinReel[] reels;

    public void InitiateMotion(SymbolType[] symbols)
    {
        lever.PullLever();

        for (int i = 0; i < reels.Length; i++)
        {
            reels[i].StartRotation(symbols[i]);
        }
    }

    public bool AreReelsSpinning()
    {
        // true if at least one is spinning
        return reels.Aggregate(false, (acc, r) => acc || r.IsSpinning());
    }
}
