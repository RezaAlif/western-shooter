using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {
    GameManager Manager;

	// Use this for initialization
	void Start () {
        Manager = GameObject.FindWithTag("Manager").GetComponent<GameManager>();
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(Vector3.right * 0.05f);
        if(transform.position.y <= -6.7)
        {
            Destroy(gameObject);
        }
        if(Manager.Begin == false)
        {
            Destroy(gameObject);
        }
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Peluru")
        {
            Destroy(gameObject);
            Manager.score += 1;
        }
    }
}
