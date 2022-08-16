using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace SpriteMask
{
    public static class MultiMaskHandler
    {
        private static readonly Dictionary<Texture2D, MaskDataHandler>[] MaskClusters = 
        {
            new Dictionary<Texture2D, MaskDataHandler>(64),
            new Dictionary<Texture2D, MaskDataHandler>(64),
            new Dictionary<Texture2D, MaskDataHandler>(64),
            new Dictionary<Texture2D, MaskDataHandler>(64),
            
            new Dictionary<Texture2D, MaskDataHandler>(64),
            new Dictionary<Texture2D, MaskDataHandler>(64),
            new Dictionary<Texture2D, MaskDataHandler>(64),
            new Dictionary<Texture2D, MaskDataHandler>(64),
        };

        private static readonly RendererCluster[] RendererClusters =
        {
            new RendererCluster(new List<MultiMaskedSprite>(), new Material[0]),
            new RendererCluster(new List<MultiMaskedSprite>(), new Material[0]),
            new RendererCluster(new List<MultiMaskedSprite>(), new Material[0]),
            new RendererCluster(new List<MultiMaskedSprite>(), new Material[0]),
            
            new RendererCluster(new List<MultiMaskedSprite>(), new Material[0]),
            new RendererCluster(new List<MultiMaskedSprite>(), new Material[0]),
            new RendererCluster(new List<MultiMaskedSprite>(), new Material[0]),
            new RendererCluster(new List<MultiMaskedSprite>(), new Material[0]),
        };

        internal static void UpdateMask()
        {
            foreach (var cluster in MaskClusters)
            {
                foreach (var mask in cluster)
                {
                    mask.Value.Update();
                }
            }
        }

        public static void MaskRegistration(IMultiMask mask)
        {
            if(mask.Sprite == null) return;
            #if UNITY_EDITOR
            if (!Application.isPlaying) MultiMaskUpdater.Init();
            #endif
            
            var masks = MaskClusters[(int)mask.Type];

            if (!masks.TryGetValue(mask.Sprite.texture, out var maskHandler))
            {
                maskHandler = new MaskDataHandler(mask);
                masks.Add(mask.Sprite.texture, maskHandler);
            }

            maskHandler.AddMask(mask);
        }
        
        public static void MaskCancellation(IMultiMask mask)
        {
            var maskData = mask.MultiMaskData;
            if (maskData == null) return;
            mask.MultiMaskData = null;
            
            var masks = MaskClusters[(int)maskData.Type];
            
            if (masks.TryGetValue(maskData.Texture, out var handler))
            {
                if (handler.RemoveMask(mask, maskData))
                {
                    masks.Remove(maskData.Texture);
                }
            }
        }
        
        internal static void MaterialRegistration(MaskType type, Material material)
        {
            var index = (int)type;
            RendererClusters[index].AddMaterial(material);
            
            var materials = RendererClusters[index].Materials;
            
            foreach (var sprite in RendererClusters[index].MaskedSprites)
            {
                sprite.Renderer.sharedMaterials = materials;
            }
        }
        
        internal static void MaterialCancellation(MaskType type, Material material)
        {
            var index = (int)type;
            RendererClusters[index].RemoveMaterial(material);

            var materials = GetMaterials(type);
            
            foreach (var sprite in RendererClusters[index].MaskedSprites)
            {
                sprite.Renderer.sharedMaterials = materials;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SpriteRegistration(MultiMaskedSprite sprite)
        {
#if UNITY_EDITOR
            if (!Application.isPlaying) MultiMaskUpdater.Init();
#endif
            RendererClusters[(int)sprite.MaskType].MaskedSprites.Add(sprite);
            sprite.Renderer.sharedMaterials = GetMaterials(sprite.MaskType);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SpriteCancellation(MultiMaskedSprite sprite)
        {
            RendererClusters[(int)sprite.MaskType].MaskedSprites.Remove(sprite);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Material[] GetMaterials(MaskType type)
        {
            var index = (int)type;
            if (RendererClusters[index].Materials.Length == 0)
            {
                return new [] { MaskMaterialHandler.DefaultMultiMaskMaterials[(int)type] };
            }
            else return RendererClusters[index].Materials;
        }

        private struct RendererCluster
        {
            public readonly List<MultiMaskedSprite> MaskedSprites;
            public Material[] Materials;

            public RendererCluster(List<MultiMaskedSprite> maskedSprites, Material[] materials)
            {
                MaskedSprites = maskedSprites;
                Materials = materials;
            }

            public void AddMaterial(Material material)
            {
                var lenght = Materials.Length;
                var newMaterials = new Material[lenght + 1];
                Array.Copy(Materials, newMaterials, lenght);
                newMaterials[lenght] = material;

                Materials = newMaterials;
            }

            public void RemoveMaterial(Material material)
            {
                var newLenght = Materials.Length - 1;
                var index = Array.IndexOf(Materials, material);
                Materials[index] = Materials[newLenght];


                var newMaterials = new Material[newLenght];
                Array.Copy(Materials, newMaterials, newLenght);

                Materials = newMaterials;
            }
        }
    }
}
