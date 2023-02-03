using UnityEngine;

namespace Extensions
{
    public static class SpriteEx
    {
        public static Texture2D ToTexture2D(this Sprite sprite)
        {
            if (sprite.rect.width != sprite.texture?.width)
            {
                Texture2D newText = new((int)sprite.rect.width, (int)sprite.rect.height);
                Color[] newColors = sprite.texture.GetPixels((int)sprite.textureRect.x,
                                                             (int)sprite.textureRect.y,
                                                             (int)sprite.textureRect.width,
                                                             (int)sprite.textureRect.height);
                newText.SetPixels(newColors);
                newText.Apply();
                return newText;
            }
            else
                return sprite.texture;
        }
    }
}