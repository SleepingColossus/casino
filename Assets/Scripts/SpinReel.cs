using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class SpinReel : MonoBehaviour
{
    public float rotationSpeedFast = 1.0f;
    public float rotationSpeedMedium = 1.0f;
    public float rotationSpeedSlow = 1.0f;
    public float slowdownPeriod = 2.0f;

    private float _slowdownElapsed;
    private ReelSpeed _reelSpeed;
    
    // Start is called before the first frame update
    void Start()
    {
        _slowdownElapsed = 0.0f;
        _reelSpeed = ReelSpeed.Idle;
    }

    // Update is called once per frame
    void Update()
    {
        if (ShouldRotate())
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

            transform.Rotate(0, 0, -rotationSpeed);
            
            _slowdownElapsed += Time.deltaTime;

            Debug.Log(_slowdownElapsed);
            
            if (_slowdownElapsed >= slowdownPeriod)
            {
                _reelSpeed = SlowDown(_reelSpeed);
                _slowdownElapsed = 0;
            }
        }
    }

    public void StartRotation()
    {
        _reelSpeed = ReelSpeed.Fast;
    }

    private bool ShouldRotate()
    {
        return _reelSpeed != ReelSpeed.Idle;
    }

    private ReelSpeed SlowDown(ReelSpeed rs)
    {
        if (rs == ReelSpeed.Fast)
        {
            return ReelSpeed.Medium;
        }

        if (rs == ReelSpeed.Medium)
        {
            return ReelSpeed.Slow;
        }

        return ReelSpeed.Idle;
    }
}
