using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FrogCell : Cell
{
    private BoxCollider _myCollider;
    private LineRenderer _tongue;
    private List<Cell> _visitedCells = new();
    public Direction MovementDirection;
    private void Awake()
    {
        _myCollider = gameObject.AddComponent<BoxCollider>();
        _myCollider.enabled = false;
    }

    private bool _isActive;
    public override void ActivateCell()
    {
        Vector3 lookDirection = VectorExtension.DirectonToVector3(_cellData.LookDirection);
        _childGameObject.transform.forward = lookDirection;
        _isActive = true;
        _myCollider.enabled = true;
        _childGameObject.SetActive(true);
        _tongue = _childGameObject.transform.GetChild(1).GetComponent<LineRenderer>();
    }
    private bool _isTongueMove;
    private int _tonguePositionIndex = 0;
    private void OnMouseDown()
    {
        print("Down");
        if (_isActive && !_isTongueMove)
        {
            //TODO: Decrease movement count
            _isTongueMove = true;
            CellManager targetCellManger = _cellManager.GetTargetCellManager(_cellData.LookDirection);
            MovementDirection = _cellData.LookDirection;
            _tongue.positionCount = 2;
            Vector3 startPosition = transform.position;
            startPosition.y = GameManager.Instance.MaxHeight + GameManager.Instance.TongueYOffset;
            _tongue.SetPosition(0, startPosition);
            _tongue.SetPosition(1, startPosition);
            _tonguePositionIndex = 1;
            StartCoroutine(TongueMovement(targetCellManger));
        }
    }

    IEnumerator TongueMovement(CellManager targetCellManager)
    {
        Vector3 targetPosition = targetCellManager.transform.position;
        targetPosition.y = GameManager.Instance.MaxHeight + GameManager.Instance.TongueYOffset;
        while (Mathf.Abs(Vector3.SqrMagnitude(targetPosition - _tongue.GetPosition(_tonguePositionIndex))) > .001f)
        {
            Vector3 currentPos = Vector3.MoveTowards(_tongue.GetPosition(_tonguePositionIndex), targetPosition, GameManager.Instance.TongueDeltaMovementAmount);
            _tongue.SetPosition(_tonguePositionIndex, currentPos);
            yield return null;
        }

        targetCellManager.OnTongueArriveCell(this, OnHitCorrectCell, OnHitWrong);
    }

    void OnHitWrong()
    {
        _visitedCells.Clear();
        StartCoroutine(ReverseTongueMovementOnHitWrongCell());
    }

    void OnHitCorrectCell(Cell cell, CellManager nextCell)
    {
        _visitedCells.Add(cell);
        if (nextCell != null)
        {
            _tongue.positionCount++;
            _tonguePositionIndex++;
            _tongue.SetPosition(_tonguePositionIndex, _tongue.GetPosition(_tonguePositionIndex - 1));
            StartCoroutine(TongueMovement(nextCell));
        }
        else
        {
            print("Collect");
            StartCoroutine(ReverseTongueMovementSuccesfull());
        }
    }

    IEnumerator ReverseTongueMovementOnHitWrongCell()
    {
        while (_tongue.positionCount != 1)
        {
            Vector3 currentPostion = _tongue.GetPosition(_tonguePositionIndex);
            Vector3 targetPosition = _tongue.GetPosition(_tonguePositionIndex - 1);

            currentPostion = Vector3.MoveTowards(currentPostion, targetPosition, GameManager.Instance.TongueDeltaMovementAmount);
            _tongue.SetPosition(_tonguePositionIndex, currentPostion);
            if (Mathf.Abs(Vector3.SqrMagnitude(currentPostion - targetPosition)) <= .01f)
            {
                _tonguePositionIndex--;
                _tongue.positionCount--;
            }
            yield return null;
        }
        _isTongueMove = false;
    }

    IEnumerator ReverseTongueMovementSuccesfull()
    {
        GameObject tongueHelperGO = new GameObject("Tongue Helper");
        Transform tongueHelperTransform = tongueHelperGO.transform;
        tongueHelperTransform.position = _tongue.GetPosition(_tonguePositionIndex);

        Cell visitedCell = _visitedCells[^1];
        _visitedCells.Remove(visitedCell);
        visitedCell.CellTypeTransform.SetParent(tongueHelperGO.transform);
        visitedCell.InvokeCellCollected(visitedCell);
        while (_tongue.positionCount != 1)
        {
            Vector3 currentPostion = _tongue.GetPosition(_tonguePositionIndex);
            Vector3 targetPosition = _tongue.GetPosition(_tonguePositionIndex - 1);

            currentPostion = Vector3.MoveTowards(currentPostion, targetPosition, GameManager.Instance.TongueDeltaMovementAmount);
            _tongue.SetPosition(_tonguePositionIndex, currentPostion);
            tongueHelperTransform.position = currentPostion;

            if (Mathf.Abs(Vector3.SqrMagnitude(currentPostion - targetPosition)) <= .01f)
            {
                _tonguePositionIndex--;
                _tongue.positionCount--;
                if (_visitedCells.Count != 0)
                {

                    visitedCell = _visitedCells[^1];

                    _visitedCells.Remove(visitedCell);

                    if (visitedCell.Data.CellType == CellType.Grape)
                    {
                        visitedCell.CellTypeTransform.SetParent(tongueHelperTransform);
                    }
                    visitedCell.InvokeCellCollected(visitedCell);
                }
            }
            yield return null;
        }
        _isTongueMove = false;
        Destroy(tongueHelperGO);
        InvokeCellCollected(this);
    }

    public override void OnTongueArriveCell(FrogCell frogCell, Action<Cell, CellManager> onCorrectCell = null, Action onWrongCell = null)
    {
        onWrongCell.Invoke();
    }


}