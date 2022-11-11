using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InfoPanel : MonoBehaviour
{
    [SerializeField] GameManager _gameManager;
    [SerializeField] TextMeshProUGUI _warriorsCountText;
    [SerializeField] TextMeshProUGUI _fermersCountText;
    [SerializeField] TextMeshProUGUI _wheatCountText;
    [SerializeField] TextMeshProUGUI _totalConsumprionText;
    [SerializeField] TextMeshProUGUI _totalProductionText;
    [SerializeField] TextMeshProUGUI _nextAttackInfoText;

    private int _warriorsCount;
    private int _fermersCount;

    private int _wheatCount;
    private int _totalConsumprion;
    private int _totalProduction;

    private void Start()
    {
        NewCycle();
    }

    public void InfoChange()
    {
        UpdateInfo();
    }
    public void NewCycle()
    {
        _nextAttackInfoText.text = _gameManager.GetNextAttackInfo();
    }

    private void UpdateInfo()
    {
        _warriorsCount = _gameManager.WarriorsCount;
        _fermersCount = _gameManager.FermersCount;
        _totalProduction = _gameManager.TotalProduction;
        _wheatCount = _gameManager.WheatCount;
        _totalConsumprion = _gameManager.TotalConsumprion;
        DisplayInfo();
    }

    private void DisplayInfo()
    {
        _warriorsCountText.text = _warriorsCount.ToString();
        _fermersCountText.text = _fermersCount.ToString();
        _wheatCountText.text = _wheatCount.ToString();
        _totalConsumprionText.text = _totalConsumprion.ToString();
        _totalProductionText.text = _totalProduction.ToString();
    }
}
