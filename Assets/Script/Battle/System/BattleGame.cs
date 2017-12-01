using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleGame : MonoBehaviour
{
    public enum State { Idle, Standby, OnBattle, Finished }
    public State state;
    public CombatManager combatManager;
    public SpellManager spellManager;
    public BattleUIManager battleUIManager;
    public DecoratorManager decoratorManager;
    public YootGame yootGame;

    [SerializeField] private YootField battleField;
    [SerializeField] private int winner;

    private static BattleGame instance;
    public static BattleGame Instance()
    {
        if (!instance)
            instance = GameObject.Find("BattleGame").GetComponent<BattleGame>();
        return instance;
    }
    
    public void Init()
    {
        combatManager.Init();
        spellManager.Init();
        state = State.Idle;
    }

    public void StartGame(YootField battleField)
    {
        this.battleField = battleField;
        StartGame();
    }

    public void StartGame()
    {
        SoundManager.Instance().PlayMusic(1);
        battleUIManager.OnEnterBattle();
        combatManager.Setup();
        decoratorManager.OnEnterBattle();
        state = State.Standby;
        StartCoroutine(StartActionAfterSeconds(StartBattle, 3.0f));
    }
    
    private void StartBattle()
    {
        combatManager.StartBattle();
        state = State.OnBattle;
    }

    public void FinishBattle(int winner)
    {
        this.winner = winner;
        decoratorManager.OnExitBattle(winner);
        StartCoroutine(StartActionAfterSeconds(FinishGame, 3.0f));
        state = State.Finished;
    }

    private void FinishGame()
    {
        SoundManager.Instance().PlayMusic(0);
        battleUIManager.OnExitBattle();
        combatManager.CleanupBattle();
        spellManager.Cleanup();
        SendResult();
        state = State.Idle;
    }

    private void SendResult()
    {
        if (battleField)
        {
            battleField.RetireHorsesButWinner(winner);
            yootGame.HandleBattleResult(winner);
        }
    }

    private IEnumerator StartActionAfterSeconds(Action action, float duration)
    {
        yield return new WaitForSeconds(duration);
        action.Invoke();
    }
}
