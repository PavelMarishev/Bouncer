using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoxScript : MonoBehaviour
{
    public Text boxHealth;
    int maxHealth, minHealth;
    int minPercent;
    int health;
    void Start()
    {
        maxHealth = GameControllerScript.level;
        minPercent = 40;
        minHealth = (int)(maxHealth * minPercent / 100);
        health = UnityEngine.Random.Range(minHealth, maxHealth);
        if (health == 0) health = 1;
        boxHealth.text = health.ToString();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball")) health--;
        boxHealth.text = health.ToString();
        if (health <= 0) Destroy(gameObject);
    }

}
