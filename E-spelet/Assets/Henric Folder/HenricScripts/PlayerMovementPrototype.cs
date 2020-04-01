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
    [SerializeField]
    private float fJumpPressedRememberTime;
    float JumpPressedRemember;
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
    [SerializeField]
    Animator _animator;
    //animation Bools Sätter dom
    bool Walking;

    [SerializeField]
    float CutJumpHeight;
    [SerializeField]
    float fHorizontalAcceleration = 1;
    [SerializeField]
    [Range(0, 1)]
    float fHorizontalDampingBasic = 0.5f;
    [SerializeField]
    [Range(0, 1)]
    float fHorizontalDampingWhenStopping = 0.5f;
    [SerializeField]
    [Range(0, 1)]
    float fHorizontalDampingWhenTurning = 0.5f;
    [SerializeField]
    float gravityScale = 8.0f;
    [SerializeField]
    static float globalGravity = -9.81f;
    private void Awake()
    {
        InputAction = new PlayerInput();
        InputAction.PlayerControls.Move.performed += ctx => MovementInput = ctx.ReadValue<Vector2>();
    }
    void Start()
    {
        RB = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
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
        TurnThePlayer();
    }

    private void ReadInput()
    {
       
        float h = MovementInput.x;
        
        if (Mathf.Abs(h) <= 0.2f)
            h = 0;
        if (Mathf.Abs(h) >= 0.2f)
        {
            Walking = true;
        }
        else Walking = false;
        


        //float v = MovementInput.y;
        JumpPressedRemember -= Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            JumpPressedRemember = fJumpPressedRememberTime;
        }
        if (Input.GetButtonUp("Jump"))
        {
            if (RB.velocity.y > 0)
            {
                RB.velocity = new Vector2(RB.velocity.x, RB.velocity.y * CutJumpHeight);
            }
        }

        if (JumpPressedRemember > 0 && Grounded)
        {
            JumpPressedRemember = 0;
            RB.velocity = new Vector2(RB.velocity.x, JumpForce);
        }

        
        AnimationValues();
        float fHorizontalVelocity = RB.velocity.x;
        fHorizontalVelocity += Input.GetAxisRaw("Horizontal");

        if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) < 0.01f)
            fHorizontalVelocity *= Mathf.Pow(1f - fHorizontalDampingWhenStopping, Time.deltaTime * 10f);
        else if (Mathf.Sign(Input.GetAxisRaw("Horizontal")) != Mathf.Sign(fHorizontalVelocity))
            fHorizontalVelocity *= Mathf.Pow(1f - fHorizontalDampingWhenTurning, Time.deltaTime * 10f);
        else
            fHorizontalVelocity *= Mathf.Pow(1f - fHorizontalDampingBasic, Time.deltaTime * 10f);

        RB.velocity = new Vector2(fHorizontalVelocity, RB.velocity.y);
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

        {
            Vector3 gravity = globalGravity * gravityScale * Vector3.up;
            RB.AddForce(gravity, ForceMode.Acceleration);
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
    private void AnimationValues()
    {
        _animator.SetBool("Walking", Walking);



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
