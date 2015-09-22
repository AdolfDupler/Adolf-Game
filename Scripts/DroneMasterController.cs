using UnityEngine;
using System.Collections;

public class DroneMasterController : MonoBehaviour {
    //For destructablity, for use with .Hit(float)
    public float health; //Drone health, self explanatory
    //For movement
    public Vector3 targetPosition; //Position drone moves to, is called in update;
    public float speed; //self explanatory
    private float angle; //turning internal float
    private float targetAngle; //ditto
    public float accuracy; //How close a drone needs to be to target pos until stop
    private Rigidbody rb; //reference to RigidBody
    public float turnspeed; // SE (Stands for self explanatory)
    //For Selection
    public bool isSelected = false; //SE
    // For Drone related Components
    public WeaponController[] weapons;
    public GameObject utility;
    public bool debugFire;


    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void Update () {

        SelectionUpdate();
        FireUpdate();
	}
    void FixedUpdate()
    {
        MovementUpdate(targetPosition); //Look, I'm not saying this implementation isn't weird, but just go with it
    }
    public void Hit(float damage)
    {
        Debug.Log(damage.ToString());

        health = health - damage;
        if (health <= 0.0f)
        {
            Destroy(this.gameObject);
        }


    }
    public void MovementUpdate(Vector3 point)
    {
        Vector3 target = point - transform.position;
        if (target.magnitude <= accuracy)
        {
            rb.velocity = Vector3.zero;
        }
        else
        {
            targetAngle = Mathf.Atan2(target.x, target.z) * (180 / Mathf.PI);
            TurnUpdate();
            rb.velocity = transform.forward * speed;
        }
    }
    private void TurnUpdate()
    {
        angle = Mathf.MoveTowardsAngle(angle, targetAngle, Time.deltaTime * turnspeed);
        rb.MoveRotation(Quaternion.Euler(0.0f, angle, 0.0f));
    }
    private void SelectionUpdate()
    {
        if (Mathf.Abs(transform.position.x - Camera.main.transform.position.x) < 5 &&
            Mathf.Abs(transform.position.z - Camera.main.transform.position.z - Camera.main.transform.position.y) < 5)
        {
            if (!isSelected)
            {
                //rend.material.SetColor("_Color", Color.green);
            }
            if (Input.GetButton("Fire1"))//A
            {
                //rend.material.SetColor("_Color", Color.red);
                isSelected = true;
            }
        }

        if (Input.GetButton("Fire2") && isSelected)//B,move towards
        {
           targetPosition = new Vector3(Camera.main.transform.position.x, 0.5f, Camera.main.transform.position.z+Camera.main.position.transform.y);
        }
        if (Input.GetButton("Fire3"))//X, unselect
        {

            isSelected = false;
        }
    }
    void FireUpdate()
    {
        if (debugFire)
            foreach (WeaponController weapon in weapons)
            {
            weapon.Fire();
            }
    }
}

