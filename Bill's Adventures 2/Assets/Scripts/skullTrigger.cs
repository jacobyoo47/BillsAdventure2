using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skullTrigger : MonoBehaviour
{

    private Eagle eagle;
    private Vector2 newTarget;
    private int currentTargetCounter = 1;
    private Vector2 currentTarget;

    // Start is called before the first frame update
    void Start()
    {
        GameObject eagleController = GameObject.FindWithTag("Eagle");
        eagle = eagleController.GetComponent<Eagle>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void SwitchTarget()
    {
        currentTarget = newTarget;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            currentTarget = new Vector2(transform.position.x, transform.position.y + 2);
            eagle.RushTarget(currentTarget);
            currentTargetCounter += 1;
        }
    }

}
