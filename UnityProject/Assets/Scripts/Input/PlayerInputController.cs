using UnityEngine;
using MobaPrototype.CameraSystem;

namespace MobaPrototype.InputSystem
{
    [RequireComponent(typeof(MobaPrototype.Units.HeroController))]
    public class PlayerInputController : MonoBehaviour
    {
        [SerializeField] private Camera worldCamera;
        [SerializeField] private LayerMask groundMask;
        [SerializeField] private MobaCameraController cameraController;

        private MobaPrototype.Units.HeroController hero;

        private void Awake()
        {
            hero = GetComponent<MobaPrototype.Units.HeroController>();
            worldCamera ??= Camera.main;
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0) && TryGetGroundPoint(out Vector3 point))
                hero.SetMoveTarget(point);

            if (Input.GetKeyDown(KeyCode.Q)) hero.CastPrimaryAbility();
            if (Input.GetKeyDown(KeyCode.E)) hero.CastHealAbility();
        }

        public void MoveFromMinimap(Vector3 worldPosition) => hero.SetMoveTarget(worldPosition);

        public void SnapCameraFromMinimap(Vector3 worldPosition) => cameraController?.SnapToWorldPosition(worldPosition);

        private bool TryGetGroundPoint(out Vector3 point)
        {
            Ray ray = worldCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, 500f, groundMask))
            {
                point = hit.point;
                return true;
            }

            point = default;
            return false;
        }
    }
}
