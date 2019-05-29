using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunRotatio : MonoBehaviour, IEntity
{
    public float speed { get; }
    public float health { get; set; }
    Vector3 MouseMovement()//Movement of mouse
    {
        return Input.mousePosition;
    }
    public void onDestroy()
    {

    }
    private void Update()
    {
        RotateToMouse rotation = new RotateToMouse(this, MouseMovement());
        rotation.Execute();
    }
}
