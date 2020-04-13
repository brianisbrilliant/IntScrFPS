using UnityEngine;

public interface IItem
{
    void Use();
    void AltUse();
    void Pickup(Transform hand);
    void Drop();
}
