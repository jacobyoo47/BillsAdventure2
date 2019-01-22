using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolCollider : MonoBehaviour
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
        if (collision.gameObject.tag == "Possum")
        {
            collision.gameObject.GetComponent<Possum>().faceRight = !collision.gameObject.GetComponent<Possum>().faceRight;
            Debug.Log("Patrol2");
        }
    }
}
