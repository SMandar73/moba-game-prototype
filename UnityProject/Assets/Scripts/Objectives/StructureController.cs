using UnityEngine;
using MobaPrototype.Core;

namespace MobaPrototype.Objectives
{
    public class StructureController : MonoBehaviour, IDamageable
    {
        [SerializeField] private Team team;
        [SerializeField] private bool ancient;
        [SerializeField] private float maxHealth = 500f;
        private float health;

        public Team Team => team;
        public bool IsAlive => health > 0f;
        public float Health => health;

        private void Awake() => health = maxHealth;

        public void TakeDamage(float amount, GameObject source)
        {
            if (!IsAlive) return;
            health = Mathf.Max(0f, health - amount);
            if (health <= 0f)
            {
                if (ancient) GameManager.Instance?.EndMatch(team == Team.Blue ? Team.Red : Team.Blue);
                gameObject.SetActive(false);
            }
        }
    }
}
