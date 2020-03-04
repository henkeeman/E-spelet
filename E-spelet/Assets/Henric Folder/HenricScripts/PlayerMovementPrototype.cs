using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class PlayerMovementPrototype : MonoBehaviour
{
    // Start is called before the first frame update

   
    //Spelarens Rigidbody
    Rigidbody Rigidbody;
    //InputActions
    PlayerInput InputAction;
    //LeftStickVectorMovement Vart man hämtar movement ifrån.
    Vector2 MovementInput;

    //Movement speed
    public float MvSpeed;
    //JumpForce
    public float JumpForce;
    //The input saved for FixedUpdate
    Vector3 MovementForce;
    [SerializeField]
    bool Jumping;


    private void Awake()
    {
        InputAction = new PlayerInput();
        InputAction.PlayerControls.Move.performed += ctx => MovementInput = ctx.ReadValue<Vector2>();
    }
    void Start()
    {
        Rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        ReadInput();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jumping = true;
            print("FUCK");
        }

    }

    private void FixedUpdate()
    {
        Move();
        Jump();
    }

    private void ReadInput()
    {
       
        float h = MovementInput.x;
        
        if (Mathf.Abs(h) <= 0.2f)
            h = 0;
        float hlerp = Mathf.Lerp(0, h, 1);


        //float v = MovementInput.y;

        MovementForce = new Vector3(-hlerp * MvSpeed, Rigidbody.velocity.y, Rigidbody.velocity.z);
    }
    private void Move()
    {

        Rigidbody.velocity = MovementForce;

    }
    private void Jump()
    {
        if (Jumping == true)
        {
            Rigidbody.AddForce(Vector3.up * JumpForce);
            Jumping = false;
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
