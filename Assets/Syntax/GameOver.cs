using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour {
    GameManager Manager;
    AudioSource Over;
    public GameObject Player;

    // Use this for initialization
    void Start () {
        Manager = GameObject.FindWithTag("Manager").GetComponent<GameManager>();
        Over = GameObject.FindWithTag("Manager").GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Enemy")
        {
            Manager.Begin = false;
            Time.timeScale = 0;
            Destroy(Player);
            Over.Play();
        }
    }
}
