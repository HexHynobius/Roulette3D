using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public int number=0;

    private void OnTriggerEnter(Collider other)
    {
        number=Int32.Parse(other.name);
        print(other.name);
    }

    private void OnTriggerExit(Collider other)
    {
        number = -1;
    }
}
