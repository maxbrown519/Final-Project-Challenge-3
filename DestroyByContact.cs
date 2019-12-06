using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByContact : MonoBehaviour
{
    public GameObject Explosion;
    public GameObject playerExplosion;
    public int scoreValue;

    //
    //public AudioSource musicSource;
    //public AudioClip musicClipThree;
    //

    private GameController gameController;

    private void Start()
    {
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        if (gameControllerObject != null)
        {
            gameController = gameControllerObject.GetComponent<GameController>();
        }
        if (gameController == null)
        {
            Debug.Log("Cannot find 'GameController Script");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Boundary" || other.tag == "Enemy")
        {
            return;
        }

        if (Explosion != null)
        {
            Instantiate(Explosion, transform.position, transform.rotation);
        }

        //Instantiate(Explosion, transform.position, transform.rotation);

        if (other.tag == "Player")
        {
           Instantiate(playerExplosion, transform.position, transform.rotation);
           gameController.GameOver();
            //
            //musicSource = musicClipThree;
            //musicSource.Play();
            //musicSource.loop = true;
            //

        }

        gameController.AddScore (scoreValue);
        Destroy(other.gameObject);
        Destroy(gameObject);
    }
}
