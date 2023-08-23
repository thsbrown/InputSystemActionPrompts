﻿using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace InputSystemActionPrompts
{
    [RequireComponent(typeof(Image))]
    public class PromptIcon : MonoBehaviour
    {
        /// <summary>
        /// This should be the full path, including binding map and action, eg "Player/Move"
        /// </summary>
        [SerializeField] private string m_Action = "Player/Move";
        
        /// <summary>
        /// The image to apply the prompt sprite to
        /// </summary>
        private Image m_Image;
        
        void Start()
        {
            m_Image = GetComponent<Image>();
            if (m_Image == null) return;
            RefreshIcon();
            // Listen to device changing
            InputDevicePromptSystem.OnActiveDeviceChanged+= DeviceChanged;
        }
        
        private void OnDestroy()
        {
            // Remove listener
            InputDevicePromptSystem.OnActiveDeviceChanged-= DeviceChanged;
        }
        
        /// <summary>
        /// Called when active input device changed
        /// </summary>
        /// <param name="obj"></param>
        private void DeviceChanged(InputDevice device)
        {
            RefreshIcon();
        }

        /// <summary>
        /// Sets the icon for the current action
        /// </summary>
        private void RefreshIcon()
        {
            var sourceSprite = InputDevicePromptSystem.GetActionPathBindingSprite(m_Action);
            m_Image.sprite = sourceSprite;
            switch (InputSystemDevicePromptSettings.GetSettings().SpriteNotFoundBehavior)
            {
                case InputSystemDevicePromptSettings.SpriteNotFoundBehaviorEnum.Default:
                    if (sourceSprite == null)
                    {
                        return;
                    }
                    break;
                case InputSystemDevicePromptSettings.SpriteNotFoundBehaviorEnum.SuppressDisplay:
                    //if we have a sprite enable the gameobject just in case it was off, if we don't disable it
                    if (sourceSprite == null)
                    {
                        gameObject.SetActive(false);
                        return;
                    }
                    gameObject.SetActive(true);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            m_Image.SetNativeSize();
        }
    }
}