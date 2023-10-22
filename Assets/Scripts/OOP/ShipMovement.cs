using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMovement : MonoBehaviour
{

    [SerializeField] private Vector3 _currentMovementVector = Vector3.zero;
    [SerializeField] [Range(10.0f, 1000.0f)] private float _maxMovementSpeed = 25.0f;
    [SerializeField] [Range(10.0f, 1000.0f)] private float _movementAccelerationRate = 50.0f;
    [SerializeField] [Range(0.0f, 100.0f)] private float _movementDrag = 4.0f;

    [SerializeField] private float _currentRotationSpeed = 0.0f;
    [SerializeField] [Range(10.0f, 1000.0f)] private float _maxRotationSpeed = 200.0f;
    [SerializeField] [Range(10.0f, 1000.0f)] private float _rotationAccelerationRate = 700.0f;
    [SerializeField] [Range(0.0f, 100.0f)] private float _rotationDrag = 5.0f;

    void Update()
    {
        TakeInput();
        ApplyDrag();
        PerformMove();
        PerformRotation();
    }

    private void PerformRotation()
    {
        _currentRotationSpeed = Mathf.Clamp(_currentRotationSpeed, -_maxRotationSpeed, _maxRotationSpeed);

        transform.Rotate(new Vector3(0,0, _currentRotationSpeed * Time.deltaTime));
    }

    private void PerformMove()
    {
        if (_currentMovementVector.magnitude > _maxMovementSpeed)
        {
            _currentMovementVector = _currentMovementVector.normalized * _maxMovementSpeed;
        }

        transform.position += _currentMovementVector * Time.deltaTime;
    }

    private void ApplyDrag()
    {
        _currentRotationSpeed *= (1.0f - _rotationDrag * Time.deltaTime);
        _currentMovementVector *= (1.0f - _movementDrag * Time.deltaTime);
    }

    private void TakeInput()
    {
        //rotationDir == -1 means left turn, == 1 means right turn.
        int rotationDir = 0;
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            rotationDir++;
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            rotationDir--;
        }
        Rotate(rotationDir);

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            Accelerate();
        }
    }

    private void Rotate(int rotationDir)
    {
        if (rotationDir == 0) return;
        _currentRotationSpeed += _rotationAccelerationRate * Time.deltaTime * rotationDir;
    }

    private void Accelerate()
    {
        _currentMovementVector += _movementAccelerationRate * Time.deltaTime * transform.right;
    }
}
