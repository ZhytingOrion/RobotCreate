using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

[ExecuteInEditMode]
public class OutlineCommandBuffer : MonoBehaviour
{

    private CommandBuffer commandBuffer = null;
    public Material renderMat = null;
    private RenderTexture renderTex = null;
    public Material outlineMat = null;

    private Renderer targetRender = null;
    public GameObject targetObj = null;

    public Color outlineColor = Color.blue;
    public int outlineWidth = 1;

    // Start is called before the first frame update
    void Start()
    {
        if(renderMat!=null && outlineMat != null)
        {
            commandBuffer = new CommandBuffer();
            renderTex = RenderTexture.GetTemporary(Screen.width, Screen.height, 0);
            commandBuffer.SetRenderTarget(renderTex);
            commandBuffer.ClearRenderTarget(true, true, Color.black);
            renderMat.SetColor("_Color", outlineColor);

            outlineMat.SetColor("_Color", outlineColor);
            outlineMat.SetFloat("_OutlineWidth", outlineWidth);

            if (targetObj != null)
                OnChangeRenderObj(targetObj);
        }
        else
        {
            enabled = false;
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            OnChangeRenderObj(targetObj);
        }
    }

    private void OnChangeRenderObj(GameObject obj)
    {
        commandBuffer.ClearRenderTarget(true, true, Color.black);
        if (obj == null)
        {
            this.targetObj = null;
            enabled = false;
            return;
        }
        this.targetObj = obj;
        this.targetRender = obj.GetComponent<Renderer>();
        if(targetRender!=null)
            commandBuffer.DrawRenderer(targetRender, renderMat);
        Renderer[] renderers = obj.GetComponentsInChildren<Renderer>();
        for(int i = 0; i<renderers.Length; ++i)
        {
            commandBuffer.DrawRenderer(renderers[i], renderMat);
        }
        enabled = true;
    }

    private void OnDestroy()
    {
        if(renderTex)
        {
            RenderTexture.ReleaseTemporary(renderTex);
            renderTex = null;
        }
        if (commandBuffer != null)
        {
            commandBuffer.Release();
            commandBuffer = null;
        }
    }

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if((renderMat && renderTex && commandBuffer != null) && targetObj != null)
        {
            Graphics.ExecuteCommandBuffer(commandBuffer);

            RenderTexture temp = RenderTexture.GetTemporary(source.width, source.height, 0);
            outlineMat.SetTexture("_OutlineTex", renderTex);
            Graphics.Blit(renderTex, temp, outlineMat, 0);  //提取轮廓

            outlineMat.SetTexture("_OutlineTex", temp);
            Graphics.Blit(source, destination, outlineMat, 1);  //叠加

            RenderTexture.ReleaseTemporary(temp);
        }
        else
        {
            Graphics.Blit(source, destination);
        }
    }
}
