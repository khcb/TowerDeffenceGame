using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Генерация волн врагов
/// </summary>
public class Waves : MonoBehaviour
{
	/// <summary>
	/// Создание одной волны врагов
	/// </summary>
	[System.Serializable]
	private class Wave
	{
		public List<Enemy> enemies = new List<Enemy>();
		public int timeNextWave = 10;
	}

	[SerializeField]
	private Text WaveText;

	[SerializeField]
	private int timeStartWave = 20;
	[SerializeField]
	private int timeNextEnemy = 5;
	[SerializeField]
	private List<Wave> waves = new List<Wave>();


	private void Start()
	{

		StartCoroutine(CreateWave());
	}

	/// <summary>
	/// Генератор волн врагов
	/// </summary>
	/// <returns></returns>
	IEnumerator CreateWave()
	{
		//Отсчет до старта
		for (int i = timeStartWave; i > 0; i--)
		{
			WaveText.text = $"Старт через {i}";
			yield return new WaitForSeconds(1f);
		}

		int currvawe = 1;
		WaveText.text = $"{currvawe} волна";

		foreach (var wave in waves)
		{
			
			WaveText.text = $"{currvawe} волна";
			foreach (var enemy in wave.enemies)
			{
				if (LVL.get.IsDie)
				{
					WaveText.text = "Поражение";
					yield break;
				}
				Instantiate(enemy);	
				yield return new WaitForSeconds(timeNextEnemy);
			}

			if (wave != waves[waves.Count - 1])
			{
				//Отсчет до следующей волны
				for (int i = wave.timeNextWave; i > 0; i--)
				{
					if (LVL.get.IsDie)
					{
						WaveText.text = "Поражение";
						yield break;
					}
					WaveText.text = $"Новая волна через {i}";
					yield return new WaitForSeconds(1f);
				}
			}
			currvawe++;
		}
		if (!LVL.get.IsDie) WaveText.text = "Победа!!!";
	}

}
