using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class Cell : MonoBehaviour
{
    protected CellData _cellData;
    protected GameObject _childGameObject;

    public void InitCellData(CellData data, GameObject cellChldGOPrefab)
    {
        _cellData = data;
        Vector3 position = transform.position;
        position.y = GameManager.Instance.MaxHeight;
        Instantiate(cellChldGOPrefab, position, Quaternion.identity, transform);
    }

    public abstract void ActivateCell();// Activate top cell

    public abstract void OnTongueArriveCell();



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
