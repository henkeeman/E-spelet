using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class PlayerMovementPrototype : MonoBehaviour
{
    // Start is called before the first frame update

    public AudioSource jumpmusic;

    //Spelarens Rigidbody
    Rigidbody RB;
    //InputActions
    PlayerInput InputAction;
    //LeftStickVectorMovement Vart man hämtar movement ifrån.
    Vector2 MovementInput;
    
    public int Id;
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

    //
    [SerializeField]
    int JumpCharges = 0;

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
    [SerializeField]
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

    [SerializeField]
    float horizontalmovement;
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
        print(MovementInput);
        if (Grounded == true)
        {
            JumpCharges = 2;
        }
        print("FAUCK");
        //float h = MovementInput.x;
        float h = 0;
        if (Arcade.GetKey(Id,ArcadeButton.Right))
        {
            h = 1;
            print("REee");
        }
        
        if (Arcade.GetKey(Id, ArcadeButton.Left))
        {
            h = -1;
            print("REee");
        }
        print(h);
        horizontalmovement = h;
        
        
        if (Mathf.Abs(h) <= 0.2f)
            h = 0;

        if (Mathf.Abs(h) >= 0.2f)
        {
            Walking = true;
        }
        else Walking = false;
        


        //float v = MovementInput.y;
        JumpPressedRemember -= Time.deltaTime;
        if (Arcade.GetKeyDown(Id, ArcadeButton.Green))
        {
            JumpPressedRemember = fJumpPressedRememberTime;
            JumpCharges -= 1;
            jumpmusic.Play();
        }
        if (Arcade.GetKeyUp(Id, ArcadeButton.Green))
        {
            if (RB.velocity.y > 0)
            {
                RB.velocity = new Vector2(RB.velocity.x, RB.velocity.y * CutJumpHeight);
            }
        }

        if (JumpPressedRemember > 0 && JumpCharges > 0)
        {
            
            JumpPressedRemember = 0;
            RB.velocity = new Vector2(RB.velocity.x, JumpForce);
        }

        
        AnimationValues();
        float fHorizontalVelocity = RB.velocity.x;
        fHorizontalVelocity += h;

        if (Mathf.Abs(horizontalmovement) < 0.01f)
            fHorizontalVelocity *= Mathf.Pow(1f - fHorizontalDampingWhenStopping, Time.deltaTime * 10f);
        else if (Mathf.Sign(horizontalmovement) != Mathf.Sign(fHorizontalVelocity))
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
        print(horizontalmovement);
        Vector2 input = MovementInput;
        Vector2 stickInput = (input);
        Vector2 NewInput;
        NewInput = new Vector2(horizontalmovement, 0);
        Vector2 NewStickInput = NewInput;

        if (NewStickInput.magnitude < RotDeadzone)
            NewStickInput = Vector2.zero;
        float x = NewStickInput.x;
        float y = NewStickInput.y;

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
