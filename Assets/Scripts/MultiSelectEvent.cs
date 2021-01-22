using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MultiSelectEvent : MonoBehaviour
{
    public GameObject scollViewContent;
    public GameObject thisButton;

    public T FindChildrenWithTag<T>(GameObject parent, string tag, bool forceActive = false) where T : Component
    {
        if (parent == null) { throw new System.ArgumentNullException(); }
        if (string.IsNullOrEmpty(tag) == true) { throw new System.ArgumentNullException(); }

        T[] list = parent.GetComponentsInChildren<T>(forceActive);
        foreach (T t in list)
        {
            if (t.CompareTag(tag) == true)
            {
                return t;
            }
        }
        return null;
    }

    public T[] FindChildrensWithTag<T>(GameObject parent, string tag, bool forceActive = false) where T : Component
    {
        if(parent == null) { throw new System.ArgumentNullException(); }
        if (string.IsNullOrEmpty(tag) == true) { throw new System.ArgumentNullException(); }
        List<T> list = new List<T>(parent.GetComponentsInChildren<T>(forceActive));
        if (list.Count == 0) { return null; }

        for (int i = list.Count - 1; i >= 0; i--)
        {
            if (list[i].CompareTag(tag) == false)
            {
                list.RemoveAt(i);
            }
        }
        return list.ToArray();
    }

    public void DrawSelectButton()
    {
        Image[] images = FindChildrensWithTag<Image>(scollViewContent, "CapturedFiles");

        for (int i = 0; i < images.Length; i++)
        {
            try
            {
                Component toggles = FindChildrenWithTag<Component>(scollViewContent, "ToggleForSelection");
                toggles.gameObject.SetActive(false);

            }
            catch
            {
                Debug.Log(i);
            }
        }

        thisButton.SetActive(false);
    }
}
