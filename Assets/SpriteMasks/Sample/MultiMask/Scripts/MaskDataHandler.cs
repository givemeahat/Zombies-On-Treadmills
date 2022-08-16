using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace SpriteMask
{
    public readonly struct MaskDataHandler
    {
        private readonly IMultiMaskData _maskData;

        public bool IsEmpty => _maskData.IsEmpty;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public MaskDataHandler(IMultiMask mask)
        {
            _maskData = new MultiMaskData(mask);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Update()
        {
            _maskData.UpdateState();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void AddMask(IMultiMask mask)
        {
            _maskData.AddMask(mask);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool RemoveMask(IMultiMask mask, IMultiMaskData maskData)
        {
            maskData.RemoveMask(mask);

            if (maskData.IsEmpty)
            {
                MultiMaskHandler.MaterialCancellation(maskData.Type, maskData.Material);
                maskData.Destroy();
                return true;
            }

            return false;
        }
    }
}
