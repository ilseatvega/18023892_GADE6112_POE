using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Unit : MonoBehaviour
{
    //declaring the field protected definitions
    protected float health;
    protected float maxHealth;
    protected float speed;
    protected float attack;
    protected float attackRange;
    protected float team;

    //constructor that receives parameteres for all the above class variables
    //setting the protected ints that were declared to the parameters of this Unit method
    public Unit(float health, float speed, float attack, float attackRange, float team, float maxHealth)
    {
        //this. to refer to the instance of the variable in this class
        this.health = health;
        this.speed = speed;
        this.attack = attack;
        this.attackRange = attackRange;
        this.team = team;
    }

    //declaring public abstracts
    public abstract void Move();
    public abstract void Damage(float damAmount);
    public abstract void Combat(GameObject enemy);
    public abstract void UnitDeath();

    //declaring new building range and closest building pos to att buildings
    //public abstract bool BuildingRange(Building enemy);
    //public abstract Building ClosestBuilding(Building[] building);
    //public abstract void BuildingCombat(Building enemy);
    
    //the abstract Save() method
    //public abstract void Save();


    //putting the accessors in the Unit class so I can get my map update to work

    public float maxHP { get { return health; } }

    //gets health and sets it to call death if health is 0
    public float Health { get { return health; } set { if (value < 0) { health = 0; } else { health = value; } } }

    public float Attack { get { return attack; } set { attack = value; } }

    //didnt use set since the value has been set and wont change (see constructor base at the top)
    public float Speed { get { return speed; } }

    public float Range { get { return attackRange; } }

    public float Team { get { return team; } set { team = value; } }
}
