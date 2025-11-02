using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;


public class playerScript : MonoBehaviour
{
	public KeyCode shootKey = KeyCode.T;
	public Transform sprite;
	// Use this for initialization
	Animator anim;
	//Speed and jump vary between characters
	public bool sniper_spawn;


	public static float Speed = 5.00f;
	public static float Jump = 2.5f;
	public static bool grounded;
	public bool ground;
	public static bool Scout = false;
	public Rigidbody2D rigid;
	Movement Move = new Movement();

	Ray2D ray;
	RaycastHit2D hit;

	public GameObject bullet;
	public GameObject bulletSpawn;
	private Store store;
	public List<GameObject> nets = new List<GameObject>();
	public GameObject selectedNet;
	public Register register;
	public bool canMove = true;


	void Start()
	{
		anim = GetComponent<Animator>();
		this.store = GameStore.Get();
		this.register = GameRegister.Get();
		this.register.player = this;
		ChangeNet(this.store.net.level);
	}
	void FixedUpdate()
	{


		GroundDetection();
		ground = grounded;

	}

	// Update is called once per frame
	void Update()
	{

		anim.SetFloat("Speed", Mathf.Abs(rigid.velocity.x));
		//anim.SetBool ("touchingGround", grounded);

		if (canMove)
		{
			Move.Motion(Speed, Jump, rigid, true, Scout, sprite);
		}
		if (Input.GetKeyDown(shootKey))
		{

		}
	}
	public void SetCanMove(bool canMove)
	{
		this.canMove = canMove;



	}
    



    public void GroundDetection()
	{
		hit = Physics2D.Raycast(GameObject.Find("Sniper_Feet").transform.position, Vector2.down);

		if (hit.distance < 0.03)
		{
			grounded = true;
		}
		if (hit.distance > 0.03)
		{
			grounded = false;
		}
	}

	void OnCollisionEnter2D(Collision2D collision)
	{




	}
	public void ChangeNet(int level)
	{
		var position = selectedNet.transform.position;
		var parent = selectedNet.transform.parent;
		DestroyImmediate(selectedNet, true);
		var netPrefab = nets[level];
		selectedNet = Instantiate(netPrefab, position, Quaternion.identity, parent);


	}
}
