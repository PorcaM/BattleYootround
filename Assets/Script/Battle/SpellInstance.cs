using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellInstance : MonoBehaviour {
    private Spell mySpell;
    private float cooldown;
    private float ratio;

    public float Cooldown
    {
        get
        {
            return cooldown;
        }

        set
        {
            cooldown = value;
            if (cooldown < 0.0f)
                cooldown = 0.0f;
        }
    }

    public float Ratio
    {
        get
        {
            return ratio;
        }

        set
        {
            ratio = value;
        }
    }

    void Start()
    {
        mySpell = new Spell();
        mySpell.Init();
    }

    void Update()
    {
        Cooldown -= Time.deltaTime;
        Ratio = Cooldown / mySpell.Cooltime;
    }

    public void Activate()
    {
        if (Cooldown <= 0.0f)
        {
            Debug.Log(mySpell.SpellName + " Activate");
            Cooldown = mySpell.Cooltime;
        }
        else
        {
            Debug.Log("cooldown: " + Cooldown);
        }
    }
}
