using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class MultiplayerPlayerController : NetworkBehaviour
{
    public GameObject gunReference;
    public GameObject Bullet;
    public GameObject Bs;
    private float inputMovementValue;
    private Rigidbody rB;
    private float spd = 2.0f;
    private bool crouched;
    public BoxCollider box;
    public int numJumps = 1;
    private int jumpPower = 110;
    private int maxMovePower = 300;
    private int airMovePower = 100;
    private int movePower = 300;
    private bool readyToFire = true;
    private Quaternion q;
    private Vector3 mPos;
    public Animator anim;
    private bool grounded = true;
    private int maxHP = 100;
    [SyncVar]
    private int hp = 100;
    private float rof = 0.25f;
    public AudioSource aud;
    public AudioSource hurt1;
    public AudioSource hurt2;
    public AudioSource hurt3;
    public AudioSource bombHurt;
    public AudioSource taunt1;
    public AudioSource taunt2;
    public AudioSource tauntLowHp;
    public GameObject muzzleFlash;
    public GameObject DeathImage;

    private void Awake()
    {
        rB = GetComponent<Rigidbody>();
        crouched = false;
        InvokeRepeating("BugLog", 1.0f, 1.0f);
    }
	// Use this for initialization
	void Start () 
    {
        GameObject s = GameObject.FindGameObjectWithTag("SkirmishScoreKeeper");
        s.GetComponent<MultiplayerScoreKeepScript>().Begin();
        hp = 100;
        GameObject cam = GameObject.FindGameObjectWithTag("MainCamera");
        if (isLocalPlayer)
        {
            DeathImage = GameObject.FindGameObjectWithTag("Deathscreen");
            cam.GetComponent<MultiPlayerCameraFollow>().SetPlayerFollow(gameObject);
            if (DeathImage != null)
            DeathImage.SetActive(false);
        }
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (hp > 0 && isLocalPlayer)
        {
            inputMovementValue = Input.GetAxis("Horizontal");
            if (inputMovementValue != 0 && !crouched)
            {
                anim.SetBool("Moving", true);
                if (inputMovementValue > 0)
                {
                    transform.localEulerAngles = new Vector3(0.0f, 0.0f, 0.0f);
                }
                else
                {
                    transform.localEulerAngles = new Vector3(0.0f, 180.0f, 0.0f);
                }
            }
            else
            {
                anim.SetBool("Moving", false);
                inputMovementValue = 0;
                if (!crouched)
                    gunReference.transform.localEulerAngles = new Vector3(20.6f, 0.0f, 270.0f);
                else
                    gunReference.transform.localEulerAngles = new Vector3(0.0f, 0.0f, 270.0f);
            }
        }
	}
    void BugLog()
    {
    }
    public void KilledAnEnemy()
    {
        if (hp < 34)
        {
            tauntLowHp.Play();
        }
        else
        {
            float rand = 2 * Random.value;
            if (rand < 1)
            {
                taunt1.Play();
            }
            else
            {
                taunt2.Play();
            }
        }
    }
    void FixedUpdate()
    {
        Shoot();
        if (hp > 0 && isLocalPlayer)
        {
            Move();
            Jump();
            Crouch();
        }
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
        hp -= x;
        if (hp <= 0)
        {
            anim.SetBool("Dead", true);
            hp = 0;
            if (isLocalPlayer && DeathImage != null)
                DeathImage.SetActive(true);
        }
        else
        {
            float rand = 4 * Random.value;
            if (rand < 1)
            {
                hurt1.Play();
            }
            else if (rand < 2)
            {
                hurt2.Play();
            }
            else if (rand < 3)
            {
                hurt3.Play();
            }
        }
    
    }
    public void DoBombDamage(int x)
    {
        hp -= x;
        if (hp <= 0)
        {
            anim.SetBool("Dead", true);
            hp = 0;
            if (isLocalPlayer && DeathImage != null)
                DeathImage.SetActive(true);
        }
        else
        {
            bombHurt.Play();
        }

    }
    private void Shoot()
    {
        if (Input.GetMouseButton(0) && readyToFire && isLocalPlayer)
        {
            CmdShoot();
        }
    }
    [Command]
    private void CmdShoot()
    {
        aud.Play();
        Vector3 aim = transform.forward;
        aim = new Vector3(aim.x, (aim.y - .1f) + (.2f * Random.value), aim.z);
        GameObject go = (GameObject)Instantiate(Bullet, Bs.transform.position, q);
        go.transform.rotation = transform.rotation;
        go.GetComponent<Rigidbody>().AddForce(aim * 500.0f);
        go.GetComponent<MultiplayerBulletScript>().setAi(false);
        GameObject troll = (GameObject)Instantiate(muzzleFlash, Bs.transform.position, q);
        troll.GetComponent<MuzzleFlashScript>().setDir(0.0f + transform.forward.z);
        troll.GetComponent<MuzzleFlashScript>().setVel(rB.velocity.y, rB.velocity.z);
        NetworkServer.Spawn(go);
        readyToFire = false;
        Invoke("Reload", rof);
    }
    private void Move()
    {
        if (Mathf.Abs(rB.velocity.z) < 3 && !crouched)
        {
            Vector3 movement = (transform.forward * Mathf.Abs(inputMovementValue) * spd * Time.deltaTime * movePower);
            rB.AddForce(movement);
        }
    }
    private void Jump()
    {
        if (numJumps > 0 && Input.GetKey(KeyCode.W) && !crouched)
        {
            numJumps -= 1;
            rB.AddForce(transform.up * jumpPower);
            movePower = airMovePower;
            rB.drag = 0.0f;
            grounded = false;
        }
    }
    public int getHP()
    {
        return hp;
    }
    public int getMaxHP()
    {
        return maxHP;
    }
    private void Decrouch()
    {
        anim.SetBool("Crouching", false);
        box.size = new Vector3(0.035f, 0.06f, 0.035f);
        box.center = new Vector3(0, 0.03f, 0.0f);
        crouched = false;
        gunReference.transform.localEulerAngles = new Vector3(350.6f, 0.0f, 270.0f);
    }
    private void Crouch()
    {
        if (grounded && Input.GetKey(KeyCode.S) && Mathf.Abs(rB.velocity.z) <= 0.05f && inputMovementValue == 0.0f)
        {
            anim.SetBool("Crouching", true);
            box.size = new Vector3(0.035f, 0.035f, 0.035f);
            box.center = new Vector3(0, 0.0175f, 0.0f);
            crouched = true;
            gunReference.transform.localEulerAngles = new Vector3(350.6f, 0.0f, 270.0f);
        }
        else
        {
            Decrouch();
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
    void Reload()
    {
        readyToFire = true;
    }
    public void setHP()
    {
        hp = 100;
        DeathImage.SetActive(false);
        anim.SetBool("Dead", false);
    }
}

