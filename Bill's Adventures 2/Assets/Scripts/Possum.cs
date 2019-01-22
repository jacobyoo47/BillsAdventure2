using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Possum : MonoBehaviour
{

    public Player Player;
    private BoxCollider2D deathCollider;
    private BoxCollider2D possumCollider;
    private Rigidbody2D rb;
    public bool dead = false;
    private Animator anim;
    private bool isPatrolling = true;

    [SerializeField]
    private float animTime;
    [SerializeField]
    public bool faceRight = false;
    [SerializeField]
    private float speed;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player").GetComponent<Player>();
        possumCollider = GetComponent<BoxCollider2D>();
        deathCollider = transform.GetChild(0).gameObject.GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Flip();
    }

    private void FixedUpdate()
    {
        Patrol();
        handleDeath();
    }

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

    private void Patrol()
    {
        if (isPatrolling)
        {
            if (faceRight)
            {
                transform.Translate(Vector2.right * speed * Time.deltaTime);
            }
            if (!faceRight)
            {
                transform.Translate(Vector3.left * speed * Time.deltaTime);
            }
        }
    }

    private void handleDeath()
    {
        if (dead)
        {
            dead = false;
            isPatrolling = false;
            possumCollider.enabled = false;
            deathCollider.enabled = false;
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
            Debug.Log("Patrol");
        }
    }
}
