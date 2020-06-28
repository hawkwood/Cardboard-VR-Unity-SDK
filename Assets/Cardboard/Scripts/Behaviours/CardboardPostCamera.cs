using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace MobfishCardboard
{
    public class CardboardPostCamera: MonoBehaviour
    {
        [SerializeField]
        private Material eyeMaterialLeft;
        [SerializeField]
        private Material eyeMaterialRight;

        private Camera postCam;

        private void Awake()
        {
            postCam = GetComponent<Camera>();
            postCam.projectionMatrix = Matrix4x4.Ortho(-1, 1, -1, 1, -0.1f, 0.5f);
        }

        private void OnEnable()
        {
            ApplyRenderTexture();
            CardboardManager.renderTextureResetEvent += ApplyRenderTexture;

            if (GraphicsSettings.renderPipelineAsset != null)
            {
#if UNITY_2019_1_OR_NEWER
                RenderPipelineManager.endCameraRendering += OnSrpCameraPostRender;
#endif
            }
            else
            {
                Camera.onPostRender += OnCameraPostRender;
            }
        }

        private void OnDisable()
        {
            CardboardManager.renderTextureResetEvent -= ApplyRenderTexture;

            if (GraphicsSettings.renderPipelineAsset != null)
            {
#if UNITY_2019_1_OR_NEWER
                RenderPipelineManager.endCameraRendering -= OnSrpCameraPostRender;
#endif
            }
            else
            {
                Camera.onPostRender -= OnCameraPostRender;
            }
        }

        private void OnCameraPostRender(Camera cam)
        {
            TryDraw();
        }

#if UNITY_2019_1_OR_NEWER
        protected virtual void OnSrpCameraPostRender(ScriptableRenderContext context, Camera givenCamera)
        {
            TryDraw();
        }
#endif

        private void TryDraw()
        { 
            if (!CardboardManager.profileAvailable)
                return;

            eyeMaterialLeft.SetPass(0);
            Graphics.DrawMeshNow(CardboardManager.viewMeshLeft, transform.position, transform.rotation);
            eyeMaterialRight.SetPass(0);
            Graphics.DrawMeshNow(CardboardManager.viewMeshRight, transform.position, transform.rotation);
        }

        private void ApplyRenderTexture()
        {
            eyeMaterialLeft.mainTexture = CardboardManager.viewTextureLeft;
            eyeMaterialRight.mainTexture = CardboardManager.viewTextureRight;
        }
    }
}