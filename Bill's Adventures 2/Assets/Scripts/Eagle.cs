using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eagle : MonoBehaviour
{

    public Player Player;

    [SerializeField]
    private float speed;

    public Vector2 target;
    private float xpos;
    public bool hovering = true;

    // Start is called before the first frame update
    void Start()
    {
        GameObject PlayerController = GameObject.FindWithTag("Player");
        Player = PlayerController.GetComponent<Player>();

    }

    // Update is called once per frame

    private void Update()
    {
        setValues();
    }

    private void FixedUpdate()
    {
        lookPlayer();

    }

    public void RushTarget(Vector2 target)
    {
        StartCoroutine(Dive(target));
    }

    private IEnumerator Dive(Vector2 target)
    {
        while (Vector2.Distance(transform.position, target) > .2f)
        {
            transform.position = Vector2.MoveTowards(transform.position, target, speed);
            yield return null;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Player.death = true;
        }
    }

    private void setValues()
    {
        xpos = Player.transform.position.x;
    }

    private void lookPlayer()
    {
        if (Player.transform.position.x > transform.position.x)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
        }
        if (Player.transform.position.x < transform.position.x)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
        }
    }

}
