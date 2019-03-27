using UnityEngine;
using UnityEngine.Serialization;

public class GameController : MonoBehaviour
{
    private SpinReel _reelLeft;
    private SpinReel _reelMid;
    private SpinReel _reelRight;

    private bool _spinning = false;

    private void Awake()
    {
        _reelLeft = GameObject.Find("Reel Left").GetComponent<SpinReel>();
        _reelMid = GameObject.Find("Reel Mid").GetComponent<SpinReel>();
        _reelRight = GameObject.Find("Reel Right").GetComponent<SpinReel>();
    }

    public void StartSpinning()
    {
        if (!_spinning)
        {
            Debug.Log("Start Spinning");
            
            _spinning = true;

            var leftReelSymbol = Symbol.PickRandomSymbol();
            var midReelSymbol = Symbol.PickRandomSymbol();
            var rightReelSymbol = Symbol.PickRandomSymbol();

            _reelLeft.StartRotation(leftReelSymbol);
            _reelMid.StartRotation(midReelSymbol);
            _reelRight.StartRotation(rightReelSymbol);
        }
    }
}
