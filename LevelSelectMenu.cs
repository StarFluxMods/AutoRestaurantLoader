using Kitchen;
using Kitchen.Modules;
using KitchenLib;
using KitchenLib.Preferences;
using System.Collections.Generic;
using UnityEngine;

namespace AutoRestaurantLoader
{
    public class LevelSelectMenu<T> : KLMenu<T>
    {
        public LevelSelectMenu(Transform container, ModuleList module_list) : base(container, module_list)
        {
        }

        public override void Setup(int player_id)
        {
            AddLabel("Slot to auto load");
            AddSelect<int>(selectedSlot);
            selectedSlot.OnChanged += delegate (object _, int result)
            {
                Mod.manager.GetPreference<PreferenceInt>("selectedSaveSlot").Set(result);
            };

            New<SpacerElement>(true);
            New<SpacerElement>(true);

            AddButton(base.Localisation["MENU_BACK_SETTINGS"], delegate (int i)
            {
                Mod.manager.Save();
                this.RequestPreviousMenu();
            }, 0, 1f, 0.2f);
        }

        private Option<int> selectedSlot = new Option<int>(new List<int> { 0, 1, 2, 3, 4, 5 }, Mod.manager.GetPreference<PreferenceInt>("selectedSaveSlot").Get(), new List<string> { "Disabled", "Slot 1", "Slot 2", "Slot 3", "Slot 4", "Slot 5" });
    }
}