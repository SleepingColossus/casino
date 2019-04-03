using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    private RotateLever _rotateLever;
    private SpinReel _reelLeft;
    private SpinReel _reelMid;
    private SpinReel _reelRight;
    private CoinSpawner _coinSpawner;

    public SymbolType[] debugMatches;
    private Queue<SymbolType> _debugQueue;

    private bool _readyForReward = false;
    private SymbolScore _score = null;

    public int balance = 0;
    public Text balanceText;
    private const string BalanceTextLabel = "Balance:";
    public int currentSteak;

    private void Awake()
    {
        _debugQueue = new Queue<SymbolType>(debugMatches);

        _rotateLever = GameObject.Find("Lever").GetComponent<RotateLever>();
        _reelLeft = GameObject.Find("Reel Left").GetComponent<SpinReel>();
        _reelMid = GameObject.Find("Reel Mid").GetComponent<SpinReel>();
        _reelRight = GameObject.Find("Reel Right").GetComponent<SpinReel>();
        _coinSpawner = GameObject.Find("CoinSpawner").GetComponent<CoinSpawner>();

        UpdateBalance(10000);
    }

    public void Update()
    {
        if (_readyForReward && CanSpin())
        {
            UpdateBalance(currentSteak * _score.SteakMultiplier);
            _coinSpawner.Spawn(_score.NumberOfCoins);
            _readyForReward = false;
        }
    }

    public void StartSpinning()
    {
        if (CanSpin())
        {
            SymbolType leftReelSymbol;
            SymbolType midReelSymbol;
            SymbolType rightReelSymbol;

            if (_debugQueue.Count > 0)
            {
                var s = _debugQueue.Dequeue();

                leftReelSymbol = midReelSymbol = rightReelSymbol = s;
            }
            else
            {
                leftReelSymbol = Symbol.PickRandomSymbol();
                midReelSymbol = Symbol.PickRandomSymbol();
                rightReelSymbol = Symbol.PickRandomSymbol();
            }
            
            if (leftReelSymbol == midReelSymbol && leftReelSymbol == rightReelSymbol)
            {
                SetReward(leftReelSymbol);
            }

            _rotateLever.PullLever();
            _reelLeft.StartRotation(leftReelSymbol);
            _reelMid.StartRotation(midReelSymbol);
            _reelRight.StartRotation(rightReelSymbol);

            UpdateBalance(-currentSteak);
        }
    }

    // true if no reels are spinning
    private bool CanSpin()
    {
        return
            !(
                _reelLeft.IsSpinning() ||
                _reelMid.IsSpinning() ||
                _reelRight.IsSpinning()
            );
    }

    private void SetReward(SymbolType s)
    {
        _readyForReward = true;
        _score = Symbol.SymbolValues[s];
    }

    private void UpdateBalance(int amount)
    {
        balance += amount;
        balanceText.text = $"{BalanceTextLabel} {balance}";
    }
}
