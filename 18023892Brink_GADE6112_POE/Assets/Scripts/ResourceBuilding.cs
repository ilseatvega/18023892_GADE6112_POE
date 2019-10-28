using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//added for healthbar
using UnityEngine.UI;

public class ResourceBuilding : Building
{
    //the healthbar slider
    public Slider healthbar;
    public Text resUpdate;

    //variables specific to Resource Building
    float resGen;
    float resGenRound = 20;
    float resRemaining = 1000;

    // Start is called before the first frame update
    void Start()
    {
        maxHealth = 200;
        health = maxHealth;
        healthbar.value = 1;
    }

    //constructor that receives parameteres for all the above class variables (except maxhealth)
    //setting the protected ints that were declared to the parameters of this ResourceBuilding method
    public ResourceBuilding(float health, float team, float resremaining, float maxHP) : base(200, team)
    {
        //this. to refer to the instance of the variable in this class
        this.health = health;
        this.team = team;
        
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

    public void resManagement()
    {
        //if resources remaining minus the resources that are being generated per round is more than 0
        if (resRemaining - resGenRound >= 0)
        {
            //there is still resources left
            //calculate new resremaining amount and resgen amount
            resGen += resGenRound;
            resRemaining -= resGenRound;
        }
        else
        {
            //no resources left
            resGen += resRemaining;
            resRemaining = 0;
        }
        Debug.Log(resRemaining);
        resUpdate.text = "Resources Remaining: " + "\n" + resRemaining;
    }

    // Update is called once per frame
    void Update()
    {
        resManagement();
    }

    //get setters -  wouldnt work unless i put them in the Building class

    public float maxHP { get { return health; } }

    //didnt use set since the value has been set and wont change (see constructor base at the top)
    public float Health { get { return health; } }

    public float Team { get { return team; } set { team = value; } }
    
    //the amount of res that has been mined
    public float minedRes { get { return resGen; } set { if (value < 0) { resGen = 0; } else { resGen = value; } } }
}
