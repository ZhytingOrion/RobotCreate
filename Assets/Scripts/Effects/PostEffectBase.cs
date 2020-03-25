using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
public class PostEffectBase : MonoBehaviour
{
    public Shader effectShader = null;
    private Material _material = null;
    public Material PostMat
    {
        get
        {
            if (_material == null)
                _material = CreateMaterial(effectShader);
            return _material;
        }
    }

    protected Material CreateMaterial(Shader shader)
    {
        if (shader == null)
            return null;
        if (!shader.isSupported)
            return null;
        Material mat = new Material(shader);
        mat.hideFlags = HideFlags.DontSave;
        if (mat)
            return mat;
        return null;
    }
}
