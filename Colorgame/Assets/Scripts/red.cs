﻿using UnityEngine;

public class red : MonoBehaviour
{
    public static bool complete;
   
    private void OnCollisionEnter(Collision other)
    {
        if (other.transform.name == "機器人")
        {
            complete = true;
        }
    }
}
