
///https://github.com/qiwucwb/UnityGradientTextureAsset
///@yeer

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
#if UNITY_2020_3_OR_NEWER
using UnityEditor.AssetImporters;
#else
using UnityEditor.Experimental.AssetImporters;
#endif



namespace Yeer.GradientTextureAsset {
    [ScriptedImporter(1, "ygradient")]
    public class GradientTextureAssetImporter : ScriptedImporter
    {
        public Gradient gradient = new Gradient() { colorKeys=new GradientColorKey[] { new GradientColorKey(Color.red,0) , new GradientColorKey(Color.blue, 1) } };
        

        public enum EType
        {
            Horizontal = 1,
            Vertical = 2,
        }

        public EType type = EType.Horizontal;

        public bool flip = false;

        [Min(1)]
        [Tooltip("Width of the gradient")]
        public int width = 256;
        [Min(1)]
        [Tooltip("Height of the gradient")]
        public int height = 8;

        [Header("Texture Setting")]
        public TextureWrapMode textureWrapMode = TextureWrapMode.Clamp;
        public FilterMode textureFilterMode = FilterMode.Bilinear;
        public TextureFormat textureFormat = TextureFormat.ARGB32;
        public bool textureMipChain = true;
        public override void OnImportAsset(AssetImportContext ctx)
        {
            Texture2D tempTexture;
            if (type == EType.Horizontal)
            {
                tempTexture = new Texture2D(width, height, textureFormat, textureMipChain);
                for (int x = 0; x < width; x++)
                {
                    var v = (x * 1.0f / width);
                    if (flip) v = 1 - v;
                    Color color = gradient.Evaluate(v);
                    for (int y = 0; y < height; y++)
                    {

                        tempTexture.SetPixel(x, y, color);
                    }
                }
            }
            else if (type == EType.Vertical)
            {
                tempTexture = new Texture2D(height, width, textureFormat, textureMipChain);
                for (int x = 0; x < width; x++)
                {
                    var v = (x * 1.0f / width);
                    if (!flip) v = 1 - v;
                    Color color = gradient.Evaluate(v);
                    for (int y = 0; y < height; y++)
                    {

                        tempTexture.SetPixel(y, x, color);
                    }
                }
            }
            else
            {
                tempTexture = new Texture2D(width, height, textureFormat, textureMipChain);
                //TODO
            }
           
            tempTexture.wrapMode = textureWrapMode;
            tempTexture.filterMode = textureFilterMode;

            tempTexture.Apply();
            ctx.AddObjectToAsset("texture", tempTexture);
            ctx.SetMainObject(tempTexture);
        }
    }


    //[CustomEditor(typeof(GrandientTextureAssetImporter)), CanEditMultipleObjects]
    //public class AsepriteImporterEditor : ScriptedImporterEditor
    //{
    //    public override void OnInspectorGUI()
    //    {
    //        EditorGUI.LabelField(new Rect(0, 0, 100, 50), "12321");

    //    }

    //    //private void PivotPropField()
    //    //{
    //    //    var prop = GetProp(nameof(AsepriteImporterSettings.Pivot));
    //    //    EditorGUILayout.PropertyField(prop);
    //    //    if (((SpriteAlignment)prop.enumValueIndex) == SpriteAlignment.Custom)
    //    //    {
    //    //        NoLabelPropField("_pivot");
    //    //    }
    //    //}



    //}
}
