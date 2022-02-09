using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    public int damage = 40;
    public Rigidbody2D rb;
    //public Animator anim;
    public GameObject ImpactEffect;
    public float WaktuImpactDestroy;
    public bool kena;

    private GameObject Enemy;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {
        rb.freezeRotation = true;
        rb.velocity = transform.right * speed;
        Enemy = GameObject.FindGameObjectWithTag("Enemy");
        if (kena == true)
        {
            rb.velocity = Vector2.zero;
            Vector3 PosisiEnemy = new Vector3(Enemy.transform.position.x, transform.position.y, transform.position.z);
            transform.parent = Enemy.transform.GetChild(0);
            transform.position = PosisiEnemy;
        }


    }

    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        kena = true;
        
        //Debug.Log("kena oy" + hitInfo.name);
        EnemyHealth enemy = hitInfo.GetComponent<EnemyHealth>();
        if(enemy != null)
        {
            enemy.TakeDamage(damage);
            //anim.Play("Damaged", -1, 0f);
        }
        //GameObject Impact = Instantiate(ImpactEffect, transform.position, Quaternion.identity);
        
        Destroy(gameObject, WaktuImpactDestroy);
        //Destroy(Impact, WaktuImpactDestroy);
    }

}
