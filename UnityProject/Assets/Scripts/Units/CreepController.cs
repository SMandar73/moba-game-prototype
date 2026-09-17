using UnityEngine;
using MobaPrototype.Core;

namespace MobaPrototype.Units
{
    public class CreepController : MonoBehaviour, IDamageable
    {
        [SerializeField] private Team team;
        [SerializeField] private float maxHealth = 70f;
        [SerializeField] private float moveSpeed = 2.5f;
        [SerializeField] private Transform laneTarget;
        private float health;

        public Team Team => team;
        public bool IsAlive => health > 0f;

        private void Awake() => health = maxHealth;

        private void Update()
        {
            if (!IsAlive || laneTarget == null) return;
            transform.position = Vector3.MoveTowards(transform.position, laneTarget.position, moveSpeed * Time.deltaTime);
        }

        public void TakeDamage(float amount, GameObject source)
        {
            health = Mathf.Max(0f, health - amount);
            if (health <= 0f) Destroy(gameObject, 0.1f);
        }
    }
}
