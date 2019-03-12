using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{

    private SpriteRenderer button_off;
    private SpriteRenderer button;
    private BoxCollider2D off_collider;
    private BoxCollider2D button_collider;
    private Block_Door block_door;

    [SerializeField]
    private GameObject door;

    // Start is called before the first frame update
    void Start()
    {
        button_off = transform.GetChild(1).GetComponent<SpriteRenderer>();
        button = GetComponent<SpriteRenderer>();
        off_collider = transform.GetChild(1).GetComponent<BoxCollider2D>();
        button_collider = GetComponent<BoxCollider2D>();
        off_collider.enabled = false;
        button_off.enabled = false;

        block_door = door.GetComponent<Block_Door>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void turnOff()
    {
        button_collider.enabled = false;
        button.enabled = false;
        button_off.enabled = true;
        off_collider.enabled = true;
        block_door.openDoor();
    }
}
