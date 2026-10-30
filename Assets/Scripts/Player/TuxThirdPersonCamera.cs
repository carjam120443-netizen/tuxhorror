using UnityEngine;

/// <summary>
/// Orbit camera for TuxHorror. Hold the mouse in the Game view to look around.
/// Escape releases the cursor; click the Game view to capture it again.
/// </summary>
public sealed class TuxThirdPersonCamera : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 targetOffset = new Vector3(0f, 1.35f, 0f);
    [SerializeField, Min(0.5f)] private float distance = 5f;
    [SerializeField, Min(0f)] private float mouseSensitivity = 2.5f;
    [SerializeField] private float minPitch = -25f;
    [SerializeField] private float maxPitch = 65f;
    [SerializeField, Min(0f)] private float positionSmoothness = 15f;

    private float yaw;
    private float pitch = 12f;

    private void Start()
    {
        if (target == null)
        {
            Debug.LogWarning("TuxThirdPersonCamera needs a target Transform.", this);
            enabled = false;
            return;
        }
        Vector3 angles = transform.eulerAngles;
        yaw = angles.y;
        pitch = NormalizePitch(angles.x);
        LockCursor();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            Cursor.lockState = CursorLockMode.None;
        if (Input.GetMouseButtonDown(0))
            LockCursor();

        if (Cursor.lockState == CursorLockMode.Locked)
        {
            yaw += Input.GetAxis("Mouse X") * mouseSensitivity;
            pitch -= Input.GetAxis("Mouse Y") * mouseSensitivity;
            pitch = Mathf.Clamp(pitch, minPitch, maxPitch);
        }
    }

    private void LateUpdate()
    {
        if (target == null) return;
        Quaternion orbitRotation = Quaternion.Euler(pitch, yaw, 0f);
        Vector3 focusPoint = target.position + targetOffset;
        Vector3 desiredPosition = focusPoint - orbitRotation * Vector3.forward * distance;
        transform.position = Vector3.Lerp(transform.position, desiredPosition,
            1f - Mathf.Exp(-positionSmoothness * Time.deltaTime));
        transform.rotation = orbitRotation;
    }

    public void SetTarget(Transform newTarget) => target = newTarget;

    private static float NormalizePitch(float angle)
    {
        if (angle > 180f) angle -= 360f;
        return angle;
    }

    private static void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void OnDisable()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
