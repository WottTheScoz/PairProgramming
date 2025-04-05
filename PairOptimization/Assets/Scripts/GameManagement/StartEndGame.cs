using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartEndGame : MonoBehaviour
{
    public InputDetector inputDetector;
    public PlayerController playerController;
    public PlayerCollision playerCollision;
    public AsteroidPooling asteroidPooling;
    public ScoreManager scoreManager;

    [Space(20)]
    public GameObject clickToBeginUI;

    // Start is called before the first frame update
    void Start()
    {
        inputDetector.OnFire += BeginGame;
    }

    void BeginGame()
    {
        // Toggles input
        inputDetector.OnFire -= BeginGame;
        inputDetector.ToggleGameplayInput(true);

        // Toggles player
        playerCollision.Rebirth();
        playerController.CanShoot(true);

        // Toggles asteroids
        asteroidPooling.ToggleAsteroids(true);

        // Toggles UI
        clickToBeginUI.SetActive(false);
        scoreManager.ResetScore();
    }

    public void EndGame()
    {
        // Toggles gameplay input
        inputDetector.ToggleGameplayInput(false);

        // Toggles player
        playerController.CanShoot(false);

        // Toggles asteroids
        asteroidPooling.ToggleAsteroids(false);
        asteroidPooling.ResetAllAsteroids();

        // Toggles UI
        clickToBeginUI.SetActive(true);

        // Toggles Main Menu input on a delay
        StartCoroutine(GameOverInputDelay());
    }

    // Prevents the player from accidentally hitting play immediately after losing
    IEnumerator GameOverInputDelay()
    {
        yield return new WaitForSeconds(1f);
        inputDetector.OnFire += BeginGame;
    }
}
