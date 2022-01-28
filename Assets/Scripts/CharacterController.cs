using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public float playerSpeed;
    public float runSpeed;
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
    public Animator anim;
    

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
    private void Update()
    {
        if(Input.GetMouseButton(0))
        {
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = false;
        }
        if(Input.GetKey(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        if(Input.GetKeyDown(KeyCode.Q))
        {
            anim.SetBool("Aiming", !anim.GetBool("Aiming"));
        }
        if(Input.GetMouseButtonDown(0))
        {
            //anim.SetBool("Firing", true);
            anim.SetTrigger("Firing");
        }
        //if (Input.GetMouseButtonUp(0))
        //{
        //    //anim.SetBool("Firing", false);
        //}
        if(Input.GetKeyDown(KeyCode.R))
        {
            anim.SetTrigger("Reload");
        }

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        PlayerMovement();
        PlayerJumpMovement();
        if(Input.GetKey(KeyCode.LeftShift))
        {
            PlayerRunMovement();
        }
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
        // transform.position += new Vector3(horizontalMovement, 0, forwardMovement);
        transform.position += cam.transform.forward * forwardMovement + cam.transform.right * horizontalMovement;
        if(horizontalMovement!=0||forwardMovement!=0)
        {
            anim.SetBool("walkrifle", true);
            if(Input.GetMouseButtonDown(0))
            {
                anim.SetTrigger("walkfire");
            }
        }
        else
        {
            anim.SetBool("walkrifle", false);
        }
        
    }
    void PlayerRunMovement()
    {
        float horizontalMovement = Input.GetAxis("Horizontal") * runSpeed;
        float forwardMovement = Input.GetAxis("Vertical") * runSpeed;
        // transform.position += new Vector3(horizontalMovement, 0, forwardMovement);
        transform.position += cam.transform.forward * forwardMovement + cam.transform.right * horizontalMovement;
        if (horizontalMovement != 0 || forwardMovement != 0)
        {
            anim.SetBool("runrifle", true);
            if(Input.GetMouseButtonDown(0))
            {
                anim.SetTrigger("runfire");
            }
        }
        else
        {
            anim.SetBool("runrifle", false);
        }

    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag=="Ammo")
        {
            print("Ammo Collected");
            Destroy(collision.gameObject);
        }
        else if(collision.gameObject.tag=="MedBox")
        {
            print("MedBox Collected");
            Destroy(collision.gameObject);
        }
    }

    void PlayerJumpMovement()
    {
        if (Input.GetKeyDown(KeyCode.Space) && PlayerGrounded()) 
        {
            rb.AddForce(0, playerJumpValue, 0);
        }
        
    }

}
