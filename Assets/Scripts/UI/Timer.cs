using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField] private float _maxTime = 15;
    [SerializeField] private float _currentTime;
    [SerializeField] private bool IsRunning = true;
    [SerializeField] private bool IsLoop = false;
    [SerializeField] private TextMeshProUGUI _timeText;
    [SerializeField] private Image _timerSprite;
    [SerializeField] GameManager _gameManager;
    [SerializeField] TextMeshProUGUI _CycleCountText;
    [SerializeField] public UnityEvent onTimeOver;

    private void Start()
    {
        _currentTime = _maxTime;
    }

    private void Update()
    {
        if (IsRunning)
        {
            if (_currentTime > 0)
            {
                _currentTime -= Time.deltaTime;
                DisplayTime(_currentTime);
            }
            else
            {
                onTimeOver.Invoke();

                if (_CycleCountText != null)
                {
                    UpdateCycleText();
                }

                if (IsLoop)
                {
                    _currentTime = _maxTime;
                }
                else
                {
                    IsRunning = false;
                }
            }
        }
    }
    public void UpdateCycleText()
    {
        _CycleCountText.text = $"Цикл: {_gameManager.CycleCount}";
    }

    public void RestartTimer()
    {
        IsRunning = true;
        _currentTime = _maxTime;
        UpdateCycleText();
    }

    private void DisplayTime(float timeToDisplay)
    {
        if (_timeText != null)
        {
            float minutes = Mathf.FloorToInt(timeToDisplay / 60);
            float seconds = Mathf.FloorToInt(timeToDisplay % 60);
            _timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }

        if (_timerSprite != null)
        {
            _timerSprite.fillAmount = _currentTime / _maxTime;
        }
    }
}
