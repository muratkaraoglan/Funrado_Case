using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameManager : Singelton<GameManager>
{
    [SerializeField] private List<CellTypeHolder> cellTypes;

    public Cell CreateBaseCell(Transform parent, CellData data, int index)
    {
        GameObject cellPrefab = cellTypes.Find(c => c.CellColor == data.CellColor).CellBasePrefab;

        Vector3 position = parent.position;
        position.y = (index + 1) * .1f;
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
                    cellGO.AddComponent<FrogCell>();
                    break;
                }
            case CellType.Grape:
                {
                    cellGO.AddComponent<GrapeCell>();
                    break;
                }
        }

        return cellGO.GetComponent<Cell>();
    }


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
