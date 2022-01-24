using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public float playerSpeed;
    private Rigidbody rb;
    public float playerJumpValue;
    private bool isGrounded;
    private CapsuleCollider capsulecollider;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        capsulecollider = GetComponent<CapsuleCollider>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        PlayerMovement();
        PlayerJumpMovement();
        

        
    }
    bool PlayerGrounded()
    {
        RaycastHit hitInfo;
        if (Physics.SphereCast(transform.position, capsulecollider.radius, Vector3.down,out hitInfo, (capsulecollider.height / 2 - capsulecollider.radius + 0.1f)))
        {
            return true;
        }
        else
        {
            return false;
        }
        
    }

    void PlayerMovement()
    {
        float horizontalMovement = Input.GetAxis("Horizontal") * playerSpeed;
        float forwardMovement = Input.GetAxis("Vertical") * playerSpeed;
        transform.position += new Vector3(horizontalMovement, 0, forwardMovement);
    }
    void PlayerJumpMovement()
    {
        if (Input.GetKeyDown(KeyCode.Space) && PlayerGrounded()) 
        {
            rb.AddForce(0, playerJumpValue, 0);
        }
        
    }

}
