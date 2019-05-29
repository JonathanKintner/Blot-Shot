using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gun : MonoBehaviour, IEntity
{
    public float speed { get; }
    public float health { get; set; }
    bool canShoot = true;

    void Update()
    {
        shooting();
    }

    public void onDestroy()
    {

    }
    
    public void shooting()
    {
        if (Input.GetKey(KeyCode.Mouse0) && canShoot == true)
        {
            Shoot shoot = new Shoot(this, Resources.Load("Prefabs/playerBullet") as GameObject, gameObject.transform.position+new Vector3(0,0,0),transform.rotation*Quaternion.Euler(0,-90,0));
            shoot.Execute();
            canShoot = false;
            StartCoroutine(bulletCooldown(0.2F));
        }
    }
    IEnumerator bulletCooldown(float cooldown)
    {
        yield return new WaitForSeconds(cooldown);
        canShoot = true;
    }
}
