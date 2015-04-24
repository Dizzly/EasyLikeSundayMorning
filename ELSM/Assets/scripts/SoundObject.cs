using UnityEngine;
using System.Collections;

public class SoundObject : MonoBehaviour {

	//the percived loudness of the object as percived by the noise wheel
	public float priority;
	// a representation of logarithmic falloff
	public float soundFalloff;


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

	void OnDestroy()
	{
		RemoveFromSoundWheel ();

	}

	void RemoveFromSoundWheel()
	{
		SoundWheel s=GameObject.FindGameObjectWithTag ("Player").GetComponent<SoundWheel> ();
		s.RemoveSoundObject (this.gameObject);
	}

}
