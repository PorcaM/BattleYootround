using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class EquipParser : MonoBehaviour
{
    public TextAsset equipFile;
    public Equipment equipment;

    private const string path = "Assets/Resources/Data/equipment.txt";

    public Equipment Init()
    {
        string[] pairs = equipFile.text.Split(' ');
        List<int> list = new List<int>();
        foreach (string pair in pairs)
        {
            int item;
            if (int.TryParse(pair, out item))
                list.Add(item);
        }
        if (list.Count != 9)
            Debug.Log("List count is not 9!!");
        equipment = Instantiate(equipment, GameObject.Find("Data").transform);
        equipment.name = "Equipment";
        equipment.Init(list);
        return equipment;
    }

    public void Save(Equipment equipment)
    {
        string data = "";
        foreach (Spell spell in equipment.spellbook.spells)
            data += spell.Id.ToString() + " ";
        foreach (Unit unit in equipment.deck.units)
            data += unit.Id.ToString() + " ";
        FileStream fs = new FileStream(path, FileMode.Truncate, FileAccess.Write);
        StreamWriter writer = new StreamWriter(fs);
        if (writer != null)
        {
            Debug.Log("Write " + data);
            writer.WriteLine(data);
            writer.Close();
        }
        else
        {
            Debug.Log("No equip file");
        }
    }
}
