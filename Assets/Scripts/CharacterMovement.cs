using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController))]

public class CharacterMovement : MonoBehaviour
{
    // Handling variables    
    public float walkRotationSpeed = 1000;
    public float walkSpeed = 10f;
    public float msMulti = 1f;
    public float aimSpeedMulti = .5f;

    //strings for different move tyes, and a var for passing into methods
    private string movementType;
    private const string running = "Running";

    //vars for dash
    private bool canDash = true;
    private float nextPossibleDash;
    public float dashCooldown = .5f;
    public float dashDistance = 5;
    public ParticleSystem dashEffect;

    // System
    private Quaternion targetRotation;

    // Components
    private CharacterController controller;
    private Camera cam;

    // Use this for initialization
    void Start()
    {
        controller = GetComponent<CharacterController>();
        cam = Camera.main;
    }

    // Update is called once per frame
    //Creates a vector from inputs and passes it to movement and rotation scripts.
    void Update()
    {
        Vector3 rawInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        
        //move and rotate character
        movementType = running;
        Look(movementType, rawInput);
        Move(movementType, rawInput);
    }

    //Handles the rotation of our character
    void Look(string movementType, Vector3 input)
    {
        if (movementType == running && input != Vector3.zero)
        {
            targetRotation = Quaternion.LookRotation(input);
            transform.eulerAngles = Vector3.up * Mathf.MoveTowardsAngle(transform.eulerAngles.y, targetRotation.eulerAngles.y, walkRotationSpeed * Time.deltaTime);
        }
    }

    //Handles the motion of our character.
    void Move(string movementType, Vector3 input)
    {
        //rough normalise vector
        input *= (Mathf.Abs(input.x) == 1 && Mathf.Abs(input.z) == 1) ? .7f : 1;
        input *= walkSpeed;
        input *= msMulti;
        input += Vector3.up * -8;

        controller.Move(input * Time.deltaTime);
    }
}

