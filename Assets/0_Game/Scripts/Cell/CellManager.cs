using System;
using System.Collections.Generic;
using UnityEngine;

public class CellManager : MonoBehaviour
{
    [SerializeField] private List<CellData> _myCellsData = new();
    [SerializeField] private List<Cell> _myCells = new();
    Dictionary<Direction, CellManager> _neighboors = new();
    [SerializeField] private List<CellManager> _neighboorList = new();
    private Cell _topCell;
    private void Awake()
    {
        GameManager.Instance.MaxHeight = _myCellsData.Count;
    }

    private void Start()
    {
        FindNeighboors();
        InitCells();
    }

    void FindNeighboors()
    {
        for (int i = 0; i < 4; i++)
        {
            Ray ray = new Ray(transform.position, VectorExtension.DirectonToVector3((Direction)i));
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.TryGetComponent(out CellManager cellManager))
                {
                    _neighboors.Add((Direction)i, cellManager);
                    _neighboorList.Add(cellManager);
                }
            }
        }
    }

    void InitCells()
    {
        for (int i = 0; i < _myCellsData.Count; i++)
        {
            Cell cell = GameManager.Instance.CreateBaseCell(transform, _myCellsData[i], i);
            _myCells.Add(cell);
            cell.OnCellCollected += OnCellCollected;
            cell.SetCellManager(this);
        }
        if (_myCells.Count == 0) {
            RemoveNeightboor(this);
            _neighboors.Clear();
            return;
        }
        _topCell = _myCells[^1];
        _topCell.ActivateCell();
    }

    private void OnCellCollected()
    {
        print("Remove Cell " + gameObject.name);
        _myCells.Remove(_topCell);
        Transform topCellTransform = _topCell.transform;
        Destroy(topCellTransform.gameObject);
        if (_myCells.Count != 0)
        {
            _topCell = _myCells[^1];
        }
        else
        {
            foreach (var neighboor in _neighboors.Values)
            {
                neighboor.RemoveNeightboor(this);
            }
        }
    }

    public void RemoveNeightboor(CellManager cellManager)
    {
        foreach (KeyValuePair<Direction, CellManager> pair in _neighboors)
        {
            if (pair.Value == cellManager)
            {
                _neighboors.Remove(pair.Key);
                break;
            }
        }
    }

    public CellManager GetTargetCellManager(Direction direction)
    {
        if (_neighboors.TryGetValue(direction, out CellManager cellManager))
        {
            return cellManager;
        }
        return null;
    }

    public void OnTongueArriveCell(FrogCell cell, Action<Cell, CellManager> onCorrectCell = null, Action onWrongCell = null)
    {
        _topCell.OnTongueArriveCell(cell, onCorrectCell, onWrongCell);
    }
}



public enum Direction
{
    Forward,
    Back,
    Right,
    Left
}
[Serializable]
public struct CellData
{
    public CellColor CellColor;
    public CellType CellType;
    public Direction LookDirection;
}
