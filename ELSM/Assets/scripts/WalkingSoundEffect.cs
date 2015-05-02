using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class WalkingSoundEffect : MonoBehaviour {

	public float timeBetweenFootfalls=0.1f;

	float forwardSpeed;
	float backwardSpeed;
	float t=0;
	public List<AudioClip> footsteps;
	// Use this for initialization
	void Start () {
		PlayerControl p = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerControl> ();
		if (p) {
			forwardSpeed=p.speed_/10;
			backwardSpeed=p.backSpeed_/10;
		}

	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey ("w")) {
		
			t+=Time.deltaTime*forwardSpeed;
		}
		if (Input.GetKey ("s")) {
			t+=Time.deltaTime*backwardSpeed;
		}
		if (t >= timeBetweenFootfalls) {
			AudioSource s=this.GetComponent<AudioSource>();
			if(!s)
			{
				Debug.LogError("No Audio source on feet");
			}
			s.PlayOneShot(footsteps[Random.Range(0,footsteps.Count)]);
			t=0;
		}
	}
}
