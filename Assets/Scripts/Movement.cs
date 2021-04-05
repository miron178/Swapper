using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField]
    private bool  canJump    = false;
    [SerializeField]
    private bool  canClimb   = false;
    [SerializeField]
    private float moveSpeed  = 5.0f;
    [SerializeField]
    private float jumpHeight = 5.0f;
    [SerializeField]
    private float climbSpeed = 3.0f;

    private CharacterController controller;
    private Vector3 playerVelocity = Vector3.zero;
    private float gravityValue = -9.81f;

    private float limitClimbingRaycastDistance = 1f;

    private float jumpHoldOffTime = 0.5f; //in seconds
    private float jumpLastTime = 0;

    private float climbHoldOffTime = 2f; //in seconds
    private float climbLastTime = 0;

    private int slimeLayer = 0;

    //Camera angle
    private TPCRotate cameraRotation;
    int climbCameraAngle;

    private void OnEnable() //runs before Start()
    {
        cameraRotation = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<TPCRotate>();
        cameraRotation.SetCurrentTarget(transform); //look at me
    }

    private void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();
        slimeLayer = LayerMask.NameToLayer("Slime");
    }

    private enum State
    {
        WALKING,
        CLIMBING,
        FALLING
    }
    private State state = State.FALLING;

    void Update()
    {

        switch (state)
        {
            case State.WALKING:
                Walk();
                break;
            case State.CLIMBING:
                Climb();
                break;
            case State.FALLING:
                Fall();
                break;
            default:
                Debug.LogError("The state is unknown.");
                break;
        }

        if (this.gameObject.layer == slimeLayer && state != State.CLIMBING)
        {
            RaycastHit hit;
            if (CanClimb() && IsTouchingClimbable(out hit))
            {
                StartClimb(hit.normal);
            }
        }
    }

    private bool IsTouchingClimbable(out RaycastHit hit)
    {
        Debug.DrawRay(transform.position, transform.forward * limitClimbingRaycastDistance, Color.red);
        int layerMask = LayerMask.GetMask("Climbable");
        return Physics.Raycast(transform.position, transform.forward, out hit, limitClimbingRaycastDistance, layerMask);
    }
    private bool IsTouchingClimbable()
    {
        return IsTouchingClimbable(out _);
    }

    private void StartWalk()
    {
        state = State.WALKING;
        playerVelocity.y = 0;
    }

    // set horizontal movement velocity
    private void UpdatePlayerVelocityXZ()
    {
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        if (move == Vector3.zero)
        {
            return;
        }

        // Adjust move direction to camera angle
        move = Quaternion.Euler(0, cameraRotation.angleCurrent, 0) * move;

        //gameObject.transform.forward = move;
        gameObject.transform.rotation = Quaternion.LookRotation(move);

        Vector3 motion = move * moveSpeed;
        playerVelocity.x = motion.x;
        playerVelocity.z = motion.z;
    }

    private bool CanJump()
    {
        float now = Time.time;
        return canJump && now - jumpLastTime > jumpHoldOffTime;
    }

    private bool CanClimb()
    {
        float now = Time.time;
        return canClimb && now - climbLastTime > climbHoldOffTime;
    }

    private void Walk()
    {
        UpdatePlayerVelocityXZ();

        if (Input.GetButtonDown("Jump") && CanJump())
        {
            playerVelocity.y += jumpHeight; //initial jump speed really
            StartFall();
        }
        else
        {
            //keep controller grounded (playerVelocity.y = 0 doesn't work)
            playerVelocity.y = gravityValue * Time.deltaTime;
        }

        Vector3 movement = playerVelocity * Time.deltaTime;
        if (movement.magnitude >= controller.minMoveDistance)
        {
            controller.Move(movement);
            if (!controller.isGrounded)
            {
                StartFall();
            }
        }
    }

    private void StartClimb(Vector3 normal)
    {
        climbCameraAngle = (int)cameraRotation.angleTarget;

        gameObject.transform.rotation = Quaternion.LookRotation(normal * -1f);
        state = State.CLIMBING;

        playerVelocity.y = 0;

        float now = Time.time;
        climbLastTime = now;
    }

    private void Climb()
    {
        if (Input.GetButtonDown("Jump"))
        {
            StartFall();
            return;
        }

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        if (h == 0.0f && v == 0.0f)
        {
            climbCameraAngle = (int)cameraRotation.angleTarget;
            return;
        }
        // else keep climbCameraAngle unchanged while moving

        Vector3 move;
        switch (climbCameraAngle)
        {
            case 0:
            default:
                move = new Vector3(h, v, 0);
                break;
            case 90:
                move = new Vector3(v, -h, 0);
                break;
            case 180:
                move = new Vector3(-h, -v, 0);
                break;
            case 270:
                move = new Vector3(-v, h, 0);
                break;
        }

        playerVelocity = move * climbSpeed;
        Vector3 movement = playerVelocity * Time.deltaTime;
        if (movement.magnitude >= controller.minMoveDistance)
        {
            controller.Move(gameObject.transform.rotation * movement);
            if (!IsTouchingClimbable())
            {
                if (move.y > 0)
                {
                    playerVelocity.y += jumpHeight; //initial jump speed really
                }
                else
                {
                    playerVelocity = Vector3.zero;
                }
                StartFall();
            }
        }
    }

    private void StartFall()
    {
        state = State.FALLING;

        float now = Time.time;
        jumpLastTime = now;
    }

    private void Fall()
    {
        UpdatePlayerVelocityXZ();

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);

        if (controller.isGrounded)
        {
            StartWalk();
        }
    }

    private void OnGUI()
    {
        GUI.Label(new Rect(0,  0, 200, 20), playerVelocity.x.ToString());
        GUI.Label(new Rect(0, 15, 200, 20), playerVelocity.y.ToString());
        GUI.Label(new Rect(0, 30, 200, 20), playerVelocity.z.ToString());
    }
}