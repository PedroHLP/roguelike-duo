using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLevelUpController : MonoBehaviour
{

    public static PlayerLevelUpController Instance;

    private int currentXP;
    private int currentXpNeededToLevelUp;
    private int currentLevel;

    [SerializeField]
    private List<int> xpNeededToLevelUp;

    public Action<int, int> OnXPChange;
    public Action<int> OnLevelUp;


    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        currentLevel = 0;
        currentXP = 0;
        currentXpNeededToLevelUp = xpNeededToLevelUp[0];
    }

    private void OnEnable()
    {
        GameManager.OnGameStateChanged += ResetXPPerLevelUp;
    }

    private void OnDisable()
    {
        GameManager.OnGameStateChanged -= ResetXPPerLevelUp;
    }

    public void AddXP()
    {
        currentXP++;
        OnXPChange?.Invoke(currentXP, currentXpNeededToLevelUp);

        if (currentXP >= currentXpNeededToLevelUp)
        {
            currentLevel++;
            OnLevelUp?.Invoke(currentLevel);
            GameManager.Instance.ChangeGameState(GameState.OnLevelUp);
        }
    }

    private void ResetXPPerLevelUp(GameState gameState)
    {
        if (gameState == GameState.Playing && GameManager.Instance.previousGameState == GameState.OnLevelUp)
        {
            currentXP -= currentXpNeededToLevelUp;
            currentXpNeededToLevelUp = xpNeededToLevelUp[currentLevel];
            OnXPChange?.Invoke(currentXP, currentXpNeededToLevelUp);
        }
    }

    public int GetCurrentLevel()
    {
        return currentLevel;
    }
}
