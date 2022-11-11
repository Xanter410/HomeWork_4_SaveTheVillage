using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int _warriorsCount = 0;
    [SerializeField] private int _warriorsCost = 4;
    [SerializeField] private int _warriorsWheatEat = 2;
    [SerializeField] private int _fermersCount = 0;
    [SerializeField] private int _fermersCost = 2;
    [SerializeField] private int _fermersWheatEat = 1;
    [SerializeField] private int _fermersProduction = 2;
    [SerializeField] private int _wheatCount = 2;
    [SerializeField] private int _startAttackCycle = 3;
    [SerializeField] private float _enemyFactorCycle = 1.5f;
    [SerializeField] Button _addWarriorButton;
    [SerializeField] Button _addFermersButton;
    [SerializeField] UnityEvent onInfoChange;
    private int _cycleCount;

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
        CheckButton();
        onInfoChange.Invoke();
    }

    public void AddWarrior()
    {
        if (_wheatCount >= _warriorsCost)
        {
            WheatCount -= _warriorsCost;
            WarriorsCount += 1;
        }
    }

    public void AddFermers()
    {
        if (_wheatCount >= _fermersCost)
        {
            WheatCount -= _fermersCost;
            FermersCount = FermersCount + 1;
        }
    }

    public void NewCycleWheat()
    {
        WheatCount += TotalProduction - TotalConsumprion;
    }

    public void GameLose()
    {

    }
    public void CheckButton()
    {
        if (_wheatCount < _warriorsCost)
        {
            _addWarriorButton.interactable = false;
        }
        else
        {
            _addWarriorButton.interactable = true;
        }

        if (_wheatCount < _fermersCost)
        {
            _addFermersButton.interactable = false;
        }
        else
        {
            _addFermersButton.interactable = true;
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
    }

    private void Attack()
    {
        int emenyCount = Mathf.RoundToInt(_cycleCount * _enemyFactorCycle);

        if (_warriorsCount >= emenyCount)
        {
            _warriorsCount -= emenyCount;
        }
        else
        {
            GameLose();
        }
    }



}
