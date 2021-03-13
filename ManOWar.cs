using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManOWar : MonoBehaviour, IEntity
{
    public float health { get; set; } = 80;
    public float speed { get; } = 4f;
    Vector3 targetLocation;
    int pointsWorth = 30;
    GameControl gamecontrol;

    void Awake()
    {
        if (GameControl.alive)
        {
            gamecontrol = GameObject.FindGameObjectWithTag("Controller").GetComponent<GameControl>();//defines controller
            targetLocation = Vector3.zero;
            new RotateTo(this, targetLocation, 999);//Moves across the center of the screen by default
            StartCoroutine(Shooting(.5f));
        }
    }

    private void OnEnable()
    {
        StartCoroutine(Shooting(.5f));
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    public void SetTargetLocation(Vector3 location)
    {
        targetLocation = location;
        new RotateTo(this, location, 9999999);
    }

    public void SetRotation(Quaternion angle)//If you dont need the specific location, this lets you change just the rotation instead
    {
        transform.rotation = angle;
        print("here2");
    }

    void Update()
    {
        transform.Translate(Vector3.forward*speed*Time.deltaTime);
        //transform.GetChild(0).transform.Rotate(0, 10 * Time.deltaTime, 0);
    }

    IEnumerator Shooting(float delay)
    {
        while (GameControl.alive)
        {
            int i = 0;
            GetComponent<AudioSource>().Play();
            while (i < 6)
            {
                new Shoot(this, Resources.Load("Prefabs/ManOWarBullet") as GameObject, transform.position, Quaternion.Euler(0, (i * 60), 0)*transform.rotation);//Evenly spaces out the bullets around the object
                i++;
            }
            yield return new WaitForSeconds(delay);
        }
    }

    void onDestroy()
    {
        GameObject explosion = Instantiate(Resources.Load("Prefabs/Explosion"), new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z), Quaternion.Euler(90, 0, 0)) as GameObject;
        explosion.GetComponent<scr_Explosion>().PlayThisSound(0);
        gamecontrol.score += pointsWorth;//adds points
        Destroy(gameObject);//destroys this
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "playerBullet(Clone)")//Detects if it makes contact with Player's bullet
        {
            health -= 20;
        }
        if (health <= 0)
        {
            onDestroy();//Runs onDestroy method to add points 
        }
    }
}
