using UnityEngine;

namespace MobaPrototype.CameraSystem
{
    public class MobaCameraController : MonoBehaviour
    {
        [SerializeField] private Transform followTarget;
        [SerializeField] private float height = 18f;
        [SerializeField] private float followSpeed = 8f;
        [SerializeField] private float panSpeed = 24f;
        [SerializeField] private float dragSpeed = 1f;
        [SerializeField] private Vector2 mapMin = new(-50f, -50f);
        [SerializeField] private Vector2 mapMax = new(50f, 50f);

        private Vector3 dragOrigin;
        private bool dragging;

        private void LateUpdate()
        {
            HandleKeyboardPan();
            HandleMiddleMouseDrag();

            if (followTarget != null && !dragging)
            {
                Vector3 target = new(followTarget.position.x, height, followTarget.position.z);
                transform.position = Vector3.Lerp(transform.position, target, followSpeed * Time.deltaTime);
            }

            ClampPosition();
        }

        private void HandleKeyboardPan()
        {
            Vector3 input = new(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
            transform.position += input.normalized * panSpeed * Time.deltaTime;
        }

        private void HandleMiddleMouseDrag()
        {
            if (Input.GetMouseButtonDown(2))
            {
                dragging = true;
                dragOrigin = Input.mousePosition;
            }

            if (Input.GetMouseButton(2))
            {
                Vector3 delta = Input.mousePosition - dragOrigin;
                transform.position -= new Vector3(delta.x, 0f, delta.y) * dragSpeed * Time.deltaTime;
                dragOrigin = Input.mousePosition;
            }

            if (Input.GetMouseButtonUp(2)) dragging = false;
        }

        public void SnapToWorldPosition(Vector3 worldPosition)
        {
            transform.position = new Vector3(worldPosition.x, height, worldPosition.z);
            ClampPosition();
        }

        private void ClampPosition()
        {
            transform.position = new Vector3(
                Mathf.Clamp(transform.position.x, mapMin.x, mapMax.x),
                height,
                Mathf.Clamp(transform.position.z, mapMin.y, mapMax.y));
        }
    }
}
