using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class SpritesImporter : AssetPostprocessor
{
    void OnPostprocessSprites(Texture2D texture, Sprite[] sprites)
    {
        TextureImporter textureImporter = assetImporter as TextureImporter;
        //textureImporter.spritePixelsPerUnit = 32;
        textureImporter.filterMode = FilterMode.Point;
        textureImporter.wrapMode = TextureWrapMode.Clamp;
        var importSettings = textureImporter.GetDefaultPlatformTextureSettings();
        importSettings.format = TextureImporterFormat.RGBA32;
        textureImporter.SetPlatformTextureSettings(importSettings);
    }
}
