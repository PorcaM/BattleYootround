using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellInstance : MonoBehaviour {
    private Spell mySpell;
    private float cooldown;

    void Start()
    {
        mySpell = new Spell();
        mySpell.Init();
    }

    void Update()
    {
        cooldown -= Time.deltaTime;
    }

    public void Activate()
    {
        if (cooldown <= 0.0f)
        {
            Debug.Log(mySpell.Name + " Activate");
            cooldown = mySpell.Cooltime;
        }
        else
        {

        }
    }
}
