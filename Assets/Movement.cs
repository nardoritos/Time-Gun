using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {
	public float movementSpeed;
	// Use this for initialization
	void Start () {
      

	}
	
	// Update is called once per frame
	void Update () {
        Mover();
    }

    void Mover()
    {
        string animacao;
        var anim = GetComponent<Animator>();
        if (Input.GetAxis("Horizontal")==0 && Input.GetAxis("Vertical") == 0)
        {
            
            animacao = "Idle";
           
        }
        else
        {
            animacao = "Walking";
            
        }
        anim.Play(animacao);
        
		float moveHorizontal = Input.GetAxis("Horizontal");
		float moveVertical = Input.GetAxis("Vertical");

		Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
		if(movement != Vector3.zero) 
			transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movement.normalized * -1), 0.15f);
	

		transform.Translate (movement * movementSpeed * Time.deltaTime *-1, Space.World);
       
        
    }

}
