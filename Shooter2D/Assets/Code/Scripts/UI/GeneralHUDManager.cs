using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GeneralHUDManager : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _waveText;

    [SerializeField]
    private TMP_Text _playerCreditText;

    [SerializeField]
    private TMP_Text _waveTimerText;

    [SerializeField]
    private TMP_Text _enemiesRemainingText;

    private TimeSpan _waveTimeSpan;

    private void Start()
    {
        GameEvents.Instance.OnWaveBegin += HideWaveUI;
        GameEvents.Instance.OnWaveEnd += ShowWaveUI;
        ShowWaveUI();
    }

    private void ShowWaveUI()
    {
        _waveTimerText.enabled = true;
        _enemiesRemainingText.enabled = false;
    }

    private void HideWaveUI()
    {
        _waveTimerText.enabled = false;
        _enemiesRemainingText.enabled = true;
    }

    private void Update()
    {
        _waveText.text = GameManager.Instance.CurWave.ToString();

        _playerCreditText.text = GameManager.Instance.PlayerCredit.ToString().PadLeft(5, '0');

        _waveTimeSpan = TimeSpan.FromSeconds(GameManager.Instance.WaveTimer);

        _waveTimerText.text = _waveTimeSpan.ToString(@"mm\:ss");

        _enemiesRemainingText.text = GameManager.Instance.RemainingEnemies.ToString();
    }
}