using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cherry : MonoBehaviour
{
    // Start is called before the first frame update

    public cherry_counter cherryCount;

    private void Start()
    {
        GameObject cherryController = GameObject.FindWithTag("GameController"); //assign cherry controller reference object
        if (cherryController != null)
        {
            cherryCount = cherryController.GetComponent<cherry_counter>();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);
        cherryCount.AddScore(1);
    }
}
