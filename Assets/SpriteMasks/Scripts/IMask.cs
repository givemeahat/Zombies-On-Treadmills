using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SpriteMask
{
    public interface IMask
    {
        Transform Transform { get; }
        Sprite Sprite { get; }

        MaskType Type { get; }
        float AlphaCutoff { get; }
    }
}
