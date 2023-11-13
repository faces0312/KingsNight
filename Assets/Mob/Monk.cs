using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monk : MonoBehaviour
{
    private BoxCollider2D boxCollider;

    public GameObject attackEffect;
    public int hp;
    private int nowHp;
    public int speed;
    public int atk;
    private bool isAtk = false;
    public float attackDelay;
    public float attackDelayApply;

    public string atkSound;

    private Animator animator;

    private bool canMove = true;

    private RaycastHit2D player_right;
    private RaycastHit2D player_left;

    private RaycastHit2D atk_right;
    private RaycastHit2D atk_left;
    private bool isRight = false;
    private bool isLeft = false;

    private bool moveRight = true;
    private bool moveLeft = false;

    public GameObject hpBar;

    void Start()
    {
        hp = Data.Instance.gameData.monkHp;
        nowHp = hp;

        speed = 8;
        attackDelay = 0.8f;

        animator = GetComponent<Animator>();
        attackDelayApply = attackDelay;
    }
    void Update()
    {
        if (nowHp <= 0)
        {
            Data.Instance.gameData.cutcount++;
            gameObject.SetActive(false);
        }
        if (nowHp == hp || hpBar.gameObject.transform.localScale.x <= 0)
            hpBar.gameObject.SetActive(false);
        else
        {
            hpBar.gameObject.SetActive(true);
        }
        if (canMove)
        {
            canMove = false;
            StartCoroutine(MoveCoroutine());
        }

        if (isAtk)
        {
            attackEffect.gameObject.SetActive(true);
            animator.SetTrigger("Attack");
            attackDelayApply = attackDelay;
            isAtk = false;
            canMove = true;
        }
    }

    private void OnEnable()
    {
        canMove = true;
        Color color = GetComponent<SpriteRenderer>().color;
        color = Color.white;
        GetComponent<SpriteRenderer>().color = color;
        attackEffect.gameObject.SetActive(false);

        hp = Data.Instance.gameData.monkHp;
        nowHp = hp;

        speed = 8;
        attackDelay = 0.8f;

        attackDelayApply = attackDelay;
    }
    IEnumerator MoveCoroutine()
    {
        float atkDistance = 3f;
        float distance = 100f;
        int layerMask = 1 << LayerMask.NameToLayer("Player");

        player_right = Physics2D.Raycast(transform.position, new Vector2(1, 0), distance, layerMask);
        player_left = Physics2D.Raycast(transform.position, new Vector2(-1, 0), distance, layerMask);


        atk_right = Physics2D.Raycast(transform.position, new Vector2(1, 0), atkDistance, layerMask);
        atk_left = Physics2D.Raycast(transform.position, new Vector2(-1, 0), atkDistance, layerMask);


        if (player_right.transform != null)
        {
            if (atk_right.transform != null)
            {
                attackDelayApply -= Time.deltaTime;
                isRight = true;
                isLeft = false;
                transform.localScale = new Vector3(-2f, 2f, 1f);
                if (attackDelayApply <= 0)
                {
                    isAtk = true;
                    yield break;
                }
            }
            else
            {
                transform.localScale = new Vector3(-2f, 2f, 1f);
                transform.Translate(speed * Time.deltaTime, 0, 0);
            }
        }
        else if (player_left.transform != null)
        {
            if (atk_left.transform != null)
            {
                attackDelayApply -= Time.deltaTime;
                isRight = false;
                isLeft = true;
                transform.localScale = new Vector3(2f, 2f, 1f);
                if (attackDelayApply <= 0)
                {
                    isAtk = true;
                    yield break;
                }
            }
            else
            {
                isRight = false;
                isLeft = true;
                transform.localScale = new Vector3(2f, 2f, 1f);
                transform.Translate(-speed * Time.deltaTime, 0, 0);
            }
        }
        else
        {
            if (moveRight)
            {
                transform.localScale = new Vector3(-2f, 2f, 1f);
                transform.Translate(speed * Time.deltaTime, 0, 0);
            }
            else
            {
                transform.localScale = new Vector3(2f, 2f, 1f);
                transform.Translate(-speed * Time.deltaTime, 0, 0);
            }
        }

        yield return new WaitForSeconds(0.01f);
        canMove = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.tag == "PlayerAttack" && gameObject.transform.tag != "NoDamage")
        {
            nowHp -= Data.Instance.gameData.knightAtk;
            hpBar.gameObject.transform.localScale = new Vector3(hpBar.gameObject.transform.localScale.x * nowHp / hp, hpBar.gameObject.transform.localScale.y, hpBar.gameObject.transform.localScale.z);
            StartCoroutine(HitEffect());
            gameObject.transform.tag = "NoDamage";
            if (isRight == true)
            {
                transform.position = Vector2.Lerp(transform.position, new Vector2(-60, 20), 2 * Time.deltaTime);
            }
            if (isLeft == true)
            {
                transform.position = Vector2.Lerp(transform.position, new Vector2(60, 20), 2 * Time.deltaTime);
            }
            Invoke("Untagged", 0.6f);
        }
        if (other.transform.tag == "PlayerSkill" && gameObject.transform.tag != "NoDamage")
        {
            nowHp -= Data.Instance.gameData.knightSkill;
            hpBar.gameObject.transform.localScale = new Vector3(hpBar.gameObject.transform.localScale.x * nowHp / hp, hpBar.gameObject.transform.localScale.y, hpBar.gameObject.transform.localScale.z);
            StartCoroutine(HitEffect());
            gameObject.transform.tag = "NoDamage";
            if (isRight == true)
            {
                transform.position = Vector2.Lerp(transform.position, new Vector2(-80, 10), 5 * Time.deltaTime);
            }
            if (isLeft == true)
            {
                transform.position = Vector2.Lerp(transform.position, new Vector2(80, 10), 5 * Time.deltaTime);
            }
            Invoke("Untagged", 0.7f);
        }
        if (other.transform.tag == "Die")
        {
            hp -= 99;
        }
        if (other.transform.tag == "MoveRight")
        {
            moveRight = true;
            moveLeft = false;
        }
        if (other.transform.tag == "MoveLeft")
        {
            moveRight = false;
            moveLeft = true;
        }
    }

    void Untagged()
    {
        gameObject.transform.tag = "Untagged";
    }
    public void AttackEnd()
    {
        attackEffect.gameObject.SetActive(false);
    }

    IEnumerator HitEffect()
    {
        Color color = GetComponent<SpriteRenderer>().color;
        color = Color.black;
        GetComponent<SpriteRenderer>().color = color;
        yield return new WaitForSeconds(0.5f);
        color = Color.white;
        GetComponent<SpriteRenderer>().color = color;
    } 
}
