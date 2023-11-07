using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

namespace InputSystemActionPrompts
{
    /// <summary>
    /// Settings for Input Device Prompt system
    /// </summary>
   
    public class InputSystemDevicePromptSettings : ScriptableObject
    {
        /// <summary>
        /// List of all actions to consider for text replacement
        /// </summary>
        public List<InputActionAsset> InputActionAssets;

        /// <summary>
        /// List of all device prompt assets to apply
        /// </summary>
        public List<InputDevicePromptData> DevicePromptAssets;
        
        /// <summary>
        /// The priority for prompt display before a button is pressed 
        /// </summary>
        public List<InputDeviceType> DefaultDevicePriority = new List<InputDeviceType>
        {
            InputDeviceType.GamePad,
            InputDeviceType.Keyboard,
            InputDeviceType.Mouse
        };
        
        public char OpenTag = '[';
        public char CloseTag = ']';
        /// <summary>
        /// Formatter used to add additional Rich Text formatting to the returned string from <see cref="InputDevicePromptSystem.InsertPromptSprites"/>
        /// <example>
        /// <![CDATA[
        /// {SPRITE} = "<sprite="PS5_Prompts" sprite="ps5_button_cross">" (unformatted).
        /// ]]><br/>
        /// <![CDATA[
        /// <size=200%>{SPRITE}</size> = "<size=200%><sprite="PS5_Prompts" sprite="ps5_button_cross"></size>" (formatted output double size).
        /// ]]>
        /// </example>
        /// </summary>
        [Tooltip("Formatter used to add additional Rich Text formatting to all text return from InputDevicePromptSystem.InsertPromptSprites and in turn PromptText. Example <size=200%>{SPRITE}</size>")]
        public string PromptSpriteFormatter = PromptSpriteFormatterSpritePlaceholder;
        
        /// <summary>
        /// Defines the behavior of <see cref="PromptText"/> and <see cref="PromptIcon"/> when a sprite is not found.
        /// <para>Default = Both components will execute their default behavior when a sprite is not found.</para>
        /// <para>SuppressDisplay = Whenever no sprite is found for [Example/Prompt] will replace it with empty string and PromptIcon will disable gameobject it is attached to.</para>
        /// </summary>
        [Tooltip("Defines the behavior of PromptText and PromptIcon when a sprite is not found.\r" +
                 "Default = Both components will execute their default behavior when a sprite is not found.\r" +
                 "SuppressDisplay = Whenever no sprite is found for [Example/Prompt] PromptText will replace it with empty string and PromptIcon will disable gameobject it is attached to.")]
        public SpriteNotFoundBehaviorEnum SpriteNotFoundBehavior;
        
        /// <summary>
        /// The amount a gamepad stick must be moved to be considered a device detection event. 
        /// </summary>
        [Range(0,1)]
        [Tooltip("The amount a gamepad stick must be moved to be considered a device detection event.")]
        public float gamepadStickDeviceDetectionThreshold = 0.1f;
        
        /// <summary>
        /// Placeholder used to denote where a sprite should be inserted in the <see cref="InputSystemDevicePromptSettings.PromptSpriteFormatter"/>
        /// </summary>
        public const string PromptSpriteFormatterSpritePlaceholder = "{SPRITE}";
        public const string SettingsDataFile = "InputSystemDevicePromptSettings";
        
        public static InputSystemDevicePromptSettings GetSettings()
        {
            var settings = Resources.Load<InputSystemDevicePromptSettings>(SettingsDataFile);
            if (settings == null)
            {
                Debug.LogWarning($"Could not find InputSystemDevicePromptSettings at path '{SettingsDataFile} - Create using Window->Input System Device Prompts->Create Settings'");
            }
            return settings;
        }

        public enum SpriteNotFoundBehaviorEnum
        {
            ///<summary>
            /// Let each component handle what happens individually if a sprite is not found.
            ///</summary>    
            Default,
            ///<summary>
            /// In the case of PromptText wherever [Example/Prompt] comes up short display nothing.
            /// In the case of PromptIcon wherever [Example/Prompt] comes up short disable Gameobject or Image component.
            ///</summary>
            SuppressDisplay
        }
    }
}