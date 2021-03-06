﻿using UnityEngine;
using System.Collections;

public class AIscript : MonoBehaviour 
{

    public GameObject gunReference;
    public GameObject Bullet;
    public GameObject Bs;
    private Rigidbody rB;
    private float spd = 2.0f;
    public Animator anim;
    private bool crouched;
    public BoxCollider box;
    public int numJumps = 1;
    private int jumpPower = 110;
    private int maxMovePower = 300;
    private int airMovePower = 100;
    private int movePower = 300;
    private int maxHP = 100;
    private bool readyToFire = true;
    private Quaternion q;
    private Vector3 mPos;
    private bool grounded = true;
    private int hp = 100;
    bool relocate = false;
    private GameObject enemyPlayer;
    private float enemyDir;
    private float decisionTime = 0.15f;
    private float rof = 0.25f;
    private bool firing = false;
    private GameObject nearestBullet;
    GameObject currentCover;
    GameObject coverNearest;
    GameObject[] coverReference;
    private int numOccurances;
    private AudioSource aud;
    public GameObject muzzleFlash;

    void Awake()
    {
        aud = GetComponent<AudioSource>();
        rB = GetComponent<Rigidbody>();
        crouched = false;
        numOccurances = 0;
        gameObject.SetActive(true);
    }
	// Use this for initialization
	void Start () 
    {
        GameObject[] g = GameObject.FindGameObjectsWithTag("player");
        coverReference = GameObject.FindGameObjectsWithTag("barricade");
        for(int i = 0; i < g.Length; i++)
        {
            if (g[i].GetComponent<PlayerController>() != null)
            {
                enemyPlayer = g[i];
                break;
            }
        }
        float derp = 1000.0f;
        for (int i = 0; i < coverReference.Length; i++)
        {
            if (enemyDir == 1)
            {
                if (Mathf.Abs(coverReference[i].transform.position.z - (enemyPlayer.transform.position.z + 10.0f)) < derp)
                {
                    derp = Mathf.Abs(coverReference[i].transform.position.z - (enemyPlayer.transform.position.z + 10.0f));
                    currentCover = coverReference[i];
                }
            }
            else
            {
                if (Mathf.Abs(coverReference[i].transform.position.z - (enemyPlayer.transform.position.z - 10.0f)) < derp)
                {
                    derp = Mathf.Abs(coverReference[i].transform.position.z - (enemyPlayer.transform.position.z - 10.0f));
                    currentCover = coverReference[i];
                }
            }
        }
        InvokeRepeating("enemyCheck", 0.2f, 0.2f);
        InvokeRepeating("decide", decisionTime, decisionTime);
	}
    public void GiveHealth(int x)
    {
        if ((hp + x) <= maxHP)
        {
            hp += x;
        }
        else
        {
            hp = maxHP;
        }
    }	
    public void DoDamage(int x)
    {
        if (hp <= x)
        {
            enemyPlayer.GetComponent<PlayerController>().KilledAnEnemy();
        }
        hp -= x;
        if (hp <= 0)
        {
            anim.SetBool("Dead", true);
            CancelInvoke("enemyCheck");
            CancelInvoke("decide");
            box.size = new Vector3(0.035f, 0.0f, 0.035f);
            box.center = new Vector3(0.2f, 0.00f, 0.0f);
        }
    }
	// Update is called once per frame
	void Update () 
    {
        if (!crouched && !relocate)
            gunReference.transform.localEulerAngles = new Vector3(20.6f, 0.0f, 270.0f);
        else if (!crouched)
            gunReference.transform.localEulerAngles = new Vector3(0.0f, 0.0f, 270.0f);
        else
            gunReference.transform.localEulerAngles = new Vector3(350.6f, 0.0f, 270.0f);
	}
    void FixedUpdate()
    {
        if (hp > 0)
        {
            Shoot();
            Move();
        }
    }
    private void enemyCheck()
    {

        coverNearest = currentCover;
        if (enemyPlayer == null)
        {
            GameObject[] g = GameObject.FindGameObjectsWithTag("player");
            for (int i = 0; i < g.Length; i++)
            {
                if (g[i].GetComponent<PlayerController>() != null)
                {
                    enemyPlayer = g[i];
                    break;
                }
            }
        }
        if (currentCover.transform.position.z < transform.position.z)
        {
            enemyDir = -1;
            transform.localEulerAngles = new Vector3(0.0f, 180.0f, 0.0f);
        }
        else
        {
            enemyDir = 1;
            transform.localEulerAngles = new Vector3(0.0f, 0.0f, 0.0f);
        }
        float derp = 1000.0f;
        float dist = 7.2f;
        for(int i = 0; i < coverReference.Length; i++ )
        {
            if (enemyPlayer.transform.position.z < transform.position.z)
            {
                float localDerp = Mathf.Abs(coverReference[i].transform.position.z - (enemyPlayer.transform.position.z + dist));
                float checkDerp = Mathf.Abs(coverReference[i].transform.position.z - (transform.position.z));
                float checkEnemyDerp = Mathf.Abs(coverReference[i].transform.position.z - (enemyPlayer.transform.position.z));
                if (localDerp < derp && checkDerp < checkEnemyDerp)
                {
                    derp = Mathf.Abs(coverReference[i].transform.position.z - (enemyPlayer.transform.position.z + dist));
                    currentCover = coverReference[i];
                }
            }
            else
            {
                float localDerp = Mathf.Abs(coverReference[i].transform.position.z - (enemyPlayer.transform.position.z - dist));
                float checkDerp = Mathf.Abs(coverReference[i].transform.position.z - (transform.position.z));
                float checkEnemyDerp = Mathf.Abs(coverReference[i].transform.position.z - (enemyPlayer.transform.position.z));
                if (localDerp < derp && checkDerp < checkEnemyDerp)
                {
                    derp = Mathf.Abs(coverReference[i].transform.position.z - (enemyPlayer.transform.position.z - dist));
                    currentCover = coverReference[i];
                }
            }
        }
    }
    private void decide()
    {
        GameObject[] bulletSelect = GameObject.FindGameObjectsWithTag("bullet");
        float derp = 1000.0f;
        for (int i = 0; i < bulletSelect.Length; i++)
        {
            if (Mathf.Abs(bulletSelect[i].transform.position.z - transform.position.z) < derp && !bulletSelect[i].GetComponent<BulletBehavior>().getAi() && !relocate && bulletSelect[i].transform.position.y < transform.position.y + .65f && (bulletSelect[i].transform.position.y >= transform.position.y + 0.48f))
            {
                derp = Mathf.Abs(bulletSelect[i].transform.position.z - transform.position.z);
                nearestBullet = bulletSelect[i];
            }
        }
        if (derp < 1.0f)
        {
            Crouch();
            CancelInvoke("Decrouch");
        }
        else
        {
            Invoke("Decrouch", 0.75f);
        }
        if (Mathf.Abs(enemyPlayer.transform.position.z - transform.position.z) < 9.5f && coverCheck())
        {
            firing = true;
        }
        else
        {
            firing = false;
        }
        float idealZ = currentCover.transform.position.z;
        if (enemyPlayer.transform.position.z > idealZ)
        {
            idealZ = currentCover.transform.position.z - 0.4f;
        }
        else
        {
            idealZ = currentCover.transform.position.z + 0.4f;
        }
        if (Mathf.Abs(idealZ - transform.position.z) > 0.1f)
        {
            anim.SetBool("Moving", true);
            relocate = true;
            numOccurances += 1;
        }
        else
        {
            anim.SetBool("Moving", false);
            relocate = false;
        }
        Vector3 adjustedHeight = transform.position;
        adjustedHeight = new Vector3(adjustedHeight.x, adjustedHeight.y + 0.2f, adjustedHeight.z);
        Ray ray = new Ray(adjustedHeight, transform.forward);
        float distance = 0.6f;
        LayerMask mask = -1;
        RaycastHit[] hits = Physics.RaycastAll(ray, distance, mask.value);

        foreach (RaycastHit ahit in hits)
        {
            if (ahit.transform.tag == "barricade" && relocate)
            {
                Jump();
                break;
            }
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
    private void Shoot()
    {
        if (readyToFire && firing)
        {
            aud.Play();
            Vector3 aim = transform.forward;
            aim = new Vector3(aim.x, (aim.y - .1f) + (.2f * Random.value), aim.z);
            GameObject go = (GameObject)Instantiate(Bullet, Bs.transform.position, q);
            go.GetComponent<Rigidbody>().AddForce(aim * 500.0f);
            go.GetComponent<BulletBehavior>().setAi(true);
            GameObject troll = (GameObject)Instantiate(muzzleFlash, Bs.transform.position, q);
            troll.GetComponent<MuzzleFlashScript>().setDir(0.0f+transform.forward.z);
            readyToFire = false;
            Invoke("Reload", rof);
        }
    }
    void Move()
    {
        if (Mathf.Abs(rB.velocity.z) < 3 && !crouched)
        {
            Vector3 movement = (transform.forward * Mathf.Abs(enemyDir) * spd * Time.deltaTime * movePower);
            rB.AddForce(movement);
        }
    }
    void Reload()
    {
        readyToFire = true;
    }
    private void Jump()
    {
        if (numJumps > 0 && !crouched)
        {
            numJumps -= 1;
            rB.AddForce(transform.up * jumpPower);
            movePower = airMovePower;
            rB.drag = 0.0f;
            grounded = false;
        }
    }
    private bool coverCheck()
    {
        if (!relocate)
        {
            if (crouched)
            {
                if ((currentCover.transform.position.z < transform.position.z && enemyPlayer.transform.position.z < transform.position.z) || (currentCover.transform.position.z > transform.position.z && enemyPlayer.transform.position.z > transform.position.z))
                {
                    return false;
                }
            }
        }
        return true;
    }
    private void Decrouch()
    {
        if (hp > 0)
        {
            anim.SetBool("Crouching", false);
            box.size = new Vector3(0.035f, 0.06f, 0.035f);
            box.center = new Vector3(0, 0.03f, 0.0f);
            crouched = false;
        }
    }
    private void Crouch()
    {
        if (hp > 0)
        {
            anim.SetBool("Crouching", true);
            numOccurances += 1;
            box.size = new Vector3(0.035f, 0.035f, 0.035f);
            box.center = new Vector3(0, 0.0175f, 0.0f);
            crouched = true;
        }
    }
    public int getHP()
    {
        return hp;
    }
    public void setHP()
    {
        anim.SetBool("Dead", false);
        hp = 100;
        box.size = new Vector3(0.035f, 0.06f, 0.035f);
        box.center = new Vector3(0, 0.03f, 0.0f);
        InvokeRepeating("enemyCheck", 0.2f, 0.2f);
        InvokeRepeating("decide", decisionTime, decisionTime);
    }
}
