using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class Cell : MonoBehaviour
{

    public void InitCellData(CellData data)
    {

    }

    public abstract void ActivateCell();// Activate top cell

    public virtual void OnTongueArriveCell()
    {

    }


}

public enum CellType
{
    Arrow,
    Frog,
    Grape
}
public enum CellColor
{
    Blue,
    Green,
    Purple,
    Red,
    Yellow
}
