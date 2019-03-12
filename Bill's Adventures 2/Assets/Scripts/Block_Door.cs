using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block_Door : MonoBehaviour
{

    [SerializeField]
    private float TranslateX;
    [SerializeField]
    private float TranslateY;
    [SerializeField]
    private float speed;
    private Vector2 target;

    // Start is called before the first frame update
    void Start()
    {
        target = new Vector2(transform.position.x + TranslateX, transform.position.y + TranslateY);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void openDoor()
    {
        Debug.Log("Opening Door");
        StartCoroutine(Open());
    }

    private IEnumerator Open()
    {
        while (Vector2.Distance(transform.position, target) > .2f)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, target, speed);
            yield return null;

        }
        Debug.Log("Done Opening");

    }
}
