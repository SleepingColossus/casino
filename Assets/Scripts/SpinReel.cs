using UnityEngine;

public class SpinReel : MonoBehaviour
{
    public float rotationSpeedFast = 1.0f;
    public float rotationSpeedMedium = 1.0f;
    public float rotationSpeedSlow = 1.0f;

    private ReelSpeed _reelSpeed;

    public float numberOfSpins = 1.0f;
    private float _degreesTotal;
    private float _degressLeft;

    private AudioSource _audioSource;
    public float fastPitch;
    public float mediumPitch;
    public float slowPitch;

    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _reelSpeed = ReelSpeed.Idle;
    }

    void Update()
    {
        if (IsSpinning())
        {
            float rotationSpeed;

            if (_reelSpeed == ReelSpeed.Fast)
            {
                rotationSpeed = rotationSpeedFast;
                _audioSource.pitch = fastPitch;
            }
            else if (_reelSpeed == ReelSpeed.Medium)
            {
                rotationSpeed = rotationSpeedMedium;
                _audioSource.pitch = mediumPitch;
            }
            else
            {
                rotationSpeed = rotationSpeedSlow;
                _audioSource.pitch = slowPitch;
            }

            transform.Rotate(0, 0, rotationSpeed);

            _degressLeft -= rotationSpeed;
            _reelSpeed = SlowDown(_reelSpeed);
        }
    }

    public void StartRotation(SymbolType st)
    {
        // reset to original angle;
        transform.Rotate(0, 0, -_degreesTotal);

        _degreesTotal = _degressLeft = Symbol.SymbolAngles[st] + 360 * numberOfSpins;
        _reelSpeed = ReelSpeed.Fast;
        _audioSource.Play();
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
            _audioSource.Stop();
            return ReelSpeed.Idle;
        }
    }
}
