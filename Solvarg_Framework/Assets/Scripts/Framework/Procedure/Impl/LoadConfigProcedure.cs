using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProcedureOwner = IFsm<ProcedureManager>;
using Newtonsoft;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Data;

public class LoadConfigProcedure : ProcedureBase
{
    private Dictionary<string, bool> m_LoadedFlag = new Dictionary<string, bool>();

    
    public override void OnEnter(ProcedureOwner fsm)
    {
        base.OnEnter(fsm);
        Debuger.LogError("进入配置流程");
        SingletonManager.Instance.Message_Subscribe(MessageRouter.LoadApplicationConfigSuccess, OnLoadConfigSuccess);
        SingletonManager.Instance.Message_Subscribe(MessageRouter.LoadApplicationConfigFailure, OnLoadConfigFailure);
        SingletonManager.Instance.Message_Subscribe(MessageRouter.LoadUIConfigSuccess, OnLoadUIConfigSuccess);
        SingletonManager.Instance.Message_Subscribe(MessageRouter.LoadUIConfigFailure, OnLoadConfigFailure);
        SingletonManager.Instance.Message_Subscribe(MessageRouter.LoadSceneConfigSuccess, OnLoadSceneConfigSuccess);
        SingletonManager.Instance.Message_Subscribe(MessageRouter.LoadSceneConfigFailure, OnLoadConfigFailure);
        SingletonManager.Instance.Message_Subscribe(MessageRouter.LoadModelConfigSuccess, OnModelConfigSuccess);
        SingletonManager.Instance.Message_Subscribe(MessageRouter.LoadModelConfigFailure, OnLoadConfigFailure);
        SingletonManager.Instance.Message_Subscribe(MessageRouter.LoadQuestInfoSuccess, OnQuestInfoSuccess);
        SingletonManager.Instance.Message_Subscribe(MessageRouter.LoadQuestInfoFailure, OnLoadConfigFailure);
        SingletonManager.Instance.Message_Subscribe(MessageRouter.LoadRoleInfoSuccess, OnRoleInfoSuccess);
        SingletonManager.Instance.Message_Subscribe(MessageRouter.LoadRoleInfoFailure, OnLoadConfigFailure);
        SingletonManager.Instance.Message_Subscribe(MessageRouter.LoadIconInfoSuccess, OnIconInfoSuccess);
        SingletonManager.Instance.Message_Subscribe(MessageRouter.LoadIconInfoFailure, OnLoadConfigFailure);
        SingletonManager.Instance.Message_Subscribe(MessageRouter.LoadItemInfoSuccess, OnItemInfoSuccess);
        SingletonManager.Instance.Message_Subscribe(MessageRouter.LoadItemInfoFailure, OnLoadConfigFailure);

        PreloadResources();
    }
    public override void OnInit(ProcedureOwner fsm)
    {
        base.OnInit(fsm);
        Debuger.LogError("初始化配置流程");
        m_LoadedFlag.Add("Config",false);
        m_LoadedFlag.Add("UIConfig",false);
        m_LoadedFlag.Add("SceneConfig",false);
        m_LoadedFlag.Add("ModelConfig",false);

        m_LoadedFlag.Add("QuestInfo", false);
        m_LoadedFlag.Add("RoleInfo",false);
        m_LoadedFlag.Add("IconInfo",false);
        m_LoadedFlag.Add("ItemInfo", false);
    }

    public override void OnLeave(ProcedureOwner fsm, bool isShutDown)
    {
        base.OnLeave(fsm, isShutDown);
        SingletonManager.Instance.Message_UnSubscribe(MessageRouter.LoadApplicationConfigSuccess, OnLoadConfigSuccess);
        SingletonManager.Instance.Message_UnSubscribe(MessageRouter.LoadApplicationConfigFailure, OnLoadConfigFailure);
        SingletonManager.Instance.Message_UnSubscribe(MessageRouter.LoadUIConfigSuccess, OnLoadUIConfigSuccess);
        SingletonManager.Instance.Message_UnSubscribe(MessageRouter.LoadUIConfigFailure, OnLoadConfigFailure);
        SingletonManager.Instance.Message_UnSubscribe(MessageRouter.LoadSceneConfigSuccess, OnLoadSceneConfigSuccess);
        SingletonManager.Instance.Message_UnSubscribe(MessageRouter.LoadSceneConfigFailure, OnLoadConfigFailure);
        SingletonManager.Instance.Message_UnSubscribe(MessageRouter.LoadModelConfigSuccess, OnModelConfigSuccess);
        SingletonManager.Instance.Message_UnSubscribe(MessageRouter.LoadModelConfigFailure, OnLoadConfigFailure);
        SingletonManager.Instance.Message_UnSubscribe(MessageRouter.LoadQuestInfoSuccess, OnQuestInfoSuccess);
        SingletonManager.Instance.Message_UnSubscribe(MessageRouter.LoadQuestInfoFailure, OnLoadConfigFailure);
        SingletonManager.Instance.Message_UnSubscribe(MessageRouter.LoadRoleInfoSuccess, OnRoleInfoSuccess);
        SingletonManager.Instance.Message_UnSubscribe(MessageRouter.LoadRoleInfoFailure, OnLoadConfigFailure);
        SingletonManager.Instance.Message_UnSubscribe(MessageRouter.LoadIconInfoSuccess, OnIconInfoSuccess);
        SingletonManager.Instance.Message_UnSubscribe(MessageRouter.LoadIconInfoFailure, OnLoadConfigFailure);
        SingletonManager.Instance.Message_UnSubscribe(MessageRouter.LoadItemInfoSuccess, OnItemInfoSuccess);
        SingletonManager.Instance.Message_UnSubscribe(MessageRouter.LoadItemInfoFailure, OnLoadConfigFailure);

        Debuger.LogError("离开配置流程");
    }

    public override void OnUpdate(ProcedureOwner fsm, float elapseSeconds, float realElapseSeconds)
    {
        base.OnUpdate(fsm, elapseSeconds, realElapseSeconds);

        foreach (var k in m_LoadedFlag)
        {
            if (k.Value == false)
            {
                return;
            }
        }
        ChangeState<PreloadProcedure>(fsm);
    }

    private async void PreloadResources()
    {
        await LoadApplicationConfig();
        await LoadUIConfig();
        await LoadSceneConfig();
        await LoadModelConfig();
        await LoadQuestInfo();
        await LoadRoleInfo();
        await LoadIconInfo();

        //后面简易加载就用模板加载
        await LoadInfo<ItemInfo>(MessageRouter.LoadItemInfoSuccess,MessageRouter.LoadItemInfoFailure);
    }

    #region 加载ApplicationCfonig
    private async Task LoadApplicationConfig() {
        //加载配置
        Debuger.Log("加载配置文件");
        List<Config> co = await JsonHelper.DeserializeFromPath<List<Config>>
            ("Assets/AssetPackage/database/json_database/ApplicationConfig.json");
        if (co != null)
        {
            Message message = new Message(MessageRouter.LoadApplicationConfigSuccess, this);
            message.Add("msg", "Config加载完毕");
            message.Add("data", co);
            SingletonManager.Instance.Message_FireAsync(message);
        }
        else
        {
            Message message = new Message(MessageRouter.LoadApplicationConfigFailure, this);
            message.Add("msg", "Config加载失败");
            SingletonManager.Instance.Message_FireAsync(message);
        }
    }

    private void OnLoadConfigSuccess(Message message)
    {
        //配置文件加载完毕
        Debuger.Log("配置文件加载完毕" + message["msg"]);
        List<Config> co = message["data"] as List<Config>;
        SingletonManager.Instance.SetApllicationConfig(co);
        Debuger.Log("游戏语言版本： " + co[0].Language);
        m_LoadedFlag["Config"] = true;
    }


    #endregion

    #region 加载UIConfig
    private async Task LoadUIConfig()
    {
        //加载配置
        Debuger.Log("加载UI配置文件");
        List<UIConfig> co = await JsonHelper.DeserializeFromPath<List<UIConfig>>
            ("Assets/AssetPackage/database/json_database/UIConfig.json");
        if (co != null)
        {
            Message message = new Message(MessageRouter.LoadUIConfigSuccess, this);
            message.Add("msg", "UIConfig加载完毕");
            message.Add("data", co);
            SingletonManager.Instance.Message_FireAsync(message);
        }
        else
        {
            Message message = new Message(MessageRouter.LoadUIConfigFailure, this);
            message.Add("msg", "UIConfig加载失败");
            SingletonManager.Instance.Message_FireAsync(message);
        }

    }

    private void OnLoadUIConfigSuccess(Message message)
    {
        //配置文件加载完毕
        Debuger.Log("配置文件加载完毕" + message["msg"]);
        List<UIConfig> co = message["data"] as List<UIConfig>;
        SingletonManager.Instance.SetUIConfig(co);
        m_LoadedFlag["UIConfig"] = true;
    }
    #endregion

    #region 加载SceneConfig
    private async Task LoadSceneConfig()
    {
        //加载配置
        Debuger.Log("加载Scene配置文件");
        List<SceneConfig> co = await JsonHelper.DeserializeFromPath<List<SceneConfig>>
            ("Assets/AssetPackage/database/json_database/SceneConfig.json");
        if (co != null)
        {
            Message message = new Message(MessageRouter.LoadSceneConfigSuccess, this);
            message.Add("msg", "SceneConfig加载完毕");
            message.Add("data", co);
            SingletonManager.Instance.Message_FireAsync(message);
        }
        else
        {
            Message message = new Message(MessageRouter.LoadSceneConfigFailure, this);
            message.Add("msg", "SceneConfig加载失败");
            SingletonManager.Instance.Message_FireAsync(message);
        }
    }

    private void OnLoadSceneConfigSuccess(Message message)
    {
        //配置文件加载完毕
        Debuger.Log("配置文件加载完毕" + message["msg"]);
        List<SceneConfig> co = message["data"] as List<SceneConfig>;
        SingletonManager.Instance.SetSceneConfig(co);
        m_LoadedFlag["SceneConfig"] = true;
    }
    #endregion

    #region 加载ModelConfig
    private async Task LoadModelConfig()
    {
        //加载配置
        Debuger.Log("加载Model配置文件");
        List<ModelConfig> co = await JsonHelper.DeserializeFromPath<List<ModelConfig>>
            ("Assets/AssetPackage/database/json_database/ModelConfig.json");
        if (co != null)
        {
            Message message = new Message(MessageRouter.LoadModelConfigSuccess, this);
            message.Add("msg", "ModelConfig加载完毕");
            message.Add("data", co);
            SingletonManager.Instance.Message_FireAsync(message);
        }
        else
        {
            Message message = new Message(MessageRouter.LoadModelConfigFailure, this);
            message.Add("msg", "ModelConfig加载失败");
            SingletonManager.Instance.Message_FireAsync(message);
        }
    }

    private void OnModelConfigSuccess(Message message)
    {
        //配置文件加载完毕
        Debuger.Log("资源配置文件加载完毕" + message["msg"]);
        List<ModelConfig> co = message["data"] as List<ModelConfig>;
        SingletonManager.Instance.SetModelConfig(co);
        m_LoadedFlag["ModelConfig"] = true;
    }
    #endregion

    #region 加载QuestInfo
    private async Task LoadQuestInfo()
    {
        //加载配置
        Debuger.Log("加载任务信息文件");
        Dictionary<string,List<QuestInfo>> co = await JsonHelper.DeserializeFromPath<Dictionary<string, List<QuestInfo>>>
            ("Assets/AssetPackage/database/json_database/QuestInfo.json");
        if (co != null)
        {
            Message message = new Message(MessageRouter.LoadQuestInfoSuccess, this);
            message.Add("msg", "任务信息加载完毕");
            message.Add("data", co);
            SingletonManager.Instance.Message_FireAsync(message);
        }
        else
        {
            Message message = new Message(MessageRouter.LoadQuestInfoFailure, this);
            message.Add("msg", "任务信息加载失败");
            SingletonManager.Instance.Message_FireAsync(message);
        }
    }

    private void OnQuestInfoSuccess(Message message)
    {
        //配置文件加载完毕
        Debuger.Log("任务信息加载完毕" + message["msg"]);
        Dictionary<string, List<QuestInfo>> co = message["data"] as Dictionary<string, List<QuestInfo>>;
        SingletonManager.Instance.SetQuestInfo(co);
        m_LoadedFlag["QuestInfo"] = true;
    }
    #endregion

    #region 加载RoleInfo
    private async Task LoadRoleInfo()
    {
        //加载配置
        Debuger.Log("加载角色信息文件");
        List<RoleInfo> co = await JsonHelper.DeserializeFromPath<List<RoleInfo>>
            ("Assets/AssetPackage/database/json_database/RoleInfo.json");
        if (co != null)
        {
            Message message = new Message(MessageRouter.LoadRoleInfoSuccess, this);
            message.Add("msg", "角色信息加载完毕");
            message.Add("data", co);
            SingletonManager.Instance.Message_FireAsync(message);
        }
        else
        {
            Message message = new Message(MessageRouter.LoadRoleInfoFailure, this);
            message.Add("msg", "角色信息加载失敗");
            SingletonManager.Instance.Message_FireAsync(message);
        }
    }

    private void OnRoleInfoSuccess(Message message)
    {
        //配置文件加载完毕
        Debuger.Log("角色信息加载完毕" + message["msg"]);
        List<RoleInfo> co = message["data"] as List<RoleInfo>;
        SingletonManager.Instance.InitRoleInfo(co);
        m_LoadedFlag["RoleInfo"] = true;
    }
    #endregion

    #region 加载IconInfo
    private async Task LoadIconInfo()
    {
        //加载配置
        Debuger.Log("加载icon信息文件");
        List<IconInfo> co = await JsonHelper.DeserializeFromPath<List<IconInfo>>
            ("Assets/AssetPackage/database/json_database/IconInfo.json");
        if (co != null)
        {
            Message message = new Message(MessageRouter.LoadIconInfoSuccess, this);
            message.Add("msg", "Icon信息加载完毕");
            message.Add("data", co);
            SingletonManager.Instance.Message_FireAsync(message);
        }
        else
        {
            Message message = new Message(MessageRouter.LoadIconInfoFailure, this);
            message.Add("msg", "Icon信息加载失敗");
            SingletonManager.Instance.Message_FireAsync(message);
        }
    }

    private void OnIconInfoSuccess(Message message)
    {
        //配置文件加载完毕
        Debuger.Log("Icon信息加载完毕" + message["msg"]);
        List<IconInfo> co = message["data"] as List<IconInfo>;
        SingletonManager.Instance.InitIconInfo(co);
        m_LoadedFlag["IconInfo"] = true;
    }
    #endregion

    #region 加载ItemInfo

    private void OnItemInfoSuccess(Message message)
    {
        //配置文件加载完毕
        Debuger.Log("IItem信息加载完毕" + message["msg"]);
        List<ItemInfo> co = message["data"] as List<ItemInfo>;
        SingletonManager.Instance.InitItemInfo(co);
        m_LoadedFlag["ItemInfo"] = true;
    }
    #endregion

    /// <summary>
    /// 简易读取数据模板
    /// </summary>
    /// <typeparam name="T">泛型</typeparam>
    /// <param name="successRoute">成功路由</param>
    /// <param name="failureRoute">失败路由</param>
    /// <returns></returns>
    private async Task LoadInfo<T>(string successRoute,string failureRoute) where T:class
    {
        //加载配置
        Debuger.Log(string.Format("加载{0}信息文件", typeof(T).Name));
        List<T> co = await JsonHelper.DeserializeFromPath<List<T>>
            (string.Format("Assets/AssetPackage/database/json_database/{0}.json", typeof(T).Name));
        if (co != null)
        {
            Message message = new Message(successRoute, this);
            message.Add("msg", typeof(T).Name+"信息加载完毕");
            message.Add("data", co);
            SingletonManager.Instance.Message_FireAsync(message);
        }
        else
        {
            Message message = new Message(failureRoute, this);
            message.Add("msg", typeof(T).Name + "信息加载失敗");
            SingletonManager.Instance.Message_FireAsync(message);
        }
    }

    private void OnLoadConfigFailure(Message message)
    {
        //配置文件加载完毕
        Debuger.Log("配置文件加载失败" + message["msg"]);
    }
}
