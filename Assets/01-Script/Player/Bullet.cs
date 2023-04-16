using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    void Start()
    {
	    Invoke("DetoryBullet" , 2); 
    }
	[SerializeField] private int Speed = 3;
    void Update()
    {
	    transform.Translate(Vector3.forward * Speed * Time.deltaTime);
    }
	private void DetoryBullet(){Destroy(gameObject);}
	protected void OnTriggerEnter(Collider other)
	{
		Destroy(gameObject);
	}
}
