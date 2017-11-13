using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;

public class TestXML : MonoBehaviour {
    public TextAsset Player;
    public TextAsset Spell;
    public TextAsset Unit;

    private Spell[] spell = new Spell[13];
    private Unit[] unit = new Unit[7];
    
    // Use this for initialization
    void Start () {
        LoadXML();
        Print();
	}
	
    private void Print()
    {
        foreach(Spell s in spell)
        {
            Debug.Log(s.SpellName);
        }
        foreach (Unit u in unit)
        {
            Debug.Log(u.type.ToString());
        }
    }

    private void LoadXML()
    {
        XmlDocument PlayerXML = new XmlDocument();
        XmlDocument SpellXML = new XmlDocument();
        XmlDocument UnitXML = new XmlDocument();

        // Player 데이터
        PlayerXML.LoadXml(Player.text);
        XmlNodeList Player_nodes = PlayerXML.SelectNodes("BYData/Player");
        foreach(XmlNode node in Player_nodes)
        {
            Debug.Log("[id] :" + node.SelectSingleNode("Id").InnerText);
            Debug.Log("[level] :" + node.SelectSingleNode("Level").InnerText);
            Debug.Log("[currentEXP] :" + node.SelectSingleNode("CurrentEXP").InnerText);
        }

        // Spell 데이터
        SpellXML.LoadXml(Spell.text);
        XmlNodeList Spell_nodes = SpellXML.SelectNodes("BYData/Spell");
        int pos = 0;
        foreach (XmlNode node in Spell_nodes)
        {
            Debug.Log("[id] :" + node.SelectSingleNode("Id").InnerText);
            spell[pos].Init();
            Debug.Log(spell[pos].Id);
            spell[pos].Id = int.Parse(node.SelectSingleNode("Id").InnerText);
            spell[pos].SpellName = node.SelectSingleNode("SpellName").InnerText;
            XmlNodeList Range_node = node.SelectNodes("Range");
            if(Range_node[0].SelectSingleNode("Type").InnerText == "Square")
            {
                int x = int.Parse(Range_node[0].SelectSingleNode("X").InnerText);
                int y = int.Parse(Range_node[0].SelectSingleNode("Y").InnerText);
                Vector2 range = new Vector2(x, y);
                spell[pos].Range = new RectRange(range);
            }
            else
            {
                float radius = float.Parse(Range_node[0].SelectSingleNode("Radius").InnerText);
                spell[pos].Range = new CircleRange(radius);
            }
            XmlNodeList Attribute_node = node.SelectNodes("Attribute");
            spell[pos].type = (Spell.Type)System.Enum.Parse(typeof(Spell.Type), Attribute_node[0].SelectSingleNode("Type").InnerText);
            spell[pos].Damage = int.Parse(Attribute_node[0].SelectSingleNode("Damage").InnerText);
            spell[pos].Duration = int.Parse(Attribute_node[0].SelectSingleNode("Duration").InnerText);

            spell[pos].Cooltime = int.Parse(node.SelectSingleNode("Cooltime").InnerText);

            pos++;
            Debug.Log(pos);
        }

        // Unit 데이터
        UnitXML.LoadXml(Unit.text);
        XmlNodeList Unit_nodes = UnitXML.SelectNodes("BYData/Unit");
        pos = 0;
        foreach (XmlNode node in Unit_nodes)
        {
            unit[pos].Id = int.Parse(node.SelectSingleNode("Id").InnerText);
            unit[pos].type = (Unit.Type)System.Enum.Parse(typeof(Unit.Type), node.SelectSingleNode("Type").InnerText);
            unit[pos].Damage = int.Parse(node.SelectSingleNode("Damage").InnerText);
            unit[pos].Armor = int.Parse(node.SelectSingleNode("Armor").InnerText);
            unit[pos].Range = int.Parse(node.SelectSingleNode("Range").InnerText);
            unit[pos].Hp = int.Parse(node.SelectSingleNode("Hp").InnerText);
            unit[pos].MovementSpeed = int.Parse(node.SelectSingleNode("MovementSpeed").InnerText);
            unit[pos].AttackSpeed = int.Parse(node.SelectSingleNode("AttackSpeed").InnerText);

            pos++;
            Debug.Log(pos);
        }
    }
}
