using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float jumpPower;
    [SerializeField]
    private float lowMultiplier;
    [SerializeField]
    private float fallMultiplier;
    [SerializeField]
    private float movePower;
    [SerializeField]
    private Transform[] groundChecks;
    [SerializeField]
    private float groundRadius;
    [SerializeField]
    private LayerMask whatIsGround;
    [SerializeField]
    private Canvas deathUI;
    [SerializeField]
    private Transform defaultCheckpoint;

    private Transform cameraTransform;
    private Iusable useable;
    private Animator playerAnim;
    private Rigidbody2D rigidBody;
    private Vector2 currentCheckpoint;
    private bool isGrounded = true;
    private bool jump = false;
    private bool highJump = false;
    public bool death = false;
    private bool canMove = true;
    private bool cameraFollow = true;
    private bool holdingJump = false;
    private bool canRespawn = false;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        playerAnim = GetComponent<Animator>();
        deathUI.enabled = false;
        cameraTransform = Camera.main.transform;
        currentCheckpoint = defaultCheckpoint.position;
    }

    private void Update()
    {
        playerInput();
        isGrounded = checkGrounded();
        handleJumpAnim();
        handleDeathAnim();
        handleCamera();
        handleRespawn();
    }

    void FixedUpdate()
    {
        // Gets -1 or 1 depending on horizontal input
        float horizontal = Input.GetAxis("Horizontal");
        playerMovement(horizontal);
        playerJump();
        betterJump();

        //playerInput();
        //Debug.Log(isGrounded);
    }

    public void resetValues()
    {
        //resets all values of the player after death.
        death = false;
        cameraFollow = true;
        canMove = true;
        deathUI.enabled = false;
        canRespawn = false;
        rigidBody.gravityScale = 1.6f;
    }

    private void handleCamera()
    {
        if (cameraFollow)
        {
            cameraTransform.position = new Vector3(transform.position.x, transform.position.y, -10);
        }
    }

    private void playerMovement(float horizontal)
    {
        if (canMove)
        {
            rigidBody.velocity = new Vector2(horizontal * movePower, rigidBody.velocity.y); // Set velocity to movePower

            //Debug.Log(rigidBody.velocity);
            playerAnim.SetFloat("speed", Mathf.Abs(horizontal)); // Handles run animation

            if (horizontal > 0)
            {
                gameObject.GetComponent<SpriteRenderer>().flipX = false;
            }
            else if (horizontal < 0)
            {
                gameObject.GetComponent<SpriteRenderer>().flipX = true;
            }
        }
    }

    private void betterJump()
    {
        if (rigidBody.velocity.y < 0)
        {
            rigidBody.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier) * Time.deltaTime;
        }
        else if (rigidBody.velocity.y > 0 && !holdingJump)
        {
            rigidBody.velocity += Vector2.up * Physics2D.gravity.y * (lowMultiplier) * Time.deltaTime;
        }
    }

    private void playerJump()
    {
        if (canMove)
        {
            if (jump && isGrounded)
            {
                Debug.Log("jumping");
                rigidBody.velocity += (new Vector2(0, jumpPower));
                //isGrounded = false;
                jump = false;
            }
        }
    }

    private void playerInput()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Use();
        }
        if(Input.GetButton("Jump"))
        {
            holdingJump = true;
        }
        if (!Input.GetButton("Jump"))
        {
            holdingJump = false;
        }
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            jump = true;
        }
        if (Input.GetKeyDown(KeyCode.F) && death != true)
        {
            death = true;
        }
        else if (Input.GetKeyDown(KeyCode.F) && death == true)
        {
            death = false;
        } 

    }
    
    private void Use()
    {
        if (useable != null)
        {
            useable.Use();
        }
    }

    private void handleJumpAnim()
    {
        if (rigidBody.velocity.y > 5)
        {
            playerAnim.SetBool("isFalling", true);
        }
        else
        {
            playerAnim.SetBool("isFalling", false);
        }
    }

    private void handleDeathAnim()
    {
        if (death)
        {
            playerAnim.SetLayerWeight(playerAnim.GetLayerIndex("Base"), 0);
            playerAnim.SetLayerWeight(playerAnim.GetLayerIndex("Death"), 1);
            canMove = false;
            canRespawn = true;
            cameraFollow = false;
            death = false;

            StartCoroutine(playerDeath());


        }
    }

    private void handleRespawn()
    {
        if (canRespawn)
        {
            if (Input.GetKeyDown(KeyCode.T))
            {
                StopAllCoroutines();
                playerAnim.SetLayerWeight(playerAnim.GetLayerIndex("Base"), 1);
                playerAnim.SetLayerWeight(playerAnim.GetLayerIndex("Death"), 0);
                GetComponent<BoxCollider2D>().enabled = true;
                rigidBody.velocity = new Vector2(0, 0);
                transform.position = currentCheckpoint;
                resetValues();
            }
        }
    }

    private IEnumerator playerDeath()
    {
        rigidBody.velocity = new Vector2(0, 0);
        rigidBody.gravityScale = 0;
        yield return new WaitForSeconds(1);

        GetComponent<BoxCollider2D>().enabled = false;

        rigidBody.AddForce(new Vector2(0, 600));
        rigidBody.gravityScale = 2.5f;
        yield return new WaitForSeconds(1);

        deathUI.enabled = true;

        Debug.Log("Death finished");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Use")
        {
            useable = collision.GetComponent<Iusable>();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (useable != null)
        {
            useable = null;
        }
    }

    private bool checkGrounded()
    {
        if (rigidBody.velocity.y <= .5)
        {
            foreach (Transform point in groundChecks) // Create array of circleOverlapAll colliders 
            {
                Collider2D[] colliders = Physics2D.OverlapCircleAll(point.position, groundRadius, whatIsGround);

                for (int i = 0; i < colliders.Length; i++)
                {
                    if (colliders[i].gameObject != gameObject)
                    {
                        return true;
                    }
                }

            }
        }
        return false;
    }


}
