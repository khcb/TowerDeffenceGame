using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class LVL : MonoBehaviour
{
	[System.Serializable]
	public class Road
	{
		public List<Transform> road;
	}

	[SerializeField]
	private List<Road> roads = new List<Road>();
	[SerializeField]
	private int hp;
	[SerializeField]
	private List<Tower> towers = new List<Tower>();

	public List<Road> Roads { get => roads; }
	public int Hp { get => hp;
		set
		{
			if (value <= 0)
			{
				hp = 0;
				isDie = true;
				Die();
			}
			else
			{
				hp = value;
				HpText.text = $"{hp} HP";
			}
		}
	}
	public List<Tower> Towers { get => towers;}
	public bool IsDie { get => isDie;}

	private bool isDie = false;
	public static LVL get;

	//Компоненты
	[SerializeField]
	private Text HpText;

	private void Awake()
	{
		get = this;
		isDie = false;
		HpText.text = $"{hp} HP";
	}

	private void Die()
	{
		HpText.text = $"Die =(";
	}
}
