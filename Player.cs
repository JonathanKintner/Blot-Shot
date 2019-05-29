using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IEntity {

    float _speed = 7.5f;
    float _health = 100;
    
    public void initializeValues(float health, float speed)//use this to instantiate the player in GameControl with more health/speed for testing
    {
        _health = health;
        _speed = speed;
    }

    public float speed
    {
        get{return (_speed);}
    }

    public float health
    {
        get { return (_health); }
        set { _health = value; }
    }

    private void Update()
    {
        if (GetMovementDirection() != Vector3.zero)
        {
            Move movement = new Move(this, GetMovementDirection());
            movement.Execute();
        }
    }

    

    public void onDestroy()
    {
        print("big dead");
    }

    Vector3 GetMovementDirection()//Movement of ship itself
    {
        float x = 0;
        float z = 0;
        if (Input.GetKey(KeyCode.W))
        {
            z += 1;
        }
        if (Input.GetKey(KeyCode.S))
        {
            z -= 1;
        }
        if (Input.GetKey(KeyCode.A))
        {
            x -= 1;
        }
        if (Input.GetKey(KeyCode.D))
        {
            x += 1;
        }
        return new Vector3(x, 0, z);
    }

    Vector3 MouseMovement()//Movement of mouse
    {
        return Input.mousePosition;
    }

    
}
