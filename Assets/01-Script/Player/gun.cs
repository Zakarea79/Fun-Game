using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gun : MonoBehaviour
{
	[System.Serializable]
	class pointShot
	{
		public Transform[] Bullet_Shot;
		public Transform Bullet;
	}
	[SerializeField] private pointShot pointshot;
	public void shotgun()
	{
		foreach (var item in pointshot.Bullet_Shot)
		{
			var b = Instantiate(pointshot.Bullet , item.position , item.rotation);
		}
	}
	public void pistol()
	{
		Instantiate(pointshot.Bullet , pointshot.Bullet_Shot[1].position , pointshot.Bullet_Shot[1].rotation);
	}
}
