using Extensions;
using UnityEngine;

namespace Components
{
    [RequireComponent(typeof(SpriteRenderer))]
    public sealed class SpriteSetter : MonoBehaviour
    {
        [SerializeField] private Color _backgroundColor;
        [SerializeField] private Color _shadowColor;
        [SerializeField] private SpriteRenderer _renderer;
        [SerializeField] private Shader _shader;
        [HideInInspector][SerializeField] private Sprite _backgroundSprite;
        [HideInInspector][SerializeField] private Sprite _linearSprite;
        [HideInInspector][SerializeField] private Sprite _colorSprite;

        private Material _material;

        void Awake()
        {
            _shader ??= Shader.Find("ShaderGraphs/Sprite");
            _renderer ??= GetComponent<SpriteRenderer>();
            _material ??= new Material(_shader);
            _renderer.material = _material;
            _material.SetColor("_BaseColor", _backgroundColor);
            _material.SetColor("_ShadowColor", _shadowColor);
        }

        void LateUpdate()
        {
            _renderer.sprite = _linearSprite;

            _material.SetTexture("_BackgroundTex", _backgroundSprite.ToTexture2D());
            _material.SetTexture("_LinearTex", _linearSprite.ToTexture2D());
            _material.SetTexture("_ColorTex", _colorSprite.ToTexture2D());
        }
    }
}