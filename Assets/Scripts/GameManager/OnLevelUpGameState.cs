using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnLevelUpGameState : BaseGameState
{
    public override void OnStateEnter(GameManager gameManager)
    {
        GameUIHandler.Instance.ToggleLevelUpScreen();
        Time.timeScale = 0;
    }

    public override void OnStateExit(GameManager gameManager)
    {
        Time.timeScale = 1;
    }

    public override void StateUpdate(GameManager gameManager)
    {
    }
}
