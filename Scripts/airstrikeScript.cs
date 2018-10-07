using UnityEngine;
using System.Collections;

public class airstrikeScript : MonoBehaviour {

    private GameObject Target;
    private Rigidbody rB;
    private bool readyToFire;
    private float rof;
    public GameObject Bullet;

    void Awake()
    {
        rB = GetComponent<Rigidbody>();
        rB.AddForce(new Vector3(0,0,1) * 65);
    }
	// Use this for initialization
	void Start () 
    {
        Target = null;
        InvokeRepeating("BoundsCheck", 1.0f, 1.0f);
        InvokeRepeating("TargetSelect", 1.0f, 1.0f);
        readyToFire = true;
        rof = 0.15f;
	}
	
    void BoundsCheck()
    {
        if (transform.position.z > 20.0f)
        {
            Destroy(gameObject);
        }
    }

    void TargetSelect()
    {
        GameObject[] e = GameObject.FindGameObjectsWithTag("enemy");
        GameObject[] p = GameObject.FindGameObjectsWithTag("player");
        float derp = 1000.0f;
        Target = null;
        for (int i = 0; i < e.Length; i++)
        {
            if (Mathf.Abs(transform.position.z - e[i].transform.position.z) < 8.0f && Mathf.Abs(transform.position.z - e[i].transform.position.z) < derp)
            {
                if (e[i].GetComponent<ReptileScript>() != null)
                {   
                    if (e[i].GetComponent<ReptileScript>().getHP() > 0)
                    {
                        derp = Mathf.Abs(transform.position.z - e[i].transform.position.z);
                        Target = e[i];
                    }
                }
                else if (e[i].GetComponent<GnomeScript>() != null)
                {   
                    if (e[i].GetComponent<GnomeScript>().getHP() > 0)
                    {
                        derp = Mathf.Abs(transform.position.z - e[i].transform.position.z);
                        Target = e[i];
                    }
                }                
            }
        }
        for (int i = 0; i < p.Length; i++)
        {
            if (Mathf.Abs(transform.position.z - p[i].transform.position.z) < 8.0f && Mathf.Abs(transform.position.z - p[i].transform.position.z) < derp && p[i].GetComponent<AIscript>() != null)
            {
                derp = Mathf.Abs(transform.position.z - p[i].transform.position.z);
                Target = p[i];
            }
        }
    }
	// Update is called once per frame
    void Update () 
    {

	}
    void FixedUpdate()
    {
        Shoot();
    }

    void Shoot()
    {
        if (Target != null && readyToFire)
        {
            Vector3 aim = Target.transform.position;
            aim = new Vector3(aim.x, aim.y, aim.z + Target.GetComponent<Rigidbody>().velocity.z);
            aim = new Vector3(aim.x-0.01f + (Random.value * 0.02f),aim.y,aim.z-0.6f + (Random.value * 1.2f));
            GameObject go = (GameObject)Instantiate(Bullet, transform.position, transform.rotation);
            go.transform.rotation = transform.rotation;
            go.GetComponent<Rigidbody>().velocity = ((aim-transform.position).normalized) * 8.0f;
            go.GetComponent<AirstrikeBullet>().setAi(false);
            readyToFire = false;
            Invoke("Reload", rof);
        }
    }
    void Reload()
    {
        readyToFire = true;
    }
}
