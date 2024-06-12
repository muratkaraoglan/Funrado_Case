using System;
using System.Collections.Generic;
using UnityEngine;

public class CellManager : MonoBehaviour
{
    [SerializeField] private List<CellData> _myCellsData = new();
    [SerializeField] private List<Cell> _myCells = new();
    Dictionary<Direction, CellManager> _neighboors = new();
    
    private Cell _topCell;
    private void Awake()
    {
        GameManager.Instance.MaxHeight = _myCellsData.Count;
    }

    private void Start()
    {
        if (_myCellsData.Count == 0) return;
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
                    if (cellManager.CellCount != 0)
                    {
                        _neighboors.Add((Direction)i, cellManager);
                    }
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
        if (_myCells.Count == 0)
        {
            return;
        }
        _topCell = _myCells[^1];
        _topCell.ActivateCell();
    }

    private void OnCellCollected(Cell cell)
    {
        if (_topCell != cell) return;
        _myCells.Remove(_topCell);
        _topCell.OnCellCollected -= OnCellCollected;
        Transform topCellTransform = _topCell.transform;
        Destroy(topCellTransform.gameObject);
        if (_myCells.Count != 0)
        {
            _topCell = _myCells[^1];
            _topCell.ActivateCell();
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

    public int CellCount => _myCellsData.Count;
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
