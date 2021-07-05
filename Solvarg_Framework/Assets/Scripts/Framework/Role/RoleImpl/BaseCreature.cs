﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCreature : BaseRole
{
    #region 参数
    public Vector2[] AnimPerArray;
    public Vector2[] AnimSkillPerArray;
    protected Animator _Anim;
    public Animator Anim => (_Anim);
    private CharacterController characterCtrl;

    public CharacterController CharacCtrl => (characterCtrl);
    private float playerraidus;
    public float PlayerRadius => (playerraidus);

    public Dictionary<ControllerType,BaseController> controllerPool = new Dictionary<ControllerType, BaseController>();

    private bool isGetHit=false;
    public bool IsGetHit => (isGetHit);

    #endregion

    /// <summary>
    /// 从当前对象中获取控制器
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public BaseController this[ControllerType type]
    {
        get
        {
            if (controllerPool.ContainsKey(type))
            {
                return controllerPool[type];
            }
            else return null;
        }
        set
        {
            RegisterController(type);
        }
    }

    public override void InitRole(RoleInfo role)
    {
        base.InitRole(role);
    }

    #region Unity callback
    protected override void Awake()
    {
        base.Awake();
        characterCtrl = GetComponent<CharacterController>();
        _Anim = GetComponent<Animator>();
    }

    protected override void Update()
    {
        base.Update();
        //把Controller交给Creater来控制,这样不会产生销毁时还存在的问题
        foreach (BaseController baseController in controllerPool.Values)
        {
            baseController.OnUpdate();
        }
    }
    #endregion


    #region 动画StateMachine相关注册,所有的Event都需要以Creature为中心回调
    void EventSkillReady()
    {
        (this[ControllerType.Animator] as AnimatorController)?.EventSkillReady();
    }

    void EventAnimBegin()
    {
        (this[ControllerType.Animator] as AnimatorController)?.EventAnimBegin();
    }

    void EventAnimEnd(int id)
    {
        (this[ControllerType.Animator] as AnimatorController)?.EventAnimEnd(id);
    }
    #endregion

    #region Controller组件调度中心
    public BaseController RegisterController(ControllerType controllerType)
    {
        BaseController control=null;
        if (controllerType == ControllerType.Animator)
        {
            if (_Anim != null)
            {
                control = new AnimatorController();
            }
        }else if (controllerType == ControllerType.Skill)
        {
            control = new SkillController();
        }

        //初始化对应的Controller
        if (control != null)
        {
            if (controllerPool.ContainsKey(controllerType))
            {
                controllerPool[controllerType] = control;
            }
            else controllerPool.Add(controllerType,control);

            Debuger.Log(info.DefaultName + "注册{ " + controllerType.ToString() +" },成功!");
            control.OnStart(this);
            control.owner = this;
        }
        return control;
        
    }
    #endregion
}
