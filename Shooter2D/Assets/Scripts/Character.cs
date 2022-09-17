using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Classe futuramente abstrata, serão utilizados as classes CowCharacter, PigCharacter e etc

public class Character : MonoBehaviour
{
    private int _character_id = 0;
    public int character_id
    {
        get { return _character_id; }
    }

    [SerializeField] float speed;
    Camera cam;
    [SerializeField] public Gun gun;
    private Vector3 direction;
    private Quaternion lookRotation;
    float angle;

    //Custom Events
    public event Action<int> onInteract;
    public void Interact(int id){
        if(onInteract != null){
            onInteract(id);
        }
    }

    public event Action<Vector3> onFire;
    public void Fire(Vector3 direction){
        if(onFire != null){
            onFire(direction);
        }
    }


    //Engine Methods
    void Start(){
        cam = Camera.main;
    }
    void Update(){
        ControlRotation();
    }

    //Methods
    public void Move(float x, float y){
        transform.position += new Vector3(x, y, 0)*speed;
    }
    private void ControlRotation(){
        Vector2 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

        Vector2 center = transform.position;

        direction = (mousePos - center).normalized;

        
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

        lookRotation = Quaternion.Euler(0,0,angle);
        gun.transform.rotation = lookRotation;
    }

}