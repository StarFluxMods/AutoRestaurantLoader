using Kitchen;
using KitchenLib.Preferences;
using KitchenMods;
using Unity.Collections;
using Unity.Entities;

namespace AutoRestaurantLoader
{
    [UpdateAfter(typeof(CreateLocationsRoom))]
    public class AutoLoadResturant : FranchiseFirstFrameSystem, IModSystem
    {
        private EntityQuery Query;
        protected override void Initialise()
        {
            base.Initialise();
            Query = GetEntityQuery(new QueryHelper()
                .All(typeof(CLocationChoice)));
        }

        protected override void OnUpdate()
        {
            using var entities = Query.ToEntityArray(Allocator.TempJob);
            foreach (var entity in entities)
            {
                if (Require(entity, out CLocationChoice location))
                {
                    if (location.Slot == Mod.manager.GetPreference<PreferenceInt>("selectedSaveSlot").Get() && location.State == SaveState.Loaded)
                    {
                        Set<SSelectedLocation>(new SSelectedLocation
                        {
                            Valid = true,
                            Selected = location
                        });
                        
                        Entity e = base.EntityManager.CreateEntity(new ComponentType[]
                        {
                            typeof(SPerformSceneTransition),
                            typeof(CDoNotPersist)
                        });
                        base.EntityManager.SetComponentData<SPerformSceneTransition>(e, new SPerformSceneTransition
                        {
                            NextScene = SceneType.LoadFullAutosave
                        });
                    }
                }
            }
            entities.Dispose();
        }
    }
}