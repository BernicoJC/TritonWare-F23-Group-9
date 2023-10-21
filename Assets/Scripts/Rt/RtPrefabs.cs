using UnityEngine;

public class RtPrefabs : MonoBehaviour
{
    [field: SerializeField]
    public Projectile Projectile { get; private set; }

    [field: SerializeField]
    public OrbitingFlame OrbitingFlame { get; private set; }
}
