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
	Material fadeinShader;

	// Use this for initialization
	void Start ()
	{
		fadeinShader = this.GetComponent<Renderer> ().material;
		if (!fadeinShader || fadeinShader.name != "FadeoutMaterial (Instance)") {
			Debug.Log ("Missing FadeoutMaterial on" + this.name);
		}
		fadeinShader.SetFloat ("_Collision", 0);
		fadeinShader.SetFloat ("_T", 0);
		SetChildPosAndColl (new Vector3 (), 0);
		SetChildT (0);
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
				fadeinShader.SetFloat ("_Collision", 1);
				fadeinShader.SetVector ("_HitPosition", new Vector4 (pos.x, pos.y, pos.z, 0));
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
				fadeinShader.SetFloat ("_T", t);
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
