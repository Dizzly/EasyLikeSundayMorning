using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class SoundWheel : MonoBehaviour {
	
	public List<GameObject> soundObjects;

	public float displayRadius=2;
	public float checkRadius=10;

	public float maxIconScale;
	// Use this for initialization
	void Start () {
		
	}

	public void AddSoundObject(GameObject s)
	{
		soundObjects.Add(s);
		s.GetComponent<SoundObject> ().displayIcon.SetActive (true);
	}
	public void RemoveSoundObject(GameObject s)
	{
		soundObjects.Remove (s);
		s.GetComponent<SoundObject> ().displayIcon.SetActive (false);
	}

	// Update is called once per frame
	void Update () {
		for(int i=0;i<soundObjects.Count;++i)
		{
			Vector3 dir= ((GameObject)soundObjects[i]).transform.position-  this.transform.position;
			float dirLen=dir.magnitude;
			dir.Normalize();
			SoundObject s=((GameObject)soundObjects[i]).GetComponent<SoundObject>();
			Vector3 pos= transform.position+dir*displayRadius;
			s.displayIcon.transform.position=pos;
			Vector3 scale=new Vector3(1,1,1)*s.priority;
			scale/=(dirLen*s.soundFalloff);
			s.displayIcon.transform.localScale= scale;

		}
	}
}
