using System;
using System.Reflection;
using BepInEx;
using BepInEx.Configuration;
using UnityEngine;

namespace PizzaMayhem
{
    /// <summary>
    /// First integration layer for ULTRAKILL's NewMovement controller.
    /// It uses reflection so this project does not ship or modify game assemblies.
    /// Game-version compatibility still needs an in-game test.
    /// </summary>
    public sealed class PizzaMovementController : MonoBehaviour
    {
        private ConfigEntry<KeyCode> machKey;
        private ConfigEntry<KeyCode> jumpKey;
        private ConfigEntry<KeyCode> dashKey;
        private ConfigEntry<float> maxMachMultiplier;
        private ConfigEntry<float> accelerationSeconds;

        private Type movementType;
        private object movementInstance;
        private FieldInfo walkSpeedField;
        private PropertyInfo walkSpeedProperty;
        private FieldInfo jumpPowerField;
        private PropertyInfo jumpPowerProperty;

        private float originalWalkSpeed;
        private float originalJumpPower;
        private bool originalsCaptured;
        private float machCharge;
        private float comboTimer;
        private int comboScore;
        private int comboMultiplier = 1;
        private float displayedMach;

        private void Awake()
        {
            machKey = Config.Bind("Controls", "MachRun", KeyCode.LeftControl,
                "Hold to accelerate into Mach Run.");
            jumpKey = Config.Bind("Controls", "JumpForCombo", KeyCode.Space,
                "Press after building speed to add a combo action.");
            dashKey = Config.Bind("Controls", "DashForCombo", KeyCode.LeftShift,
                "Press after building speed to add a combo action.");
            maxMachMultiplier = Config.Bind("Movement", "MaxMachMultiplier", 1.8f,
                "Maximum multiplier applied to ULTRAKILL walkSpeed while Mach Run is held.");
            accelerationSeconds = Config.Bind("Movement", "AccelerationSeconds", 1.4f,
                "Seconds needed to reach maximum Mach speed.");

            movementType = Type.GetType("NewMovement, Assembly-CSharp");
            if (movementType == null)
                Logger.LogWarning("Could not locate NewMovement in Assembly-CSharp. Movement integration will retry.");

            Logger.LogInfo("Pizza Tower-style acceleration and combo prototype initialized.");
        }

        private void Update()
        {
            if (!TryBindPlayer())
            {
                displayedMach = 1f;
                return;
            }

            bool running = Input.GetKey(machKey.Value);
            float ramp = Mathf.Max(0.1f, accelerationSeconds.Value);
            if (running)
                machCharge = Mathf.MoveTowards(machCharge, 1f, Time.deltaTime / ramp);
            else
                machCharge = Mathf.MoveTowards(machCharge, 0f, Time.deltaTime / 0.45f);

            displayedMach = Mathf.Lerp(1f, Mathf.Max(1f, maxMachMultiplier.Value), machCharge);
            ApplyMovementMultipliers(displayedMach);

            if (comboTimer > 0f)
                comboTimer -= Time.deltaTime;
            else
            {
                comboScore = 0;
                comboMultiplier = 1;
            }

            if (machCharge > 0.65f && Input.GetKeyDown(jumpKey.Value))
                AddComboAction(100);
            if (machCharge > 0.65f && Input.GetKeyDown(dashKey.Value))
                AddComboAction(150);
            if (machCharge > 0.85f)
                comboScore += Mathf.RoundToInt(5f * comboMultiplier * Time.deltaTime);
        }

        private bool TryBindPlayer()
        {
            if (movementType == null)
                movementType = Type.GetType("NewMovement, Assembly-CSharp");
            if (movementType == null)
                return false;

            object instance = null;
            PropertyInfo instanceProperty = movementType.GetProperty("Instance",
                BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static);
            if (instanceProperty != null)
                instance = instanceProperty.GetValue(null, null);

            if (instance == null)
            {
                FieldInfo instanceField = movementType.GetField("Instance",
                    BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static);
                if (instanceField != null)
                    instance = instanceField.GetValue(null);
            }

            if (instance == null || instance == movementInstance)
                return instance != null && originalsCaptured;

            RestoreOriginals();
            movementInstance = instance;
            walkSpeedField = movementType.GetField("walkSpeed",
                BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            walkSpeedProperty = movementType.GetProperty("walkSpeed",
                BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            jumpPowerField = movementType.GetField("jumpPower",
                BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            jumpPowerProperty = movementType.GetProperty("jumpPower",
                BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

            if (!TryRead(walkSpeedField, walkSpeedProperty, out originalWalkSpeed))
            {
                Logger.LogWarning("NewMovement was found, but walkSpeed was not readable. Check this game's Assembly-CSharp.");
                movementInstance = null;
                return false;
            }

            originalsCaptured = TryRead(jumpPowerField, jumpPowerProperty, out originalJumpPower);
            if (!originalsCaptured)
                originalJumpPower = 0f;

            Logger.LogInfo("Bound to ULTRAKILL NewMovement. Base walk speed: " + originalWalkSpeed);
            return true;
        }

        private void ApplyMovementMultipliers(float multiplier)
        {
            TryWrite(walkSpeedField, walkSpeedProperty, originalWalkSpeed * multiplier);
            if (originalsCaptured && originalJumpPower > 0f)
                TryWrite(jumpPowerField, jumpPowerProperty, originalJumpPower * Mathf.Lerp(1f, 1.12f, machCharge));
        }

        private void AddComboAction(int points)
        {
            comboScore += points * comboMultiplier;
            comboMultiplier = Mathf.Min(8, comboMultiplier + 1);
            comboTimer = 3.0f;
        }

        private bool TryRead(FieldInfo field, PropertyInfo property, out float value)
        {
            value = 0f;
            try
            {
                object raw = field != null ? field.GetValue(movementInstance) :
                    (property != null && property.CanRead ? property.GetValue(movementInstance, null) : null);
                if (raw == null) return false;
                value = Convert.ToSingle(raw);
                return true;
            }
            catch (Exception ex)
            {
                Logger.LogWarning("Could not read movement value: " + ex.Message);
                return false;
            }
        }

        private void TryWrite(FieldInfo field, PropertyInfo property, float value)
        {
            try
            {
                if (field != null && field.FieldType == typeof(float))
                    field.SetValue(movementInstance, value);
                else if (property != null && property.CanWrite && property.PropertyType == typeof(float))
                    property.SetValue(movementInstance, value, null);
            }
            catch (Exception ex)
            {
                Logger.LogWarning("Could not write movement value: " + ex.Message);
            }
        }

        private void RestoreOriginals()
        {
            if (movementInstance != null && originalsCaptured)
            {
                TryWrite(walkSpeedField, walkSpeedProperty, originalWalkSpeed);
                if (originalJumpPower > 0f)
                    TryWrite(jumpPowerField, jumpPowerProperty, originalJumpPower);
            }
            originalsCaptured = false;
        }

        private void OnGUI()
        {
            GUI.Label(new Rect(18, 18, 320, 24), "PIZZA MAYHEM  |  MACH x" + displayedMach.ToString("0.00"));
            GUI.Label(new Rect(18, 42, 320, 24), "COMBO  " + comboScore + "   x" + comboMultiplier);
        }

        private void OnDestroy()
        {
            RestoreOriginals();
        }
    }
}
