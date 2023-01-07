using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EActionType
{
    None = -1,

    //雷电芽衣
    Idle = 0,
    Run,
    Jump,
    AttackOne,
    AttackTwo,
    AttackThree,
    AttackFour,
    AttackFive,
    EvadeForwardOne,
    EvadeForwardTwo,
    EvadeBackwardOne,
    EvadeBackwardTwo,
    EvadeEndToAttack,
    JumpTurnSword,
    BeHitRetreat,
    BeHitOne,
    BeHitTwo,
    Parry,

    Count,





    //雷律芽衣
    HerrscherOfThunderMeiIdle = 10000,
    HerrscherOfThunderEvadeForwardOne,
    HerrscherOfThunderEvadeForwardTwo,
    //雷律招架技能
    HerrscherOfThunderWeaPonTurn,
    DoublieKnifeAttack,
    //雷律技能其一
    SkillOneLandThorn,
    HerrscherOfThunderMeiSkillTwoSwordTrack,
    SkillThreeJumpTurnSword,
    //技能贝塞尔曲线
    SkillBezierCuves,
    //技能大
    SkillBig,
    //雷律受击其一
    HerrscherOfThunderBeHitOne,
    //雷律受击其二
    HerrscherOfThunderBeHitTwo,
    //雷律五连击
    HerrscherOfThunderAtkOne,
    HerrscherOfThunderAtkTwo,
    HerrscherOfThunderAtkThree,
    HerrscherOfThunderAtkFour,
    HerrscherOfThunderAtkFive,
    HerrscherOfThunderEvadeBackwardOne,
    HerrscherOfThunderEvadeBackwardTwo,
    Walk,





    //QiuQiuPopple
    QiuQiuPoppleWalk = 20000,
    QiuQiuPoppleJump,
    QiuQiuPoppleRun,
    QiuQiuPoppleBeHitOne,
    QiuQiuPoppleBeHitTwo,
    QiuQiuPoppleAttackOne,
    QiuQiuPoppleAttackTwo,
    QiuQiuPoppleDie,

}
