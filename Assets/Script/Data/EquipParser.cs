using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class EquipParser : MonoBehaviour
{
    public TextAsset equipFile;
    public Equipment equipment;
    public string folder;

    public Equipment Init()
    {
        int[] data;
        string path = equipFilePath();
        try
        {
            FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);
            StreamReader reader = new StreamReader(fs);
            
            string[] pairs = reader.ReadLine().Split(' ');
            List<int> list = new List<int>();
            foreach (string pair in pairs)
            {
                int item;
                if (int.TryParse(pair, out item))
                    list.Add(item);
            }
            data = list.ToArray();
        }
        catch (FileNotFoundException e)
        {
            int[] temp = { 1, 2, 3, 4, 1, 2, 3, 4, 5 };
            data = temp;
        }
        equipment = Instantiate(equipment, GameObject.Find("Data").transform);
        equipment.name = "Equipment";
        equipment.Init(data);
        return equipment;
    }

    public void Save(Equipment equipment)
    {
        string data = "";
        foreach (Spell spell in equipment.spellbook.spells)
            data += spell.Id.ToString() + " ";
        foreach (Unit unit in equipment.deck.units)
            data += unit.Id.ToString() + " ";
        string path = equipFilePath();
        Debug.Log("path: " + path);
        FileStream fs = new FileStream(path, FileMode.CreateNew, FileAccess.Write);
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

    private string equipFilePath()
    {
        // 안드로이드 경로
        if (Application.platform == RuntimePlatform.Android)
        {
            folder = Application.persistentDataPath;
            folder = folder.Substring(0, folder.LastIndexOf('/'));
            folder = Path.Combine(folder, "equipment");
        }
        // 유니티 경로
        else if (Application.isEditor)
        {
            folder = Application.dataPath;
            var stringPath = folder + "/..";
            folder = Path.GetFullPath(stringPath);
            folder = Path.Combine(folder, "equipment");
        }
        System.IO.Directory.CreateDirectory(folder);

        string fileName = string.Format("{0}/equipData.txt", folder);
        return fileName;
    }
}
