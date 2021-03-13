using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shark : MonoBehaviour, IEntity
{

    public float health { get; set; } = 40;
    public float speed { get; set; } = 7.5f;
    float radius = 8;
    int aistate = 0;
    Coroutine co;
    float rotateSpeed = 10f;
    Vector3 exitPoint;
    GameControl gamecontrol;

    void Start()
    {
        gamecontrol = GameObject.FindGameObjectWithTag("Controller").GetComponent<GameControl>();
        exitPoint = gamecontrol.ReturnSpawnPoint();
    }

    void Update()
    {
        switch (aistate)
        {
            case 0:
                if (Vector3.Distance(transform.position, gamecontrol.GetPlayerLoc()) > radius)//Moves toward player until close enough
                {
                    new RotateTo(this, gamecontrol.GetPlayerLoc(), 10);
                    transform.Translate(Vector3.forward * speed * Time.deltaTime);
                }
                else
                {
                    aistate = 1;
                    co = StartCoroutine(Shooting(1));
                    this.Invoke("Leave",6f);
                }
                break;
            case 1:
                rotateSpeed = 6;
                if (Vector3.Distance(transform.position, gamecontrol.GetPlayerLoc()) > radius)//Adjusts based on distance
                {
                    transform.Translate(Vector3.forward * Mathf.Abs(Vector3.Distance(transform.position,gamecontrol.GetPlayerLoc())-radius));
                    rotateSpeed += 5;
                }
                if(Vector3.Distance(transform.position, gamecontrol.GetPlayerLoc()) < radius)//Adjusts based on distance
                {
                    transform.Translate(Vector3.back * Mathf.Abs(Vector3.Distance(transform.position, gamecontrol.GetPlayerLoc()) - radius));
                    rotateSpeed += 5;
                }
                transform.Translate(Vector3.left * Time.deltaTime * rotateSpeed);
                new RotateTo(this, gamecontrol.GetPlayerLoc(), 99);
                break;
                
            case 3:
                Destroy(gameObject, 4);
                new RotateTo(this, exitPoint, 10);
                transform.Translate(Vector3.forward * speed * Time.deltaTime);
                break;
        }
    }

    IEnumerator Shooting(float delay)
    {
        while (true)
        {
            new Shoot(this, Resources.Load("Prefabs/SharkBullet") as GameObject,transform.position,transform.rotation * Quaternion.Euler(0,180,0));
            GetComponent<AudioSource>().Play();
            yield return new WaitForSeconds(delay);
        }
    }

    void Leave()
    {
        StopCoroutine(co);
        aistate = 3;
    }

    void onDestroy()
    {
        GameObject explosion = Instantiate(Resources.Load("Prefabs/Explosion"), new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z), Quaternion.Euler(90, 0, 0)) as GameObject;
        explosion.GetComponent<scr_Explosion>().PlayThisSound(0);
        gamecontrol.score += 10;
        Destroy(this.gameObject);
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
