using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class PlayerMovementPrototype : MonoBehaviour
{
    // Start is called before the first frame update

   
    //Spelarens Rigidbody
    Rigidbody RB;
    //InputActions
    PlayerInput InputAction;
    //LeftStickVectorMovement Vart man hämtar movement ifrån.
    Vector2 MovementInput;

    //Movement speed
    public float MvSpeed;
    //JumpForce
    public float JumpForce;
    //Ifall man hoppar eller inte
    [SerializeField]
    bool Jumping;
    //The input saved for FixedUpdate

    Vector3 MovementForce;

    //Hastigheten på karaktärern
    [SerializeField]
    float MaxSpeed;

    //Grounded check (Alla variabler ) 
    //Kollar vilka layers som räknas som mark
    [SerializeField]
    LayerMask playerLayerMask;
    //Ursprungspositionen ifrån vart man kollar vad som är ground
    [SerializeField]
    Transform GroundCheckTrans;
    //Ground Checkens radius Ifall man t.ex höjer värdet så kan gubben hoppa ifall en är långt ifrån marken
    [SerializeField]
    float CheckRadius;
    //Ifall man är på marken eller inte
    public bool Grounded;
    [SerializeField]
    float Deadzone;
    [SerializeField]
    float RotDeadzone;
    [SerializeField]
    float Smooth;

    private void Awake()
    {
        InputAction = new PlayerInput();
        InputAction.PlayerControls.Move.performed += ctx => MovementInput = ctx.ReadValue<Vector2>();
    }
    void Start()
    {
        RB = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        ReadInput();


    }

    private void FixedUpdate()
    {
        GroundCheck();
        Move();
        Jump();
        TurnThePlayer();
    }

    private void ReadInput()
    {
       
        float h = MovementInput.x;
        
        if (Mathf.Abs(h) <= 0.2f)
            h = 0;
        float hlerp = Mathf.Lerp(0, h, 1);


        //float v = MovementInput.y;
        if (Input.GetKeyDown(KeyCode.Space) && Grounded)
        {
            Jumping = true;
            print("FUCK");
        }

        MovementForce = new Vector3(hlerp,0,0);
    }
    void GroundCheck()
    {

        if (Physics.CheckSphere(GroundCheckTrans.position, CheckRadius, playerLayerMask))
        {
            Grounded = true;
        }
        else Grounded = false;

    }
    private void Move()
    {
        
        var h = MovementInput.x;
        
        var velocity = RB.velocity;
        var percent = velocity.magnitude / MaxSpeed;
        if (velocity.magnitude >= MaxSpeed)
        {
            
        }
        MovementForce = new Vector3(h, 0, 0)*MvSpeed*(1+percent);
        RB.AddForce(Vector3.down * Time.deltaTime * 100);
        RB.AddForce(MovementForce*MvSpeed,ForceMode.Impulse);
        if (velocity.magnitude>=MaxSpeed)
        {
            RB.velocity = Vector3.ClampMagnitude(RB.velocity, MaxSpeed);
        }
        //Rigidbody.AddForce(-MovementForce * MvSpeed / 2);
        //Rigidbody.velocity = MovementForce;

    }
    
    private void Jump()
    {
        if (Jumping == true)
        {
            RB.AddForce(Vector3.up * JumpForce, ForceMode.Impulse);
            Jumping = false;
        }

    }
    private void TurnThePlayer()
    {

        Vector2 input = MovementInput;
        Vector2 stickInput = (input);
        if (stickInput.magnitude < RotDeadzone)
            stickInput = Vector2.zero;
        float x = stickInput.x;
        float y = stickInput.y;

        Vector3 lookDirection = new Vector3(0, 0, -x);
        var lookRot = Camera.main.transform.TransformDirection(lookDirection);
        
        if (lookRot!= Vector3.zero)
        {
            Quaternion newRotation = Quaternion.LookRotation(lookDirection);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, newRotation, Smooth * Time.deltaTime);

        }


    }





    private void OnEnable()
    {
        InputAction.Enable();
    }
    private void OnDisable()
    {
        InputAction.Disable();
    }
}
