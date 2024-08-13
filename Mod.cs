using KitchenLib;
using KitchenLib.Event;
using KitchenMods;
using System.Reflection;
using Kitchen;
using KitchenLib.Preferences;
using UnityEngine;

namespace AutoRestaurantLoader
{
    public class Mod : BaseMod, IModSystem
    {
        public const string MOD_GUID = "com.starfluxgames.autorestaurantloader";
        public const string MOD_NAME = "Auto Restaurant Loader";
        public const string MOD_VERSION = "0.1.2";
        public const string MOD_AUTHOR = "StarFluxGames";
        public const string MOD_GAMEVERSION = ">=1.2.0";
        
        public static PreferenceManager manager;

        public static AssetBundle Bundle;

        public Mod() : base(MOD_GUID, MOD_NAME, MOD_AUTHOR, MOD_VERSION, MOD_GAMEVERSION, Assembly.GetExecutingAssembly()) { }

        protected override void OnInitialise()
        {
            LogWarning($"{MOD_GUID} v{MOD_VERSION} in use!");
        }

        protected override void OnUpdate()
        {
        }

        protected override void OnPostActivate(KitchenMods.Mod mod)
        {
            manager = new PreferenceManager(MOD_GUID);
            manager.RegisterPreference(new PreferenceInt("selectedSaveSlot", 0));
            manager.Load();

            Events.MainMenuView_SetupMenusEvent += (s, args) =>
            {
                args.addMenu.Invoke(args.instance, new object[] { typeof(LevelSelectMenu<MenuAction>), new LevelSelectMenu<MenuAction>(args.instance.ButtonContainer, args.module_list) });
            };
            Events.PlayerPauseView_SetupMenusEvent += (s, args) =>
            {
                args.addMenu.Invoke(args.instance, new object[] { typeof(LevelSelectMenu<MenuAction>), new LevelSelectMenu<MenuAction>(args.instance.ButtonContainer, args.module_list) });
            };
            
            ModsPreferencesMenu<MenuAction>.RegisterMenu("Auto Restaurant Loader", typeof(LevelSelectMenu<MenuAction>), typeof(MenuAction));
            ModsPreferencesMenu<MenuAction>.RegisterMenu("Auto Restaurant Loader", typeof(LevelSelectMenu<MenuAction>), typeof(MenuAction));
        }
        #region Logging
        public static void LogInfo(string _log) { Debug.Log($"[{MOD_NAME}] " + _log); }
        public static void LogWarning(string _log) { Debug.LogWarning($"[{MOD_NAME}] " + _log); }
        public static void LogError(string _log) { Debug.LogError($"[{MOD_NAME}] " + _log); }
        public static void LogInfo(object _log) { LogInfo(_log.ToString()); }
        public static void LogWarning(object _log) { LogWarning(_log.ToString()); }
        public static void LogError(object _log) { LogError(_log.ToString()); }
        #endregion
    }
}
