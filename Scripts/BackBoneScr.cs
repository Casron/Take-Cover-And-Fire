using UnityEngine;
using System.Collections;

public class BackBoneScr : MonoBehaviour {

    public GameObject boneRef;
    public Camera Cam;
    public Vector3 pos;
    public Vector3 mPos;
	// Use this for initialization
	
	// Update is called once per frame
	void Update () 
    {
        pos = Cam.WorldToScreenPoint(boneRef.transform.position);
        mPos = Input.mousePosition;
        boneRef.transform.localEulerAngles = new Vector3(0, 0, ((180.0f*Mathf.Atan((mPos.y - pos.y) / (mPos.x - pos.x)))/Mathf.PI));
	}
}
