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
    float cost;
    //the healthbar slider
    public Slider healthbar;
    [SerializeField]
    GameObject resourceB;
    ResourceBuilding resource;
    float getRes;

    GameObject goldr = GameObject.Find("Gold Ranged Spawn");
    GameObject goldm = GameObject.Find("Gold Melee Spawn");
    GameObject greenm = GameObject.Find("Green Melee Spawn");
    GameObject greenr = GameObject.Find("Green Ranged Spawn");

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
        goldm.GetComponent<GoldMelee>().enabled = false;
        greenm.GetComponent<GreenMelee>().enabled = false;
        goldr.GetComponent<GoldRanged>().enabled = false;
        greenr.GetComponent<GreenRanged>().enabled = false;

        maxHealth = 200;
        health = maxHealth;
        healthbar.value = 1;
        cost = 200;
        resource = resourceB.GetComponent<ResourceBuilding>();
        getRes = resource.resGen;
    }

    // Update is called once per frame
    void Update()
    {
        if (getRes >= cost)
        {
            goldm.gameObject.GetComponent<GoldMelee>().enabled = true;
            greenm.GetComponent<GreenMelee>().enabled = true;
            goldr.GetComponent<GoldRanged>().enabled = true;
            greenr.GetComponent<GreenRanged>().enabled = true;

            getRes -= cost;
        }
        else
        {
            goldm.GetComponent<GoldMelee>().enabled = false;
            greenm.GetComponent<GreenMelee>().enabled = false;
            goldr.GetComponent<GoldRanged>().enabled = false;
            greenr.GetComponent<GreenRanged>().enabled = false;
        }
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
    
    //get setters -  wouldnt work unless i put them in the Building class

    public float maxHP { get { return health; } }

    //didnt use set since the value has been set and wont change (see constructor base at the top)
    public float Health { get { return health; } }

    public float Team { get { return team; } set { team = value; } }
}
