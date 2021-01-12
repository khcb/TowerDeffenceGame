using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
	public Tower tower;

	void OnMouseDown()
	{
		tower.Build(transform);
	}

	public void Create()
	{
		
	}
}
