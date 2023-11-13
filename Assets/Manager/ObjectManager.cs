using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    public ObjectPool objectPool;
    //¸÷ »ý¼º
    public float mobCoolTime;
    private float applyMobCoolTime;
    private string[] mobObj;
    public GameObject mobStart1;
    public GameObject mobStart2;
    public GameObject mobStart3;
    public GameObject mobStart4;

    //ºÒµ¢ÀÌ »ý¼º
    public float fireBallCoolTime;
    private float applyFireBallCoolTime;
    public GameObject fireBall;
    public GameObject fireStart1;
    public GameObject fireStart2;
    public GameObject fireStart3;
    public GameObject fireStart4;

    //Áï»ç ¸®ÆÛ
    public float reaperCoolTime;
    private float applyReaperCoolTime;
    public GameObject reaper1;
    public GameObject reaper2;
    public GameObject reaper3;
    public GameObject reaper4;
    public GameObject reaperEffect1;
    public GameObject reaperEffect2;
    public GameObject reaperEffect3;
    public GameObject reaperEffect4;
    private bool isReaperEffect1=false;
    private bool isReaperEffect2=false;
    private bool isReaperEffect3=false;
    private bool isReaperEffect4=false;

    //º¸½º¸÷(½ºÄÌ·¹Åæ)
    public GameObject skeletonPrefab;
    public float skeletonCoolTime;
    private float applySkeletonCoolTime;


    private void Awake()
    {
        mobObj = new string[] { "Michael", "Monk", "Thief" };
    }
    // Start is called before the first frame update
    void Start()
    {

        applyReaperCoolTime = reaperCoolTime;
        applyFireBallCoolTime = fireBallCoolTime;
        applyMobCoolTime = mobCoolTime;
        applySkeletonCoolTime = skeletonCoolTime;


        reaper1.gameObject.SetActive(false);
        reaper2.gameObject.SetActive(false);
        reaper3.gameObject.SetActive(false);
        reaper4.gameObject.SetActive(false);
        reaperEffect1.gameObject.SetActive(false);
        reaperEffect2.gameObject.SetActive(false);
        reaperEffect3.gameObject.SetActive(false);
        reaperEffect4.gameObject.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        //ºÒµ¢ÀÌ
        if (applyFireBallCoolTime >= 0)
            applyFireBallCoolTime -= Time.deltaTime;
        else
        {
            FireBallFire();
            applyFireBallCoolTime = fireBallCoolTime;
        }
        //½ºÄÌ·¹Åæ
        if (applySkeletonCoolTime >= 0)
            applySkeletonCoolTime -= Time.deltaTime;
        else
        {
            SkeletonPreFab();
            applySkeletonCoolTime = skeletonCoolTime;
        }
        //»ç½Å
        if (applyReaperCoolTime >= 0)
            applyReaperCoolTime -= Time.deltaTime;
        else
        {
            ReaperAttack();
            applyReaperCoolTime = reaperCoolTime;
        }
        if(isReaperEffect1 == true)
            reaperEffect1.gameObject.transform.Translate(8 * Time.deltaTime, 0, 0);
        if (isReaperEffect2 == true)
            reaperEffect2.gameObject.transform.Translate(8 * Time.deltaTime, 0, 0);
        if (isReaperEffect3 == true)
            reaperEffect3.gameObject.transform.Translate(8 * Time.deltaTime, 0, 0);
        if (isReaperEffect4 == true)
            reaperEffect4.gameObject.transform.Translate(8 * Time.deltaTime, 0, 0);

        //¸÷
        if (applyMobCoolTime >= 0)
            applyMobCoolTime -= Time.deltaTime;
        else
        {
            MobRespawn();
            MobRespawn();
            MobRespawn();
            MobRespawn();
            MobRespawn();
            applyMobCoolTime = mobCoolTime;
        }
    }

    void ReaperAttack()
    {
        Invoke("DisReaperAttack", 23f);
        int ranReaper = Random.Range(1, 5);
        if (ranReaper == 1)
        {
            isReaperEffect1 = true;
            reaper1.gameObject.SetActive(true);
            reaperEffect1.gameObject.SetActive(true);
            reaperEffect1.gameObject.transform.position = new Vector3(-50f, -0.9f);

        }
        else if (ranReaper == 2)
        {
            isReaperEffect2 = true;
            reaper2.gameObject.SetActive(true);
            reaperEffect2.gameObject.SetActive(true);
            reaperEffect2.gameObject.transform.position = new Vector3(-50f, 4);
        }
        else if (ranReaper == 3)
        {
            isReaperEffect3 = true;
            reaper3.gameObject.SetActive(true);
            reaperEffect3.gameObject.SetActive(true);
            reaperEffect3.gameObject.transform.position = new Vector3(49f, -0.9f);
        }
        else if (ranReaper == 4)
        {
            isReaperEffect4 = true;
            reaper4.gameObject.SetActive(true);
            reaperEffect4.gameObject.SetActive(true);
            reaperEffect4.gameObject.transform.position = new Vector3(49f, -0.9f);
        }
    }
    void DisReaperAttack()
    {
        isReaperEffect1 = false;
        isReaperEffect2 = false;
        isReaperEffect3 = false;
        isReaperEffect4 = false;

        reaper1.gameObject.SetActive(false);
        reaperEffect1.gameObject.SetActive(false);
        reaper2.gameObject.SetActive(false);
        reaperEffect2.gameObject.SetActive(false);
        reaper3.gameObject.SetActive(false);
        reaperEffect3.gameObject.SetActive(false);
        reaper4.gameObject.SetActive(false);
        reaperEffect4.gameObject.SetActive(false);

    }
    void FireBallFire()
    {
        int ranFireBall = Random.Range(1, 5);
        if (ranFireBall == 1)
            Instantiate(fireBall, fireStart1.transform);
        else if (ranFireBall == 2)
            Instantiate(fireBall, fireStart2.transform);
        else if (ranFireBall == 3)
            Instantiate(fireBall, fireStart3.transform);
        else if (ranFireBall == 4)
            Instantiate(fireBall, fireStart4.transform);
    }

    void SkeletonPreFab()
    {
        int ranFireBall = Random.Range(1, 5);
        if (ranFireBall == 1)
            Instantiate(skeletonPrefab, fireStart1.transform);
        else if (ranFireBall == 2)
            Instantiate(skeletonPrefab, fireStart2.transform);
        else if (ranFireBall == 3)
            Instantiate(skeletonPrefab, fireStart3.transform);
        else if (ranFireBall == 4)
            Instantiate(skeletonPrefab, fireStart4.transform);
    }
    void MobRespawn()
    {
        int ranEnemy = Random.Range(0, 3);
        int ranMobPoint = Random.Range(1, 5);

        GameObject mob = objectPool.MakeObj(mobObj[ranEnemy]);
        if(ranMobPoint == 1)
            mob.transform.position = mobStart1.transform.position;
        if (ranMobPoint == 2)
            mob.transform.position = mobStart2.transform.position;
        if (ranMobPoint == 3)
            mob.transform.position = mobStart3.transform.position;
        if (ranMobPoint == 4)
            mob.transform.position = mobStart4.transform.position;

    }
}
