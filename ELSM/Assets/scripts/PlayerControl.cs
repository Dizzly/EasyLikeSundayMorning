using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour {


	public float speed_ =8;
	public float backSpeed_=3;
	public float rotSpeed_=0.4f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKey("w")) {
			Vector3 dir=transform.forward;
			GetComponent<Rigidbody>().AddForce(dir*speed_);
		}
		if (Input.GetKey ("a")) {
			GetComponent<Rigidbody>().AddTorque(-transform.up*rotSpeed_);
		}
		if (Input.GetKey ("d")) {
			GetComponent<Rigidbody>().AddTorque(transform.up*rotSpeed_);
		}
		if (Input.GetKey("s")) {
			Vector3 dir=transform.forward;
			GetComponent<Rigidbody>().AddForce(-dir*backSpeed_);
		}


	}
}
