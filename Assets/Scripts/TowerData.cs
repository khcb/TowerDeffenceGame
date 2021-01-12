using UnityEngine;

[CreateAssetMenu(fileName = "TowerData", menuName = "ScriptableObjects/TowerData", order = 1)]
public class TowerData : ScriptableObject
{
	public Sprite sprite;

	[SerializeField]
	private int cost;
	[SerializeField]
	private int hp;
	[SerializeField]
	private float radius;
	[SerializeField]
	private float shotTime;
	[SerializeField]
	private int damage;

	public int Cost { get => cost; set => cost = (value > 0)?value:0; }
	public int Hp { get => hp; set => hp = (value > 0) ? value : 0; }
	public float Radius { get => radius; set => radius = (value > 0) ? value : 0; }
	public float ShotTime { get => shotTime; set => shotTime = (value > 0) ? value : 0; }
	public int Damage { get => damage; set => damage = value; }
}
