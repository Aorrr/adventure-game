using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class SkullKing : Enemy
{
    [SerializeField] Player player;
    [SerializeField] FireSkull skull;
    [SerializeField] GameObject skullKingPath;
    [SerializeField] float moveSpeed = 2f;
    [SerializeField] float SpawnSkullCD = 10f;
    [SerializeField] Fire iceFire;
    [SerializeField] kingScream scream;
    [SerializeField] summon icyPortal;


    [SerializeField] GameObject BossHealthBar;

    Sanity sanity;
    bool isStatic = true;
    bool SummonSkull = true;
    List<Transform> wayPoints;
    bool canMove = true;
    //Rigidbody2D body;
    Animator animator;
    EnemyBody enemyBody;
    Light2D bossLight;
    bool ultimateSummon = false;

    // index for controlling king movement
    int nextPosIndex = 5;
    int currentPosIndex = 5;


    // For the sprint of fire skull king
    [SerializeField] float sprintCD = 3f;
    float sinceLastSprint = 0f;
    Vector2 targetPosition;
    float movementThisFrame;
    bool couldDamage = false;
    bool attacking = false;
    bool ifPrep = false;

    // timer for fire attack;
    float fireCD = 6f;
    float sinceLastFireAttack = 0f;

    bool ifShouldStopFire = true;

    bool songPlayed = false;
    bool fixDirection = false;

    // Start is called before the first frame update
    void Start()
    {
        PrepareStats();
        sanity = FindObjectOfType<Sanity>();
        enemyBody = GetComponentInChildren<EnemyBody>();
        animator = GetComponent<Animator>();
        wayPoints = new List<Transform>();
        //body = GetComponent<Rigidbody2D>();
        foreach(Transform child in skullKingPath.transform)
        {
            wayPoints.Add(child);
        }
        bossLight = GetComponent<Light2D>();
        bossLight.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(isStatic) { return; }
        if (GetHealthPercentage() >= 0.35)
            {
            if (canMove)
            {
                MoveToNextLocation();
            }

            if (IfRage())
            {
                if (SummonSkull)
                {
                    nextPosIndex = 0;
                    if (currentPosIndex == 0)
                    {
                        canMove = false;
                        StartCoroutine(StayForSeconds(4));
                        ArmorUp();
                        StartCoroutine(SummonSkulls());
                    }  
                    
                }

                if(!songPlayed)
                {
                    FindObjectOfType<LevelMusicPlayer>().SwitchSong();
                    songPlayed  = true;
                    BossHealthBar.SetActive(true);
                }
            } 
            } else if(GetHealthPercentage() > 0)
            {
                if(SummonSkull)
            {
                StartCoroutine(StayForSeconds(3));
                StartCoroutine(SummonSkulls());
                ultimateSummon = true;
                FindObjectOfType<LevelMusicPlayer>().FinalStage();
                SpawnSkullCD += 5;
                ArmorUp();
            }   else
            {
                nextPosIndex = 5;
                if (currentPosIndex != 5)
                {
                    MoveToNextLocation();
                }
                else
                {
                    Sprint();
                }
            }
            }
        UpdateLightStatus();
        FireAttack();
        if(!fixDirection)
        {
            CorrectRotation();
        }
    }

    IEnumerator SummonSkulls()
    {
        SummonSkull = false;

        int minSummon = 1 + ((int)((1 - GetHealthPercentage()) * 100)) / 10;
        minSummon = Mathf.Max(6, minSummon);

        int num = Random.Range(minSummon, wayPoints.Count);
        List<int> indexes = new List<int>();
        var numberList = Enumerable.Range(0, wayPoints.Count).ToList();

        scream.StartScream();

        while (indexes.Count <= num)
        {
            int index = Random.Range(0, numberList.Count);
            indexes.Add(numberList[index]);
            numberList.Remove(index);
   
        }

        foreach(int index in indexes)
        {
            Instantiate(icyPortal, wayPoints[index].position, Quaternion.identity);
        }

        yield return new WaitForSeconds(SpawnSkullCD);
        SummonSkull = true;
    }

    IEnumerator StayForSeconds(int interval)
    {
        canMove = false;
        OnFire();
        yield return new WaitForSeconds(interval);
        canMove = true;
        Extinguish();
    }

    public void MoveToNextLocation()
    {
        targetPosition = wayPoints[nextPosIndex].transform.position;
        movementThisFrame = moveSpeed * Time.deltaTime;

        if (transform.position.x == targetPosition.x && transform.position.y == targetPosition.y)
        {
            currentPosIndex = nextPosIndex;
            nextPosIndex = Random.Range(0, 5);
            if (nextPosIndex == currentPosIndex)
            {
                nextPosIndex = (currentPosIndex + 1) % 5;
            }
        }
        else
        {
            transform.position = Vector2.MoveTowards
                (transform.position, targetPosition, movementThisFrame);
        }
    }

    public void UpdateLightStatus()
    {
        bossLight.enabled = myAnimator.GetBool("Fire");
    }
    
    public void Sprint()
    {
        sinceLastSprint += Time.deltaTime;
        if (sinceLastSprint < sprintCD) { Extinguish();  return; }

        if(!attacking && !ifPrep)
        {
            scream.StartScream();
            ArmorUp();
            OnFire();

            StartCoroutine(PrepareFireAttack());
        } else if(attacking)
        {
            transform.position = Vector2.MoveTowards
            (transform.position, targetPosition, movementThisFrame);
            if (enemyBody.GetComponent<BoxCollider2D>().IsTouchingLayers(LayerMask.GetMask("Player")))
            {
                player.Hurt(GetDamage(), "MAGICAL");
  
                couldDamage = false;
                attacking = false;
                Extinguish();
                sinceLastSprint = 0;
            }
            else if (transform.position.x == targetPosition.x && transform.position.y == targetPosition.y)
            {
                attacking = false;
                couldDamage = false;
                Extinguish();
                sinceLastSprint = 0;
            }
        } 

    }


    IEnumerator PrepareFireAttack()
    {
        ifPrep = true;
        yield return new WaitForSeconds(2);
        targetPosition = player.transform.position;
        movementThisFrame = moveSpeed * 4 * Time.deltaTime;
        couldDamage = true;
        attacking = true;
        ifPrep = false;
    }

    public void ToggleDamageStatus(bool status)
    {
        couldDamage = status;
    }

    private void FireAttack()
    {
        sinceLastFireAttack += Time.deltaTime;

        if(sinceLastFireAttack > fireCD - 1.5)
        {
            GetComponent<SpriteRenderer>().color = new Color(62 / 255f, 133 / 255f, 255 / 255f);
        }

        if(fireCD - sinceLastFireAttack < 0.6)
        {
            if(!myAnimator.GetBool("Fire"))
            {
                myAnimator.SetBool("Fire", true);
                ifShouldStopFire = true;

                fixDirection = true;
                StartCoroutine(BreathFixDirection());
            }
        }

        if(sinceLastFireAttack > fireCD)
        {
            if(ifShouldStopFire)
            {
                Extinguish();
            }
            iceFire.StartFire();
            sinceLastFireAttack = 0;
            GetComponent<SpriteRenderer>().color = Color.white;
        }
    }

    IEnumerator BreathFixDirection()
    {
        yield return new WaitForSeconds(1);
        fixDirection = false;
    }

    private void CorrectRotation()
    {
        if(player.transform.position.x > transform.position.x)
        {
            transform.localScale = new Vector2(-Mathf.Abs(transform.localScale.x)
                , transform.localScale.y);
        } else
        {
            transform.localScale = new Vector2(Mathf.Abs(transform.localScale.x)
    , transform.localScale.y);
        }
    }

    public override void PlayerDetected()
    {
        ToggleRage(true);
    }

    public override void PlayerNotDetected()
    {
        base.PlayerNotDetected();
    }
    
    public override void Hurt(int damage, string type, string method)
    {
        base.Hurt(damage, type, method);
    }

    public void OnFire()
    {
        myAnimator.SetBool("Fire", true);
        ifShouldStopFire = false;
    }

    public void Extinguish()
    {
        myAnimator.SetBool("Fire", false);
    }

    public void ArmorUp()
    {

        int num = FindObjectsOfType<FireSkull>().Count();

        int amrInc = num * 2;
        int mrInc = num * 1;
        int dmgInc = num;
        armour += amrInc;
        magicalResistance += mrInc;
        Damage += num/2;

        popUpObject.SetText("ARMOR + " + amrInc);

        GameObject popUpAmr = Instantiate<GameObject>
        (popUpObject.gameObject, transform.position + new Vector3(0f, 2f, 0f), Quaternion.identity);


        popUpObject.SetText("MAGICAL RESISTANCE + " + mrInc);

        GameObject popUpMr = Instantiate<GameObject>
        (popUpObject.gameObject, transform.position, Quaternion.identity);

        Destroy(popUpAmr, 2f);
        Destroy(popUpMr, 2f);
    }

    public int GetDmg()
    {
        return Damage;
    }

    public void ToggleStaticStatus()
    {
        isStatic = !isStatic;
    }

    public void Appear()
    {
        GetComponent<Renderer>().enabled = true;
        myAnimator.SetTrigger("Appear");
    }
}
