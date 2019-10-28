using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//added for healthbar
using UnityEngine.UI;

public class WizardUnit : Unit
{
    //serialise to test if it changes
    //bool the determine whether unit is on ground
    [SerializeField]
    private bool onGround;
    //the healthbar slider
    public Slider healthbar;
    //list of game obj in radius of wizard's attack
    public List<GameObject> inRange = new List<GameObject>();

    //using a base constructor to access Unit's variables (properties must be accessed from WizardUnit which is why base is used)
    public WizardUnit(int health, int speed, int attack, int attackRange, int team, int maxHP) : base(60, 10, 5, 5, team, 60)
    {
        //this. to refer to the instance of the variable in this class
        this.health = health;
        this.speed = speed;
        this.attack = attack;
        this.attackRange = attackRange;
        this.team = team;
    }
    //initialise when game starts
    private void Start()
    {
        maxHealth = 60;
        health = maxHealth;
        Attack = 5;
        healthbar.value = 1;
    }
    //oncollionenter - when this unit collides with an object (floor)
    private void OnCollisionEnter(Collision col)
    {
        //if gameobject collides with floor
        if (col.gameObject.tag == "Floor")
        {
            //they are on ground
            onGround = true;
        }
    }

    //exit for when they stop touching the floor - otherwise they attempt to move in the air
    private void OnCollisionExit(Collision col)
    {
        if (col.gameObject.tag == "Floor")
        {
            onGround = false;
        }
    }

    //method for the wizard aoe attack
    public void WizCombat()
    {
        //looping through each gameobject in the list
        foreach (GameObject go in inRange)
        {
            //if go not empty
            if (go != null)
            {
                //perform combat on all units in list
                Combat(go);
            }
        }
    }

    //MOVE
    public override void Move()
    {
        //set speed to 10f
        speed = 10f;
        //attack range of the meleeunit
        attackRange = 40f;
        //if health less than 50%, run away
        if (healthbar.value <= 0.5)
        {
            //having them move back
            transform.Translate(Vector3.back * speed * Time.deltaTime);
        }
        //creating a list of enemies to store units not on our team
        List<GameObject> enemies = new List<GameObject>();

            //if the game object is not in in same team (gold in this case) then add to enemy list
            //will cycle through all 3 ifs until it finds one that is true
            if (!gameObject.CompareTag("Gold Team"))
            {
                foreach (GameObject u in GameObject.FindGameObjectsWithTag("Gold Team"))
                {
                    enemies.Add(u);
                }
            }
            //same as above
            if (!gameObject.CompareTag("Green Team"))
            {
                foreach (GameObject u in GameObject.FindGameObjectsWithTag("Green Team"))
                {
                    enemies.Add(u);
                }
            }
            //same as above
            if (!gameObject.CompareTag("Wizards"))
            {
                foreach (GameObject u in GameObject.FindGameObjectsWithTag("Wizards"))
                {
                    enemies.Add(u);
                }
            }

            //finding closest unit pos
            GameObject closest = gameObject;
            float closestDistance = float.MaxValue;

            //foreach gamobj in the enemy list
            foreach (GameObject u in enemies)
            {
                //if distance is less than the closest dist
                if (Vector3.Distance(gameObject.transform.position, u.transform.position) < closestDistance)
                {
                //then this is the closest distance
                    closestDistance = Vector3.Distance(gameObject.transform.position, u.transform.position);
                    closest = u;
                }
            }

            //if the closests isnt the game obj itself
            if (!closest.Equals(gameObject))
            {
                //look at the closest enemy
                transform.LookAt(closest.transform.position);
                //if they are not within att range
                if (closestDistance > attackRange)
                {
                    //having them move forward
                    transform.Translate(Vector3.forward * speed * Time.deltaTime);
                }
                //if they are within attack range
                else if (closestDistance <= attackRange)
                {
                    //attaaack!
                    WizCombat();
                }
            }
        }
    //
    public override void Damage(float damAmount)
    {
        //health decreases according to damage taken
        health -= damAmount;

        //if healthbar not null
        if (healthbar != null)
        {
            //healthbar's calue is the health divided by the max
            healthbar.value = health / maxHealth;
        }
        //if healthbar reaches 0, destroy this unit
        if (healthbar.value == 0)
        {
            UnitDeath();
        }
    }
    //COMBAT
    public override void Combat(GameObject enemy)
    {
        Unit u = enemy.GetComponent<Unit>();
        //attack unit
        u.Damage(attack * Time.deltaTime);
    }

    public override void UnitDeath()
    {
        //destroy gameobj
        Destroy(gameObject);
    }

    //UPDATES every frame
    private void Update()
    {
        //if unit is on the ground, they can move towards their closest enemy
        if (onGround == true)
        {
            Move();
        }
    }

    //GETTERS AND SETTERS
    public float maxHP { get { return health; } }

    //gets health and sets it to call death if health is 0
    public float Health { get { return health; } set { if (value < 0) { health = 0; this.UnitDeath(); } else { health = value; } } }

    public float Attack { get { return attack; } set { attack = value; } }

    //didnt use set since the value has been set and wont change (see constructor base at the top)
    public float Speed { get { return speed; } }

    public float Range { get { return attackRange; } }

    public float Team { get { return team; } set { team = value; } }
}
