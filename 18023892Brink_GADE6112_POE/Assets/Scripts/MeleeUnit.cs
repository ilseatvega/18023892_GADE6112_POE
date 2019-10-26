using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class MeleeUnit : Unit
{
    [SerializeField]
    private bool onGround;

    //using a base constructor to access Unit's variables (properties must be accessed from MeleeUnit which is why base is used)
        //health, speed, attackrange and symbol changed to fit the MeleeUnit
        public MeleeUnit(int health, int speed, int attack, int attackRange, int team, int maxHP) : base(120, 10, attack, 1, team, 120)
    {
        //this. to refer to the instance of the variable in this class
        this.health = health;
        this.speed = speed;
        this.attack = attack;
        this.attackRange = attackRange;
        this.team = team;
    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Floor")
        {
            onGround = true;
        }
    }

    public override void Move()
    {
        //set speed to -10f
        speed = -10f;
        attackRange = 3f;
        //creating a list of enemies
        List<GameObject> enemies = new List<GameObject>();
        
        //if the game object is not in in gold team (same team) then add to enemy list
        //will cycle through all 3 ifs until it finds one that is true
        if (!gameObject.CompareTag("Gold Team"))
        {
            foreach (GameObject go in GameObject.FindGameObjectsWithTag("Gold Team"))
            {
                enemies.Add(go);
            }
        }
        //same as above
        if (!gameObject.CompareTag("Green Team"))
        {
            foreach (GameObject go in GameObject.FindGameObjectsWithTag("Green Team"))
            {
                enemies.Add(go);
            }
        }
        //same as above
        if (!gameObject.CompareTag("Wizards"))
        {
            foreach (GameObject go in GameObject.FindGameObjectsWithTag("Wizards"))
            {
                enemies.Add(go);
            }
        }
        
        //finding closest unit pos
        GameObject closest = gameObject;
        float closestDistance = float.MaxValue;
        
        foreach (GameObject go in enemies)
        {
            if (Vector3.Distance(gameObject.transform.position, go.transform.position) < closestDistance)
            {
                closestDistance = Vector3.Distance(gameObject.transform.position, go.transform.position);
                closest = go;
            }
        }

        if (!closest.Equals(gameObject))
        {
            transform.LookAt(closest.transform.position);
            if (closestDistance > attackRange)
            {
                transform.Translate(transform.forward * speed * Time.deltaTime);
            }
        }
    }
    public override void Combat()
    {

    }
    //determining if within range by using raytracing
    public override bool WithinRange()
    {
        return false;
    }
    public override void UnitDeath()
    {
        Destroy(gameObject);
    }

    //updates every frame
    private void Update()
    {
        GameObject floor = GameObject.FindGameObjectWithTag("Floor");
        if (onGround)
        {
            Move();
        }   
    }
    //public override GameObject ClosestUnitPos(string target)
    //{
    //        GameObject[] gos = GameObject.FindGameObjectsWithTag(target);

    //        GameObject closest = null;
    //        float distance = Mathf.Infinity;
    //        Vector3 position = transform.position;
    //        foreach (GameObject go in gos)
    //        {
    //            Vector3 diff = go.transform.position - position;
    //            float curDistance = diff.sqrMagnitude;

    //            if (curDistance < distance)
    //            {
    //                closest = go;
    //                distance = curDistance;
    //            }
    //        }

    //        return closest;
    //}
}
