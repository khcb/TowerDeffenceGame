using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
	private float speed = 5f;
	private Transform target;
	private int damage;

	public Transform Target { get => target; set => target = value; }
	public int Damage { get => damage; set => damage = value; }

	private void Update()
	{
		if (Target != null)
		{
			transform.position = Vector3.MoveTowards(
				 transform.position,
				 Target.position,
				 Time.deltaTime * speed);
		}
	}

	private void OnTriggerEnter2D(Collider2D coll)
	{
		if (coll.tag == "Enemy")
		{
			Enemy enemy = coll.GetComponent<Enemy>();
			enemy.Attacked(Damage);
			Destroy(this.gameObject);
		}
	}

}
