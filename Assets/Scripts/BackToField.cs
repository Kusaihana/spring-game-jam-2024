using UnityEngine;

public class BackToField : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            CanvasManager.Instance.UnloadCanvas("RestaurantCanvas");
            CanvasManager.Instance.LoadCanvas("FieldCanvas");
            TimerManager.Instance.StartTimer();
        }
    }
}
