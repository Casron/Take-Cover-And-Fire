using UnityEngine;
using System.Collections;

public class FallingBombScript : MonoBehaviour {

    public Material mat1;
    public Material mat2;
    public GameObject explosion;

    private Renderer rend;
	// Use this for initialization
	void Start () 
    {
        rend = GetComponent<MeshRenderer>();
        Invoke("ColorswapRed", 0.1f);
        rend.material = mat2;
        transform.localEulerAngles = new Vector3(90.0f, 0.0f, 0.0f);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void ColorswapRed()
    {
        rend.material = mat2;
        Invoke("ColorswapBlack", 0.1f);
    }
    void ColorswapBlack()
    {
        rend.material = mat1;
        Invoke("ColorswapRed", 0.1f);
    }
    void OnTriggerEnter(Collider Other)
    {
        Instantiate(explosion,transform.position,transform.rotation);
        Destroy(gameObject);
    }
}
