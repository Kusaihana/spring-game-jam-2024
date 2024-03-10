using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DefaultNamespace;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class RestaurantManager : MonoBehaviour
{
    public static RestaurantManager Instance { get; private set; }
    
    public GameObject playerCollectGameObject;
    public GameObject welcomeScreen;
    public GameObject instructionsScreen;
    public GameObject riddleScreen;
    public GameObject resultsScreen;
    
    public Screens currentScreen = Screens.Welcome;
    private GameObject _currentScreen;
    private int _currentRiddleIndex = 0;
    private int _playerScore = 0;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); 
        }
        else
        {
            Destroy(gameObject); 
        }
    }
    private void Start()
    {
        TransitionToNextScreen(Screens.Welcome);
    }

    private void Update()
    {
        switch (currentScreen)
        {
            case Screens.Welcome:
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    TransitionToNextScreen(Screens.Instructions);
                }
                break;
            case Screens.Instructions:
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    TransitionToNextScreen(Screens.Riddle);
                }

                break;
            case Screens.Riddle:
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    TransitionToNextScreen(Screens.Field);
                }
                break;
        }
    }

    private void TransitionToNextScreen(Screens nextScreen)
    {
        switch (currentScreen)
        {
            case Screens.Welcome:
                ShowWelcomeScreen(false);
                break;
            case Screens.Instructions:
                ShowInstructionsScreen(false);
                break;
            case Screens.Riddle:
                ShowRiddleScreen(false);
                break;
        }
        switch (nextScreen)
        {
            case Screens.Welcome:
                ShowWelcomeScreen(true);
                break;
            case Screens.Instructions:
                ShowInstructionsScreen(true);
                break;
            case Screens.Riddle:
                ShowRiddleScreen(true);
                break;
            case Screens.Field:
                GoToField();
                break;
           
        }

        currentScreen = nextScreen;
    }
    
    private void ShowWelcomeScreen(bool show)
    {
        welcomeScreen.SetActive(show);
    }
    private void ShowInstructionsScreen(bool show)
    {
        instructionsScreen.SetActive(show);
    }
    private void ShowRiddleScreen(bool show)
    {
        if (show)
        {
            //update riddle text
            TextMeshProUGUI riddleText = riddleScreen.GetComponentInChildren<TextMeshProUGUI>();
            riddleText.text = Dish.TextList[_currentRiddleIndex] + "\n\nReady to gather?\n(spacebar to continue)";
        }
        
        riddleScreen.SetActive(show);
    }
    private void ShowResultsScreen(bool show)
    {
        resultsScreen.SetActive(show);
    }
    private void GoToField()
    {
        TimerManager.OnTimerFinished += ShowCollected;
        CanvasManager.Instance.UnloadCanvas("RestaurantCanvas");
        CanvasManager.Instance.LoadCanvas("FieldCanvas");
        TimerManager.Instance.StartTimer();
    }
    
    public void ShowCollected()
    {
        CanvasManager.Instance.UnloadCanvas("FieldCanvas");
        CanvasManager.Instance.LoadCanvas("RestaurantCanvas");
        
        TimerManager.OnTimerFinished -= ShowCollected;
        PlayerCollect playerCollect = playerCollectGameObject.GetComponent<PlayerCollect>();

        int roundScore = calculatePlayerScore(playerCollect.CurrentlyHeldItems);
        
        TextMeshProUGUI resultsText = resultsScreen.GetComponentInChildren<TextMeshProUGUI>();
        
        resultsText.text = BuildResultsString(playerCollect.CurrentlyHeldItems);


        currentScreen = Screens.Results;
        ShowResultsScreen(true);//
    }

    private int calculatePlayerScore(Dictionary<string, int> playerCollectCurrentlyHeldItems)
    {
        List<CollectionItem> recipeRequirements = Dish.RecipeRequirements[_currentRiddleIndex].ToList();
        int score = 0;
        
        foreach (var requirement in recipeRequirements)
        {
            if (playerCollectCurrentlyHeldItems.ContainsKey(requirement.Name))
            {
                //check amount
                if (requirement.Amount <= playerCollectCurrentlyHeldItems[requirement.Name])
                    score += requirement.Amount;
                else
                    score += playerCollectCurrentlyHeldItems[requirement.Name] - requirement.Amount;
            }
            else
            {
                //they missed an item completely
                score -= requirement.Amount;
            }
        }

        return score;
    }

    private string BuildResultsString(Dictionary<string, int> itemsCollected)
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine("-- Mushrooms Collected --");
        if (itemsCollected.Count == 0)
        {
            sb.AppendLine("No mushrooms collected :(");
        }
        else
        {
            foreach (var item in itemsCollected)
            {
                sb.AppendLine(item.Value + " " + item.Key);
            }
        }

        sb.AppendLine();
        sb.AppendLine("-- Mushrooms Required --");
        sb.AppendLine(Dish.RequirementsList[_currentRiddleIndex]);
        return sb.ToString();
    }
}
