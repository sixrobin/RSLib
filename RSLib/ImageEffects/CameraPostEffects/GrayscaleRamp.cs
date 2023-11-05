namespace RSLib.ImageEffects.CameraPostEffects
{
    using RSLib.Extensions;
    using UnityEngine;

    [ExecuteInEditMode]
    [AddComponentMenu("RSLib/Camera Post Effects/Grayscale Ramp")]
    public class GrayscaleRamp : CameraPostEffect
    {
        private static readonly int RAMP_TEX_ID = Shader.PropertyToID("_RampTex");
        private static readonly int RAMP_OFFSET_ID = Shader.PropertyToID("_RampOffset");
        private static readonly int WEIGHT_ID = Shader.PropertyToID("_Weight");
        
        [SerializeField]
        private Texture2D _textureRamp = null;
        [SerializeField, Range(-1f, 1f)]
        private float _offset = 0f;
        [SerializeField, Range(0f, 1f)]
        private float _weight = 1f;

        public bool Inverted;
        private Texture2D _initRamp;
        
        protected override string ShaderName => "RSLib/Post Effects/Grayscale Ramp";

        public Texture2D TextureRamp => _textureRamp;

        public float Offset
        {
            get => _offset;
            set => _offset = Mathf.Clamp(value, -1f, 1f);
        }

        public void SetRamp(Texture2D ramp)
        {
            _textureRamp = ramp;
        }

        public void ResetRamp()
        {
            _textureRamp = _initRamp;
        }

        private void Awake()
        {
            _initRamp = _textureRamp;
        }

        protected override void OnBeforeRenderImage(RenderTexture source, RenderTexture destination, Material material)
        {
            material.SetTexture(RAMP_TEX_ID, Inverted && _textureRamp != null ? _textureRamp.FlipX() : _textureRamp);
            material.SetFloat(RAMP_OFFSET_ID, _offset);
            material.SetFloat(WEIGHT_ID, _weight);
        }
    }
}