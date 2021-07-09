# SFWork

冬狼的游戏堆栈库

这个库是会一直更新到游戏Demo出来(完整的代码),这个公开库内的资源都将会是免费和开源的以及部分自己做的.(真正的效果Demo会用自己买的资源)

设计图在这 - 

[http://146.56.209.11:90/%e6%b8%b8%e8%ae%be-solvarg/](http://146.56.209.11:90/%e6%b8%b8%e8%ae%be-solvarg/)

![](MenuDemo.jpg)

## 目前包含的功能

计划将大部分的Unity的Update放在一个Update下调用

单例(继承IMonobehaviour) -> 单例管理器

然后尽量使用当前较新的技术

目前功能模块见下

- Singleton -> SingletonManager(管理Singleton的Unity生命周期)
- Procedure(控制整个游戏的流程-基于有限状态机)
- UI(管理UI - 基于有限状态机和Asset管理模块)
- Asset(基于Addressable 管理资源的AB与加载)
- MessageDispatcher(消息分发,用MessageRouter进行接口调用的查找)
- Fsm(有限状态机)
- Timer(延时执行时的模块,通常用于冷却之类的)
- excel与Json的转换 - 基于此延伸出的配置数据库方案
- Dialog(运行时全局提示)
- Google Poly Shader 特效集
- Image Effect 与仿PR转场特效集
	- 墨水风格转场实现完毕!
		- Shader需要加上 ZTest Always Cull Off ZWrite Off ,暂不知道为什么不加会导致UI粒子系统失效
- Setting (游戏配置)
- Scene(场景加载与离开管理)
- RoleManager(角色管理系统)
- PlayerCenter(玩家中心)
	- 角色管理系统
- 成长系统
- UI画布上的粒子系统(UIParticleSystem)
- 玩法流程(基于Procedure和场景管理系统,实现多种玩法可以在一个项目下)
- CameraManager(摄像机控制器)
- 任务系统(Quest) [基础框架和简单任务搭建完毕]
	- 分为: 任务前校验(是否可以接取任务),任务可接取,任务进行中,任务完成,任务失败,丢弃任务这几个阶段
	- 以边缘节点为当前实际任务存储
- 对话系统
	- 打字机效果,头像
	- 分支（最多四路分支）
	- 触发事件
	- 基于XNode的节点编辑器
- 动画系统
- 技能系统中普攻的连击部分
- 将Controller等抽象成组件,每个组件都会被回调注册Owner,即Player或者Enemy等等
	- AnimatorController
	- SkillController
	- WeaponController
- 物品系统 (武器系统和背包系统之类的要依赖于物品系统)
	- 依赖Icon系统
	- 极弱关联成长系统(等级限制)
	- 后面商店系统也会被关联上(弱关联,是否允许出售,出售价格)
	- 物品工厂方法,通过物品Id和泛型获取物品(这点暂时加了个TODO,后面可能需要根据物品类型自动转换泛型类型)
- Icon系统
	- icon工厂,生成对应icon的Texture2D
	- 后面可以添加其他方法,来控制icon的表现,比如加边缘光,置灰之类的
- 武器系统
	- 强依赖于物品系统
	- 基于Scriptobject做武器挂点系统,在这里关联ItemId利用DoubleMap关联武器Id和物品Id)
	- 挂点系统强依赖与角色系统
	- 模块化武器,可以丢弃,拾取,夺取,切换
	- 将会反作用成长系统中的属性
- 集成仿Odin开源项目NaughtyAttributes
- 鼠标控制器 <职责: 鼠标的控制中心>
	- 更改鼠标样式
	- 控制不同条件下鼠标样式的切换
	- 后面会拓展按下时触发的事件,以及获取当前鼠标位置等状态

demo入口: Scenes/Loading 这个场景下!


## 暂时计划添加功能

- 移植技能系统到这里(基于动画状态机,进行中)  
	- 动画系统完成
	- 连击完成
	- 原本基于Mono配置的技能系统丢进来了,准备重构成数据驱动
- 背包系统
	- 强依赖与物品系统和UI系统
	- 还有强依赖与大部分物品系统延伸子系统,诸如武器系统
- 战斗系统(仿命运之轮(一个古早手机游戏))
	- 计划写两个,一个回合
	- 一个ACT
	- 尽量保证两个系统强关联
- 存档系统
	- 打算直接用PlayerPrefabs做
- 本地化
- Base于骨骼节点的装饰系统

## Demo级企划

> 作为Demo,这个项目将会实现各种各样的游戏系统,但是当第一歌游戏系统的原型做出来后,后续的开发将会闭源
>> 当然,暂时只是企划,目前主要做的Demo是项目原型
>> 当项目原型完毕后这个仓库就会开始长期其他类型游戏的Demo开发


# Solvarg...
![](ProgressDemo.jpg)

![](InkDemo.png)

![](Dialogue.png)

![](WeaponSystem.jpg)

![](Attack.jpg)

![](WeaponRelease.jpg)