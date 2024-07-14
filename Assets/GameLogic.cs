using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameLogic : MonoBehaviour
{
    public Camera cam;

    //Spawning dots
    public GameObject dot;
    public float timeBetweenSpawns = 1f;
    private float dotTimer = 0.0f;

    //score
    private int score = 0;
    public TMP_Text scoreText;

    //Game timer
    private float gameTimer = 30f;
    public TMP_Text gameTimer_text;

    //restart button
    public GameObject restartButton;

    // Start is called before the first frame update
    void Start()
    {
        //start timer
        dotTimer = 0.5f;

        scoreText.text = "Score: 0";

        restartButton.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //GAME TIMER
        gameTimer -= Time.deltaTime;
        if (gameTimer <= 0)
        {
            gameTimer = 0;
            gameTimer_text.text = "Time: 0";
            restartButton.SetActive(true);
            return;
        }

        gameTimer_text.text = $"Timer: {Mathf.Floor(gameTimer).ToString()}";

        //clicking dots
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 worldPoint = cam.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);

            if(hit.collider != null && !hit.collider.gameObject.GetComponent<dot>().audioSource.isPlaying)
            {
                hit.collider.gameObject.GetComponent<dot>().audioSource.Play();

                hit.collider.gameObject.GetComponent<SpriteRenderer>().enabled = false;
                hit.collider.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;

                Destroy(hit.collider.gameObject, hit.collider.gameObject.GetComponent<dot>().audioSource.clip.length);
                score += hit.collider.gameObject.GetComponent<dot>().pointValue;
                scoreText.text = "Score: " + score.ToString();
            }
        }

        //spawning dots
        dotTimer -= Time.deltaTime;

        if(dotTimer <= 0.0f)
        {
            dotTimer = timeBetweenSpawns;
            SpawnDot();
        }
    }

    void SpawnDot()
    {
        GameObject newDot = Instantiate(dot);

        int xPos = Random.Range(0, cam.scaledPixelWidth);
        int yPos = Random.Range(0, cam.scaledPixelHeight - 100);

        Vector3 spawnPoint = new Vector3(xPos, yPos, 0);
        spawnPoint = cam.ScreenToWorldPoint(spawnPoint);
        spawnPoint.z = 0f;

        newDot.transform.position = spawnPoint;
    }

    public void Reset()
    {
        //reload the current scene, effectively restarting it
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
