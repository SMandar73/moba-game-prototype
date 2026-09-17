using UnityEngine;

namespace MobaPrototype.Core
{
    public enum Team { Blue, Red, Neutral }

    public interface IDamageable
    {
        Team Team { get; }
        bool IsAlive { get; }
        void TakeDamage(float amount, GameObject source);
    }
}
