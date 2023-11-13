using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour
{
    public TextMeshProUGUI money;
    public TextMeshProUGUI score;

    //상점
    public GameObject store;//상점
    public GameObject lessmoney;
    public GameObject lessMoneyText;
    public TextMeshProUGUI atkLv;
    public TextMeshProUGUI hpLv;
    public TextMeshProUGUI skillLv;
    public TextMeshProUGUI atkGold;
    public TextMeshProUGUI hpGold;
    public TextMeshProUGUI skillGold;
    public AudioSource upSound;
    public AudioSource storeSound;
    public AudioSource bgm;
    //옵션
    public GameObject option;
    public Button soundOn;
    public Button soundOff;

    //설명서
    public GameObject explain;

    public void Start()
    {
        Time.timeScale = 1;
        lessmoney.gameObject.SetActive(false);
        store.gameObject.SetActive(false);
        explain.gameObject.SetActive(false);
        option.gameObject.SetActive(false);

        if (Data.Instance.gameData.first == false)
        {
            option.gameObject.SetActive(true);
            explain.gameObject.SetActive(true);
            Data.Instance.gameData.first = true;
        }

        atkLv.text = "공격" + '\n' + "Lv " + Data.Instance.gameData.knightAtkLv + '\n' + "+ " + Data.Instance.gameData.knightAtkLv;
        hpLv.text = "체력" + '\n' + "Lv " + Data.Instance.gameData.knightHpLv + '\n' + "+ " + Data.Instance.gameData.knightHpLv * 5;
        skillLv.text = "스킬 공격력" + '\n' + "Lv " + Data.Instance.gameData.knightSkillLv + '\n' + "+ " + Data.Instance.gameData.knightSkillLv * 2;

        atkGold.text = (5 + (Data.Instance.gameData.knightAtkLv * 5)) + " G";
        hpGold.text = (5 + (Data.Instance.gameData.knightHpLv * 5)) + " G";
        skillGold.text = (5 + (Data.Instance.gameData.knightSkillLv * 5)) + " G";
    }
    private void Update()
    {
        money.text = Data.Instance.gameData.money.ToString();
        score.text = Data.Instance.gameData.hightScore.ToString();

        if (Data.Instance.gameData.audioOff == false)
        {
            upSound.volume = 1;
            storeSound.volume = 1;
            bgm.volume = 1;
            soundOn.interactable = false;
            soundOff.interactable = true;
        }
        else
        {
            upSound.volume = 0;
            storeSound.volume = 0;
            bgm.volume = 0;
            soundOn.interactable = true;
            soundOff.interactable = false;
        }
    }
    public void GameStart()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void Store()
    {
        storeSound.Play();
        store.gameObject.SetActive(true);
    }
    public void Option()
    {
        storeSound.Play();
        option.gameObject.SetActive(true);
    }
    public void HomePage()
    {
        Application.OpenURL("https://cafe.naver.com/kingsknight");
    }
    public void SoundOn()
    {
        Data.Instance.gameData.audioOff = false;
        Data.Instance.SaveGameData();
    }
    public void SoundOff()
    {
        Data.Instance.gameData.audioOff = true;
        Data.Instance.SaveGameData();
    }
    public void Cancel()
    {
        storeSound.Play();
        option.gameObject.SetActive(false);
        store.gameObject.SetActive(false);
    }
    public void AtkLvUp()
    {
        upSound.Play();
        if (Data.Instance.gameData.money < 5 + (Data.Instance.gameData.knightAtkLv * 5))
        {
            lessmoney.gameObject.SetActive(true);
            StartCoroutine(LessMoneyUp());
            return;
        }
        Data.Instance.gameData.money = Data.Instance.gameData.money - (5 + (Data.Instance.gameData.knightAtkLv * 5));
        Data.Instance.gameData.knightAtkLv++;
        atkLv.text = "공격" + '\n' + "Lv " + Data.Instance.gameData.knightAtkLv + '\n' + "+ " + Data.Instance.gameData.knightAtkLv;
        atkGold.text = (5 + (Data.Instance.gameData.knightAtkLv * 5)) + " G";

        Data.Instance.SaveGameData();
    }
    public void HpLvUp()
    {
        upSound.Play();
        if (Data.Instance.gameData.money < 5 + (Data.Instance.gameData.knightHpLv * 5))
        {
            lessmoney.gameObject.SetActive(true);
            StartCoroutine(LessMoneyUp());
            return;
        }
        Data.Instance.gameData.money = Data.Instance.gameData.money - (5 + (Data.Instance.gameData.knightHpLv * 5));
        Data.Instance.gameData.knightHpLv++;
        hpLv.text = "체력" + '\n' + "Lv " + Data.Instance.gameData.knightHpLv + '\n' + "+ " + Data.Instance.gameData.knightHpLv * 5;
        hpGold.text = (5 + (Data.Instance.gameData.knightHpLv * 5)) + " G";

        Data.Instance.SaveGameData();
    }
    public void SkillLvUp()
    {
        upSound.Play();
        if (Data.Instance.gameData.money < 8 + (Data.Instance.gameData.knightSkillLv * 8))
        {
            lessmoney.gameObject.SetActive(true);
            StartCoroutine(LessMoneyUp());
            return;
        }
        Data.Instance.gameData.money = Data.Instance.gameData.money - (8 + (Data.Instance.gameData.knightSkillLv * 8));
        Data.Instance.gameData.knightSkillLv++;
        skillLv.text = "스킬 공격력" + '\n' + "Lv " + Data.Instance.gameData.knightSkillLv + '\n' + "+ " + Data.Instance.gameData.knightSkillLv * 2;
        skillGold.text = (5 + (Data.Instance.gameData.knightSkillLv * 5)) + " G";

        Data.Instance.SaveGameData();
    }

    IEnumerator LessMoneyUp()
    {
        for(int i=0;i<20;i++)
        {
            lessMoneyText.gameObject.transform.position = new Vector3(lessMoneyText.gameObject.transform.position.x, lessMoneyText.gameObject.transform.position.y + 3);
            yield return new WaitForSeconds(0.05f);
        }
        lessMoneyText.gameObject.transform.position = new Vector3(lessMoneyText.gameObject.transform.position.x, 540);
        lessmoney.gameObject.SetActive(false);

    }


    public void Explain()
    {
        explain.gameObject.SetActive(true);
    }
    public void DisExplain()
    {
        explain.gameObject.SetActive(false);
    }
}
