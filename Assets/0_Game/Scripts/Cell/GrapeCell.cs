using System;
using System.Collections;
using UnityEngine;

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
            CellManager nextCellManager = _cellManager.GetTargetCellManager(frogCell.MovementDirection);
            onCorrectCell.Invoke(this, nextCellManager);
            StartCoroutine(ScaleGrape());
        }
        else
        {
            onWrongCell.Invoke();
        }
    }

    IEnumerator ScaleGrape()
    {
        Vector3 currentScale = CellTypeTransform.localScale;
        Vector3 targetScale = currentScale * 1.2f;
        float elapsedTime = 0;
        float targetTime = .3f;
        while (elapsedTime <= targetTime)
        {
            elapsedTime += Time.deltaTime;
            Vector3 scale = Vector3.Lerp(currentScale, targetScale, elapsedTime / targetTime);
            CellTypeTransform.localScale = scale;
            yield return null;
        }

        elapsedTime = 0;
        while (elapsedTime <= targetTime)
        {
            elapsedTime += Time.deltaTime;
            Vector3 scale = Vector3.Lerp(targetScale, currentScale, elapsedTime / targetTime);
            CellTypeTransform.localScale = scale;
            yield return null;
        }
        CellTypeTransform.localScale = currentScale;
    }
}
