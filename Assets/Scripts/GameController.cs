using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public SymbolType[] debugMatches;
    private Queue<SymbolType> _debugQueue;

    private bool _readyForReward;
    private SymbolScore _score;

    public int initialBalance;
    private int _balance;

    public AudioClip winSound;
    public AudioClip jackPotSound;
    private AudioSource _audioSource;

    private int _wager;

    public MotionController motionController;
    public UIController uiController;
    public CoinSpawner coinSpawner;

    private void Awake()
    {
        _debugQueue = new Queue<SymbolType>(debugMatches);
        _audioSource = GetComponent<AudioSource>();
        UpdateBalance(initialBalance);
    }

    public void Update()
    {
        if (_readyForReward && CanSpin())
        {
            UpdateBalance(_wager * _score.SteakMultiplier);
            coinSpawner.Spawn(_score.NumberOfCoins);
            _audioSource.Play();
            _readyForReward = false;
        }
    }

    public void Spin(int wager)
    {
        _wager = wager;

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

            motionController.InitiateMotion(new []{leftReelSymbol, midReelSymbol, rightReelSymbol});

            UpdateBalance(-_wager);
        }
    }

    // true if no reels are spinning
    private bool CanSpin()
    {
        return !motionController.AreReelsSpinning();
    }

    private void SetReward(SymbolType s)
    {
        _readyForReward = true;
        _score = Symbol.SymbolValues[s];

        if (s == SymbolType.Dollar)
        {
            _audioSource.clip = jackPotSound;
        }
        else
        {
            _audioSource.clip = winSound;
        }
    }

    private void UpdateBalance(int amount)
    {
        _balance += amount;
        uiController.UpdateUIState(_balance);
    }
}
