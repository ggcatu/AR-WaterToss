using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterAttack : MonoBehaviour {
    Rigidbody rb;

    private int direction;
    private GameObject leftSpawn;
    private GameObject rightSpawn;
    // Use this for initialization
    void Start () {
        direction = this.gameObject.transform.position.x > 0 ? -1 : 1;
        rb = gameObject.GetComponent<Rigidbody>();
        rb.AddForce(direction * 180, 300, 0);
        leftSpawn = GameObject.Find("LeftSpawn");
        rightSpawn = GameObject.Find("RightSpawn");
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Target")
        {
            float lDistance = Vector3.Distance(this.transform.position, leftSpawn.transform.position);
            float rDistance = Vector3.Distance(this.transform.position, rightSpawn.transform.position);
            float distance;
            if (lDistance < rDistance)
            {
                distance = lDistance;
            } else
            {
                distance = rDistance;
            }
            Debug.Log(distance + "Distancia ");
            Vector3 fuerza = new Vector3(direction * 90, 225, 0);
            fuerza = fuerza * Mathf.Exp(-distance/5);
            Debug.Log(fuerza + " Fuerza ");
            other.transform.parent.GetComponent<Rigidbody>().AddForce(fuerza);
        } else if (other.tag == "Finish")
        {
            Destroy(gameObject);
            Debug.Log("Destruyendoo");
        }
        
    }
}
