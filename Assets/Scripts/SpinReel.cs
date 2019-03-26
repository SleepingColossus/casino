using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class SpinReel : MonoBehaviour
{
    public float rotationSpeed = 1.0f;

    private bool _shouldRotate;
    
    // Start is called before the first frame update
    void Start()
    {
        _shouldRotate = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (_shouldRotate)
        {
            transform.Rotate(0, 0, -rotationSpeed);
        }
    }

    public void StartRotation()
    {
        _shouldRotate = true;
    }

    public void StopRotation()
    {
        _shouldRotate = false;
    }
}
