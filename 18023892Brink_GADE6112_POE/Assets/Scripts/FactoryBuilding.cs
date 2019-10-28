using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//added for healthbar
using UnityEngine.UI;

public class FactoryBuilding : Building
{
    //variables specific to Resource Building
    //float unitType;
    //float speed = 5;
    //cost
    float cost = 50;
    //the healthbar slider
    public Slider healthbar;


    //constructor that receives parameteres for all the above class variables (except maxhealth)
    //setting the protected ints that were declared to the parameters of this ResourceBuilding method
    public FactoryBuilding(float health, float team, /*float unitType,*/ float maxHP) : base(200, team)
    {
        //this. to refer to the instance of the variable in this class
        this.health = health;
        this.team = team;
        //this.unitType = unitType;
    }

    // Start is called before the first frame update
    void Start()
    {
        maxHealth = 200;
        health = maxHealth;
        healthbar.value = 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

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
            Destruction();
        }
    }

    //overriding the abstract methods created in Building
    public override void Destruction()
    {
        //destroy gameobj
        Destroy(gameObject);
    }

    //check for closest building that has resources required
    //public bool closestResources(Building[] buildings)
    //{
    //}


    //get setters -  wouldnt work unless i put them in the Building class

    public float maxHP { get { return health; } }

    //didnt use set since the value has been set and wont change (see constructor base at the top)
    public float Health { get { return health; } }

    public float Team { get { return team; } set { team = value; } }
}
