using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Default : MonoBehaviour {
    public GameObject Shot;
    public Transform GameTransform;
    public Transform leftSpawn;
    public Transform rightSpawn;
    public float scale;
    public float fireRate;

    private float nextFire;
    private GameObject obj;
    private Rigidbody rb;
    private GameObject leftBubs;
    private GameObject rightBubs;

    private ParticleSystem plBubs;
    private ParticleSystem prBubs;

    private AudioSource lsBubs;
    private AudioSource rsBubs;

    private bool ShootingLeft = false;
    private bool ShootingRight = false;
    // Use this for initialization
    void Awake () {
        Physics.gravity = new Vector3(0, -1F, 0);
        nextFire = Time.time;
        leftBubs = GameObject.Find("BurbujaIzquierda");
        plBubs = leftBubs.GetComponent<ParticleSystem>();
        lsBubs = leftBubs.GetComponent<AudioSource>();
        rightBubs = GameObject.Find("BurbujaDerecha");
        prBubs = rightBubs.GetComponent<ParticleSystem>();
        rsBubs = rightBubs.GetComponent<AudioSource>();
    }
	
    public void shootLeft()
    {
        if (Time.time >= nextFire && ShootingLeft)
        {
            plBubs.Play();
            lsBubs.Play();
            nextFire = Time.time + fireRate;
            obj = Instantiate(Shot, leftSpawn.position, leftSpawn.rotation);
            obj.transform.localScale = scale * new Vector3(1f, 1f, 1f);
        } else if (!ShootingLeft)
        {
            if (plBubs.isPlaying)
            {
                
                plBubs.Stop();
            }
            if (lsBubs.isPlaying)
            {
                lsBubs.Stop();
            }
            
        }
    }

    public void shootRight()
    {
        if (Time.time >= nextFire && ShootingRight)
        {
            prBubs.Play();
            rsBubs.Play();
            nextFire = Time.time + fireRate;
            obj = Instantiate(Shot, rightSpawn.position, rightSpawn.rotation);
            obj.transform.localScale = scale * new Vector3(1f, 1f, 1f);
        } else if (!ShootingRight){
            if (prBubs.isPlaying)
            {
                prBubs.Stop();
                
            }
            if (rsBubs.isPlaying)
            {
                rsBubs.Stop();
            }
            
        }
    }

	// Update is called once per frame
	void Update () {
        shootLeft();
        shootRight();
        ShootingLeft = false;
        ShootingRight = false;
    }

    void OnMarkerFound(ARMarker mark)
    {
        if (mark.Tag == "left")
        {
            ShootingLeft = true;
            
        }

        if (mark.Tag == "right")
        {
            ShootingRight = true;
        }
    }
    void OnMarkerTracked(ARMarker mark)
    {
        if (mark.Tag == "left")
        {
            ShootingLeft = true;

        }

        if (mark.Tag == "right")
        {
            ShootingRight = true;
        }
    }
    void OnMarkerLost(ARMarker mark)
    {
        Debug.Log(mark.tag + "Now not visible");
        if (mark.Tag == "left")
        {
            Debug.Log("Turning off");
            ShootingLeft = false;
        }

        if (mark.Tag == "right")
        {
            ShootingRight = true;
           
        }
    }
}
