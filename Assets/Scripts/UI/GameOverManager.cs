using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    //public GameObject gameOverUI;

    //public AudioClip deathSound;

    private Transform playerTransform;

    // Start is called before the first frame update
    void Start()
    {
        //gameOverUI.SetActive(false);
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerTransform = player.transform;
        }
        else
        {
            Debug.LogError("Player not found");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ShowGameOver()
    {
        Debug.Log("Game Over");
        // Play the death sound at the player's position
        //if (playerTransform != null && deathSound != null)
        //{
        //    AudioSource.PlayClipAtPoint(deathSound, playerTransform.position);
        //}

        // Display the GameOver UI and freeze the game
        //gameOverUI.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 0f;
    }

    public void RestartGame()
    {
        Debug.Log("Game Restart");
        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;

        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
