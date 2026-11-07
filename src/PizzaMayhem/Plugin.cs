using BepInEx;
using UnityEngine;

namespace PizzaMayhem
{
    [BepInPlugin("mellizospf.pizzamayhem", "ULTRAKILL: Pizza Mayhem", "0.2.0")]
    public sealed class Plugin : BaseUnityPlugin
    {
        private void Awake()
        {
            Logger.LogInfo("Pizza Mayhem 0.2.0 loading movement prototype.");
            gameObject.AddComponent<PizzaMovementController>();
            Logger.LogInfo("Pizza Mayhem movement prototype attached.");
        }
    }
}
