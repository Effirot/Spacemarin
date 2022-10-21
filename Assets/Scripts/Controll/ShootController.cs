using UnityEngine;
using System.Collections.Generic;
using System;
using System.Collections;
using System.Linq;

#nullable enable

public class ShootController : MonoBehaviour{
    [field: SerializeField]public GameObject? BulletPrefab { get; set; }

    public virtual void Shoot(Vector2 targeting){
        if(BulletPrefab == null) throw new ArgumentNullException("Bullet prefab is null");
        if(!BulletPrefab.GetComponent<Rigidbody2D>()) throw new ArgumentException("Bullet hasn't Rigidbody2d component"); 
        
        GameObject bullet = Instantiate(BulletPrefab, transform.position, Quaternion.Euler(0, 0, Mathf.Atan2(targeting.x, targeting.y)));
        bullet.GetComponent<Rigidbody2D>().AddForce(targeting);
    }

}