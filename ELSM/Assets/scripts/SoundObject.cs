using UnityEngine;
using System.Collections;

public class SoundObject : MonoBehaviour {
	
	// a representation of logarithmic falloff
	public float soundFalloffDist;


	//sound prefab, do not use in code!
	public GameObject displayIconPrefab;
	[HideInInspector]
	public GameObject displayIcon;

	// Use this for initialization
	void Start () {
		displayIcon = GameObject.Instantiate (displayIconPrefab);
		displayIcon.SetActive (false);
		SoundWheel s = GameObject.FindGameObjectWithTag ("Player").GetComponent<SoundWheel> ();
		s.AddSoundObject (this.gameObject);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnDisable()
	{
		RemoveFromSoundWheel ();
	}


	void RemoveFromSoundWheel()
	{
		SoundWheel s=GameObject.FindGameObjectWithTag ("Player").GetComponent<SoundWheel> ();
		s.RemoveSoundObject (this.gameObject);
	}

}
