using UnityEngine;

namespace SpriteMask
{
    [RequireComponent(typeof(SpriteRenderer)), ExecuteInEditMode]
    public class MultiMaskedSprite : MonoBehaviour
    {
        [Tooltip("Material that applied if mask is disabled")]
        public Material UnmaskedMaterial;
        
        public MaskType MaskType;

        internal SpriteRenderer Renderer;

        private void OnEnable()
        {
#if UNITY_EDITOR
            _oldMaskType = MaskType;
#endif
            Renderer = GetComponent<SpriteRenderer>();
            MultiMaskHandler.SpriteRegistration(this);
        }

        private void OnDisable()
        {
            MultiMaskHandler.SpriteCancellation(this);
            Renderer.sharedMaterial = UnmaskedMaterial != null ? UnmaskedMaterial : ((int)MaskType % 2 == 1 ? MaskMaterialHandler.DefaultUnlit : MaskMaterialHandler.DefaultLit);
        }
        
#if UNITY_EDITOR
        [SerializeField, HideInInspector]
        private MaskType _oldMaskType;
        private void OnValidate()
        {
            if (_oldMaskType != MaskType)
            {
                MultiMaskHandler.SpriteCancellation(this);
                _oldMaskType = MaskType;
                MultiMaskHandler.SpriteRegistration(this);
            }
        }
#endif
    }
}
