using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Classe futuramente abstrata, serão utilizados as classes CowCharacter, PigCharacter e etc

public class Character : MonoBehaviour
{
    public int _character_id;
    public int character_id
    {
        get { return _character_id; }
    }

    Rigidbody2D body;
    private float baseSpeed = 10;
    [SerializeField] public Gun gun;


    public void SetPlayerControlling(Player p)
    {
        if(p != null){
            _character_id = p.PlayerId;
        }
        else 
            _character_id = 0;
        gun.SetOwner(this);
    }


    [Header("Status")]
    public int Health;
    public int Strenght;
    public int Speed;
    public int Aim;

    //Custom Events
    public event Action<int> onInteract;
    public void Interact(int id){
        if(onInteract != null){
            onInteract(id);
        }
    }

    public event Action<Vector2, int, int> onFire;
    public void Fire(Vector2 direction){
        if(onFire != null){
            onFire(direction, Strenght, Aim);
        }
    }

    public event Action onReleaseFire;
    public void ReleaseFire(){
        if(onReleaseFire != null){
            onReleaseFire();
        }
    }

    public event Action onReload;
    public void Reload(){
        if(onReload != null){
            onReload();
        }
    }

    public event Action onSwitchLight;
    public void SwitchLight(){
        if(onSwitchLight != null){
            onSwitchLight();
        }
    }


    //Engine Methods
    void Start(){
        body = GetComponent<Rigidbody2D>();
    }
    void Update(){
        //ControlRotation();
    }

    //Methods
    public void Move(Vector2 velocity){
        body.velocity= velocity*baseSpeed*(Speed/100);
    }
    public void ControlRotation(Vector2 direction){
        float angle;
        if (direction.x > 0) // virado pra direita
        {
            transform.localScale = new Vector3(1, 1, 1);
            angle = -Vector2.SignedAngle(direction,  Vector2.right);
        }
        else // virado pra esquerda
        {
            transform.localScale = new Vector3(-1, 1, 1);
            angle = -Vector2.SignedAngle(direction, Vector2.left);
        }

        gun.transform.rotation = Quaternion.Euler(0,0,angle);
    }

}