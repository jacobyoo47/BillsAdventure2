using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PossumDeath : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Possum possum = this.GetComponentInParent<Possum>();
            possum.dead = true;

            Rigidbody2D player_rb = collision.gameObject.GetComponent<Rigidbody2D>();

            Vector2 temp = player_rb.velocity;
            temp.y = 0;
            player_rb.velocity = temp;
            player_rb.velocity += new Vector2(0, 10);
            if (Input.GetButton("Jump"))
            {
                player_rb.velocity += new Vector2(0, 5);
            }
        }
    }
}
