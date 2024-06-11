using System;
using System.Collections.Generic;
using UnityEngine;

public class CellManager : MonoBehaviour
{
    [SerializeField] private List<CellData> _myCellsData = new();
    [SerializeField] private List<Cell> _myCells = new();
    Dictionary<Direction, CellManager> _neighboors = new();

    private void Start()
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
                }
            }
        }

        for (int i = 0; i < _myCellsData.Count; i++)
        {
            _myCells.Add(GameManager.Instance.CreateBaseCell(transform, _myCellsData[i], i));
        }
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
