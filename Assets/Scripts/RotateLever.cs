using UnityEngine;

public class RotateLever : MonoBehaviour
{
    private LeverState _leverState;
    private const float _rotateBy = 90;
    private float _remainingRotation;

    public float rotationSpeed;

    public AudioClip leverSound;
    private AudioSource _leverSource;


    void Start()
    {
        _leverState = LeverState.Idle;
        _remainingRotation = _rotateBy;
        _leverSource = GetComponent<AudioSource>();
        _leverSource.clip = leverSound;
    }

    void Update()
    {
        if (_leverState != LeverState.Idle)
        {

            var rotationDirection = _leverState == LeverState.Down ? -1 : 1;

            float yAngle = rotationSpeed * rotationDirection;
            transform.Rotate(0, yAngle, 0);

            _remainingRotation -= rotationSpeed;

            if (_remainingRotation <= 0)
            {
                _leverState = ChangeDirection(_leverState);
                _remainingRotation = _rotateBy;
            }
        }
    }

    public void PullLever()
    {
        _leverSource.Play();
        _leverState = LeverState.Down;
    }

    private LeverState ChangeDirection(LeverState ls)
    {
        if(ls == LeverState.Down)
        {
            return LeverState.Up;
        }

        return LeverState.Idle;
    }
}
