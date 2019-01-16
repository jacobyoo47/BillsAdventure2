
using UnityEngine;

public class sign : MonoBehaviour, Iusable
{
    [SerializeField]
    private Canvas canvas;

    public void Awake()
    {
        canvas.enabled = false;
    }
    public void Use()
    {
        Debug.Log("sign used");
        if (canvas.enabled == false)
        {
            canvas.enabled = true;
        }
        else
        {
            canvas.enabled = false;
        }

    }
}
