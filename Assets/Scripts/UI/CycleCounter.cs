using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CycleCounter : MonoBehaviour
{
    [SerializeField] GameManager _gameManager;
    [SerializeField] TextMeshProUGUI _CycleCountText;

    private void Start()
    {
        UpdateCycleText();
    }

    public void UpdateCycleText()
    {
        _CycleCountText.text = $"Цикл: {_gameManager.CycleCount}";
    }
}
