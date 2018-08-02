using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameControllerScript : MonoBehaviour
{
    public GameObject ballPrefab;
    public GameObject arrowPointer;
    public GameObject lvl;
    public GameObject tutor;
    public GameObject gameSpeed;
    public Text ballsCountText;
    public Text scoreText;
    public Text bestScoreText;
    public static float speed = 12f;
    public static Vector2 destination;
    public static int maxBalls;
    public bool isFlying;
    public static Vector3 newPos;
    public static int level;
    public static int ballsBack;
    Quaternion startRotation;
    Vector2 swipeFrom;
    int ballsToSpawn;
    bool hold;
    bool released;
    GameObject firstball;
    float swipingEdge = 25f;

    void Start()
    {
        nullificationOfStatics();

        startRotation = transform.rotation;
        firstball = Instantiate(ballPrefab, transform.position, startRotation, transform);
        ballsToSpawn = maxBalls;
        ballsToSpawn--;
        Physics2D.IgnoreLayerCollision(8, 8);
        bestScoreText.text = PlayerPrefs.GetInt("BestScore").ToString();
        changeLevel();
        gameSpeed.SetActive(false);
    }

    private static void nullificationOfStatics()
    {
        destination = Vector2.zero;
        maxBalls = 1;
        newPos = Vector3.zero;
        level = 0;
        ballsBack = 0;
    }

    void Update()
    {
        if (isFlying)
        {
            if (int.Parse(ballsCountText.text) != maxBalls) ballsCountText.text = maxBalls.ToString();
            if (ballsBack == maxBalls)
            {
                gameSpeed.GetComponent<TimeScaleScript>().setDefault();
                gameSpeed.SetActive(false);
                isFlying = false;
                changeLevel();
            }
        }
        if (!isFlying)
        {
            if (ballsBack != 0)
            {
                ballsBack = 0;
                ballsToSpawn = maxBalls;
            }

            spawnBallAtNewPos();
            BallLaunching();
        }

    }

    private void BallLaunching()
    {
        if (Input.mousePosition.y > swipingEdge)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                swipeFrom = Input.mousePosition;
                hold = true;

            }
            else if (Input.GetKeyUp(KeyCode.Mouse0))
            {
                released = true;
                hold = false;
            }
        }
        if (hold)
        {
            arrowPointer.SetActive(true);
            Vector2 newDestination = ((Vector2)Input.mousePosition - swipeFrom).normalized * -1;
            if (newDestination.y > 0.2) destination = newDestination;
            if (destination.y > 0 ) transform.up = destination;

        }
        if (released)
        {
            gameSpeed.SetActive(true);
            tutor.SetActive(false);
            released = false;
            arrowPointer.SetActive(false);
            firstball.GetComponent<BallScript>().SendBall(destination, speed);
            InvokeRepeating("spawnBalls", 0.1f, 0.1f);
            isFlying = true;
        }
    }

    private void spawnBallAtNewPos()
    {
        if (newPos != Vector3.zero)
        {
            newPos.y = transform.position.y;
            transform.position = newPos;
            firstball = Instantiate(ballPrefab, newPos, startRotation, transform);
            ballsToSpawn--;
            newPos = Vector3.zero;
        }
    }

    internal static void gameOver()
    {
        if (PlayerPrefs.GetInt("BestScore") < level) PlayerPrefs.SetInt("BestScore", level);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void spawnBalls()
    {
        if (ballsToSpawn > 0)
        {
            GameObject spawnedBall = Instantiate(ballPrefab, transform.position, startRotation, transform);
            spawnedBall.GetComponent<BallScript>().SendBall(destination, speed);
            ballsToSpawn--;
        }
        else CancelInvoke("spawnBalls");
    }
    void changeLevel()
    {
        level += 1;
        scoreText.text = level.ToString();
        lvl.GetComponent<LevelScript>().nextLevel();
    }
}
