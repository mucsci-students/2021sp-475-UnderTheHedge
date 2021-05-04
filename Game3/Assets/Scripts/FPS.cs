using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (CharacterController))]

public class FPS : MonoBehaviour
{
    public Camera playerCamera;

    public float walkingSpeed;
    public float runningSpeed;
    public float jumpSpeed;
    public float gravity;
    public float lookSpeed;
    public float lookXLimit;

    public float stamina;
    private bool tired;

    private float originalWalk;
    private float originalRun;
    private float originaljump;
    private float originalStamina;

    private float health;

    CharacterController characterController;
    Vector3 moveDirection = Vector3.zero;
    float rotationX = 0;

    public bool canMove = true;

    AudioSource audio;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        audio = GetComponent<AudioSource>();

        // lock and hide mouse cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        originalWalk = walkingSpeed;
        originalRun = runningSpeed;
        originaljump = jumpSpeed;
        originalStamina = stamina;

        tired = false;
    }

    void Update()
    {
        // basic movement
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 leftOrRight = transform.TransformDirection(Vector3.right);

        bool isMoving = false;
        bool isRunning = false;

        if (Input.GetAxis("Vertical") != 0) 
            isMoving = true; 
        else
            isMoving = false;

        if (isMoving && !audio.isPlaying) 
            audio.Play();

        if (!isMoving) 
            audio.Stop();

        // left-shift allows player to sprint
        // stops the player from running when they are tired
        if (!tired)
            isRunning = Input.GetKey(KeyCode.LeftShift);

        // left-control allows player to crouch
        bool isCrouching = Input.GetKey (KeyCode.LeftControl);

        // while crouching, player cannot move except for mouse
        if (isCrouching)
        {
            playerCamera.transform.localPosition = new Vector3 (0f, .82f, 0f);

            walkingSpeed = 0;
            runningSpeed = 0;
            jumpSpeed = 0;
        }

        // stop crouching, restore movement
        else
        {
            playerCamera.transform.localPosition = new Vector3 (0f, 1.64f, 0f);

            walkingSpeed = originalWalk;
            runningSpeed = originalRun;
            jumpSpeed = originaljump;
        }

        // determine character speed based on if they are running or walking
        float Xspeed = canMove ? (isRunning ? runningSpeed : walkingSpeed) * Input.GetAxis("Vertical") : 0;
        float Yspeed = canMove ? (isRunning ? runningSpeed : walkingSpeed) * Input.GetAxis("Horizontal") : 0;
        float movementDirectionY = moveDirection.y;
        moveDirection = (forward * Xspeed) + (leftOrRight * Yspeed);

        // while running, drain the player's stamina
        // if they run out of stamina, the player becomes tired
        if (isRunning)
        {
            stamina -= .075f;

            if (stamina < 0)
                tired = true;
        }

        // gradually restore stamina overtime when not running
        // "tired" status goes away when stamina reaches 50%
        else
        {
            if (stamina < originalStamina)
            {
                stamina += .0375f;
            }

            if (stamina >= originalStamina)   
                tired = false;
        }

        if (Input.GetButton("Jump") && canMove && characterController.isGrounded)
            moveDirection.y = jumpSpeed;
    
        else
            moveDirection.y = movementDirectionY;

        // apply gravity to character
        if (!characterController.isGrounded)
            moveDirection.y -= gravity * Time.deltaTime;

        // Move the controller
        characterController.Move(moveDirection * Time.deltaTime);

        // player and camera rotation
        if (canMove)
        {
            rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
        }
    }
}