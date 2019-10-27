using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Building : MonoBehaviour
{//declaring the protected variables
    protected float health;
    protected float maxHealth;
    protected float team;

    //constructor that receives parameteres for all the above class variables (except maxhealth)
    //setting the protected ints that were declared to the parameters of this Building method
    public Building(float health, float team)
    {
        //this. to refer to the instance of the variable in this class
        this.health = health;
        this.team = team;
    }

    //the abstract methods that will later be overridden
    public abstract void Destruction();
    //the abstract Save() method
    //public abstract void Save();

    //get setters -  wouldnt work unless i put them in the Building class
    //didnt use set since the value has been set and wont change (see constructor base at the top)
    public float maxHP { get { return health; } }
    
    public float Health { get { return health; } set { health = value; } }

    public float Team { get { return team; } set { team = value; } }
}
