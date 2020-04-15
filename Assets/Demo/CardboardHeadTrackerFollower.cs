using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MobfishCardboard;
using UnityEngine.UI;

public class CardboardHeadTrackerFollower : MonoBehaviour
{
    [SerializeField]
    private Transform target;

    [SerializeField]
    private Text debugText;

    // Start is called before the first frame update
    void Start()
    {
        if (target == null)
        {
            target = transform;
        }

        CardboardHeadTracker.CreateTracker();
        CardboardHeadTracker.ResumeTracker();
    }

    // Update is called once per frame
    void Update()
    {
        CardboardHeadTracker.UpdatePoseGyro();
        if (!Application.isEditor)
            transform.localRotation = CardboardHeadTracker.trackerUnityRotation;
        Update_DebugInfo();
    }

    void Update_DebugInfo()
    {
        if (debugText != null)
        {
            debugText.text = string.Format("device rot={0}, \r\nUnity rot={1}",
                CardboardHeadTracker.trackerRawRotation.eulerAngles,
                CardboardHeadTracker.trackerUnityRotation.eulerAngles);
        }
    }
}
