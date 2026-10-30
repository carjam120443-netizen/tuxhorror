using UnityEngine;

/// <summary>
/// Basic third-person movement for TuxHorror.
/// Uses Unity's legacy Input API. Set Active Input Handling to Both
/// (or Input Manager (Old)) in Project Settings.
/// </summary>
[RequireComponent(typeof(CharacterController))]
public sealed class TuxThirdPersonController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField, Min(0f)] private float walkSpeed = 4f;
    [SerializeField, Min(0f)] private float runSpeed = 7f;
    [SerializeField, Min(0f)] private float rotationSpeed = 12f;
    [SerializeField, Min(0f)] private float jumpHeight = 1.4f;
    [SerializeField] private float gravity = -20f;
    [Header("Camera")]
    [SerializeField] private Transform cameraTransform;

    private CharacterController characterController;
    private Vector3 verticalVelocity;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        if (cameraTransform == null && Camera.main != null)
            cameraTransform = Camera.main.transform;
    }

    private void Update() => MoveCharacter();

    private void MoveCharacter()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 input = Vector3.ClampMagnitude(new Vector3(horizontal, 0f, vertical), 1f);

        Vector3 cameraForward = cameraTransform != null ? cameraTransform.forward : transform.forward;
        Vector3 cameraRight = cameraTransform != null ? cameraTransform.right : transform.right;
        cameraForward.y = 0f;
        cameraRight.y = 0f;
        cameraForward.Normalize();
        cameraRight.Normalize();

        Vector3 moveDirection = cameraForward * input.z + cameraRight * input.x;
        if (moveDirection.sqrMagnitude > 0.001f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation,
                rotationSpeed * Time.deltaTime);
        }

        float speed = Input.GetKey(KeyCode.LeftShift) ? runSpeed : walkSpeed;
        characterController.Move(moveDirection * speed * Time.deltaTime);

        if (characterController.isGrounded && verticalVelocity.y < 0f)
            verticalVelocity.y = -2f;
        if (characterController.isGrounded && Input.GetButtonDown("Jump"))
            verticalVelocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);

        verticalVelocity.y += gravity * Time.deltaTime;
        characterController.Move(verticalVelocity * Time.deltaTime);
    }

    public void SetCamera(Transform newCamera) => cameraTransform = newCamera;
}
