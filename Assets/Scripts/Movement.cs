using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {
	private Timeback timeController;
	public float movementSpeed;
	Rigidbody rb;
	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
		timeController = FindObjectOfType (typeof(Timeback)) as Timeback;
	}
	
	// Update is called once per frame
	void Update () {
		if (!timeController.isReversing) {
			Mover ();
			Pular ();
		}
    }

    void Mover()
    {
        string animacao;
        var anim = GetComponent<Animator>();
        if (Input.GetAxis("Horizontal")==0)
        {
            
            animacao = "Idle";
           
        }
        else
        {
            animacao = "Walking";
            
        }
        anim.Play(animacao);
        
		float moveHorizontal = Input.GetAxis("Horizontal");


		Vector3 movement = new Vector3(moveHorizontal, 0.0f, 0);
		if(movement != Vector3.zero) 
			transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movement.normalized * -1), 0.15f);
	

		transform.Translate (movement * movementSpeed * Time.deltaTime *-1, Space.World);
       
        
    }

	void Pular(){
		if(Input.GetKeyDown(KeyCode.Space)){
			rb.velocity= new Vector3(0,0,0);
			rb.AddForce (Vector3.up * 1000f);
			
		}
	}
}
