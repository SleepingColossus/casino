using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class SpinReel : MonoBehaviour
{
    public float rotationSpeedFast = 1.0f;
    public float rotationSpeedMedium = 1.0f;
    public float rotationSpeedSlow = 1.0f;

    private ReelSpeed _reelSpeed;

    public float numberOfSpins = 1.0f;
    private float _degreesTotal;
    private float _degressLeft;
    
    // Start is called before the first frame update
    void Start()
    {
        _reelSpeed = ReelSpeed.Idle;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsSpinning())
        {
            float rotationSpeed;

            if (_reelSpeed == ReelSpeed.Fast)
            {
                rotationSpeed = rotationSpeedFast;
            }
            else if (_reelSpeed == ReelSpeed.Medium)
            {
                rotationSpeed = rotationSpeedMedium;
            }
            else
            {
                rotationSpeed = rotationSpeedSlow;
            }

            transform.Rotate(0, 0, rotationSpeed);

            _degressLeft -= rotationSpeed;
            _reelSpeed = SlowDown(_reelSpeed);
        }
    }

    public void StartRotation(SymbolType st)
    {
        _degreesTotal = _degressLeft = Symbol.SymbolAngles[st] + 360 * numberOfSpins;
        _reelSpeed = ReelSpeed.Fast;
    }

    public bool IsSpinning()
    {
        return _reelSpeed != ReelSpeed.Idle;
    }

    private ReelSpeed SlowDown(ReelSpeed rs)
    {
        if(_degressLeft > _degreesTotal * 0.3)
        {
            return ReelSpeed.Fast;
        }
        else if(_degressLeft > _degreesTotal * 0.1)
        {
            return ReelSpeed.Medium;
        }
        else if(_degressLeft > 0)
        {
            return ReelSpeed.Slow;
        }
        else
        {
            return ReelSpeed.Idle;
        }
    }
}
