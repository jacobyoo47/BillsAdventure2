using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour, Iusable
{
    private GameObject twinkle;
    private Animator twinkleAnim;
    private SpriteRenderer twinkleRender;
    public Player player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        twinkle = transform.GetChild(0).gameObject;
        twinkleAnim = twinkle.GetComponent<Animator>();
        twinkleRender = twinkle.GetComponent<SpriteRenderer>();
        twinkleRender.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Use()
    {
        Debug.Log("Used Checkpoint");

        player.currentCheckpoint = transform.position;
        StartCoroutine(twinkleAnimation());
    }

    private IEnumerator twinkleAnimation()
    {
        twinkleRender.enabled = true;
        twinkleAnim.SetTrigger("Checkpoint");
        yield return new WaitForSeconds(1f);
        twinkleRender.enabled = false;
    }
}
