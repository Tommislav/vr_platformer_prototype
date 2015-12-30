using UnityEngine;
using System.Collections;
using UnityEditor;

public class VRToggle : MonoBehaviour {

	[MenuItem("VR/Enable VR Mode")]
	public static void enableVR() {
		setVRMode(true);
	}

	[MenuItem("VR/Enable Editor Mode")]
	public static void enableEditor() {
		setVRMode(false);
	}

	private static void setVRMode(bool enabled) {

		Transform vrRoot = GameObject.Find("/CameraHolder").transform;
		Transform editorRoot = GameObject.Find("/World").transform;

		Transform vrCam = vrRoot.Find("OVRCameraRig");
		Transform editorCam = editorRoot.Find("Player/EditorCamera");

		if (vrCam == null) { throw new System.Exception("vrCam is null"); }
		if (editorCam == null) { throw new System.Exception("editorCam is null"); }


		PlayerSettings.virtualRealitySupported = enabled;
		vrCam.gameObject.SetActive(enabled);
		editorCam.gameObject.SetActive(!enabled);
	}

}
