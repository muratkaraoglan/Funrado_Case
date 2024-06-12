using System;

public class GrapeCell : Cell
{
    public override void ActivateCell()
    {
        _childGameObject.SetActive(true);
    }

    public override void OnTongueArriveCell(FrogCell frogCell, Action<Cell, CellManager> onCorrectCell = null, Action onWrongCell = null)
    {
        if (frogCell.Data.CellColor == _cellData.CellColor)
        {
            CellManager nextCellManager = _cellManager.GetTargetCellManager(frogCell.Data.LookDirection);
            onCorrectCell.Invoke(this, nextCellManager);
        }
        else
        {
            onWrongCell.Invoke();
        }
    }
}
