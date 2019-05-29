using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move/*Move entity given a direction*/: Command
{
    Vector3 _direction;

    public Move(IEntity entity, Vector3 direction) : base(entity)
    {
        _direction = direction;
    }

    public override void Execute()
    {
        _entity.transform.position += _direction * _entity.speed * Time.deltaTime;
    }
}

public class RotateToMouse/*Rotates the player to face mouse position*/: Command
{
    Vector3 _point;

    public RotateToMouse(IEntity entity, Vector3 point) : base(entity)
    {
        _point = point;
    }

    public override void Execute()
    {
        Vector3 entityloc = Camera.main.WorldToScreenPoint(_entity.transform.position);
        Vector3 dir = _point - entityloc;
        float dist = Mathf.Sqrt((dir.x *dir.x) + (dir.y *dir.y));
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        float spd = Mathf.Min(8 * Time.deltaTime, 1);
        if (dist > 35)
        {
            _entity.transform.rotation = Quaternion.AngleAxis(-angle, Vector3.up);
        }
    }
}

public class RotateTo/*Rotates the entity to face a position*/: Command
{
    Vector3 _point;
    float _turnSpeed;

    public RotateTo(IEntity entity, Vector3 point, float turnspeed) : base(entity)
    {
        _point = point;
        _turnSpeed = turnspeed;
    }

    public override void Execute()
    {
        Vector3 entityloc = _entity.transform.position;
        Quaternion dir = Quaternion.LookRotation(_point - entityloc);
        float spd = Mathf.Min(_turnSpeed * Time.deltaTime, 1);
        _entity.transform.rotation = Quaternion.Lerp(_entity.transform.rotation, dir, spd);
    }
}

public class RotateAround /*Rotates entity around a point*/: Command
{
    float _speed;//rotation speed
    Vector3 _point;//point to rotate around
    float _targetDist;//Distance maintained from point by entity (Rotation radius)
    public RotateAround(IEntity entity, float speed, Vector3 point, float targetDist): base(entity)
    {
        _point = point;
        _speed = speed;
        _targetDist = targetDist;
    }
    public override void Execute()
    {
        //finds distance between entity and point 
        Vector3 distVect = _point - _entity.transform.position;
        //Finds hypotenuse 
        float dist = Mathf.Sqrt((distVect.x * distVect.x) + (distVect.z * distVect.z));

        //Checks distance between what orbit radius is and should be
        if (dist != _targetDist)
        {
           float difference = _targetDist - dist;
           _entity.transform.localPosition += (_entity.transform.position * difference * Time.deltaTime);
        }

        _entity.transform.RotateAround(_point, Vector3.up, _speed*Time.deltaTime);
        
    }
}