using UnityEngine;

public interface IHighlightable
{
    Material ReturnMatGO();
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
    void CancelPickup();
}

public interface IPickupable
{
    GameObject ReturnGO();
    void Pickup(Material mat);
    void Release(Material mat);
    void MoveGO(Vector3 pos);
    void AlignWithGroundBelow();
    void DropObj();
}

public interface IInteractable
{
    void Interact();
    void CheckInput();
}

