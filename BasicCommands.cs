using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEntity/*Used by both enemies and player*/
{
    Transform transform { get; }
    float speed { get; }
    float health { get; set; }
    void onDestroy();
    //ensures an entity has a transform and speed to allow us to use the same commands for both players and enemies
}


public abstract class Command//base command class, any new commands must contain an execute function
{
    protected IEntity _entity;

    public Command(IEntity entity)
    {
        _entity = entity;
    }

    public abstract void Execute();

}

public class ChangeHealthBy/*take damage / heal*/: Command
{
    float _amount;

    public ChangeHealthBy(IEntity entity, float amount) : base(entity)
    {
        _amount = amount;
    }

    public override void Execute()
    {
        _entity.health = _entity.health + _amount;
    }
}

public class Shoot/*Shoot specified bullet*/: Command
{
    GameObject _bullet;
    Quaternion _direction;
    Vector3 _spawnPoint;
    public Shoot(IEntity entity, GameObject bullet, Vector3 spawnPoint, Quaternion direction): base(entity)
    {
        _bullet = bullet;
        _direction = direction;
        _spawnPoint = spawnPoint;
    }
    public override void Execute()
    {
        GameObject newBullet = GameObject.Instantiate(_bullet, _spawnPoint, _direction) as GameObject;
    }
}


