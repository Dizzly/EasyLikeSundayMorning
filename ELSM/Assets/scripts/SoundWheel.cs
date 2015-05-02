using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class SoundWheel : MonoBehaviour {
	
	public List<GameObject> soundObjects;

	public float displayRadius=2;

	public float whiteoutMin=0.70f;
	public float whiteoutMax=0.75f;
	public float falloffVolume=0.1f;

	public float maxIconScale=3.0f;
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

		List<float> scales = new List<float> ();
		float highest = 0;
		for (int i=0; i<soundObjects.Count; ++i) {
			AudioSource source=((GameObject)soundObjects[i]).GetComponent<AudioSource>();
			SoundObject s=((GameObject)soundObjects[i]).GetComponent<SoundObject>();
			float dist=Vector3.Distance(soundObjects[i].transform.position, this.transform.position);

			if(dist>s.soundFalloffDist)
			{
				scales.Add(0);
				continue;
			}

			float minDist=source.minDistance;
			float diff=dist/minDist;

			float percivedVol=source.volume/diff*2;
			if(percivedVol>highest)
			{
				highest=percivedVol;
			}
			scales.Add(percivedVol);
		}
		float clampVal = Mathf.Clamp (highest, whiteoutMin, whiteoutMax);
		float whiteout = (whiteoutMax - clampVal) / (whiteoutMax - whiteoutMin);
		for (int i=0; i<scales.Count; ++i) {
		
			if(scales[i]<highest&&whiteout<1)
			{
				scales[i]*=whiteout;
			}
			if(scales[i]<falloffVolume)
			{
				scales[i]=0;
			}

			scales[i]*=maxIconScale;
			scales[i]=Mathf.Clamp(scales[i],0,maxIconScale);
		}

		for(int i=0;i<soundObjects.Count;++i)
		{
			Vector3 normDir= ((GameObject)soundObjects[i]).transform.position-  this.transform.position;
			Vector3 dir=normDir;
			normDir.Normalize();

			//placement code
			SoundObject s=((GameObject)soundObjects[i]).GetComponent<SoundObject>();
			AudioSource source=((GameObject)soundObjects[i]).GetComponent<AudioSource>();

			Vector3 pos= transform.position+normDir*displayRadius;
			if(Vector3.Distance(pos,transform.position)>
			   Vector3.Distance(transform.position,soundObjects[i].transform.position))
			{
				pos=soundObjects[i].transform.position;
			}
			s.displayIcon.transform.position=pos;
		
			s.displayIcon.transform.localScale=new Vector3(scales[i],scales[i],scales[i]);

		}
	}
}
