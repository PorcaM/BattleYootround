using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecoratorManager : MonoBehaviour {
    public EnterBattleDecorator enterBattleDecorator;
    public ExitBattleDecorator exitBattleDecorator;

    public void OnEnterBattle()
    {
        enterBattleDecorator.ShowCountdown(3);
    }

    public void OnExitBattle(int winner)
    {
        exitBattleDecorator.ShowVictory(winner);
    }
}
