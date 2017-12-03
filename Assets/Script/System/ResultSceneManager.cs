using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultSceneManager : MonoBehaviour
{
    public Text titleText;
    public Transform unitParent;
    [SerializeField] private int winner;
    [SerializeField] private bool isWin;
    [SerializeField] private List<GameObject> models;

    [SerializeField] private int[] animationNum = { 3, 6 };

    void Start()
    {
        SetWin();
        PlayBGM();
        UpdateTitleText();
        CreateUnits();
        Destroy(GameObject.Find("YootGameResult"));
    }

    private void SetWin()
    {
        GameObject YGR = GameObject.Find("YootGameResult");
        if (YGR)
            winner = GameObject.Find("YootGameResult").GetComponent<YootGameResult>().winner;
        else
            winner = 1;
        if (winner == 0)
            isWin = true;
        else
            isWin = false;
    }

    private void PlayBGM()
    {
        SoundManager.Instance().PlayMusic(winner);
    }

    private void UpdateTitleText()
    {
        if (isWin)
            titleText.text = "Victory!!";
        else
            titleText.text = "Defeat..";
    }

    private void CreateUnits()
    {
        GameObject equipment = GameObject.Find("Equipment");
        if (equipment)
        {
            const float interval = 1.2f;
            const float size = 2.5f;
            int i = 0;
            foreach (Unit unit in equipment.GetComponent<Equipment>().deck.units)
            {
                GameObject model = CreateModel(unit, unitParent);
                models.Add(model);
                model.GetComponent<Animator>().SetInteger("animation", animationNum[winner]);
                model.transform.position = new Vector3((i++ - 2) * interval, 0, 0);
                model.transform.LookAt(Camera.main.transform);
                model.transform.localScale = new Vector3(size, size, size);
            }
        }
    }

    private GameObject CreateModel(Unit unit, Transform parent)
    {
        const string path = "UnitImages/Models/";
        string modelName = unit.UnitClass.ToString().ToLower();
        GameObject model = Instantiate(Resources.Load(path + modelName, typeof(GameObject)), parent) as GameObject;
        return model;
    }
}
