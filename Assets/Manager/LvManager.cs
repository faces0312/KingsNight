using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LvManager : MonoBehaviour
{
    public Knight player;
    public float playTime;
    public TextMeshProUGUI cutCount;
    public TextMeshProUGUI warningText;
    public TextMeshProUGUI HpText;

    public TextMeshProUGUI timeText;
    public RectTransform hpBar;


    //¿É¼Ç
    public GameObject option;
    public GameObject explain;
    public Button soundOn;
    public Button soundOff;
    public AudioSource bgm;

    public void Option()
    {
        Time.timeScale = 0;
        option.gameObject.SetActive(true);
    }
    public void Cancel()
    {
        Time.timeScale = 1;
        option.gameObject.SetActive(false);
    }
    public void Explain()
    {
        explain.gameObject.SetActive(true);
    }
    public void DisExplain()
    {
        explain.gameObject.SetActive(false);
    }
    public void GoMain()
    {
        Time.timeScale = 1;
        Data.Instance.gameData.playCount++;
        SceneManager.LoadScene("Main");
    }
    public void GoGame()
    {
        Cancel();
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
    void Start()
    {
        explain.gameObject.SetActive(false);
        option.gameObject.SetActive(false);
        Data.Instance.gameData.cutcount = 0;
        Data.Instance.gameData.playTimeLv = 0;

        Data.Instance.gameData.michaelHp = 6;
        Data.Instance.gameData.monkHp = 4;
        Data.Instance.gameData.thiefHp = 3;
        Data.Instance.gameData.skeletonHp = 15;

        Data.Instance.gameData.michaelAtk = 2;
        Data.Instance.gameData.monkAtk = 4;
        Data.Instance.gameData.thiefAtk = 5;
        Data.Instance.gameData.skeletonAtk = 5;

        Data.Instance.gameData.knightAtk = 2 + Data.Instance.gameData.knightAtkLv;
        Data.Instance.gameData.knightSkill = 4 + Data.Instance.gameData.knightSkillLv * 2;


        warningText.gameObject.SetActive(false);

        HpText.text = player.hp.ToString();
        hpBar.sizeDelta = new Vector2(hpBar.rect.width * player.hp / (30 + Data.Instance.gameData.knightHpLv * 5), hpBar.rect.height);
    }

    // Update is called once per frame
    void Update()
    {
        if (Data.Instance.gameData.audioOff == false)
        {
            player.atkSound.volume = 1;
            player.skillSound.volume = 1;
            player.dieSound.volume = 1;
            bgm.volume = 1;
            soundOn.interactable = false;
            soundOff.interactable = true;
        }
        else
        {
            player.atkSound.volume = 0;
            player.skillSound.volume = 0;
            player.dieSound.volume = 0;
            bgm.volume = 0;
            soundOn.interactable = true;
            soundOff.interactable = false;
        }

        timeText.text = Data.Instance.gameData.playTimeLv.ToString() + " : " + ((int)playTime).ToString();
        cutCount.text = Data.Instance.gameData.cutcount.ToString();
        if (playTime >= 60)
        {
            Data.Instance.gameData.playTimeLv++;
            warningText.gameObject.SetActive(true);
            StartCoroutine(DisWarningText());
            MobStatUp();
            playTime = 0;
        }
        playTime += Time.deltaTime;


    }

    void MobStatUp()
    {
        Data.Instance.gameData.michaelHp = Data.Instance.gameData.michaelHp + (Data.Instance.gameData.playTimeLv * 3);
        Data.Instance.gameData.monkHp = Data.Instance.gameData.monkHp + (Data.Instance.gameData.playTimeLv * 2);
        Data.Instance.gameData.thiefHp = Data.Instance.gameData.thiefHp + (Data.Instance.gameData.playTimeLv * 1);
        Data.Instance.gameData.skeletonHp = Data.Instance.gameData.skeletonHp + (Data.Instance.gameData.playTimeLv * 5);

        Data.Instance.gameData.michaelAtk = Data.Instance.gameData.michaelAtk + (Data.Instance.gameData.playTimeLv * 1);
        Data.Instance.gameData.monkAtk = Data.Instance.gameData.monkAtk + (Data.Instance.gameData.playTimeLv * 2);
        Data.Instance.gameData.thiefAtk = Data.Instance.gameData.thiefAtk + (Data.Instance.gameData.playTimeLv * 3);
        Data.Instance.gameData.skeletonAtk = Data.Instance.gameData.skeletonAtk + (Data.Instance.gameData.playTimeLv * 5);
    }

    IEnumerator DisWarningText()
    {
        yield return new WaitForSeconds(3f);
        warningText.gameObject.SetActive(false);
    }

    public void KnightHp()
    {
        HpText.text = player.hp.ToString();
        hpBar.sizeDelta = new Vector2(688 * player.hp / (30 + Data.Instance.gameData.knightHpLv * 5), hpBar.rect.height);
    }

}
