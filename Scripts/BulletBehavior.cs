using UnityEngine;
using System.Collections;

public class BulletBehavior : MonoBehaviour {

    private bool ai = true;
    private Rigidbody rB;
    public GameObject bloodSpray;
    public GameObject stoneSpray;
    public GameObject stoneCoverHit;

	// Use this for initialization
	void Awake () 
    {
        Invoke("OutOfRange", 0.9f);
        rB = GetComponent<Rigidbody>();

	}
    public bool getAi()
    {
        return ai;
    }
    public void setAi(bool tf)
    {
        ai = tf;
    }
	// Update is called once per frame
    void OnTriggerEnter(Collider other)
    {
        GameObject g = other.gameObject;
        if (g.tag == "player" || g.tag == "enemy")
        {
            if (g.GetComponent<PlayerController>() != false)
            {
                g.GetComponent<PlayerController>().DoDamage(34);
                Quaternion q = transform.rotation;
                GameObject go = (GameObject)Instantiate(bloodSpray, transform.position, q);
                if (rB.velocity.z > 0)
                {
                    go.transform.localEulerAngles = new Vector3(0, 0, 0);
                }
                else
                {
                    go.transform.localEulerAngles = new Vector3(0, 180, 0);
                }
            }
            else if (g.GetComponent<AIscript>() != false)
            {
                g.GetComponent<AIscript>().DoDamage(34);
                Quaternion q = transform.rotation;
                GameObject go = (GameObject)Instantiate(bloodSpray, transform.position, q);
                if (rB.velocity.z > 0)
                {
                    go.transform.localEulerAngles = new Vector3(0, 0, 0);
                }
                else
                {
                    go.transform.localEulerAngles = new Vector3(0, 180, 0);
                }
            }
            else if (g.GetComponent<GnomeScript>() != false)
            {
                g.GetComponent<GnomeScript>().DoDamage(34);
                Quaternion q = transform.rotation;
                GameObject go = (GameObject)Instantiate(bloodSpray, transform.position, q);
                if (rB.velocity.z > 0)
                {
                    go.transform.localEulerAngles = new Vector3(0, 0, 0);
                }
                else
                {
                    go.transform.localEulerAngles = new Vector3(0, 180, 0);
                }
            }
            else if (g.GetComponent<ReptileScript>() != false)
            {
                g.GetComponent<ReptileScript>().DoDamage(34);
                Quaternion q = transform.rotation;
                GameObject go = (GameObject)Instantiate(bloodSpray, transform.position, q);
                if (rB.velocity.z > 0)
                {
                    go.transform.localEulerAngles = new Vector3(0, 0, 0);
                }
                else
                {
                    go.transform.localEulerAngles = new Vector3(0, 180, 0);
                }
            }
        }
        if (g.tag != "bullet" && g.tag != "superfluous")
        {
            if (g.tag == "barricade")
            {
                Quaternion q = transform.rotation;
                g.transform.parent.GetComponent<StoneCoverScript>().Hit();
                GameObject ro = (GameObject)Instantiate(stoneSpray, transform.position, q);
                if (rB.velocity.z > 0)
                {
                    ro.transform.localEulerAngles = new Vector3(0, 180, 0);
                }
                else
                {
                    ro.transform.localEulerAngles = new Vector3(0, 0, 0);
                }
                GameObject lo = (GameObject)Instantiate(stoneCoverHit, transform.position - (rB.velocity * 3*Time.deltaTime), q);
                lo.GetComponent<StoneChipScript>().SetDirection(-rB.velocity.x, -rB.velocity.y, -rB.velocity.z);
                lo = (GameObject)Instantiate(stoneCoverHit, transform.position - (rB.velocity * Time.deltaTime), q);
                lo.GetComponent<StoneChipScript>().SetDirection(-rB.velocity.x, -rB.velocity.y, -rB.velocity.z);
                lo = (GameObject)Instantiate(stoneCoverHit, transform.position - (rB.velocity * Time.deltaTime), q);
                lo.GetComponent<StoneChipScript>().SetDirection(-rB.velocity.x, -rB.velocity.y, -rB.velocity.z);
            }
            Destroy(gameObject);
        }
    }
    void OutOfRange()
    {
        Destroy(gameObject);
    }
}
