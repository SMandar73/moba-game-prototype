using UnityEngine;
using UnityEngine.UI;
using MobaPrototype.CameraSystem;
using MobaPrototype.InputSystem;

namespace MobaPrototype.UI
{
    public class MinimapController : MonoBehaviour
    {
        [SerializeField] private Camera minimapCamera;
        [SerializeField] private RawImage minimapImage;
        [SerializeField] private PlayerInputController playerInput;
        [SerializeField] private MobaCameraController worldCamera;
        [SerializeField] private RectTransform minimapRect;
        [SerializeField] private LayerMask groundMask;

        private void Update()
        {
            if (!Input.GetMouseButtonDown(0) && !Input.GetMouseButtonDown(1)) return;
            if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(minimapRect, Input.mousePosition, null, out Vector2 local)) return;
            if (!minimapRect.rect.Contains(local)) return;

            Vector2 normalized = Rect.PointToNormalized(minimapRect.rect, local);
            Ray ray = minimapCamera.ViewportPointToRay(new Vector3(normalized.x, normalized.y, 0f));
            if (!Physics.Raycast(ray, out RaycastHit hit, 500f, groundMask)) return;

            if (Input.GetMouseButtonDown(0)) worldCamera.SnapToWorldPosition(hit.point);
            if (Input.GetMouseButtonDown(1)) playerInput.MoveFromMinimap(hit.point);
        }
    }
}
