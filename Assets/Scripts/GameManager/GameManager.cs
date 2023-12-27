using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private BaseGameState currentState;
    private GameState currentGameState;
    [HideInInspector] public GameState previousGameState;
    PlayingGameState playingGameState = new PlayingGameState();
    OnMenuGameState onMenuGameState = new OnMenuGameState();
    OnPauseGameState onPauseGameState = new OnPauseGameState();
    OnLevelUpGameState onLevelUpGameState = new OnLevelUpGameState();
    OnDieGameState onDieGameState = new OnDieGameState();


    public static Action<GameState> OnGameStateChanged;

    public float scrollingSpeed;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }

        DontDestroyOnLoad(gameObject);
        ChangeGameState(GameState.Playing);
    }

    public void ChangeGameState(GameState gameState)
    {
        BaseGameState newState = gameState switch
        {
            GameState.OnMenu => onMenuGameState,
            GameState.Playing => playingGameState,
            GameState.OnPause => onPauseGameState,
            GameState.OnLevelUp => onLevelUpGameState,
            GameState.OnDie => onDieGameState,
            _ => onMenuGameState
        };

        currentState?.OnStateExit(this);
        previousGameState = currentGameState;
        currentState = newState;
        currentGameState = gameState;
        currentState?.OnStateEnter(this);

        OnGameStateChanged?.Invoke(gameState);
    }
}

public enum GameState
{
    OnMenu,
    Playing,
    OnLevelUp,
    OnPause,
    OnDie
}