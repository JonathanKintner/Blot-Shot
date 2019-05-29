using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testAI : MonoBehaviour, IEntity
{
    GameControl gamecontrol;
    float _health = 20;
    int aiState = 1;
    float targetRadius = 4;
    public float speed { get { return (1); } }
    public float health {
        get { return (20); }
        set { _health += value; }
    }

    private void Start()
    {
        gamecontrol = GameObject.FindGameObjectWithTag("Controller").GetComponent<GameControl>();
    }

    void Update()
    {
        if (aiState == 0)
        {
            if (gamecontrol.GetDistanceTo(transform.position, gamecontrol.GetPlayerLoc()) < targetRadius)
            {
                Move movement = new Move(this, gamecontrol.GetPlayerLoc());
                movement.Execute();
            }
            else
            {
                aiState += 1;
            }
        }
        else
        {
            RotateAround rot = new RotateAround(this, 200, gamecontrol.GetPlayerLoc(), targetRadius);
            rot.Execute();
            RotateTo rot2 = new RotateTo(this, gamecontrol.GetPlayerLoc(), 999);
            rot2.Execute();
        }
    }

    public void onDestroy()
    {
        print("done");
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "playerBullet(Clone)")
        {
            Destroy(other);
            ChangeHealthBy damage = new ChangeHealthBy(this, -40f);
            damage.Execute();
            if(_health <= 0)
            {
                onDestroy();
            }
        }
    }
}
