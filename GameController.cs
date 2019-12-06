using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject[] hazards;
    public Vector3 spawnValues;
    public int hazardCount;
    public float spawnWait;
    public float startWait;
    public float waveWait;

    public Text ScoreText;
    public Text restartText;
    public Text gameOverText;

    //
    public AudioSource musicSource;
    public AudioClip musicClipOne;
    public AudioClip musicClipTwo;
    public AudioClip musicClipThree;
    //


    private bool gameOver;
    private bool restart;
    private int score;

    void Start()
    {
        gameOver = false;
        restart = false;
        restartText.text = "";
        gameOverText.text = "";
        score = 0;
        UpdateScore();
        StartCoroutine(SpawnWaves());
    }

    private void Update()
    {
        if (restart)
        {
            if (Input.GetKeyDown(KeyCode.G))
            {
                SceneManager.LoadScene("SampleScene");
            }
        }

        if (Input.GetKey("escape"))
            Application.Quit();

    }

    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startWait);
        while (true)
        {
            for (int i = 0; i < hazardCount; i++)
            {
                GameObject hazard = hazards[Random.Range (0, hazards.Length)];
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate(hazard, spawnPosition, spawnRotation);
                yield return new WaitForSeconds(spawnWait);
            }
            yield return new WaitForSeconds(waveWait);

            if (gameOver)
            {
                restartText.text = "Press 'G' for restart";
                restart = true;
                //
                //if (musicSource.clip.name == musicClipTwo.name)
                //    break;
                //musicSource.clip = musicClipThree;
                //musicSource.Play();
                //musicSource.loop = true;
                //
                break;
            }
        }
    }

    public void AddScore(int newScoreValue)
    {
        score += newScoreValue;
        UpdateScore();
    }

    void UpdateScore()
    {
        ScoreText.text = "Points: " + score;
        if (score >= 100)
        {
            gameOverText.text = "You win! Game by Maximillan Brown";
            gameOver = true;
            restart = true;
           //
           if (musicSource.clip.name == musicClipTwo.name)
                return;
            musicSource.clip = musicClipTwo;
            musicSource.Play();
            musicSource.loop = true;
            //
        }
        //
        //if (score < 100)
        // {
        //   if (musicSource.clip.name == musicClipOne.name)
        //        return;

        //        musicSource.clip = musicClipOne;
        //  musicSource.Play();
        //  musicSource.loop = true;
        // }
        //
    }

    public void GameOver()
    {
        gameOverText.text = "Game Over";
        gameOver = true;
        //
        if (gameOver == true && score < 100)
        {
        musicSource.clip = musicClipThree;
        musicSource.Play();
        musicSource.loop = true;
        }
        //
    }
}