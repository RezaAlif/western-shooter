using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour {
    float Timing;
    public GameObject Enemy;
    public float Target;
    GameManager Manager;

    // Use this for initialization
    void Start () {
        Manager = GameObject.FindWithTag("Manager").GetComponent<GameManager>();
    }
	
	// Update is called once per frame
	void Update () {
        if(Manager.Begin == true)
        {
            Timing += Time.deltaTime;
        }
        if(Timing >= Target)
        {
            Instantiate(Enemy,transform.position, transform.rotation);
            Timing = 0;
        }
	}
}
