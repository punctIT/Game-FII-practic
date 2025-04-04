using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;
    public float groundDrag = 5f;
    public float jumpForce = 4f;
    public float jumpCooldown = 0.25f;
    public float airMultiplier = 0.3f;
    public float fallMultiplier = 3.5f;
    public float lowJumpMultiplier = 3f;
    private bool readyToJump = true;
    [HideInInspector]
    public bool JumpActive = false;
    
    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode sprintKey = KeyCode.LeftShift;
    public KeyCode crouchKey = KeyCode.C;
    
    [Header("Ground Check")]
    public float playerHeight = 2f;
    public float groundCheckRadius = 0.3f;
    public LayerMask whatIsGround;
    private bool grounded;
    
    [Header("Crouch")]
    private CapsuleCollider playerCollider;
    [HideInInspector]
    public bool isCrouching;
    public Transform orientation;
    private float horizontalInput;
    private float verticalInput;
    private Vector3 moveDirection;
    private Rigidbody rb;
    private float lastJumpTime;
    private float originalHeight;
    private float crouchHeight = 1f;
    
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerCollider = GetComponent<CapsuleCollider>();
        originalHeight = playerCollider.height;
        rb.freezeRotation = true;
        readyToJump = true;
    }

    private void Update()
    {
        GroundCheck();
        MyInput();
        rb.drag = grounded ? groundDrag : 0;

        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (rb.velocity.y > 0 && !Input.GetKey(jumpKey))
        {
            rb.velocity += Vector3.up * Physics.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
    }

    private void FixedUpdate()
    {
        MovePlayer();
        SpeedControl();
    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if (Input.GetKeyDown(jumpKey) && readyToJump && grounded && JumpActive)
        {
            readyToJump = false;
            Jump();
            lastJumpTime = Time.time;
            StartCoroutine(JumpCooldown());
        }

        if (Input.GetKeyDown(crouchKey)) StartCrouch();
        if (Input.GetKeyUp(crouchKey)) StopCrouch();
    }

    private void MovePlayer()
    {
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;
        moveDirection.Normalize();

        float currentSpeed = Input.GetKey(sprintKey) ? moveSpeed * 2.5f : moveSpeed;
        if (isCrouching) currentSpeed *= 0.5f;
        if (!grounded) currentSpeed *= airMultiplier;

        rb.AddForce(moveDirection * currentSpeed * 10f, ForceMode.Force);
    }

    private void StartCrouch()
    {
        if (grounded)
        {
            isCrouching = true;
            playerCollider.height = crouchHeight;
            transform.position -= new Vector3(0, (originalHeight - crouchHeight) / 2, 0);
        }
    }

    private void StopCrouch()
    {
        if (!Physics.Raycast(transform.position, Vector3.up, originalHeight - crouchHeight, whatIsGround))
        {
            isCrouching = false;
            playerCollider.height = originalHeight;
            transform.position += new Vector3(0, (originalHeight - crouchHeight) / 2, 0);
        }
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        float maxSpeed = Input.GetKey(sprintKey) ? moveSpeed * 0.5f : moveSpeed;
        if (isCrouching) maxSpeed *= 0.5f;
        
        if (flatVel.magnitude > maxSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * maxSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }

    private void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    private IEnumerator JumpCooldown()
    {
        yield return new WaitForSeconds(jumpCooldown);
        readyToJump = true;
    }

    private void GroundCheck()
    {
        grounded = Physics.CheckSphere(transform.position - new Vector3(0, playerHeight / 2, 0), groundCheckRadius, whatIsGround);
    }

    private void OnDrawGizmosSelected()
    {
        Vector3 spherePosition = transform.position - new Vector3(0, playerHeight / 2, 0);
        Gizmos.color = grounded ? Color.green : Color.red;
        Gizmos.DrawWireSphere(spherePosition, groundCheckRadius);
    }
}
