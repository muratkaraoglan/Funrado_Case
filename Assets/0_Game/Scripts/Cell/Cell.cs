using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class Cell : MonoBehaviour
{
    protected CellData _cellData;
    protected GameObject _childGameObject;
    protected CellManager _cellManager;
    public event Action<Cell> OnCellCollected = _ => { };
    public void InitCellData(CellData data, GameObject cellChldGOPrefab)
    {
        _cellData = data;
        Vector3 position = transform.position;
        position.y = GameManager.Instance.MaxHeight;
        _childGameObject = Instantiate(cellChldGOPrefab, position, Quaternion.identity, transform);

    }

    public abstract void ActivateCell();// Activate top cell

    public abstract void OnTongueArriveCell(FrogCell frogCell, Action<Cell, CellManager> onCorrectCell = null, Action onWrongCell = null);

    public void SetCellManager(CellManager cellManager) => _cellManager = cellManager;
    public CellData Data => _cellData;
    public Transform CellTypeTransform => _childGameObject.transform;
    public void InvokeCellCollected(Cell cell) => OnCellCollected.Invoke(cell);
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
