using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimerManager : MonoBehaviour
{
    public static TimerManager Instance { get; private set; }
    public float time = 120;
    private float _timeRemaining;
    private bool _timerStarted;
    [SerializeField] private TextMeshProUGUI _timerText;
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

    private void Update()
    {
        if (!_timerStarted)
            return;
        
        _timeRemaining -= Time.deltaTime;
        

        if (_timeRemaining < 0)
        {
            _timerStarted = false;
            _timeRemaining = 0;
        }

        _timerText.text = "Timer: " + (int)_timeRemaining;
    }

    public void StartTimer()
    {
        _timeRemaining = time;
        _timerStarted = true;
    }
}
