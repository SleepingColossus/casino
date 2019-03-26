using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateLever : MonoBehaviour
{
    private bool _shouldRotate;
    private const float _rotateBy = 90;
    private int _rotationDirection;
    private float _remainingRotation;
    private float _rotationSpeed;
    
    // Start is called before the first frame update
    void Start()
    {
        _shouldRotate = true;
        _rotationDirection = -1;
        _rotationSpeed = 1;
        _remainingRotation = _rotateBy;
    }

    // Update is called once per frame
    void Update()
    {
        if (_shouldRotate)
        {
            float yAngle = _rotationSpeed * _rotationDirection;
            transform.Rotate(0, yAngle, 0);

            _remainingRotation -= _rotationSpeed;

            if (_remainingRotation <= 0)
            {
                _shouldRotate = false;
                _rotationDirection *= -1;
                _remainingRotation = _rotateBy;
            }
        }
    }
}
