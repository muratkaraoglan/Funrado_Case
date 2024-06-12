using UnityEngine;
using UnityEngine.EventSystems;

public class FrogCell : Cell
{
    private BoxCollider _myCollider;
    private void Awake()
    {
        _myCollider = gameObject.AddComponent<BoxCollider>();
        _myCollider.enabled = false;
    }

    private bool _isActive;
    public override void ActivateCell()
    {
        _isActive = true;
        _myCollider.enabled = true;
        //TODO: calculate frog rotation from direction 
    }

    private void OnMouseDown()
    {
        print("Down");
    }

    public override void OnTongueArriveCell()
    {
        
    }
}