using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageController : MonoBehaviour
{
    // Static singleton property
    public static ImageController Instance { get; private set; }

    private List<Texture2D> imageTextureList = new List<Texture2D>();

    private void Start()
    {
        // Save a reference to the ImageController component as our singleton instance
        Instance = this;
    }

    public void AddImageTextureList(Texture2D texture)
    {
        this.imageTextureList.Add(texture);
    }

    public List<Texture2D> GetImageTextureList()
    {
        return this.imageTextureList;
    }

    public void ClearImageTextureList()
    {
        this.imageTextureList.Clear();
    }
}
