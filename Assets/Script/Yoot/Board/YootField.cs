using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YootField : MonoBehaviour
{
    public int id;
    public List<Horse> guests;
    public HorseRoute.Type milestone;
    private DecoEnterBattle deco;

    public void Init(int id)
    {
        this.id = id;
        guests = new List<Horse>();
        milestone = HorseRoute.Type.Summer;
        deco = GameObject.Find("Decos").GetComponent<DecoEnterBattle>();
        deco.action = new System.Action(EnterBattle);
    }

    public bool IsEmpty()
    {
        return guests.Count == 0;
    }

    public bool IsGoal()
    {
        return this == YootBoard.GetStartPoint();
    }

    public Horse Guest(int i)
    {
        return guests[i];
    }

    public void Leave(Horse horse)
    {
        guests.Remove(horse);
    }

    public void Arrive(Horse horse)
    {
        CheckIn(horse);
        SetMilestone(horse);
        if (IsEncounter())
            HandleEncounter(horse);
        else
            RequestTurnEnd(horse);
    }

    private void CheckIn(Horse horse)
    {
        guests.Add(horse);
        horse.currField = this;
    }

    private void SetMilestone(Horse horse)
    {
        if (milestone != HorseRoute.Type.Summer)
            horse.routeType = milestone;
    }

    private bool IsEncounter()
    {
        return guests.Count > 1;
    }

    private void HandleEncounter(Horse horse)
    {
        if (horse.IsEnemyWith(Guest(0)))
        {
            if(!YootGame.isNetwork)
                ReadyBattle();
            else
            {
                GameObject[] tmp = GameObject.FindGameObjectsWithTag("TurnProcessor");
                Debug.Log("---tmp[0]---");
                Debug.Log(tmp[0].GetComponent<TurnProcessor>().currentState);
                Debug.Log(tmp[0].GetComponent<TurnProcessor>().owner.playerID);
                Debug.Log("---tmp[1]---");
                Debug.Log(tmp[1].GetComponent<TurnProcessor>().currentState);
                Debug.Log(tmp[1].GetComponent<TurnProcessor>().owner.playerID);

                if (tmp[0].GetComponent<TurnProcessor>().currentState != TurnProcessor.ProcessState.Wait && tmp[0].GetComponent<TurnProcessor>().owner.playerID == 0)
                {
                    Debug.Log("Network battle Occur!!");
                    bool check = BYClient.myClient.Send(BYMessage.MyMsgType.BattleOccur, new BYMessage.EmptyMessage());
                    Debug.Log("Send state : " + check);
                }
                else if(tmp[1].GetComponent<TurnProcessor>().currentState != TurnProcessor.ProcessState.Wait && tmp[1].GetComponent<TurnProcessor>().owner.playerID == 0)
                {
                    Debug.Log("Network battle Occur!!");
                    bool check = BYClient.myClient.Send(BYMessage.MyMsgType.BattleOccur, new BYMessage.EmptyMessage());
                    Debug.Log("Send state : " + check);
                }
                else
                {
                    Debug.Log("Network battle occur, but waiting battle occur message from server...");
                }
            }
        }
        else
        {
            horse.CarryHorse(Guest(0));
            RequestTurnEnd(horse);
        }
    }

    private void RequestTurnEnd(Horse horse)
    {
        horse.owner.turnProcessor.RecvEnd();
    }

    public void ReadyBattle()
    {
        const float timerInterval = 1.5f;
        deco.action = new System.Action(EnterBattle);
        deco.OnEnterBattle(timerInterval);
    }

    private void EnterBattle()
    {
        BattleGame battleGame = GameObject.Find("BattleGame").GetComponent<BattleGame>();
        battleGame.StartGame(this);
    }

    public void RetireHorsesButWinner(int winner)
    {
        foreach (Horse horse in guests)
        {
            if (horse.IsOwner(winner) == false)
            {
                horse.Defeat();
                RequestTurnEnd(horse);
            }
        }
    }
}
