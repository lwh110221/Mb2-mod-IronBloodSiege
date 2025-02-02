// 铁血攻城
// 作者：Ahao
// 版本：1.0.0
// email ：2285813721@qq.com，ahao221x@gmail.com
using TaleWorlds.Core;
using TaleWorlds.MountAndBlade;
using TaleWorlds.Library;
using TaleWorlds.Localization;
using System;
using IronBloodSiege.Behavior;
using IronBloodSiege.Setting;
using IronBloodSiege.Util;

namespace IronBloodSiege
{
    public class SubModule : MBSubModuleBase
    {
        protected override void OnSubModuleLoad()
        {
            base.OnSubModuleLoad();
            #if DEBUG
            try 
            {
                Util.Logger.LogDebug("初始化", "铁血攻城Mod开始加载");
                Util.Logger.LogDebug("初始化", $"当前目录: {System.IO.Directory.GetCurrentDirectory()}");
                Util.Logger.LogDebug("初始化", $"程序集版本: {typeof(SubModule).Assembly.GetName().Version}");
                Util.Logger.LogDebug("初始化", $"程序集位置: {typeof(SubModule).Assembly.Location}");
                Util.Logger.LogDebug("初始化", $"调试模式已启用");
                
                // 检查是否正确加载了TaleWorlds.Engine
                var engineAssembly = typeof(TaleWorlds.Engine.MBDebug).Assembly;
                Util.Logger.LogDebug("初始化", $"TaleWorlds.Engine版本: {engineAssembly.GetName().Version}");
            }
            catch (Exception ex)
            {
                Util.Logger.LogError("初始化", ex);
            }
            #endif
        }

        public override void OnInitialState()
        {
            base.OnInitialState();
            ShowModInfo();
        }

        public override void OnGameInitializationFinished(Game game)
        {
            base.OnGameInitializationFinished(game);
            ShowModInfo();
        }

        private void ShowModInfo()
        {
            try
            {
                InformationManager.DisplayMessage(new InformationMessage(
                    new TextObject("{=ibs_mod_loaded}IronBlood Siege - Loaded Successfully! Author: Ahao").ToString(), 
                    Constants.InfoColor));
                InformationManager.DisplayMessage(new InformationMessage(
                    new TextObject("{=ibs_mod_email}email: ahao221x@gmail.com").ToString(), 
                    Constants.InfoColor));
            }
            catch (Exception ex)
            {
                InformationManager.DisplayMessage(new InformationMessage(
                    new TextObject("{=ibs_error_display}IronBlood Siege display error: {MESSAGE}")
                        .SetTextVariable("MESSAGE", ex.Message)
                        .ToString(),
                    Constants.ErrorColor));
            }
        }

        public override void OnBeforeMissionBehaviorInitialize(Mission mission)
        {
            base.OnBeforeMissionBehaviorInitialize(mission);
            try
            {
                if (mission != null)
                {
                    #if DEBUG
                    Util.Logger.LogDebug("OnBeforeMissionBehaviorInitialize", 
                        $"Mission Mode: {mission.Mode}, " +
                        $"Scene Name: {mission.SceneName}, " +
                        $"IsSiegeBattle: {mission.IsSiegeBattle}, " +
                        $"IsSallyOutBattle: {mission.IsSallyOutBattle}");
                    #endif

                    mission.AddMissionBehavior(new SiegeMoraleBehavior());
                    if (Settings.Instance.IsEnabled)
                    {
                        InformationManager.DisplayMessage(new InformationMessage(
                            new TextObject("{=ibs_mod_enabled}IronBlood Siege is enabled").ToString(), 
                            Constants.InfoColor));
                    }
                    else 
                    {
                        InformationManager.DisplayMessage(new InformationMessage(
                            new TextObject("{=ibs_mod_disabled}IronBlood Siege is disabled").ToString(), 
                            Constants.ErrorColor));
                    }
                }
            }
            catch (Exception ex)
            {
                InformationManager.DisplayMessage(new InformationMessage(
                    new TextObject("{=ibs_error_behavior}IronBlood Siege behavior error: {MESSAGE}")
                        .SetTextVariable("MESSAGE", ex.Message)
                        .ToString(),
                    Constants.ErrorColor));
            }
        }
    }
} 