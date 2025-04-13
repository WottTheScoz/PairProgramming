using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidPooling : ObjectPooling
{
    [Tooltip("Aims each asteroid towards the player.")]
    public Transform player;
    [Tooltip("The parent object of all asteroids.")]
    public Transform asteroids;
    [Tooltip("The parent object of all possible starting points")]
    public Transform startingPoints;
    [Tooltip("The interval between which each asteroid is shot, so long as there are asteroids still pooled.")]
    public float intervalLength = 1f;

    Transform[] startingPointsArray;

    float spawnTimer = 0;
    State spawnTimerState;

    #region Unity Methods
    // Start is called before the first frame update
    void Start()
    {
        // Fills the object pool array with all asteroid objects
        SetUpPool(asteroids);

        // Gets all possible starting points
        startingPointsArray = new Transform[startingPoints.childCount];
        for(int i = 0; i < startingPointsArray.Length; ++i)
        {
            startingPointsArray[i] = startingPoints.GetChild(i);
        }

        // Prevents script from "spawning" asteroids on start.
        spawnTimerState = State.Off;
    }

    void Update()
    {
        SpawnSystem();
    }
    #endregion

    #region Asteroid Pooling System
    void ShootAsteroid()
    {
        // Loop assumes each object has an Asteroid component.
        foreach(GameObject asteroidObj in objectPool)
        {
            Asteroid asteroid = asteroidObj.GetComponent<Asteroid>();

            if(asteroid.IsInPool())
            {
                // Picks a random spawn point and shoots the asteroid from there.
                asteroidObj.transform.position = startingPointsArray[Random.Range(0, startingPointsArray.Length)].position;
                asteroid.ShootSelf(player);
                break;
            }
        }
    }

    void SpawnSystem()
    {
        if(spawnTimerState == State.On)
        {
            if(spawnTimer < intervalLength)
            {
                spawnTimer += Time.deltaTime;
            }
            else
            {
                ShootAsteroid();
                spawnTimer = 0;
            }
        }
    }

    // Resets the position of all asteroids. Called by other StartEndGame.
    public void ResetAllAsteroids()
    {
        foreach(GameObject asteroidObj in objectPool)
        {
            Asteroid asteroid = asteroidObj.GetComponent<Asteroid>();

            if(!asteroid.IsInPool())
            {
                asteroid.BackToPool();
            }
        }
    }
    #endregion

    #region State Modifiers
    public void ToggleAsteroids(bool turnOn)
    {
        spawnTimerState = ToggleState(turnOn);
    }
    #endregion
}
