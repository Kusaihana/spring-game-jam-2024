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
    public GameObject endScreen;
    public GameObject pictureHappyResult;
    public GameObject pictureMehResult;
    public GameObject pictureSadResult;
    public GameObject player;
    public GameObject playerSpawn;
    
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
            case Screens.Results:
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    
                    _currentRiddleIndex++;
                    if (_currentRiddleIndex < Dish.RecipeRequirements.Length)
                    {
                        //no more riddles...end game
                        TransitionToNextScreen(Screens.Riddle);
                    }
                    else
                    {
                        TransitionToNextScreen(Screens.End);
                    }
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
            case Screens.Results:
                ShowResultsScreen(false);
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
            case Screens.End:
                ShowEndScreen(true);
                break;
           
        }

        currentScreen = nextScreen;
    }

    private void ShowEndScreen(bool show)
    {
        endScreen.SetActive(show);
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
        player.transform.position = playerSpawn.transform.position;
        
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

        int roundPercentScore = CalculatePlayerScorePercent(playerCollect.CurrentlyHeldItems);
        
        TextMeshProUGUI resultsText = resultsScreen.GetComponentInChildren<TextMeshProUGUI>();
        
        resultsText.text = BuildResultsString(playerCollect.CurrentlyHeldItems, roundPercentScore);

        showFace(roundPercentScore);
        
        currentScreen = Screens.Results;
        ShowResultsScreen(true);//
        
        
        
    }

    private void showFace(int roundPercentScore)
    {
        pictureHappyResult.SetActive(false);
        pictureMehResult.SetActive(false);
        pictureSadResult.SetActive(false);
        
        //show face
        if (roundPercentScore >= 90)
        {
            pictureHappyResult.SetActive(true);
        }else if (roundPercentScore >= 50)
        {
            pictureMehResult.SetActive(true);
        }
        else
        {
            pictureSadResult.SetActive(true);
        }
    }
    private int CalculatePlayerScorePercent(Dictionary<MushroomType, int> playerCollectCurrentlyHeldItems)
    {
        List<CollectionItem> recipeRequirements = Dish.RecipeRequirements[_currentRiddleIndex].ToList();
        int score = 0;
        int total = 0;
        foreach (var requirement in recipeRequirements)
        {
            if (playerCollectCurrentlyHeldItems.ContainsKey(requirement.Id))
            {
                score += Math.Clamp(playerCollectCurrentlyHeldItems[requirement.Id], 0, requirement.Amount);
            }
            

            total += requirement.Amount;
        }

        return (int)((float)score/total * 100);
    }

    private string BuildResultsString(Dictionary<MushroomType, int> itemsCollected, int roundScorePercent)
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
                sb.AppendLine(item.Value + " " + MushroomTypeNames.myEnumDescriptions[item.Key]);
            }
        }

        sb.AppendLine();
        sb.AppendLine("-- Mushrooms Required --");
        var recipeRequirements = Dish.RecipeRequirements[_currentRiddleIndex];
        foreach (var recipeRequirement in recipeRequirements)
        {
            sb.AppendLine(recipeRequirement.Amount + " " + recipeRequirement.Id);
        }

        sb.AppendLine();
        sb.AppendLine("-- Accuracy --");
        sb.AppendLine(roundScorePercent + "%");

        sb.AppendLine("Spacebar to get next recipe.");
        return sb.ToString();
    }
}
