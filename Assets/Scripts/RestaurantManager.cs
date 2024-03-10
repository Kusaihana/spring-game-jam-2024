using System;
using System.Text;
using TMPro;
using UnityEngine;

public class RestaurantManager : MonoBehaviour
{
    public GameObject PlayerCollectGameObject;

    public TextMeshProUGUI OrderText;

    private void Awake()
    {
        TimerManager.OnTimerFinished += ShowCollected;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowCollected()
    {
        PlayerCollect playerCollect = PlayerCollectGameObject.GetComponent<PlayerCollect>();

        StringBuilder sb = new StringBuilder();
        sb.AppendLine("--Mushrooms Collected--");
        foreach (var item in playerCollect.CurrentlyHeldItems)
        {
            sb.AppendLine(item.Value + " " + item.Key);
        }

        OrderText.text = sb.ToString();
    }
}
