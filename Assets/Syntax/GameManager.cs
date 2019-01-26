using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    public int score;
    public Text Skor;
    public bool Begin;
    public GameObject Player;
    public Transform Spawn;
    public GameObject Menu;

    // Use this for initialization
    void Start () {
        Instantiate(Player, Spawn.position, Spawn.rotation);
        Time.timeScale = 1;
    }
	
	// Update is called once per frame
	void Update () {
        Skor.text = "Score : " + score.ToString();
        if(Begin == false)
        {
            Menu.SetActive(true);
        }
	}

    public void Mulai(GameObject Menu)
    {
        Menu.SetActive(false);
        Time.timeScale = 1;
        Begin = true;
        score = 0;
        Instantiate(Player, Spawn.position, Spawn.rotation);
    }

    public void Exit()
    {
        Application.LoadLevel("MainMenu");
    }
}
