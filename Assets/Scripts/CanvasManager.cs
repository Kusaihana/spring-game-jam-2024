using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    public static CanvasManager Instance = new CanvasManager();


    private void Awake()
    {
        List<Canvas> canvasList = GetComponentsInChildren<Canvas>().ToList();
    }

    public void LoadCanvas()
    {
        
    }
}
