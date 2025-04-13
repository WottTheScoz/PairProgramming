using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public BulletPooling bulletPooling;
    public TextMeshProUGUI scoreUI;

    GameObject[] bulletObjects;

    int score = 0;

    void Start()
    {
        // Subscribes UpdateScore to all bullets only once the bullet pool has been filled.
        StartCoroutine(SubscribeToBullets());

        // Sets the initial score. Text should display "null" if this doesn't run properly
        scoreUI.text = "" + score;
    }

    // Adds a point every time a bullet hits an asteroid
    void UpdateScore()
    {
        ++score;
        scoreUI.text = "" + score;
    }

    // Resets the score. Called by StartEndGame
    public void ResetScore()
    {
        score = 0;
        scoreUI.text = "" + score;
    }

    // Gets each bullet in the pool and subscribes UpdateScore to it. Needs to be called after BulletPooling's Start method.
    IEnumerator SubscribeToBullets()
    {
        yield return new WaitForSeconds(0.1f);
        bulletObjects = bulletPooling.GetBulletPool();

        foreach(GameObject bulletObj in bulletObjects)
        {
            Bullet bullet = bulletObj.GetComponent<Bullet>();

            bullet.OnHitAsteroid += UpdateScore;
        }
    }
}
