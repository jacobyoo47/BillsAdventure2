using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PossumV2 : MonoBehaviour
{
    // PUBLIC VARIABLES
    public Player Player;
    public Vector2 velocity;
    public bool dead = false;

    // PRIVATE VARIABLES
    private BoxCollider2D possumCollider;
    private Rigidbody2D rb;
    private Animator anim;
    private bool isPatrolling = true;
    private bool grounded = false;

    // SERIALIZED FIELDS
    [SerializeField]
    private float animTime;
    [SerializeField]
    public bool faceRight = false;
    [SerializeField]
    private float speed;
    [SerializeField]
    private float gravityScale;
    [SerializeField]
    private LayerMask whatisFloor;

    // Enemy states:
    public enum EnemyState
    {
        patrolling,
        falling,
        dead
    }

    // Initialize enemy as falling:
    public EnemyState state = EnemyState.patrolling;

    // Start is called before the first frame update
    void Start()
    {
        // Initialize values:
        Player = GameObject.Find("Player").GetComponent<Player>();
        possumCollider = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        enabled = false;
    }

    private void OnBecameVisible()
    {
        enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePosition();
        Flip();
    }

    private void FixedUpdate()
    {
        handleDeath();
    }

    void UpdatePosition()
    {
        if (state != EnemyState.dead)
        {
            Vector2 pos = transform.position;

            /*
            if (state == EnemyState.falling)
            {
                pos.y += velocity.y * Time.deltaTime;

                velocity.y -= gravityScale * Time.deltaTime;
            }
            */
            if (state == EnemyState.patrolling)
            {
                if (!faceRight)
                {
                    pos.x -= velocity.x * Time.deltaTime;
                } else
                {
                    pos.x += velocity.x * Time.deltaTime;
                }
            }

            /*if (velocity.y <= 0)
            {
                pos = CheckGrounded(pos);
            }*/

            transform.position = pos;
        }
    }

    // Decided to just use built-in gravity instead.
    /*
    Vector2 CheckGrounded(Vector2 pos)
    {
        Vector2 originLeft = new Vector2(pos.x - .67f, pos.y - .6f);
        Vector2 originMiddle = new Vector2(pos.x, pos.y - .6f);
        Vector2 originRight = new Vector2(pos.x + .65f, pos.y - .6f);

        Debug.DrawRay(originLeft, new Vector2(0, -.5f), Color.red, .0f, false);
        Debug.DrawRay(originMiddle, new Vector2(0, -.5f), Color.red, .0f, false);
        Debug.DrawRay(originRight, new Vector2(0, -.5f), Color.red, .0f, false);

        RaycastHit2D groundLeft = Physics2D.Raycast(originLeft, Vector2.down, velocity.y * Time.deltaTime, whatisFloor);
        RaycastHit2D groundMiddle = Physics2D.Raycast(originMiddle, Vector2.down, velocity.y * Time.deltaTime, whatisFloor);
        RaycastHit2D groundRight = Physics2D.Raycast(originRight, Vector2.down, velocity.y * Time.deltaTime, whatisFloor);

        if (groundLeft.collider != null || groundMiddle.collider != null || groundRight.collider != null)
        {
            RaycastHit2D hitRay = groundRight;

            if (groundLeft)
            {
                hitRay = groundLeft;
            } else if (groundMiddle)
            {
                hitRay = groundMiddle;
            } else if (groundRight)
            {
                hitRay = groundRight;
            }
            
            //pos.y = hitRay.collider.

            grounded = true;

            velocity.y = 0;

            state = EnemyState.patrolling;
        }
        else
        {
            if (state != EnemyState.falling)
            {
                Fall();
            }
        }

        return pos;

    }
    */
    // Decided to just use built-in gravity instead.

    private void Flip()
    {
        if (faceRight)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
        if (!faceRight)
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }
    }

    private void handleDeath()
    {
        if (state == EnemyState.dead)
        {
            rb.gravityScale = 0;
            possumCollider.enabled = false;
            StartCoroutine(Die());
            //anim.Play("enemy_death");
        }
    }

    private IEnumerator Die()
    {
        anim.SetTrigger("death");
        yield return new WaitForSeconds(animTime);
        Destroy(this.gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Player.death = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Patrol")
        {
            if (faceRight)
            {
                transform.Translate(Vector2.left * speed * .01f);
            }
            if (!faceRight)
            {
                transform.Translate(Vector2.right * speed * .01f);
            }
            faceRight = !faceRight;
            //Debug.Log("Patrol");
        }
    }
}
