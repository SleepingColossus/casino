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
    private ButtonManager _buttonManager;

    public SymbolType[] debugMatches;
    private Queue<SymbolType> _debugQueue;

    private bool _readyForReward;
    private SymbolScore _score;

    public int initialBalance;
    private int _balance;
    public Text balanceText;
    private const string BalanceTextLabel = "Balance:";

    public AudioClip winSound;
    public AudioClip jackPotSound;
    private AudioSource _audioSource;

    private int _wager;
    public Text betLog;

    private void Awake()
    {
        _debugQueue = new Queue<SymbolType>(debugMatches);

        _rotateLever = GameObject.Find("Lever").GetComponent<RotateLever>();
        _reelLeft = GameObject.Find("Reel Left").GetComponent<SpinReel>();
        _reelMid = GameObject.Find("Reel Mid").GetComponent<SpinReel>();
        _reelRight = GameObject.Find("Reel Right").GetComponent<SpinReel>();
        _coinSpawner = GameObject.Find("CoinSpawner").GetComponent<CoinSpawner>();
        _buttonManager = GameObject.Find("ButtonManager").GetComponent<ButtonManager>();
        _wager = 100;

        _audioSource = GetComponent<AudioSource>();
        UpdateBalance(initialBalance);
    }

    public void Update()
    {
        if (_readyForReward && CanSpin())
        {
            UpdateBalance(_wager * _score.SteakMultiplier);
            _coinSpawner.Spawn(_score.NumberOfCoins);
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

            _rotateLever.PullLever();
            _reelLeft.StartRotation(leftReelSymbol);
            _reelMid.StartRotation(midReelSymbol);
            _reelRight.StartRotation(rightReelSymbol);

            UpdateBalance(-_wager);
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
        balanceText.text = $"{BalanceTextLabel} {_balance}";
        betLog.text += $"\n{amount}";
        _buttonManager.SetButtonState(_balance);
    }
}
