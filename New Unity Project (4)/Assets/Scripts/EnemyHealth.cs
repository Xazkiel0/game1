using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour {

	public int health = 100;
    public Animator anim;
	public GameObject deathEffect;
    public GameObject sprite;
    public float scale;
    public float DestroyDuarEffect = 0.5f;
    public float DestroyEnemy = 1.3f;

    public GameObject kaumenang;

    public void Update()
    {
        kaumenang = GameObject.Find("Game Clear");
        scale = sprite.transform.localScale.x;
        if (scale >= 0.1f)
        {
            scale = -1f;
        }
        else if (scale <= 0.1f)
        {
            scale = 1f;
        }
    }

    public void TakeDamage (int damage)
	{
		health -= damage;

        StartCoroutine(DamageAnimation());

        if (health <= 0)
		{
            //anim.SetTrigger("Death");
            StartCoroutine(Die());
		}
	}

	IEnumerator Die ()
	{
        
        yield return new WaitForSeconds(DestroyEnemy);
        GameObject duaarr = Instantiate(deathEffect, transform.position, Quaternion.identity);

        duaarr.transform.localScale = new Vector3(scale,1f,1f);

        //kaumenang.SetActive(true);
        Destroy(gameObject);
        Destroy(duaarr, DestroyDuarEffect);
        
	}

    IEnumerator DamageAnimation()
    {
        anim.Play("Damaged");
        yield return new WaitForSeconds(.1f);
    }

}
