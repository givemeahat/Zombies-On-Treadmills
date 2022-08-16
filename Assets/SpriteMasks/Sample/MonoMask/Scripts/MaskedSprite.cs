using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpriteMask
{
    [ExecuteInEditMode]
    public class MaskedSprite : MonoBehaviour
    {
        [Tooltip("Material that applied if mask is disabled")]
        public Material UnmaskedMaterial;
        [SerializeField]
        private Mask Mask;
        
        private SpriteRenderer Rend;
        
        private void OnEnable()
        {
            ApplyMask();
        }
        
        private void OnDisable()
        {
            var rend = GetComponent<SpriteRenderer>();
            
            if (rend == null) return;
            rend.sharedMaterial = UnmaskedMaterial != null ? UnmaskedMaterial : (Mask == null || (int)Mask.Type % 2 == 1 ? MaskMaterialHandler.DefaultUnlit : MaskMaterialHandler.DefaultLit);
            
#if UNITY_EDITOR
            if(Mask != null) Mask.OnMaterialChanged -= ApplyMask;
#endif
        }

        private void ApplyMask()
        {
            var rend = GetComponent<SpriteRenderer>();
            
            if (rend == null || Mask == null) return;

            rend.sharedMaterial = Mask.GetMaterial();
            
#if UNITY_EDITOR
            Mask.OnMaterialChanged -= ApplyMask;
            Mask.OnMaterialChanged += ApplyMask;
#endif
        }
        
#if UNITY_EDITOR

        [SerializeField, HideInInspector]
        private Mask OldMask;
        private void OnValidate()
        {
            if (enabled) ApplyMask();

            if (OldMask != Mask)
            {
                if(OldMask != null) OldMask.OnMaterialChanged -= ApplyMask;
                OldMask = Mask;
            }
        }

#endif
    }
}
