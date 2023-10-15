using UnityEngine;

[RequireComponent(typeof(OwningPlayer))]
public class OwnedObject : MonoBehaviour
{
    public OwningPlayer Owner { get; private set; }
    
    protected virtual void Awake()
    {
        Owner = GetComponent<OwningPlayer>();
        Owner.OnSet += OnOwnerSet;
    }

    protected virtual void OnOwnerSet() { }

    protected virtual void OnDestroy()
    {
        Owner.OnSet -= OnOwnerSet;
    }
}
