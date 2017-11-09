using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YootField : MonoBehaviour {
    private int id;
    public YootField back;
    public YootField next;
    public YootField side;
    private const int outsideCount = 20;

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

    void Start()
    {
        Id = -1;
        back = next = side = null;
    }

    public void Init(int id)
    {
        Debug.Log(id);
        Id = id;
        if (IsOutside(Id))
            OutsideInit();
        else
            InsideInit();
    }

    private static bool IsOutside(int id)
    {
        return id < outsideCount;
    }

    private void OutsideInit()
    {
        InitBackNext();
        if (IsWithBypass())
            InitSide();
    }

    private void InitBackNext()
    {
        back = YootBoard.Fields[(Id + YootBoard.YootFieldCount - 1) % outsideCount];
        next = YootBoard.Fields[(Id + 1) % outsideCount];
    }

    private bool IsWithBypass()
    {
        return Id == 5 || Id == 10 || Id == 22;
    }

    private void InitSide()
    {
        if(Id == 5)
        {
            side = YootBoard.Fields[20];
        }
        if (Id == 10)
        {
            side = YootBoard.Fields[25];
        }
        if (Id == 22)
        {
            side = YootBoard.Fields[28];
        }
    }    

    private void InsideInit()
    {
        OutsideInit();
        if (Id == 20)
        {
            back = YootBoard.Fields[5];
        }
        if (Id == 24)
        {
            next = YootBoard.Fields[15];
        }
        if (Id == 25)
        {
            back = YootBoard.Fields[10];
        }
        if (Id == 28)
        {
            next = YootBoard.Fields[0];
        }
    }
}
