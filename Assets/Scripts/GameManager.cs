using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private SoundController soundController;
    [SerializeField] private int _startWarriorsCount = 0;
    private int _warriorsCount;
    private int _totalWarriorsCount;
    [SerializeField] private int _warriorsCost = 4;
    [SerializeField] private int _warriorsWheatEat = 2;

    [SerializeField] private int _startFermersCount = 0;
    private int _fermersCount;
    private int _totalFermersCount;
    [SerializeField] private int _fermersCost = 2;
    [SerializeField] private int _fermersWheatEat = 1;
    [SerializeField] private int _fermersProduction = 2;

    [SerializeField] private int _startWheatCount = 2;
    private int _wheatCount;
    private int _totalWheatCount;
    [SerializeField] Button _addWarriorButton;
    [SerializeField] private Image _warriorButtonTimerSprite;
    [SerializeField] private int _warriorsCreateDelay = 4;
    private float _warriorsCurrTimer = -2;

    [SerializeField] Button _addFermersButton;
    [SerializeField] private Image _fermersButtonTimerSprite;
    [SerializeField] private int _fermersCreateDelay = 2;
    private float _fermersCurrTimer = -2;

    [SerializeField] Timer _cycleTimer;
    [SerializeField] private int _startAttackCycle = 3;
    [SerializeField] private float _enemyFactorCycle = 1.5f;
    [SerializeField] UnityEvent onInfoChange;
    private int _cycleCount;

    [SerializeField] private Canvas _endCanvas;
    [SerializeField] private TextMeshProUGUI _endTextTitle;
    [SerializeField] private TextMeshProUGUI _endTextInfo;

    [SerializeField] private TextMeshProUGUI _winTextInfo;
    [SerializeField] private int _winCountFermers = 100;
    [SerializeField] private int _winCountWheat = 500;
    [SerializeField] private int _winCountCycle = 10;

    [SerializeField] private Button _timeScaleButtonStop;
    [SerializeField] private Button _timeScaleButtonSpeedX1;
    [SerializeField] private Button _timeScaleButtonSpeedX2;

    public int WarriorsCount { 
        get { return _warriorsCount; } 
        private set { 
            _warriorsCount = value;
            onInfoChange.Invoke();
        }
    }
    public int FermersCount { 
        get { return _fermersCount; }
        private set
        {
            _fermersCount = value;
            onInfoChange.Invoke();
        }
    }
    public int WheatCount { 
        get { return _wheatCount; }
        private set
        {
            _wheatCount = value;
            onInfoChange.Invoke();
            CheckButton();
        }
    }
    public int TotalConsumprion { get { return _warriorsCount * _warriorsWheatEat + _fermersCount * _fermersWheatEat; } }
    public int TotalProduction { get { return _fermersProduction * _fermersCount; } }
    public int CycleCount { get { return _cycleCount; } }

    private void Start()
    {
        StartGame();
    }

    private void Update()
    {
        if (_warriorsCurrTimer > 0)
        {
            _warriorsCurrTimer -= Time.deltaTime;
            _warriorButtonTimerSprite.fillAmount = _warriorsCurrTimer / _warriorsCreateDelay;
        }
        else if (_warriorsCurrTimer > -1)
        {
            soundController.PlayAudioSpawnWarrior();

            WarriorsCount++;
            _totalWarriorsCount++;

            _warriorsCurrTimer = -2;
            CheckButton();
        }
        if (_fermersCurrTimer > 0)
        {
            _fermersCurrTimer -= Time.deltaTime;
            _fermersButtonTimerSprite.fillAmount = _fermersCurrTimer / _fermersCreateDelay;
        }
        else if (_fermersCurrTimer > -1)
        {
            soundController.PlayAudioSpawnFermer();

            FermersCount++;
            _totalFermersCount++;

            _fermersCurrTimer = -2;
            CheckButton();
        }
    }

    public void TimeSet(int multiplier)
    {
        switch (multiplier)
        {
            case 0:
                Time.timeScale = multiplier;
                _timeScaleButtonStop.interactable = false;
                _timeScaleButtonSpeedX1.interactable = true;
                _timeScaleButtonSpeedX2.interactable = true;
                break;
            case 1:
                Time.timeScale = multiplier;
                _timeScaleButtonStop.interactable = true;
                _timeScaleButtonSpeedX1.interactable = false;
                _timeScaleButtonSpeedX2.interactable = true;
                break;
            case 2:
                Time.timeScale = multiplier;
                _timeScaleButtonStop.interactable = true;
                _timeScaleButtonSpeedX1.interactable = true;
                _timeScaleButtonSpeedX2.interactable = false;
                break;
            default:
                break;
        }
    }

    public void StartGame()
    {
        TimeSet(1);
        _cycleCount = 0;
        _cycleTimer.RestartTimer();
        _fermersCount = _startFermersCount;
        _warriorsCount = _startWarriorsCount;
        _wheatCount = _startWheatCount;
        CheckButton();
        onInfoChange.Invoke();
        _endCanvas.enabled = false;

        _winTextInfo.text = $"1. Наличие {_winCountFermers} фермеров\r\n" +
            $"2. Запасы зерна в {_winCountWheat} ед.\r\n" +
            $"3. Пережиты {_winCountCycle} циклов.";
    }

    public void AddWarrior()
    {
        if (_wheatCount >= _warriorsCost)
        {
            _warriorsCurrTimer = _warriorsCreateDelay;
            _addWarriorButton.interactable = false;
            WheatCount -= _warriorsCost;
        }
    }

    public void AddFermers()
    {
        if (_wheatCount >= _fermersCost)
        {
            _fermersCurrTimer = _fermersCreateDelay;
            _addFermersButton.interactable = false;
            WheatCount -= _fermersCost;
        }
    }

    public void NewCycleWheat()
    {
        WheatCount += TotalProduction - TotalConsumprion;

        if (WheatCount < 0)
        {
            GameLose("Поражение от голода!");
        }

        _totalWheatCount += TotalProduction - TotalConsumprion;
    }

    public void GameLose(string title)
    {
        Time.timeScale = 0;
        _endCanvas.enabled = true;

        _endTextTitle.text = title;

        _endTextInfo.text = $"Циклов пережито: {CycleCount} \r\n" +
            $"Всего произведенно зерна: {_totalWheatCount}\r\n" +
            $"Общее количество\r\n" +
            $"Воинов: {_totalWarriorsCount}\r\n" +
            $"Фермеров: {_totalFermersCount}";
    }

    public void GameWin()
    {
        Time.timeScale = 0;
        _endCanvas.enabled = true;

        _endTextTitle.text = $"!!ПОБЕДА!!";

        _endTextInfo.text = $"Циклов пережито: {CycleCount} \r\n" +
            $"Всего произведенно зерна: {_totalWheatCount}\r\n" +
            $"Общее количество\r\n" +
            $"Воинов: {_totalWarriorsCount}\r\n" +
            $"Фермеров: {_totalFermersCount}";
    }

    public void CheckButton()
    {
        if (_warriorsCurrTimer == -2)
        {
            if (_wheatCount < _warriorsCost)
            {
                _addWarriorButton.interactable = false;
            }
            else
            {
                _addWarriorButton.interactable = true;
            }
        }

        if (_fermersCurrTimer == -2)
        {
            if (_wheatCount < _fermersCost)
            {
                _addFermersButton.interactable = false;
            }
            else
            {
                _addFermersButton.interactable = true;
            }
        }
    }

    public string GetNextAttackInfo()
    {
        if (_cycleCount+1 > _startAttackCycle)
        {
            int emenyCount = Mathf.RoundToInt((_cycleCount + 1) * _enemyFactorCycle);

            return $"Количество противников в следующем цикле: {emenyCount}";
        }
        else
        {
            return $"Следующая атака ожидается на: {_startAttackCycle} Цикле.";
        }
    }

    public void NewCycle()
    {
        _cycleCount++;

        if (_cycleCount > _startAttackCycle)
        {
            Attack();
        }

        CheckWinGame();
    }

    private void CheckWinGame()
    {
        if (FermersCount >= _winCountFermers && WheatCount >= _winCountWheat && CycleCount > _winCountCycle)
        {
            GameWin();
        }
    }

    private void Attack()
    {
        int emenyCount = Mathf.RoundToInt(_cycleCount * _enemyFactorCycle);

        soundController.PlayAudioAttack();

        if (_warriorsCount >= emenyCount)
        {
            _warriorsCount -= emenyCount;
        }
        else
        {
            GameLose("Поражение из-за нападения!");
        }
    }
}
