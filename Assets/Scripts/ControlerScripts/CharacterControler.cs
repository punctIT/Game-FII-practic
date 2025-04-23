using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public InventoryData InventoryManager;
    [Header("Step Climbing")]
    public float stepHeight = 0.3f;
    public float stepSmooth = 0.1f;
    public float stepRayLength = 0.3f;

    [Header("Movement")]
    public float moveSpeed = 5f;
    public float groundDrag = 5f;
    public float jumpForce = 4f;
    public float jumpCooldown = 0.25f;
    public float airMultiplier = 0.3f;
    public float fallMultiplier = 3.5f;
    public float lowJumpMultiplier = 3f;
    private bool readyToJump = true;

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
    
    [Header("Footstep Sounds")] 
    public AudioClip footstepSound;          // Sunetul pasului
    public float stepInterval = 0.5f;        // Interval între pași
    private float nextStepTime = 0f;         // Timpul următorului pas
    private AudioSource audioSource; 

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerCollider = GetComponent<CapsuleCollider>();
        originalHeight = playerCollider.height;
        rb.freezeRotation = true;
        readyToJump = true;
        if (InventoryManager != null)
        {
            rb.position = InventoryManager.playerPosititon;
            Debug.Log("Pozitia setata la: " + InventoryManager.playerPosititon);
        }
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();
    }

    private void Update()
    {
        GroundCheck();
        MyInput();
        rb.drag = grounded ? groundDrag : 0;

        HandleFootsteps();

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
        StepClimb();
        SpeedControl();
    }

    private void HandleFootsteps()
{
    // Nu reda pași dacă nu e pe sol sau viteza e foarte mică
    Vector3 flatVelocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
    
    if (!grounded || flatVelocity.magnitude < 0.1f)
        return;

    if (Time.time >= nextStepTime)
    {
        if (footstepSound != null)
            audioSource.PlayOneShot(footstepSound);

        nextStepTime = Time.time + stepInterval;
    }
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
    private void StepClimb()
    {
        if (!grounded || isCrouching) return;
        
        float rayForwardDistance = 0.5f;
        float lowerRayHeight = 0.1f;
        float upperRayHeight = stepHeight;

        Vector3 originLower = transform.position + new Vector3(0f, lowerRayHeight, 0f);
        Vector3 originUpper = transform.position + new Vector3(0f, upperRayHeight, 0f);

        // Cast din fața jucătorului
        if (Physics.Raycast(originLower, orientation.forward, out RaycastHit hitLower, rayForwardDistance, whatIsGround))
        {
            if (!Physics.Raycast(originUpper, orientation.forward, rayForwardDistance, whatIsGround))
            {
                rb.position += new Vector3(0f, stepSmooth, 0f);
            }
        }

        // Debug rays (opțional)
        Debug.DrawRay(originLower, orientation.forward * rayForwardDistance, Color.red);
        Debug.DrawRay(originUpper, orientation.forward * rayForwardDistance, Color.green);
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
