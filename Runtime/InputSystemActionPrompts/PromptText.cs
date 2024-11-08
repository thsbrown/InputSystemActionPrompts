using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

namespace InputSystemActionPrompts
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class PromptText : MonoBehaviour
    {
        [SerializeField] private InputActionAsset m_InputActionAsset;
        
        /// <summary>
        /// Cached TextMeshProUGUI component that we'll apply the prompt sprites to
        /// </summary>
        private TextMeshProUGUI m_TextField;

        /// <summary>
        /// Cached original text, so we can reapply it if the input device changes
        /// </summary>
        private string m_OriginalText;
        
        
        void Start()
        {
            m_TextField = GetComponent<TextMeshProUGUI>();
            if (m_TextField == null) return;
            m_OriginalText=m_TextField.text;
            RefreshText();
            // Listen to device changing
            InputDevicePromptSystem.OnActiveDeviceChanged+= DeviceChanged;
        }

        private void OnDestroy()
        {
            // Remove listener
            InputDevicePromptSystem.OnActiveDeviceChanged-= DeviceChanged;
        }
        
        /// <summary>
        /// Updates the text field the promp field is utilizing, ensuring prompt icons work and update appropriately as well. 
        /// </summary>
        /// <param name="text">The new text that will be applied to the text field. Should retain the action binding text such as [UI/Submit]</param>
        public void UpdateText(string text)
        {
            m_OriginalText = text;
            RefreshText();
        }

        /// <summary>
        /// Called when active input device changed
        /// </summary>
        /// <param name="obj"></param>
        private void DeviceChanged(InputDevice device)
        {
            RefreshText();
        }

        /// <summary>
        /// Applies text with prompt sprites to the TextMeshProUGUI component
        /// </summary>
        private void RefreshText()
        {
            if (m_TextField == null) return;
            m_TextField.text = InputDevicePromptSystem.InsertPromptSprites(m_OriginalText);
        }
    }
}
