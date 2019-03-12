using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button_collider : MonoBehaviour
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
            Button button = this.GetComponentInParent<Button>();
            this.GetComponent<BoxCollider2D>().enabled = false;
            button.turnOff();
        }
    }
}
