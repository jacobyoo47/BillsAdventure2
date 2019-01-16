
using UnityEngine;

public class door : MonoBehaviour, Iusable
{

    [SerializeField]
    private GameObject player;

    [SerializeField]
    private float offsetX;

    [SerializeField]
    private GameObject destinationObject;

    //private Rigidbody2D playerRigid;

    private void Awake()
    {
        //playerRigid = player.GetComponent<Rigidbody2D>();
    }

    public void Use()
    {
        Debug.Log("Door used");
        player.transform.position = (new Vector2(destinationObject.transform.position.x + offsetX, destinationObject.transform.position.y));
    }
}
