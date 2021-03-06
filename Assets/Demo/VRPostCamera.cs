﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using MobfishCardboard;

namespace MobfishCardboardDemo
{
    public class VRPostCamera: MonoBehaviour
    {
        [SerializeField]
        private Material eyeMaterialLeft;
        [SerializeField]
        private Material eyeMaterialRight;

        private Camera postCam;

        private void Awake()
        {
            postCam = GetComponent<Camera>();
            postCam.projectionMatrix = Matrix4x4.identity;
        }

        private void OnPostRender()
        {
            if (CardboardManager.viewMeshLeft == null || CardboardManager.viewMeshRight == null)
                return;

            eyeMaterialLeft.mainTexture = CardboardManager.viewTextureLeft;
            eyeMaterialRight.mainTexture = CardboardManager.viewTextureRight;
            eyeMaterialLeft.SetPass(0);
            Graphics.DrawMeshNow(CardboardManager.viewMeshLeft, transform.position, transform.rotation);
            eyeMaterialRight.SetPass(0);
            Graphics.DrawMeshNow(CardboardManager.viewMeshRight, transform.position, transform.rotation);
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}