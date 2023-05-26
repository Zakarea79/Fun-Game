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
	[SerializeField] private int DistanceEnemy = 10;
	public List<GameObject> EnemyList;
	private CharacterController characterController;
	internal bool AttakAction = false;
	//--------------------------------
	private RaycastHit hit;
	private GameObject CloseObject;
	#if UNITY_EDITOR
	private RaycastHit hitDebug;
	#endif
	protected void Start()
	{
		characterController = gameObject.GetComponent<CharacterController>();
	}
	float x;
	float y;

	public GameObject ClosestTarget(List<GameObject> enemy)
    {
        GameObject ClosestOBJ = null;
        try
        {
            if (enemy.Count > 0)
            {
                foreach (var obj in enemy)
                {
                    if (ClosestOBJ == null)
                    {
                        ClosestOBJ = obj;
                    }
                    if (Vector3.Distance(transform.position, obj.transform.position) <= Vector3.Distance(transform.position, ClosestOBJ.transform.position))
                    {
                        ClosestOBJ = obj;
                    }
                }
                return ClosestOBJ;
            }
            else
            {
                return null;
            }
        }
        catch
        {
            return null;
        }
    }
    void Update()
	{
		
		//---------------------------------SHOT------------------------------------
		//if(ZInput.GetKeyPress("shot"))
		//{
		//	animPlayer.SetBool(gun.ToString() , true);
		//}else
		//{
		//	animPlayer.SetBool(gun.ToString() , false);
		//}
		//---------------------------------SHOT------------------------------------
		
		x = ZInput.GetAxis("Horizontal");
		y = ZInput.GetAxis("Vertical");
		if (x != 0 && y != 0)
		{
			animPlayer.SetBool(gun.ToString(), false);
			var Rotatev = new Vector3(-x, 0, -y);
			Quaternion quaternion = Quaternion.LookRotation(Rotatev);
			transform.rotation = quaternion;
		}
		else if(x == 0 && y == 0) 
		{
			CloseObject = ClosestTarget(EnemyList);
			if (CloseObject != null && Vector3.Distance(transform.position, CloseObject.transform.position) < DistanceEnemy)
			{
				transform.LookAt(new Vector3(CloseObject.transform.position.x,
					0, CloseObject.transform.position.z));
				animPlayer.SetBool(gun.ToString(), true);
			}
            else
            {
				animPlayer.SetBool(gun.ToString(), false);
			}
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
		if(System.Math.Abs(x) > .3f || System.Math.Abs(y) > .3f)
		{
			xmove = x;
			ymove = y;
		}
		//if(System.Math.Abs(ZInput.GetAxis("S-Horizontal")) > 0 || System.Math.Abs(ZInput.GetAxis("S-Horizontal")) > 0)
		//{
		//	var Rotatev = new Vector3(-ZInput.GetAxis("S-Horizontal"), 0, -ZInput.GetAxis("S-Vertical"));
		//	Quaternion quaternion = Quaternion.LookRotation(Rotatev);
		//	transform.rotation = quaternion;
		//}
		
		characterController.Move((new Vector3(-xmove, -1 , -ymove) * Speed * Time.deltaTime));
		
		if(ZInput.GetKeyDown("change gun"))
		{
			animPlayer.SetBool(gun.ToString(), false);
			if ((int)gun == 1) gun--; else gun++;
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
