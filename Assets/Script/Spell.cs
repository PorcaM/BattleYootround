using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell : MonoBehaviour {
    public enum Type { Attack,Assist,Special};
    public Type type;
    private int id;

    public int Id
    {
        get
        {
            return id;
        }

        set
        {
            id = value;
        }
    }

    public void Init()
    {
        type = Type.Attack;
        Id = 0;
    }
}
