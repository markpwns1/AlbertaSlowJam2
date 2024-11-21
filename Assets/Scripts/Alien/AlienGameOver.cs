using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienGameOver : MonoBehaviour
{
    public GameOverManager gameOverManager;
    public Collider gameOverCollider;

    //private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        //rb = GetComponent<Rigidbody>();
    }

    //private void OnTriggerEnter(Collider other)
    //{

    //    //Debug.Log(other.gameObject);
    //    //Debug.Log(gameOverCollider.gameObject);
    //    // if statement to check when other collides with gameOverCollider Specifically
    //    if (other.CompareTag("Player"))
    //    {
    //        if (gameOverCollider.CompareTag("GameOverCollider"))
    //        {
    //            Debug.Log("Alien touched the player inside function");
    //            gameOverManager.ShowGameOver();
    //        }
    //    }


    //}
    private void OnTriggerEnter(Collider other)
    {
        // Check if the player's collider triggered the alien's trigger collider
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered the alien's game-over area.");
            gameOverManager.ShowGameOver();
        }
    }
}
