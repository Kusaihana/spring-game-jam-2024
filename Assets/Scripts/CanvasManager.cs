using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    public static CanvasManager Instance { get; private set; }

    private List<Canvas> canvasList;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); 
            canvasList = GetComponentsInChildren<Canvas>(true).ToList();
        }
        else
        {
            Destroy(gameObject); 
        }
    }

    public void LoadCanvas(string canvasName)
    {
        ManageCanvas(canvasName, true);
    }

    public void UnloadCanvas(string canvasName)
    {
        ManageCanvas(canvasName, false);
    }

    private void ManageCanvas(string canvasName, bool setActive)
    {
        Canvas canvasFound = canvasList.Find(canvas => canvas.name == canvasName);
        if (canvasFound == null)
            return;

        canvasFound.gameObject.SetActive(setActive);
    }
}