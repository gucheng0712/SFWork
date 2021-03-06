using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 技能信息
/// </summary>
public class SFAction_SkillInfo : SFAction_BaseAction
{
    [HideInInspector]
    public SkillBindType skillBindTtype;

    [HideInInspector]
    public string objName;

    List<GameObject> dsList;

    private void Awake()
    {
        dsList = new List<GameObject>();
    }

    public override void TrigAction()
    {
        GameObject.Destroy(owner.gameObject);
    }

    public void SetOwner(GameObject owner)
    {   
        /*给每个子SkillInfo赋上owner的值
        for(int i = 0; i < ses.Length; ++i)
        {
            ses[i].owner = owner;
            ses[i].skillInfo = this;
            dsList.Add(ses[i].gameObject);
        }
        */
    }

    public void DestroyAllInst()
    {
        while(dsList.Count > 0)
        {
            GameObject tmp = dsList[0];
            dsList.Remove(tmp);
            GameObject.Destroy(tmp);
        }
    }

    public void AddEffect(GameObject effect)
    {
        dsList.Add(effect);
    }
}
