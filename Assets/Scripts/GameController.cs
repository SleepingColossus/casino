using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public SymbolType[] debugMatches;
    private Queue<SymbolType> _debugQueue;

    private bool _readyForReward;
    private Reward _reward;

    public int initialBalance;
    private int _balance;
    private int _wager;

    public MotionController motionController;
    public UIController uiController;
    public CoinSpawner coinSpawner;
    public AudioController audioController;
    public GameOverController gameOverController;

    private void Awake()
    {
        _debugQueue = new Queue<SymbolType>(debugMatches);
        UpdateBalance(initialBalance);
    }

    public void Update()
    {
        if (_readyForReward && CanSpin)
        {
            UpdateBalance(_wager * _reward.SteakMultiplier);
            coinSpawner.Spawn(_reward.NumberOfCoins);
            audioController.PlayMatchSound();
            _readyForReward = false;
        }

        if (_balance <= 0 && CanSpin)
        {
            gameOverController.EndGame();
        }
    }

    public void Spin(int wager)
    {
        _wager = wager;

        if (CanSpin)
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

            motionController.InitiateMotion(new []{leftReelSymbol, midReelSymbol, rightReelSymbol});

            UpdateBalance(-_wager);
        }
    }

    // true if no reels are spinning
    private bool CanSpin => !motionController.AreReelsSpinning();

    private void SetReward(SymbolType s)
    {
        _readyForReward = true;
        _reward = Symbol.SymbolMetadata[s].Reward;

        audioController.SetMatchSound(s);
    }

    private void UpdateBalance(int amount)
    {
        _balance += amount;
        uiController.UpdateUIState(_balance, amount);
    }
}
