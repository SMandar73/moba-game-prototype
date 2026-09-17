using UnityEngine;
using MobaPrototype.Core;
using MobaPrototype.Units;

namespace MobaPrototype.Abilities
{
    public class AbilityController : MonoBehaviour
    {
        [SerializeField] private float fireballCooldown = 3f;
        [SerializeField] private float fireballDamage = 35f;
        [SerializeField] private float healCooldown = 7f;
        [SerializeField] private float healAmount = 45f;
        [SerializeField] private GameObject projectilePrefab;
        [SerializeField] private Transform castPoint;

        private float nextFireballTime;
        private float nextHealTime;
        private HeroController owner;

        private void Awake() => owner = GetComponent<HeroController>();

        public void CastFireball()
        {
            if (Time.time < nextFireballTime) return;
            nextFireballTime = Time.time + fireballCooldown;

            Vector3 origin = castPoint != null ? castPoint.position : transform.position + transform.forward;
            if (projectilePrefab != null)
            {
                GameObject projectile = Instantiate(projectilePrefab, origin, transform.rotation);
                if (projectile.TryGetComponent(out Projectile projectileComponent))
                    projectileComponent.Initialize(owner.Team, fireballDamage, transform.forward, gameObject);
            }
        }

        public void CastHeal(HeroController target)
        {
            if (target == null || Time.time < nextHealTime) return;
            nextHealTime = Time.time + healCooldown;
            target.SendMessage("ReceiveHealing", healAmount, SendMessageOptions.DontRequireReceiver);
        }
    }

    public class Projectile : MonoBehaviour
    {
        [SerializeField] private float speed = 16f;
        [SerializeField] private float lifetime = 3f;
        private Team team;
        private float damage;
        private Vector3 direction;
        private GameObject source;

        public void Initialize(Team projectileTeam, float projectileDamage, Vector3 projectileDirection, GameObject projectileSource)
        {
            team = projectileTeam;
            damage = projectileDamage;
            direction = projectileDirection.normalized;
            source = projectileSource;
        }

        private void Update()
        {
            transform.position += direction * speed * Time.deltaTime;
            lifetime -= Time.deltaTime;
            if (lifetime <= 0f) Destroy(gameObject);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject == source) return;
            if (other.TryGetComponent(out IDamageable target) && target.Team != team)
            {
                target.TakeDamage(damage, source);
                Destroy(gameObject);
            }
        }
    }
}
