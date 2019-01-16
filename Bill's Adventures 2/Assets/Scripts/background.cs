using UnityEngine;

public class background : MonoBehaviour
{
    [SerializeField]
    private float backgroundSize;
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private float parallaxSpeed;

    private Transform cameraTransform;
    private Transform[] backgrounds;
    private Rigidbody2D player_rb;
    private float viewZone = 5;
    private int leftBack = 0;
    private int rightBack = 2;

    private void Start()
    {
        cameraTransform = Camera.main.transform;
        backgrounds = new Transform[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            backgrounds[i] = transform.GetChild(i);
        }

        player_rb = player.GetComponent<Rigidbody2D>();

    }

    private void Update()
    {
        if ((backgrounds[rightBack].position.x - cameraTransform.position.x) < viewZone)
        {
            scroll_right();
        }

        if ((cameraTransform.position.x - backgrounds[leftBack].position.x) < viewZone)
        {
            scroll_left();
        }

        // Parallax Background

        if (player_rb.velocity.x != 0)
        {
            Vector2 newPos = transform.position; //Copy to temp variable
            newPos.x = transform.position.x - (parallaxSpeed * player_rb.velocity.x); //modify variable
            transform.position = newPos; //save back to original transform
        }

    }

    private void scroll_right()
    {
        float newRightPos = backgrounds[rightBack].position.x + backgroundSize;
        backgrounds[leftBack].position = new Vector2(newRightPos, backgrounds[0].position.y);

        rightBack = leftBack;
        if (leftBack < 2)
        {
            leftBack++;
        }
        else
        {
            leftBack = 0;
        }

    }

    private void scroll_left()
    {
        float newLeftPos = backgrounds[leftBack].position.x - backgroundSize;
        backgrounds[rightBack].position = new Vector2(newLeftPos, backgrounds[0].position.y);

        leftBack = rightBack;
        if (rightBack > 0)
        {
            rightBack--;
        }
        else
        {
            rightBack = 2;
        }

    }
}
