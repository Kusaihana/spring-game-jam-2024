using System;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestaurantTrigger : MonoBehaviour, ITouchable
{
    public GameObject informationPrompt;

    private GameObject informationPromptInstance;

    private bool canEnterRestaurant = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && canEnterRestaurant)
        {
            EnterRestaurant();
        }
    }

    private void EnterRestaurant()
    {
        
        SceneManager.LoadScene("Restaurant");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.tag.Equals("PlayerDetectionSphere"))
            return;

        canEnterRestaurant = true;
        Debug.Log("in restaurant zone");
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (!other.tag.Equals("PlayerDetectionSphere"))
            return;

        canEnterRestaurant = false;
        Debug.Log("out of restaurant zone");
    }

    public void ShowPrompt()
    {
        informationPromptInstance = InfoDisplay.Instance.AddUIElement(informationPrompt);
    }

    public void HidePrompt()
    {
        if(informationPromptInstance)
            Destroy(informationPromptInstance);
    }
}
