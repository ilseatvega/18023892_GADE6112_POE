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
    public float resGen;
    public float resGenRound;
    public float resRemaining;
    float cost;
    //spawn delay
    public float resDelay;

    //GameObject goldr = GameObject.FindGameObjectWithTag("GoldR");
    //GameObject goldm = GameObject.FindGameObjectWithTag("GoldM");
    //GameObject greenm = GameObject.FindGameObjectWithTag("GreenR");
    //GameObject greenr = GameObject.FindGameObjectWithTag("GreenM");

    // Start is called before the first frame update
    void Start()
    {
        //GameObject goldr = GameObject.FindGameObjectWithTag("GoldR");
        //GameObject goldm = GameObject.FindGameObjectWithTag("GoldM");
        //GameObject greenm = GameObject.FindGameObjectWithTag("GreenR");
        //GameObject greenr = GameObject.FindGameObjectWithTag("GreenM");

        //goldm.GetComponent<GoldMelee>().enabled = false;
        //greenm.GetComponent<GreenMelee>().enabled = false;
        //goldr.GetComponent<GoldRanged>().enabled = false;
        //greenr.GetComponent<GreenRanged>().enabled = false;

        maxHealth = 100;
        health = maxHealth;
        healthbar.value = 1;
        resGenRound = 20;
        resRemaining = 1000;
        cost = 200;
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
        resDelay += Time.deltaTime;
        //updating resources every second
        if (resDelay >= 1)
        {
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
            resDelay = 0;
        }
            resUpdate.text = "Resources Gathered: " + "\n" + resGen;

            //if (resGen >= cost)
            //{
            //    if (goldm !=null && goldr != null && greenm !=null && greenr != null)
            //    {
            //        goldm.GetComponent<GoldMelee>().enabled = true;
            //        greenm.GetComponent<GreenMelee>().enabled = true;
            //        goldr.GetComponent<GoldRanged>().enabled = true;
            //        greenr.GetComponent<GreenRanged>().enabled = true;
            //        resGen -= cost;
            //    }
            //}
            //else
            //{
            //    goldm.GetComponent<GoldMelee>().enabled = false;
            //    greenm.GetComponent<GreenMelee>().enabled = false;
            //    goldr.GetComponent<GoldRanged>().enabled = false;
            //    greenr.GetComponent<GreenRanged>().enabled = false;
            //}
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
