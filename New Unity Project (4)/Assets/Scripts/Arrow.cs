using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public int damage = 40;
    public bool BisaHancur;
    public float waktuHancur = 2f;
    public Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        //rb.velocity = transform.right * speed;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        EnemyHealth enemy = hitInfo.GetComponent<EnemyHealth>();
        if (enemy != null)
        {
            Debug.Log(enemy.transform.name);
            enemy.TakeDamage(damage);
            //anim.Play("Damaged", -1, 0f);
        }
        //GameObject Impact = Instantiate(ImpactEffect, transform.position, Quaternion.identity);
        if(BisaHancur == true)
        {
            Destroy(gameObject, waktuHancur);
        }
        //Destroy(gameObject);
        //Destroy(Impact, 0.3f);
    }
}
