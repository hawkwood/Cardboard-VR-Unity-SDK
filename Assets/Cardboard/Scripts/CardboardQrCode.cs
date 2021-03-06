﻿#if (UNITY_IOS || UNITY_ANDROID) && !UNITY_EDITOR
#define NATIVE_PLUGIN_EXIST
#endif

using System;
using System.Runtime.InteropServices;
using UnityEngine;

namespace MobfishCardboard
{
    public static class CardboardQrCode
    {
        private static IntPtr _encodedDeviceParams;
        private static int _paramsSize;
        private static byte[] encodedBytes;

        #if NATIVE_PLUGIN_EXIST
        [DllImport(CardboardUtility.DLLName)]
        private static extern void CardboardQrCode_scanQrCodeAndSaveDeviceParams();

        //todo is this correct?
        //https://developers.google.com/cardboard/reference/c/group/qrcode-scanner#cardboardqrcode_getsaveddeviceparams
        [DllImport(CardboardUtility.DLLName)]
        private static extern void CardboardQrCode_getSavedDeviceParams(ref IntPtr encoded_device_params, ref int size);

        #else

        private static void CardboardQrCode_scanQrCodeAndSaveDeviceParams()
        {
        }

        private static void CardboardQrCode_getSavedDeviceParams(ref IntPtr encoded_device_params, ref int size)
        {
            size = 0;
        }

        #endif

        public static void StartScanQrCode()
        {
            CardboardQrCode_scanQrCodeAndSaveDeviceParams();
        }

        public static void RetrieveDeviceParam()
        {
            CardboardQrCode_getSavedDeviceParams(ref _encodedDeviceParams, ref _paramsSize);

            Debug.Log("Feature Test RetrieveDeviceParam size=" + _paramsSize);
            encodedBytes = ReadByteArray(_encodedDeviceParams, _paramsSize);
            Debug.Log("Feature Test RetrieveDeviceParam params length=" + encodedBytes.Length + ", byte=\r\n " +
                string.Join(" , ", encodedBytes));

        }

        public static (IntPtr, int) GetDeviceParamsPointer()
        {
            return (_encodedDeviceParams, _paramsSize);
        }

        public static (byte[], int) GetDeviceParamsByte()
        {
            return (encodedBytes, _paramsSize);
        }

        private static byte[] ReadByteArray(IntPtr pointer, int size)
        {
            var result = new byte[size];
            if (size > 0)
                Marshal.Copy(pointer, result, 0, size);
            return result;
        }

    }
}