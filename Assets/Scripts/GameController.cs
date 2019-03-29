using UnityEngine;
using UnityEngine.Serialization;

public class GameController : MonoBehaviour
{
    private SpinReel _reelLeft;
    private SpinReel _reelMid;
    private SpinReel _reelRight;

    private void Awake()
    {
        _reelLeft = GameObject.Find("Reel Left").GetComponent<SpinReel>();
        _reelMid = GameObject.Find("Reel Mid").GetComponent<SpinReel>();
        _reelRight = GameObject.Find("Reel Right").GetComponent<SpinReel>();
    }

    public void StartSpinning()
    {
        if (CanSpin())
        {
            var leftReelSymbol = Symbol.PickRandomSymbol();
            var midReelSymbol = Symbol.PickRandomSymbol();
            var rightReelSymbol = Symbol.PickRandomSymbol();
            
            Debug.Log($"{leftReelSymbol} - {midReelSymbol} - {rightReelSymbol}");

            _reelLeft.StartRotation(leftReelSymbol);
            _reelMid.StartRotation(midReelSymbol);
            _reelRight.StartRotation(rightReelSymbol);
        }
    }

    // true if no reels are spinning
    private bool CanSpin()
    {
        return
            !
            _reelLeft.IsSpinning() ||
            _reelMid.IsSpinning() ||
            _reelRight.IsSpinning();
    }
}
