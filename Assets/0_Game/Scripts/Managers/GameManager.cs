using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[DefaultExecutionOrder(-9)]
public class GameManager : Singelton<GameManager>
{
    [SerializeField] private List<CellTypeHolder> cellTypes;
    [SerializeField] private float _cellHeightOffset = .1f;
    [SerializeField] private float _maxHeight = 0;
    [SerializeField] private int _numberOfMove;

    [field: SerializeField, Min(.001f)] public float TongueDeltaMovementAmount { get; private set; } = .01f;
    [field: SerializeField] public float TongueYOffset { get; private set; } = .2f;
    public float MaxHeight
    {
        get { return _maxHeight * _cellHeightOffset + _cellHeightOffset; }
        set
        {
            _maxHeight = value > _maxHeight ? value : _maxHeight;
        }
    }

    public Cell CreateBaseCell(Transform parent, CellData data, int index)
    {
        CellTypeHolder cellTypeHolder = cellTypes.Find(c => c.CellColor == data.CellColor);

        GameObject cellPrefab = cellTypeHolder.CellBasePrefab;

        Vector3 position = parent.position;
        position.y = (index + 1) * _cellHeightOffset;
        GameObject cellGO = Instantiate(cellPrefab, position, Quaternion.identity, parent);

        Cell cell;
        switch (data.CellType)
        {
            case CellType.Arrow:
                {
                    cellGO.AddComponent<ArrowCell>();
                    break;
                }
            case CellType.Frog:
                {
                    cellGO.AddComponent<FrogCell>().OnTouchFrog += OnTouchFrog; ;

                    break;
                }
            case CellType.Grape:
                {
                    cellGO.AddComponent<GrapeCell>();
                    break;
                }
        }
        cell = cellGO.GetComponent<Cell>();
        cell.InitCellData(data, CellTypeToGameObject(data.CellType, cellTypeHolder));

        return cell;
    }

    private void OnTouchFrog()
    {
        _numberOfMove--;
    }

    GameObject CellTypeToGameObject(CellType cellType, CellTypeHolder cellTypeHolder) =>

        cellType switch
        {
            CellType.Arrow => cellTypeHolder.CellArrowPrefab,
            CellType.Frog => cellTypeHolder.CellFrogPrefab,
            CellType.Grape => cellTypeHolder.CellGrapePrefab,
            _ => null
        };
    public bool IsCanMove => _numberOfMove > 0;
}

[Serializable]
public class CellTypeHolder
{
    public CellColor CellColor;
    public GameObject CellBasePrefab;
    public GameObject CellFrogPrefab;
    public GameObject CellGrapePrefab;
    public GameObject CellArrowPrefab;
}
