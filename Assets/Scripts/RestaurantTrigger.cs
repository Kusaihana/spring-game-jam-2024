using DefaultNamespace;
using UnityEngine;

public class RestaurantTrigger : MonoBehaviour, ITouchable
{
    public GameObject informationPrompt;

    private GameObject _informationPromptInstance;

    private bool _canEnterRestaurant = false;

    private void TimeIsUp()
    {
        EnterRestaurant();
    }
    private void Start()
    {
        TimerManager.OnTimerFinished += TimeIsUp;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _canEnterRestaurant)
        {
            EnterRestaurant();
        }
    }

    private void EnterRestaurant()
    {
        CanvasManager.Instance.UnloadCanvas("FieldCanvas");
        CanvasManager.Instance.LoadCanvas("RestaurantCanvas");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.tag.Equals("PlayerDetectionSphere"))
            return;

        _canEnterRestaurant = true;
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (!other.tag.Equals("PlayerDetectionSphere"))
            return;

        _canEnterRestaurant = false;
    }

    public void ShowPrompt()
    {
        _informationPromptInstance = InfoDisplay.Instance.AddUIElement(informationPrompt);
    }

    public void HidePrompt()
    {
        if(_informationPromptInstance)
            Destroy(_informationPromptInstance);
    }
}
