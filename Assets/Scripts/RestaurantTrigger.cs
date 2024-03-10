using DefaultNamespace;
using UnityEngine;

public class RestaurantTrigger : MonoBehaviour, ITouchable
{
    public GameObject informationPrompt;

    private GameObject _informationPromptInstance;

    private bool _canEnterRestaurant = false;

    private void TimeIsUp()
    {
        if(RestaurantManager.Instance.currentScreen == Screens.Field)
            EnterRestaurant();
    }
    private void Start()
    {
        TimerManager.OnTimerFinished += TimeIsUp;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _canEnterRestaurant && RestaurantManager.Instance.currentScreen == Screens.Field)
        {
            _canEnterRestaurant = false;
            EnterRestaurant();
        }
    }

    private void EnterRestaurant()
    {
        RestaurantManager.Instance.ShowCollected();
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
