using UnityEngine;
using MobaPrototype.Core;

namespace MobaPrototype.Units
{
    [RequireComponent(typeof(CharacterController))]
    public class HeroController : MonoBehaviour, IDamageable
    {
        [Header("Identity")]
        [SerializeField] private Team team = Team.Blue;
        [SerializeField] private string heroName = "Prototype Hero";

        [Header("Stats")]
        [SerializeField] private float maxHealth = 150f;
        [SerializeField] private float moveSpeed = 7f;
        [SerializeField] private float attackDamage = 20f;
        [SerializeField] private float attackRange = 2.5f;

        [Header("Abilities")]
        [SerializeField] private AbilityController abilityController;

        private CharacterController characterController;
        private Vector3 moveTarget;
        private float health;
        private float respawnTimer;

        public Team Team => team;
        public bool IsAlive => health > 0f;
        public float Health => health;
        public float MaxHealth => maxHealth;
        public string HeroName => heroName;

        private void Awake()
        {
            characterController = GetComponent<CharacterController>();
            health = maxHealth;
            moveTarget = transform.position;
        }

        private void Update()
        {
            if (!IsAlive)
            {
                respawnTimer -= Time.deltaTime;
                if (respawnTimer <= 0f) Respawn();
                return;
            }

            Vector3 direction = moveTarget - transform.position;
            direction.y = 0f;
            if (direction.magnitude > 0.15f)
            {
                Vector3 velocity = direction.normalized * moveSpeed;
                characterController.Move(velocity * Time.deltaTime);
                transform.forward = Vector3.Lerp(transform.forward, direction.normalized, 12f * Time.deltaTime);
            }
        }

        public void SetMoveTarget(Vector3 target)
        {
            target.y = transform.position.y;
            moveTarget = target;
        }

        public void CastPrimaryAbility() => abilityController?.CastFireball();
        public void CastHealAbility() => abilityController?.CastHeal(this);

        public void BasicAttack(IDamageable target)
        {
            if (target != null && target.IsAlive) target.TakeDamage(attackDamage, gameObject);
        }

        public void TakeDamage(float amount, GameObject source)
        {
            if (!IsAlive) return;
            health = Mathf.Max(0f, health - amount);
            if (health <= 0f)
            {
                respawnTimer = 5f;
                gameObject.SetActive(false);
            }
        }

        private void Respawn()
        {
            health = maxHealth;
            gameObject.SetActive(true);
        }
    }
}
