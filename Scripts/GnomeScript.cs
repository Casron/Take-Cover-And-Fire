using UnityEngine;
using System.Collections;

public class GnomeScript : MonoBehaviour 
{

    public Animator anim;
    public GameObject axe;
    private GameObject enemyPlayer;
    private Rigidbody rB;
    private int maxMovePower = 80;
    private int airMovePower = 20;
    private int movePower = 80;
    private int jumpPower = 300;
    private float spd  = 2.0f;
    private int numJumps = 1;
    private int hp = 30;
    public BoxCollider box;
    private bool grounded = true;
    public GameObject scorekeeper;

	// Use this for initialization
	void Start () 
    {
        GameObject s = GameObject.FindGameObjectWithTag("ScoreKeeper");
        scorekeeper = s;
        GameObject[] g = GameObject.FindGameObjectsWithTag("player");
        for (int i = 0; i < g.Length; i++)
        {
            if (g[i].GetComponent<PlayerController>() != null)
            {
                enemyPlayer = g[i];
                break;
            }
        }
        InvokeRepeating("Decide", 0.2f, 0.2f);
	}
	void Awake()
    {
        rB = GetComponent<Rigidbody>();
    }
    void Decide()
    {

        if (enemyPlayer.transform.position.z > transform.position.z)
        {
            transform.localEulerAngles = new Vector3(0.0f, 0.0f, 0.0f);
        }
        else
        {
            transform.localEulerAngles = new Vector3(0.0f, 180.0f, 0.0f);
        }

        Ray ray = new Ray(transform.position, transform.forward);
        float distance = 0.6f;
        LayerMask mask = -1;
        RaycastHit[] hits = Physics.RaycastAll(ray, distance, mask.value);

        foreach (RaycastHit ahit in hits)
        {
            if ((ahit.transform.tag == "barricade" || ahit.transform.gameObject.GetComponent<ReptileScript>() != false) && grounded && numJumps > 0)
            {
                Jump();
                break;
            }
        }
    }
    void Move()
    {
        if (Mathf.Abs(rB.velocity.z) < 2 && Mathf.Abs(transform.position.z-enemyPlayer.transform.position.z) > 0.5 && hp > 0)
        {
            Vector3 movement = (transform.forward * spd * Time.deltaTime * movePower);
            rB.AddForce(movement);
            anim.SetBool("Attacking", false);
        }
        else if (hp > 0 && Mathf.Abs(transform.position.z-enemyPlayer.transform.position.z) <= 0.5)
        {
            anim.SetBool("Attacking", true);
        }
    }

    public void DoDamage(int x)
    {

        if (hp > 0 && hp <= x)
        {

            Invoke("Del", 15.0f);
            scorekeeper.GetComponent<ScoreScript>().UpdateScore(5);
            float derp = 2.0f * Random.value;
            if (derp <= 1)
            {
                anim.SetBool("Dead1", true);
            }
            else
            {
                anim.SetBool("Dead2", true);
            }
            Destroy(axe);
            CancelInvoke("Decide");
            box.size = new Vector3(0.035f, 0.0f, 0.035f);
            box.center = new Vector3(0.3f, 0.00f, 0.0f);
        }
        hp -= x;
    }
	// Update is called once per frame
	void Update () 
    {
	
	}
    void FixedUpdate()
    {
        Move();
    }
    private void Jump()
    {
        if (numJumps > 0)
        {
            numJumps -= 1;
            rB.AddForce(transform.up * jumpPower);
            movePower = airMovePower;
            rB.drag = 0.0f;
            grounded = false;
        }
    }
    public void JumpReset()
    {
        numJumps = 1;
        movePower = maxMovePower;
        rB.drag = 0.05f;
        float tempZ = rB.velocity.z;
        rB.velocity = new Vector3(0.0f, 0.0f, tempZ);
        grounded = true;
    }
    public void Knockup()
    {
        grounded = false;
    }
    private void Del()
    {
        Destroy(gameObject);
    }
    public int getHP()
    {
        return hp;
    }
}
