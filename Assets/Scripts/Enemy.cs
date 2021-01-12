using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
	[SerializeField]
	private int hp;
	[SerializeField]
	private int damage;
	[SerializeField]
	private float speed = 2f;
	private int nextPos = 1;
	private int indRoad;

	/// <summary>
	/// номер следующей точки дороги
	/// </summary>
	public int NextPos { get => nextPos;
		set
		{
			if (value <= 0) nextPos = 1;
			else if (value >= LVL.get.Roads[0].road.Count) nextPos = LVL.get.Roads[0].road.Count - 1;
			else nextPos = value;
		}
	}

	public int Damage { get => damage; set => damage = value; }

	private void Start()
	{
		transform.position = LVL.get.Roads[0].road[0].position;
		Random r = new Random();
		indRoad = Random.Range(0, LVL.get.Roads.Count);
	}
	private void Update()
	{
		Go();
	}

	/// <summary>
	/// Движение Врага по дороге
	/// </summary>
	private void Go()
	{
		var tempRoad = LVL.get.Roads[indRoad].road;
		if (transform.position == tempRoad[tempRoad.Count-1].transform.position)
		{
			LVL.get.Hp -= Damage;
			Destroy(this.gameObject);
			return;
		}

		if (transform.position == tempRoad[NextPos].position) NextPos++;
		if (NextPos < tempRoad.Count) transform.position = Vector3.MoveTowards(
				 transform.position,
				 tempRoad[NextPos].position,
				 Time.deltaTime * speed);


	}

	public void Attacked(int damage)
	{
		hp -= damage;
		if(hp <= 0) Die();
	}

	private void Die()
	{
		Destroy(this.gameObject);
	}
}
