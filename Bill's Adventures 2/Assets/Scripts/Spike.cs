using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{

    [SerializeField]
    public Player Player;

    // Start is called before the first frame update
    void Start()
    {
        GameObject PlayerController = GameObject.FindWithTag("Player");
        Player = PlayerController.GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player.death = true;
    }
}
