using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public string enemyName;
    [Header("Movement")]
    public float moveSpeed;

    [Header("Health")]
    public int health;
    public int maxHealth = 100;

    [Header("Fire")]
    public int fireDamage = 5;
    public int fireHitSpeed = 1;
    public float burnTime = 3;
    public bool isBurning;
    public GameObject fire;
    float curBurnTime;
    float curFireHitTime;

    [Header("Force")]
    public float force;
    public float stunTime = 1;

    [Header("Energy")]
    public float arcRange = 5;
    public int arcDamage = 5;

    Material material;
    float colTime;

    [Header("AI")]
    public NavMeshAgent aI;
    public GameObject player;

    void Awake()
    {
        health = maxHealth;
        material = GetComponent<MeshRenderer>().materials[0];
        aI = GetComponent<NavMeshAgent>();
        fire = Instantiate(Resources.Load<GameObject>("Fire"), transform);
    }

    void Start()
    {
        //player = GameManager.instance.player;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        //Flashing red timer
        material.color = Color.red;
        colTime = .15f;
        if (health <= 0) { Die(); }

    }

    public virtual void Update()
    {
        aI.speed = moveSpeed;

        if (isBurning) { Fire(); }
        if (health > maxHealth) { health = maxHealth; }

        //Flashing red countdown
        if (colTime <= 0) { colTime = 0; material.color = Color.white; }
        else { colTime -= Time.deltaTime; }

        //ACTIVATE FIRE!!!!
        fire.SetActive(isBurning);

    }

    #region Effects
    /*   
    public void ApplyEffect(Spells effect)
    {
        switch (effect)
        {
            case Spells.Fire:
                curBurnTime = 0;
                isBurning = true;
                break;
            case Spells.Force:
                StartCoroutine(ApplyForce());
                break;
            case Spells.Energy:
                Electrocute();
                break;
            default:
                break;
        }
    }
    */
    #endregion

    public void Fire()
    {
        curBurnTime += Time.deltaTime;
        if (curBurnTime >= burnTime)
        {
            isBurning = false;
            curBurnTime = burnTime;
        }

        curFireHitTime += Time.deltaTime;
        if (curFireHitTime >= fireHitSpeed)
        {
            curFireHitTime = 0;

            TakeDamage(5);
        }
    }

    public IEnumerator ApplyForce()
    {
        aI.enabled = false;
        Rigidbody rigidbody;
        rigidbody = gameObject.AddComponent<Rigidbody>();
        rigidbody.freezeRotation = true;
        rigidbody.AddForce((player.transform.position - transform.position).normalized * -force, ForceMode.Impulse);
        yield return new WaitForSeconds(stunTime);
        Destroy(rigidbody);
        aI.enabled = true;

    }

    public void Electrocute()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, arcRange);
        int i = 0;
        while (i < hitColliders.Length)
        {
            if (hitColliders[i].GetComponent<Enemy>())
            {
                hitColliders[i].GetComponent<Enemy>().TakeDamage(arcDamage);
            }
            i++;
        }
    }

    //private void OnBecameVisible()
    //{ 
    //    GameManager.instance.topics.Add(new Topic(enemyName,TopicTypes.Creatures));
    //}

    void Die()
    {
        Destroy(gameObject);
    }


}
