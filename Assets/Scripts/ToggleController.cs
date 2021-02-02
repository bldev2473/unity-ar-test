using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleController : MonoBehaviour
{
    Toggle m_toggle;
    Sprite m_sprite;
    Texture2D m_texture;

    void Start()
    {
        // Fetch the Toggle GameObject
        m_toggle = GetComponent<Toggle>();

        // Convert sprite of image component to texture
        m_sprite = m_toggle.GetComponentInParent<Image>().sprite;
        m_texture = new Texture2D((int)m_sprite.rect.width, (int)m_sprite.rect.height);
        var pixels = m_sprite.texture.GetPixels((int)m_sprite.textureRect.x,
                                                (int)m_sprite.textureRect.y,
                                                (int)m_sprite.textureRect.width,
                                                (int)m_sprite.textureRect.height);
        m_texture.SetPixels(pixels);
        m_texture.Apply();

        // Add listener for when the state of the Toggle changes, and output the state
        m_toggle.onValueChanged.AddListener(delegate {
            ToggleValueChanged(m_toggle);
        });
    }

    // Output the new state of the Toggle into Text when the user uses the Toggle
    void ToggleValueChanged(Toggle change)
    {
        if (m_toggle.isOn)
        {
            ImageController.Instance.AddImageTextureList(m_texture);
        }
    }
}
