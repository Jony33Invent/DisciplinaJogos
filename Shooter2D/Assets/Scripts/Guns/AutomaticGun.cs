using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AutomaticGun : Gun
{
    [Header("AutomaticSpecifics")]
    [SerializeField] protected GameObject bullet;
    [SerializeField] protected float spread;
    protected override void Fire(Vector3 direction){
        if(cd <= 0 && cur_magazine > 0){
            base.Fire(direction);
            Vector3 _direction = Quaternion.AngleAxis(-Random.Range(-spread, spread), new Vector3(0, 0, 1)) * direction;

            GameObject _bullet = Instantiate(bullet, spawnTransf.transform.position, Quaternion.Euler(0, 0, 0));
            Bullet bulletScript=_bullet.GetComponent<Bullet>();
            bulletScript.SetDirection(_direction);
            bulletScript.SetPlayer(player);

        }
    }

    protected abstract override void ReloadProps(float time);

    // Start is called before the first frame update
    // protected virtual new void Start()
    // {
    //     base.Start();
    // }

    // // Update is called once per frame
    // protected virtual new void Update()
    // {
    //     base.Update();
    // }
}
