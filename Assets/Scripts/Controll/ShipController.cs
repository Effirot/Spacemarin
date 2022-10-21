using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Runtime.Serialization;
using System.Linq;
using ClassLibrary;
using System;

public partial class ShipController : MonoBehaviour
{
    [field: SerializeField]public bool IsControlling { get; set; } = false;
    [field: SerializeField]public bool IsAttacking { get; set; } = false;

    [SerializeField]Vector2 MoveVector;
    [Range(0f, 10f)]public float MoveSpeed;

    Vector2 TargetPoint = Vector2.zero;

    [field: SerializeField]public ShootController[] ShootController;

    protected virtual void FixedUpdate()
    {
        #region Moving
            TargetPoint += (MoveVector * MoveSpeed) / 10;
            transform.position = Vector3.Lerp(transform.position, TargetPoint, 0.09f);
        #endregion

        #region Rotating
            var mouse = Input.mousePosition;
            var screenPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            var offset = new Vector2(transform.position.x - screenPoint.x, transform.position.y - screenPoint.y);
            var angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;
            
            var rot = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, IsControlling? angle + 90 : 0), 0.09f);
            transform.rotation = rot;
        #endregion
    }

    void OnMove(InputValue value){
        if(IsControlling) 
            MoveVector = value.Get<Vector2>();
    }
    public void OnFire(InputValue value) => Array.ForEach(ShootController, a=>a.Shoot(transform.up * 20));
}
