using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Башня базовый класс
/// </summary>
public class Tower : MonoBehaviour
{
	//Переменные
	[SerializeField]
	private Bullet bullet;
	[SerializeField]
	private List<TowerData> towers = new List<TowerData>();
	private int lvlTower = 0;
	private int hp = 100;
	private float radius;
	private float shotTime = 2f;
	private int damage;
	private List<Transform> targets = new List<Transform>();
	private Transform target;

	//Свойства
	private int LVLTower {
		get => lvlTower;
		set
		{
			if (value < 0) lvlTower = 0;
			else if (value >= towers.Count) lvlTower = towers.Count - 1;
			else lvlTower = value;
		}
	}

	/// <summary>
	/// Жизни
	/// </summary>
	public int Hp { get => hp; set => hp = value; }
	/// <summary>
	/// Радиус стрельбы
	/// </summary>
	public float Radius { get => radius; set => radius = value; }
	/// <summary>
	/// Время перезарядки
	/// </summary>


	//Компоненты
	[SerializeField]
	private SpriteRenderer SpriteRenderer;
	[SerializeField]
	private CircleCollider2D radiusColl;

	/// <summary>
	/// Инициализация
	/// </summary>
	/// <param name="lvlTower">Уровень башни</param>
	private void Init(int lvlTower)
	{
		LVLTower = lvlTower;
		if (towers[LVLTower] != null)
		{
			damage = towers[LVLTower].Damage;
			Hp = towers[LVLTower].Hp;
			Radius = towers[LVLTower].Radius;
			radiusColl.radius = Radius;

			shotTime = towers[LVLTower].ShotTime;
			if(towers[LVLTower].sprite!= null)
				SpriteRenderer.sprite = towers[LVLTower].sprite;
		}

		Debug.Log($"Создана башня уровня {LVLTower}\n" +
			$"Здоровье = {Hp}\n" +
			$"Рудиус атаки = {Radius}\n" +
			$"урон атаки = {damage}\n" +
			$"Время перезарядки = {shotTime}\n");
	}

	/// <summary>
	/// Атака
	/// </summary>
	/// <param name="unit">Противник</param>
	//private void Attacked(Unit unit)
	//{
	//	Debug.Log($"Получен урон {damage}");
	//	Hp -= damage;
	//	if (Hp <= 0) Die();
	//}

	/// <summary>
	/// Получение урона
	/// </summary>
	/// <param name="damage">ед. урона</param>
	private void Attacked(int damage)
	{
		Debug.Log($"Получен урон {damage}");
		Hp -= damage;
		if (Hp <= 0) Die();
	}

	/// <summary>
	/// Башня разрушена
	/// </summary>
	private void Die()
	{
		Debug.Log("Башня разрушена");
	}

	/// <summary>
	/// Постройка Башни
	/// </summary>
	public void Build(Transform pos)
	{
		Tower temp = Instantiate(this.gameObject).GetComponent<Tower>();
		temp.Init(0);
		temp.transform.position = pos.position;
	}

	/// <summary>
	/// Апгрейд башни
	/// </summary>
	private void Upgrade()
	{
		LVLTower++;
		Init(LVLTower);
	}

	/// <summary>
	/// Стрельба
	/// </summary>
	private IEnumerator Shoot()
	{

		while(target != null)
		{
			Bullet tempB = Instantiate(bullet);
			tempB.transform.position = transform.position;
			tempB.Damage = damage;
			tempB.Target = target;
			yield return new WaitForSeconds(shotTime);
		}
		
	}

	//Если враг попал в радиус то добавляем его в список
	private void OnTriggerEnter2D(Collider2D coll)
	{
		if (coll.tag == "Enemy")
		{
			Enemy enemy = coll.GetComponent<Enemy>();
			targets.Add(enemy.transform);
			target = targets[0];
			StartCoroutine(Shoot());
		}
	}
	//Если враг вышел из радиуса то убираем его из списка
	private void OnTriggerExit2D(Collider2D coll)
	{
		if (coll.tag == "Enemy")
		{
			Enemy enemy = coll.GetComponent<Enemy>();
			targets.Remove(enemy.transform);
			target = null;
			if (targets.Count > 0)
			{
				if (targets[0] != null) target = targets[0];
			}
		}
	}
}
