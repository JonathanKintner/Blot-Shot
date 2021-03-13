using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wolf : MonoBehaviour, IEntity
{
    public float health { get; set; } = 60;
    public float speed { get; set; } = 7;
    int aiState = 0;
    float radius = 8;
    GameControl gamecontrol;
    int pointsWorth = 30;
    float referenceRotation;
    float counter = 0;
    Vector3 targetLocation = new Vector3(999,999,999);

    void Start()
    {
        gamecontrol = GameObject.FindGameObjectWithTag("Controller").GetComponent<GameControl>();
    }

    void Update()
    {
        WolfBehaviour();
    }

    void WolfBehaviour()
    {
        switch (aiState)
        {
            case 0:
                if (Vector3.Distance(transform.position, gamecontrol.GetPlayerLoc()) > radius)//Moves toward player until close enough
                {
                    new RotateTo(this, gamecontrol.GetPlayerLoc(), 10);
                    transform.Translate(Vector3.forward * speed * Time.deltaTime);
                }
                else
                {
                    referenceRotation = transform.localRotation.y+180;
                    aiState = 1;
                }
                break;
            case 1:
                if (counter <= 2)
                {
                    float rotateSpeed = 6;
                    if (Vector3.Distance(transform.position, gamecontrol.GetPlayerLoc()) > radius)//Adjusts based on distance
                    {
                        transform.Translate(Vector3.forward * Mathf.Abs(Vector3.Distance(transform.position, gamecontrol.GetPlayerLoc()) - radius));
                        rotateSpeed += 5;
                    }
                    if (Vector3.Distance(transform.position, gamecontrol.GetPlayerLoc()) < radius)//Adjusts based on distance
                    {
                        transform.Translate(Vector3.back * Mathf.Abs(Vector3.Distance(transform.position, gamecontrol.GetPlayerLoc()) - radius));
                        rotateSpeed += 5;
                    }

                    transform.Translate(Vector3.left * 12.5f * Time.deltaTime);
                    counter += Time.deltaTime;
                    new RotateTo(this, gamecontrol.GetPlayerLoc(), 999);
                }
                else
                {
                    aiState = 2;
                    this.Invoke("TargetPlayer", 2f);
                }
                break;
            case 2:
                if (targetLocation == new Vector3(999,999,999))
                {
                    new RotateTo(this, gamecontrol.GetPlayerLoc(), 2.5f);
                }
                else
                {
                    speed = 3;
                    aiState = 3;
                }
                break;
            case 3:
                if (Vector3.Distance(targetLocation, transform.position) > 1)
                {
                    new RotateTo(this, targetLocation, 999);
                    new Move(this, targetLocation);
                }
                else
                {
                    targetLocation = new Vector3(999, 999, 999);
                    aiState = 2;
                    this.Invoke("TargetPlayer", 2f);
                }
                break;
        }
    }

    void TargetPlayer()
    {
        targetLocation = gamecontrol.GetPlayerLoc();
    }

    void onDestroy()
    {
        GameObject explosion = Instantiate(Resources.Load("Prefabs/Explosion"), new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z), Quaternion.Euler(90, 0, 0)) as GameObject;
        explosion.GetComponent<scr_Explosion>().PlayThisSound(0);
        gamecontrol.score += pointsWorth;
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "playerBullet(Clone)")
        {
            health -= 20;
            if(health <= 0)
            {
                onDestroy();
            }
        }
    }
}
