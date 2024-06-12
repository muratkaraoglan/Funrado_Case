using System;
using UnityEngine;

public class ArrowCell : Cell
{
    public override void ActivateCell()
    {
        Vector3 lookDirection = VectorExtension.DirectonToVector3(_cellData.LookDirection);
        _childGameObject.transform.forward = lookDirection;
        _childGameObject.SetActive(true);
    }

    public override void OnTongueArriveCell(FrogCell frogCell, Action<Cell, CellManager> onCorrectCell = null, Action onWrongCell = null)
    {
        if (frogCell.Data.CellColor == _cellData.CellColor)
        {
            CellManager nextCellManager = _cellManager.GetTargetCellManager(Data.LookDirection);
            onCorrectCell.Invoke(this, nextCellManager);
        }
        else
        {
            onWrongCell.Invoke();
        }
    }
}
