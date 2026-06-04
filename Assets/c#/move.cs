using UnityEngine;

public class move : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float speed = 5f;

    [Header("View")]
    [SerializeField] private Transform cameraPivot;
    [SerializeField] private float mouseSensitivity = 100f;

    private Vector2 moveInput;
    private float xRotation;

    private void Start()
    {
        if (rb == null)
        {
            rb = GetComponent<Rigidbody>();
        }

        if (rb != null)
        {
            rb.freezeRotation = true;
            rb.useGravity = true;
        }

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        moveInput.x = Input.GetAxis("Horizontal");
        moveInput.y = Input.GetAxis("Vertical");

        HandleLook();
    }

    private void FixedUpdate()
    {
        if (rb == null)
        {
            return;
        }

        Vector3 move = transform.right * moveInput.x + transform.forward * moveInput.y;
        Vector3 targetVelocity = move * speed;
        targetVelocity.y = rb.velocity.y;

        rb.velocity = targetVelocity;
    }

    private void HandleLook()
    {
        if (cameraPivot == null)
        {
            return;
        }

        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        cameraPivot.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }
}
