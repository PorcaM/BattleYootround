using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellActivator : MonoBehaviour {
    public enum Flow { Ready, SelectArea, Activate }
    public Flow flow;
    public SpellInstance readySpell;
    public SpellArea areaPrefab;
    public SpellArea createdArea;
    public SpellManager spellManager;

    public void SelectSpell(SpellInstance spell)
    {
        if (flow == Flow.SelectArea)
        {
            Destroy(createdArea.gameObject);
        }
        readySpell = spell;
        ShowArea();
    }

    private void ShowArea()
    {        
        createdArea = Instantiate(areaPrefab, transform) as SpellArea;
        float r = 0.3f * readySpell.radius;
        createdArea.transform.localScale = new Vector3(r, 1.0f, r);
        flow = Flow.SelectArea;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (flow == Flow.SelectArea)
            {
                HandleSpellActivate();
                Destroy(createdArea.gameObject);
                flow = Flow.Ready;
            }
        }
    }

    private void HandleSpellActivate()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("EnemyUnit");
        Debug.Log(createdArea.transform.position);
        foreach(GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(enemy.transform.position, createdArea.transform.position);
            if (distance <= readySpell.radius)
            {
            Debug.Log(enemy.name + " : " +distance);
                enemy.GetComponent<UnitInstance>().UnderAttack(readySpell.damage);
            }
        }
    }
}
