using BepInEx;
using UnityEngine;

namespace PizzaMayhem
{
    [BepInPlugin("mellizospf.pizzamayhem", "ULTRAKILL: Pizza Mayhem", "0.1.0")]
    public sealed class Plugin : BaseUnityPlugin
    {
        private void Awake()
        {
            Logger.LogInfo("Pizza Mayhem loaded. Prototype systems are under development.");
            gameObject.hideFlags = HideFlags.HideAndDontSave;
        }
    }
}
