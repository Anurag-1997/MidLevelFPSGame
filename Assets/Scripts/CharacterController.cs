using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public float playerSpeed;
    private Rigidbody rb;
    public float playerJumpValue;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
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
    void PlayerMovement()
    {
        float horizontalMovement = Input.GetAxis("Horizontal") * playerSpeed;
        float forwardMovement = Input.GetAxis("Vertical") * playerSpeed;
        transform.position += new Vector3(horizontalMovement, 0, forwardMovement);
    }
    void PlayerJumpMovement()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(0, playerJumpValue, 0);
        }
        
    }

}
