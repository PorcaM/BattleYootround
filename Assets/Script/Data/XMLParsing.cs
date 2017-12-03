using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System;

public class XMLParsing : MonoBehaviour
{
    public TextAsset Player;
    public TextAsset Spell;
    public TextAsset Unit;

    public PlayerProfile playerProfile;

    public List<Spell> spells;
    public List<Unit> units;

    public void Init()
    {
        AllocateMemory();
        // ParsePlayerXML();
        ParseSpellRecord();
        ParseUnitRecord();
        InitRecords();
    }

    private void AllocateMemory()
    {
        spells = new List<Spell>();
        units = new List<Unit>();
    }

    private void ParsePlayerXML()
    {
        XmlDocument PlayerXML = new XmlDocument();
        PlayerXML.LoadXml(Player.text);
        XmlNodeList playerNodes = PlayerXML.SelectNodes("BYData/Player");
        foreach (XmlNode node in playerNodes)
        {
            playerProfile.level = Int32.Parse(node.SelectSingleNode("Level").InnerText);
            playerProfile.exp = Int32.Parse(node.SelectSingleNode("CurrentEXP").InnerText);
        }
    }

    private void ParseSpellRecord()
    {
        XmlDocument SpellXML = new XmlDocument();
        SpellXML.LoadXml(Spell.text);
        XmlNodeList nodes = SpellXML.SelectNodes("BYData/Spell");
        foreach (XmlNode node in nodes)
        {
            spells.Add(CreateSpellByParing(node));
        }
    }

    private Spell CreateSpellByParing(XmlNode node)
    {
        Spell spell = new Spell();
        spell.Id = int.Parse(node.SelectSingleNode("Id").InnerText);
        spell.SpellName = node.SelectSingleNode("SpellName").InnerText;
        XmlNodeList Range_node = node.SelectNodes("Range");
        float radius = float.Parse(Range_node[0].SelectSingleNode("Radius").InnerText);
        spell.Range = new CircleRange(radius);
        XmlNodeList Attribute_node = node.SelectNodes("Attribute");
        spell.type = (Spell.Type)System.Enum.Parse(typeof(Spell.Type), Attribute_node[0].SelectSingleNode("Type").InnerText);
        spell.Damage = float.Parse(Attribute_node[0].SelectSingleNode("Damage").InnerText);
        spell.Duration = float.Parse(Attribute_node[0].SelectSingleNode("Duration").InnerText);
        spell.Cooltime = float.Parse(node.SelectSingleNode("Cooltime").InnerText);
        spell.target = node.SelectSingleNode("Target").InnerText;
        spell.description = node.SelectSingleNode("Description").InnerText;
        return spell;
    }

    private void ParseUnitRecord()
    {
        XmlDocument UnitXML = new XmlDocument();

        // Unit 데이터
        UnitXML.LoadXml(Unit.text);
        XmlNodeList Nodes = UnitXML.SelectNodes("BYData/Unit");
        foreach (XmlNode node in Nodes)
        {
            units.Add(CreateUnitByParing(node));
        }
    }

    private Unit CreateUnitByParing(XmlNode node)
    {
        Unit unit = new Unit
        {
            Id = int.Parse(node.SelectSingleNode("Id").InnerText),
            UnitClass = (Unit.ClassType)System.Enum.Parse(typeof(Unit.ClassType), node.SelectSingleNode("Type").InnerText),
            Damage = double.Parse(node.SelectSingleNode("Damage").InnerText),
            Armor = double.Parse(node.SelectSingleNode("Armor").InnerText),
            Range = double.Parse(node.SelectSingleNode("Range").InnerText),
            Hp = double.Parse(node.SelectSingleNode("Hp").InnerText),
            MovementSpeed = double.Parse(node.SelectSingleNode("MovementSpeed").InnerText),
            AttackSpeed = double.Parse(node.SelectSingleNode("AttackSpeed").InnerText),
            position = int.Parse(node.SelectSingleNode("Position").InnerText)
        };
        return unit;
    }

    private void InitRecords()
    {
        SpellRecord.Init(spells.ToArray());
        UnitRecord.Init(units.ToArray());
    }
}
