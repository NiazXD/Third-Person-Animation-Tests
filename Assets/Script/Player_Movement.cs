using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    

    public float moveSpeed=12f;
    public float gravity = -9.81f;
    public float jumpForce = 2f;

    public float mouseSensitivity = 2f;
    public Transform cameraHolder;
    public float minPitch = -40f;
    public float maxPitch = 70f;

    CharacterController controller;
    Animator animator;
    Vector3 velocity;
    float cameraPitch;

    float inputCalc;

    void Start()
    {
        controller=GetComponent<CharacterController>();
        animator=GetComponent<Animator>();

        Cursor.lockState= CursorLockMode.Locked;
        Cursor.visible = false;
    }

    
    void Update()
    {
        Look();
        Move();
        HandleAnimations();
        HandleUpperBody();
    }

    void Move()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 moveDir = (transform.right * x + transform.forward * z).normalized;
        inputCalc = moveDir.magnitude;
        controller.Move(moveDir * moveSpeed * Time.deltaTime);

        if (controller.isGrounded && velocity.y<0)
        {
            velocity.y = -2f;
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }


    void Look()
    {
        float mouseX=Input.GetAxis("Mouse X")*mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        transform.Rotate(Vector3.up * mouseX);

        cameraPitch -= mouseY;
        cameraPitch = Mathf.Clamp(cameraPitch, minPitch, maxPitch);
        cameraHolder.localRotation = Quaternion.Euler(cameraPitch, 0f, 0f);
    }

    void HandleAnimations()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");



        animator.SetFloat("Speed", inputCalc);
        animator.SetFloat("MoveX", x);
        animator.SetFloat("MoveZ", z);

    }

    void HandleUpperBody()
    {
        bool hasGun = true;
        float targetWeight = hasGun ? 1f : 0f;

        animator.SetLayerWeight(animator.GetLayerIndex("UpperBody"), Mathf.Lerp(animator.GetLayerWeight(animator.GetLayerIndex("UpperBody")), targetWeight, Time.deltaTime * 10f));
    }
}
