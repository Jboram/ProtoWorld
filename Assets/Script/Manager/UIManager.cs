using UnityEngine;
using System.Collections.Generic;
using System;
using UnityEditor.PackageManager.UI;
using TMPro;

namespace ProtoWorld 
{
    public class UIManager : SingletonMonoBehaviour<UIManager>
    {

        private Dictionary<Type, HudBase> hudDictionary = new Dictionary<Type, HudBase>();
        private Dictionary<Type, WindowBase> windowDictionary = new Dictionary<Type, WindowBase>();

        [SerializeField] private List<GameObject> hudPrefabs = new List<GameObject>();
        [SerializeField] private List<GameObject> windowPrefabs = new List<GameObject>();

        protected override void OnAwakeSingleton()
        {
            LoadUI();
        }

        private void LoadUI()
        {
            // HUD 프리팹 로드
            foreach (var hudPrefab in hudPrefabs)
            {
                var baseComponent = hudPrefab.GetComponent<HudBase>();
                if (baseComponent != null)
                {
                    Type type = baseComponent.GetType();
                    if (!hudDictionary.ContainsKey(type))
                    {
                        GameObject hud = Instantiate(hudPrefab);
                        var instanceComponent = hud.GetComponent<HudBase>();
                        hud.SetActive(false);
                        hudDictionary.Add(type, instanceComponent);
                    }
                }
            }

            // 윈도우 프리팹 로드
            foreach (var windowPrefab in windowPrefabs)
            {
                var baseComponent = windowPrefab.GetComponent<WindowBase>();
                if (baseComponent != null)
                {
                    Type type = baseComponent.GetType();
                    if (!windowDictionary.ContainsKey(type))
                    {
                        GameObject window = Instantiate(windowPrefab);
                        var instanceComponent = window.GetComponent<WindowBase>();
                        window.SetActive(false);
                        windowDictionary.Add(type, instanceComponent);
                    }
                }
            }
        }
        public T GetHud<T>() where T : HudBase
        {
            Type type = typeof(T);
            if (hudDictionary.TryGetValue(type, out HudBase hud))
            {
                return hud as T;
            }
            Debug.LogWarning($"Hud of type '{type}' not found.");
            return null;
        }

        public T GetWindow<T>() where T : WindowBase
        {
            Type type = typeof(T);
            if (windowDictionary.TryGetValue(type, out WindowBase window))
            {
                return window as T;
            }
            Debug.LogWarning($"Window of type '{type}' not found.");
            return null;
        }

        public T ShowHud<T>() where T : HudBase
        {
            T hud = GetHud<T>();
            if (hud != null)
            {
                hud.Show();
                return hud;
            }

            return null;
        }

        public void HideHud<T>() where T : HudBase
        {
            T hud = GetHud<T>();
            if (hud != null)
            {
                hud.Hide();
            }
        }

        public T OpenWindow<T>() where T : WindowBase
        {
            T window = GetWindow<T>();
            if (window != null)
            {
                window.Show();
                return window;
            }

            return null;
        }

        public void CloseWindow<T>() where T : WindowBase
        {
            T window = GetWindow<T>();
            if (window != null)
            {
                window.Hide();
            }
        }
    }
}