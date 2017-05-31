using UnityEngine;

public interface IHighlightable
{
    void Highlight();
    void DeHighlight();
}

public interface ICanPickup
{
    void Pickup(GameObject GO);
    void CheckPickup(GameObject GO);
    void Carry(Vector3 hit);
    void Release();
    void CheckRelease();
}

public interface IPickupable
{
    GameObject ReturnGO();
    void MoveGO(Vector3 pos);
    void AlignWithGroundBelow();
}

public interface IInteractable
{
    void Interact();
    void CheckInput();
}

