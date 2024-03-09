using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    public static CanvasManager Instance = new CanvasManager();


    private void Awake()
    {
        GetComponentsInChildren<Canvas>();
    }

    public void LoadCanvas()
    {
        
    }
}
