using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    void Start()
    {
	    Destroy(gameObject , 2f); 
    }
	[SerializeField] private int Speed = 3;
    void Update()
    {
	    transform.Translate(Vector3.forward * Speed * Time.deltaTime);
    }
	protected void OnTriggerEnter(Collider other)
	{
		Destroy(gameObject);
	}
}
