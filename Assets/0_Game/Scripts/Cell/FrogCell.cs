using UnityEngine.EventSystems;

public class FrogCell : Cell
{
    private bool _isActive;
    public override void ActivateCell()
    {
        _isActive = true;
    }

    private void OnMouseDown()
    {
        print("Dıwn");
    }
}