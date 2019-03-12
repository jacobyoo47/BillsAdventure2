using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneMarker : MonoBehaviour
{

    [SerializeField]
    private GameObject door;
    private Block_Door block_door;
    // Start is called before the first frame update
    void Start()
    {
        block_door = door.GetComponent<Block_Door>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            block_door.openDoor();
        }
    }
}
