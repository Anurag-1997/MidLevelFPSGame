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
    public GameObject cam;
    private Quaternion camRotation;
    private Quaternion playerRotation;
    public float mouseSens;
    public float minX = -90.0f;
    public float maxX = 90.0f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        capsulecollider = GetComponent<CapsuleCollider>();
    }
    // Start is called before the first frame update
    void Start()
    {
        camRotation = cam.transform.localRotation;
        playerRotation = transform.localRotation;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        PlayerMovement();
        PlayerJumpMovement();
        float mousex = Input.GetAxis("Mouse X") * mouseSens;
        float mousey = Input.GetAxis("Mouse Y") * mouseSens;
        camRotation = camRotation * Quaternion.Euler(-mousey, 0, 0);
        playerRotation = playerRotation * Quaternion.Euler(0, mousex, 0);
        transform.localRotation = playerRotation;
        cam.transform.localRotation = camRotation;
        camRotation = ClampRotationOnXaxis(camRotation);
        

        
    }

    Quaternion ClampRotationOnXaxis(Quaternion value)
    {
        value.x /= value.w;
        value.y /= value.w;
        value.z /= value.w;
        value.w = 1.0f;
        float angleX = 2.0f * Mathf.Rad2Deg * Mathf.Atan(value.x);
        angleX = Mathf.Clamp(angleX, minX, maxX);
        value.x = Mathf.Tan(0.5f * Mathf.Deg2Rad * angleX);
        return value;
        
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
