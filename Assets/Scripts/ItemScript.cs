using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemScript : MonoBehaviour
{
    public GameObject ballPrefab;
    Transform ballCol;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            ballCol = collision.gameObject.transform;
            Invoke("spawnBall", 0.1f);
            gameObject.SetActive(false);
        }
    }

    void spawnBall()
    {
        GameObject ball = Instantiate(ballPrefab, ballCol.position, ballCol.rotation);
        ball.GetComponent<BallScript>().SendBall(GameControllerScript.destination, GameControllerScript.speed);
        GameControllerScript.maxBalls++;
        Destroy(gameObject);

    }
}
