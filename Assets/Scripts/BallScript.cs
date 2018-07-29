using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour
{
    bool isMoving;
    void Start()
    {

    }

    public void SendBall(Vector3 dir, float speed)
    {
        isMoving = true;
        GetComponent<Rigidbody2D>().AddForce(dir * speed, ForceMode2D.Impulse);
        GetComponent<Rigidbody2D>().simulated = true;

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "LowPaddle")
        {
            if (GameControllerScript.newPos == Vector3.zero) GameControllerScript.newPos = (Vector2)transform.position;
            GameControllerScript.ballsBack++;
            Destroy(gameObject);
        }
    }
}
