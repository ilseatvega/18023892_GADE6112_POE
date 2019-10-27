using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FactoryBuilding : Building
{
    //variables specific to Resource Building
    float unitType;
    float speed = 5;
    //cost
    float cost = 20;

    //random to be used throughout the class
    Random rnd = new Random();

    //constructor that receives parameteres for all the above class variables (except maxhealth)
    //setting the protected ints that were declared to the parameters of this ResourceBuilding method
    public FactoryBuilding(float health, float team, float unitType, float maxHP) : base(200, team)
    {
        //this. to refer to the instance of the variable in this class
        this.health = health;
        this.team = team;
        this.unitType = unitType;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //spawnDelay += Time.deltaTime;
        //if (spawnDelay >= 5)
        //{
        //    Spawn();
        //}
    }

    //overriding the abstract methods created in Building
    public override void Destruction()
    {

    }

    //spawn random units
    public void Spawn()
    {
        //Instantiate(greenPrefab, new Vector3(), Quaternion.identity);
        ////setting the delay back to 0
        //spawnDelay = 0;
    }

    //check for closest building that has resources required
    //public bool closestResources(Building[] buildings)
    //{
    //}

    //get accessor for production speed
    public float Speed { get { return speed; } }

    //get setters -  wouldnt work unless i put them in the Building class

    public float maxHP { get { return health; } }

    //didnt use set since the value has been set and wont change (see constructor base at the top)
    public float Health { get { return health; } }

    public float Team { get { return team; } set { team = value; } }
}
