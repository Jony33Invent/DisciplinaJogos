using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticEnemy : Enemy
{
    [SerializeField] Vector3 speed = new Vector3(0.1f,0,0);
    protected override void Movement()
    {
        transform.position += speed;
    } 

    protected override void Damage()
    {
        //print("DAMAGE");
        StartCoroutine(DamageFx());
    }
    void Update()
    {
        Movement();
    }

}