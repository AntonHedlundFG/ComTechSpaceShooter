using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMovement : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        TakeInput();
    }

    private void TakeInput()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            Accelerate();
        }

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

        if (Input.GetKey(KeyCode.Space))
        {

        }
    }

    private void Rotate(int rotationDir)
    {
        throw new NotImplementedException();
    }

    private void Accelerate()
    {
        throw new NotImplementedException();
    }
}
