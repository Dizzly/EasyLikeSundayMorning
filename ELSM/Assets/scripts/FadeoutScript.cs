using UnityEngine;
using System.Collections;

public class FadeoutScript : MonoBehaviour
{


	bool active = false;
	bool recede = false;
	bool hanging = false;
	public float spreadSpeedMultiplier = 1;
	public float recedeSpeedMultiplier = 1;
	public float recedeAfterSecs = 4;
	public float hangAtRecedeSecs = 1;
	float t = 0;
	float hangT = 0;
	// Use this for initialization
	void Start ()
	{

		SetPosAndColl (new Vector3 (), 0);
		SetT (0);
		SetChildPosAndColl (new Vector3 (), 0);
		SetChildT (0);
	}

	void SetPosAndColl(Vector3 pos, float col)
	{
		Renderer r = this.GetComponent<Renderer> ();
		if (!r) {
			return;
		}
		for (int i=0; i<r.materials.Length; ++i) {
			r.materials[i].SetFloat ("_Collision", col);
			r.materials[i].SetVector("_HitPosition", new Vector4 (pos.x, pos.y, pos.z, 0));
		}
	}

	void SetChildPosAndColl(Vector3 pos, float col)
	{
		for(int i=0;i<this.gameObject.transform.childCount;++i)
		{
			Material m=this.gameObject.transform.GetChild(i).GetComponent<Renderer>().material;
			if(m)
			{
				m.SetFloat ("_Collision", col);
				m.SetVector ("_HitPosition", new Vector4 (pos.x, pos.y, pos.z, 0));
			}
		}
	}

	void SetT(float t)
	{
		Renderer r = this.GetComponent<Renderer> ();
		if (!r) {
			return;
		}
		for (int i=0; i<r.materials.Length; ++i) {
			r.materials[i].SetFloat ("_T", t);
		}
	}

	void SetChildT(float t)
	{
		for (int i=0; i<this.gameObject.transform.childCount; ++i) {
			Material m= this.gameObject.transform.GetChild(i).GetComponent<Renderer>().material;
			if(m)
			{
				m.SetFloat("_T",t);

			}
		
		}

	}

	void OnCollisionEnter (Collision col)
	{
		if (col.collider.CompareTag ("Player")) {
			if (!active) {
				Vector3 pos = col.contacts [0].point;
				SetChildPosAndColl(pos,1);
				SetPosAndColl(pos,1);
					active = true;
			}
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (active) {
			if (!hanging) {
				if (!recede) {
					t += Time.deltaTime * spreadSpeedMultiplier;
				} else {
					t -= Time.deltaTime * recedeSpeedMultiplier;
				}
				if (t <= 0) {
					active = false;
					recede = false;
					hangT = 0;
					hanging = false;
					t = 0;
				}
				SetT (t);
				SetChildT(t);

				if (t > recedeAfterSecs && !recede) {
					if (hangAtRecedeSecs > 0) {
						hanging = true;
						hangT = hangAtRecedeSecs;
					} else {
						recede = true;
					}
				}
			} else {
				hangT -= Time.deltaTime;
				if (hangT < 0) {
					hanging = false;
					recede = true;
				}

			}
		}
	}
}
