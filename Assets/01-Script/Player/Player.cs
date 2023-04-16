using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	public enum Gun
	{
		pistol , shotgun
	}
	[SerializeField] private Gun gun;
	[SerializeField] private float Speed = 10f;
	[SerializeField] private Animator animPlayer;
	[SerializeField] private Transform[] Guns;
	private CharacterController characterController;
	internal bool AttakAction = false;
	//--------------------------------
	private RaycastHit hit;
	#if UNITY_EDITOR
	private RaycastHit hitDebug;
	#endif
	protected void Start()
	{
		characterController = gameObject.GetComponent<CharacterController>();
	}
	float x;
	float y;
    void Update()
	{
		
		//---------------------------------SHOT------------------------------------
		if(ZInput.GetKeyPress("shot"))
		{
			animPlayer.SetBool("" + gun , true);
		}else
		{
			animPlayer.SetBool("" + gun , false);
		}
		//---------------------------------SHOT------------------------------------
		
		
		if(ChackPlayAnim("Armature|shoot with shotgun") == false && ChackPlayAnim("Armature|shoot with pistol") == false)
		{
			x = ZInput.GetAxis("Horizontal");
			y = ZInput.GetAxis("Vertical");
		}
		else
		{
			x = 0;
			y = 0;
		}
		
		float xmove = 0;
		float ymove = 0;
		if(System.Math.Abs(x) > System.Math.Abs(y))
		{
			animPlayer.SetFloat("walk-" + gun  , System.Math.Abs(x));
		}
		else if(System.Math.Abs(x) < System.Math.Abs(y))
		{
			animPlayer.SetFloat("walk-" + gun  , System.Math.Abs(y));
		}
		else
		{
			if(System.Math.Abs(x) == System.Math.Abs(y))
			{
				animPlayer.SetFloat("walk-" + gun  , System.Math.Abs(x));
			}
			else
			{
				animPlayer.SetFloat("walk-" + gun  ,0);
			}
		}
		if(System.Math.Abs(x) > .3 || System.Math.Abs(y) > .3)
		{
			xmove = x;
			ymove = y;
		}
		if(System.Math.Abs(ZInput.GetAxis("S-Horizontal")) > 0 || System.Math.Abs(ZInput.GetAxis("S-Horizontal")) > 0)
		{
			var Rotatev = new Vector3(-ZInput.GetAxis("S-Horizontal"), 0, -ZInput.GetAxis("S-Vertical"));
			Quaternion quaternion = Quaternion.LookRotation(Rotatev);
			transform.rotation = quaternion;
		}
		
		characterController.Move((new Vector3(-xmove, -1 , -ymove) * Speed * Time.deltaTime));
		
		if(ZInput.GetKeyDown("change gun"))
		{
			if((int)gun == 1)
			{
				gun --;
			}
			else
			{
				gun ++;
			}
			foreach (var item in Guns)
			{
				item.gameObject.SetActive(false);
			}
			Guns[(int)gun].gameObject.SetActive(true);
		}
	}
	
	public void FierFlay(int input){ transform.GetChild(5).GetComponent<SphereCollider>().radius = input;}
    
	public void resevarable(string statename){animPlayer.SetInteger(statename , 0);}
    
	private bool ChackPlayAnim(string name){return animPlayer.GetCurrentAnimatorStateInfo(0).IsName(name);}
}
