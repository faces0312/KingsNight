using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//�ٸ� �κп��� �����ϰ� �ʹٸ�, DataController.Instance.SaveGameData()

[Serializable]
public class GameData
{
    public bool first;
    public int hightScore;//�ְ��ھ�
    public int money;//�Ӵ�
    public int cutcount;//���� ����
    public int playTimeLv;//�÷���Ÿ�ӿ� ���� ���̵�
    public int playCount;

    public bool audioOff;
    //Hp ����
    public int knightHpLv;
    //HP ��
    public int knightHp;//ĳ���� Hp
    public int michaelHp;//��ī�� Hp
    public int monkHp;//��ũ Hp
    public int thiefHp;//���� Hp
    public int skeletonHp;//���̷��� Hp

    //Atk ����
    public int knightAtkLv;
    public int knightSkillLv;
    //Atk ��
    public int knightAtk;//ĳ���� Atk
    public int knightSkill;//ĳ���� Atk
    public int michaelAtk;//��ī�� Atk
    public int monkAtk;//��ũ Atk
    public int thiefAtk;//���� Atk
    public int skeletonAtk;//���̷��� Atk

    //Speed ����
    public int knightSpeedLv;
    //Speed ��
    public int knightSpeed;//ĳ���� Speed
    
}
