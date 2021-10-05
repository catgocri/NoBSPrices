using BepInEx;
using BepInEx.Configuration;
using HarmonyLib;
using UnityEngine;

namespace catgocri.NoBSPrices.PotionCraft
{
    
    [BepInPlugin("net.catgocri.PotionCraft.NoBSPrices", "No Bullshit Prices", "1.0")]
    public class NoBSPricesPlugin : BaseUnityPlugin
    {
        public static ConfigEntry<float> configGetDiscountForItem;
        void Awake()
        {
            Debug.Log($"[NoBSPrices]: Loaded");
            configGetDiscountForItem = Config.Bind("Settings", "priceModifier", 0.5f, "The merchant price modifier. 0.5 = basePrice * 0.5. 1 = basePrice * 1. 2 = basePrice * 2."); 
            //RecipeMapObjectAwakeEvent.OnRecipeMapObjectAwake += (sender, args) => MakePricesNotBad();
            var harmony = new Harmony("net.catgocri.PotionCraft.NoBSPrices");
            harmony.PatchAll();
        }
    }
    [HarmonyPatch(typeof(TradeManager), "GetDiscountForItem")]
    public static class MyPatch3 
    {
        static bool Prefix(ref float __result)
        {
            __result = NoBSPricesPlugin.configGetDiscountForItem.Value;
            return false;
        }
    }
}