using UnityEngine;
using System.Collections;

public class YetiController : MonoBehaviour {

	private Rigidbody2D _rigidBody;
	private Animator _animator;

	private int _health;
	private bool _jump;
	private bool _grounded;


	// Use this for initialization
	void Start () {
	
		_rigidBody = GetComponent<Rigidbody2D> ();
		_animator = GetComponent<Animator> ();

	}
	
	// Update is called once per frame
	void Update () 
	{
		//reading key press or mouse input: do it here
		//reading Input.GetAxis? Here or FixedUpdate
		if (!_jump) 
		{
			_jump = Input.GetButtonDown ("Fire1");
		}
	}
	void FixedUpdate ()
	{
		if (_jump)
		{
			_jump = false;
			_rigidBody.AddForce (new Vector2 (0, 1000));
			_animator.SetTrigger("Jumping");
			_grounded = false;
			_animator.SetBool("Grounded", false);
		}

		//Read input which will be from -1 to +1
		var horizontal = Input.GetAxis ("Horizontal");

		//localScale is a Vector3, so contains an x,y,z
		var localScale = transform.localScale;

		//flip yeti left if input x < 0, right if > 0
		if (horizontal < 0) {
						//transform.localScale=-1;
			localScale.x = -1;
				} 

		else if (horizontal > 0) 
		{
			localScale.x = 1;
		}

		//Set the transforms localScale
		transform.localScale = localScale;

		//Set the animator variable for running
		if (horizontal != 0)
		{
			_animator.SetBool ("Running", true);
		}
		else 
		{
			_animator.SetBool("Running", false);
		}

		_rigidBody.velocity = new Vector2 (horizontal * 20, _rigidBody.velocity.y);
	}

	void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.tag == "Platform")
		{
			_grounded = true;
			_animator.SetBool("Grounded", true);
		}
	}
}
