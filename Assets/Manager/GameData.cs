using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//다른 부분에서 저장하고 싶다면, DataController.Instance.SaveGameData()

[Serializable]
public class GameData
{
    public bool first;
    public int hightScore;//최고스코어
    public int money;//머니
    public int cutcount;//잡은 몹수
    public int playTimeLv;//플레이타임에 따른 난이도
    public int playCount;

    public bool audioOff;
    //Hp 레벨
    public int knightHpLv;
    //HP 값
    public int knightHp;//캐릭터 Hp
    public int michaelHp;//미카엘 Hp
    public int monkHp;//뭉크 Hp
    public int thiefHp;//도적 Hp
    public int skeletonHp;//스켈레톤 Hp

    //Atk 레벨
    public int knightAtkLv;
    public int knightSkillLv;
    //Atk 값
    public int knightAtk;//캐릭터 Atk
    public int knightSkill;//캐릭터 Atk
    public int michaelAtk;//미카엘 Atk
    public int monkAtk;//뭉크 Atk
    public int thiefAtk;//도적 Atk
    public int skeletonAtk;//스켈레톤 Atk

    //Speed 레벨
    public int knightSpeedLv;
    //Speed 값
    public int knightSpeed;//캐릭터 Speed
    
}
