using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class Knight : MonoBehaviour
{
    public LvManager hpbar;
    public Result resultpage;

    private bool isUpDown;
    private bool locationUp;//위치가 위이면 true

    public int hp;

    public float speed;
    private bool canMove = true;
    private bool isLeftMove = true;
    private bool lastLeftMove;

    public bool isAtk = false;

    public bool isSkill = false;

    public Button atkButton; 
    public float atkDelay;
    private float applyAtkDelay;
    private Animator animator;
    public Animator animatorAtkEffect;
    public Animator animatorSkillView;
    public GameObject AttackEffect;
    public GameObject AttackEffectCollider;
    public GameObject skillEffect1;
    public GameObject skillEffect2;
    private bool isSkillEffect = false;
    public Image skillCool;

    public TextMeshProUGUI damagetext;
    private bool textMove=false;

    public GameObject result;

    public AudioSource atkSound;
    public AudioSource skillSound;
    public AudioSource dieSound;
    void Start()
    {
        result.gameObject.SetActive(false);

        hp = 30 + Data.Instance.gameData.knightHpLv * 5;
        animator = GetComponent<Animator>();
        animator.SetBool("Walking", true);
        skillCool.gameObject.SetActive(false);
        AttackEffect.gameObject.SetActive(false);
        AttackEffectCollider.gameObject.SetActive(false);
        damagetext.gameObject.SetActive(false);

        isUpDown = true;
    }

    void Update()
    {
        if(hp<=0)
        {
            dieSound.Play();
            result.gameObject.SetActive(true);
            resultpage.ResultPage();
            Time.timeScale = 0;
        }
        if(canMove)
        {
            if (isLeftMove)
            {
                transform.localScale = new Vector3(3f, 3f, 1f);
                transform.Translate(-speed * Time.deltaTime, 0, 0);
            }
            else
            {
                transform.localScale = new Vector3(-3f, 3f, 1f);
                transform.Translate(speed * Time.deltaTime, 0, 0);
            }
        }

        if(isSkillEffect)
        {
            skillEffect1.gameObject.SetActive(true);
            skillEffect2.gameObject.SetActive(true);
            skillEffect1.transform.Translate(-8 * Time.deltaTime, 0, 0);
            skillEffect2.transform.Translate(8 * Time.deltaTime, 0, 0);
        }
        else
        {
            skillEffect1.gameObject.SetActive(false);
            skillEffect2.gameObject.SetActive(false);
        }

        if(textMove)
        {
            damagetext.gameObject.transform.Translate(0, 15 * Time.deltaTime, 0);
        }

        if(applyAtkDelay >= 0)
        {
            applyAtkDelay -= Time.deltaTime;
        }
        else if(isSkill == false)
        {
            atkButton.gameObject.GetComponent<Button>().interactable = true;
        }
    }

    public void LeftMove()
    {
        if (isAtk == true || isSkill == true)
            return;
        isLeftMove = true;
        animator.SetBool("Walking", true);
    }
    public void RightMove()
    {
        if (isAtk == true || isSkill == true)
            return;
        isLeftMove = false;
        animator.SetBool("Walking", true);
    }
    public void UpMove()
    {
        if (locationUp || isUpDown == false)
            return;
        gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 5.5f, gameObject.transform.position.z);
        locationUp = true;
        isUpDown = false;
        Invoke("IsUpDownTrue", 0.5f);
    }
    public void DownMove()
    {
        if (locationUp == false || isUpDown == false)
            return;
        gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - 5.5f, gameObject.transform.position.z);
        locationUp = false;
        isUpDown = false;
        Invoke("IsUpDownTrue", 0.5f);
    }
    void IsUpDownTrue()
    {
        isUpDown = true;
    }
    public void Attack()
    {
        if (isAtk == true || isSkill == true)
            return;
        isAtk = true;
        if(isLeftMove == true)
            lastLeftMove = true;
        else
            lastLeftMove = false;
        applyAtkDelay = atkDelay;
        atkButton.gameObject.GetComponent<Button>().interactable = false;
        canMove = false;
        AttackEffect.gameObject.SetActive(true);
        animator.SetBool("Walking", false);
        animator.SetBool("Attacking", true);
        StartCoroutine(AtkEffectCoroutine());
        animator.SetBool("Skilling", false);
        animatorSkillView.SetBool("SkillView", false);
    }
    public void AttackEnd()
    {
        atkSound.Stop();
        isAtk = false;
        canMove = true;
        if (lastLeftMove == true)
            LeftMove();
        else
            RightMove();
        AttackEffect.gameObject.SetActive(false);
        AttackEffectCollider.gameObject.SetActive(false);
        animator.SetBool("Walking", true);
        animator.SetBool("Attacking", false);
        animatorAtkEffect.SetBool("AtkEffect", false);
        animator.SetBool("Skilling", false);
        animatorSkillView.SetBool("SkillView", false);
    }

    IEnumerator AtkEffectCoroutine()
    {
        yield return new WaitForSeconds(0.1f);
        animatorAtkEffect.SetBool("AtkEffect", true);
    }
    void AttackCollider()
    {
        atkSound.Play();
        AttackEffectCollider.gameObject.SetActive(true);
    }
    public void Skill()
    {
        if (isSkill == true)
            return;
        skillCool.gameObject.SetActive(true);
        skillCool.fillAmount = 1;
        isSkill = true;
        isAtk = false;
        if (isLeftMove == true)
            lastLeftMove = true;
        else
            lastLeftMove = false;
        canMove = false;
        AttackEffect.gameObject.SetActive(false);
        AttackEffectCollider.gameObject.SetActive(false);
        atkButton.gameObject.GetComponent<Button>().interactable = false;
        StartCoroutine(SkillCoool());
        animator.SetBool("Walking", false);
        animator.SetBool("Attacking", false);
        animatorAtkEffect.SetBool("AtkEffect", false);
        animator.SetBool("Skilling", true);
        skillEffect1.transform.position = new Vector3(gameObject.transform.position.x - 0.5f, gameObject.transform.position.y + 0.1f, gameObject.transform.position.z);
        skillEffect2.transform.position = new Vector3(gameObject.transform.position.x + 0.5f, gameObject.transform.position.y + 0.1f, gameObject.transform.position.z);
        StartCoroutine(SkillEffectCoroutine());
        StartCoroutine(SkillEffectEnd());
        animatorSkillView.SetBool("SkillView", true);
    }
    IEnumerator SkillEffectCoroutine()
    {
        yield return new WaitForSeconds(0.7f);
        isSkillEffect = true;
    }
    IEnumerator SkillCoool()
    {
        for(int i=0;i<100;i++)
        {
            skillCool.fillAmount -= 0.01f;
            yield return new WaitForSeconds(0.1f);
        }
        skillCool.gameObject.SetActive(false);
    }
    IEnumerator SkillEffectEnd()
    {
        yield return new WaitForSeconds(2f);
        isSkillEffect = false;
    }
    public void SkillEnd()
    {
        atkButton.gameObject.GetComponent<Button>().interactable = true;
        isSkill = false;
        canMove = true;
        if (lastLeftMove == true)
            LeftMove();
        else
            RightMove();
        animator.SetBool("Walking", true);
        animator.SetBool("Attacking", false);
        animatorAtkEffect.SetBool("AtkEffect", false);
        animator.SetBool("Skilling", false);
        animatorSkillView.SetBool("SkillView", false);
    }
    public void SkillSound()
    {
        skillSound.Play();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.tag == "MichaelAtk" && gameObject.transform.tag != "NoDamage")
        {
            damagetext.gameObject.SetActive(true);
            damagetext.gameObject.transform.position = Camera.main.WorldToScreenPoint(transform.position + new Vector3(0, 1f, 0));
            damagetext.text = "-" + Data.Instance.gameData.michaelAtk.ToString();
            textMove = true;
            hp -= Data.Instance.gameData.michaelAtk;
            gameObject.transform.tag = "NoDamage";
            Invoke("Untagged", 1f);
            StartCoroutine(HitPlayer());
            hpbar.KnightHp();
        }
        if (other.transform.tag == "MonkAtk" && gameObject.transform.tag != "NoDamage")
        {
            damagetext.gameObject.SetActive(true);
            damagetext.gameObject.transform.position = Camera.main.WorldToScreenPoint(transform.position + new Vector3(0, 1f, 0));
            damagetext.text = "-" + Data.Instance.gameData.monkAtk.ToString();
            textMove = true;
            hp -= Data.Instance.gameData.monkAtk;
            gameObject.transform.tag = "NoDamage";
            Invoke("Untagged", 1f);
            StartCoroutine(HitPlayer());
            hpbar.KnightHp();
        }
        if (other.transform.tag == "ThiefAtk" && gameObject.transform.tag != "NoDamage")
        {
            damagetext.gameObject.SetActive(true);
            damagetext.gameObject.transform.position = Camera.main.WorldToScreenPoint(transform.position + new Vector3(0, 1f, 0));
            damagetext.text = "-" + Data.Instance.gameData.thiefAtk.ToString();
            textMove = true;
            hp -= Data.Instance.gameData.thiefAtk;
            gameObject.transform.tag = "NoDamage";
            Invoke("Untagged", 1f);
            StartCoroutine(HitPlayer());
            hpbar.KnightHp();
        }

        if (other.transform.tag == "FireBall")
        {
            damagetext.gameObject.SetActive(true);
            damagetext.gameObject.transform.position = Camera.main.WorldToScreenPoint(transform.position + new Vector3(0, 1.5f, 0));
            damagetext.text = "-10";
            textMove = true;
            hp -= 10;
            gameObject.transform.tag = "NoDamage";
            Invoke("Untagged", 1f);
            StartCoroutine(HitPlayer());
            hpbar.KnightHp();
        }

        if (other.transform.tag == "SkeletonAtk")
        {
            damagetext.gameObject.SetActive(true);
            damagetext.gameObject.transform.position = Camera.main.WorldToScreenPoint(transform.position + new Vector3(0, 1.5f, 0));
            damagetext.text = "-10";
            textMove = true;
            hp -= Data.Instance.gameData.skeletonAtk;
            gameObject.transform.tag = "NoDamage";
            Invoke("Untagged", 1f);
            StartCoroutine(HitPlayer());
            hpbar.KnightHp();
        }
        if (other.transform.tag == "Die" || other.transform.tag == "ReaperAtk")
        {
            hp -= 99999;
            hpbar.KnightHp();
        }
    }
    void Untagged()
    {
        textMove = true;
        damagetext.gameObject.SetActive(false);
        gameObject.transform.tag = "Untagged";
    }
    IEnumerator HitPlayer()
    {
        Color color = GetComponent<SpriteRenderer>().color;
        color.a = 0;
        GetComponent<SpriteRenderer>().color = color;
        yield return new WaitForSeconds(0.15f);
        color.a = 1f;
        GetComponent<SpriteRenderer>().color = color;
        yield return new WaitForSeconds(0.15f);
        color.a = 0;
        GetComponent<SpriteRenderer>().color = color;
        yield return new WaitForSeconds(0.15f);
        color.a = 1f;
        GetComponent<SpriteRenderer>().color = color;
        color.a = 0;
        GetComponent<SpriteRenderer>().color = color;
        yield return new WaitForSeconds(0.15f);
        color.a = 1f;
        GetComponent<SpriteRenderer>().color = color;
    }
}
