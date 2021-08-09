using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Worq
{
	[DisallowMultipleComponent]
	public class Waypoint : MonoBehaviour
	{
		private string waypointName;

		public void createNewWaypoint (GameObject go)
		{
			int len = 0;
			for (int i = 0; i < go.transform.childCount; i += 1) {
				if (go.transform.GetChild (i).GetComponent <WaypointIdentifier> ())
					len++;
			}
			waypointName = "waypoint " + (len + 1);

			Shader mShader = Shader.Find ("Standard");
			Material mMat = new Material (mShader);
			mMat.color = go.GetComponent <WaypointRoute> ().groupColor;

			GameObject waypoint = GameObject.CreatePrimitive (PrimitiveType.Sphere);
			waypoint.transform.SetParent (go.transform);
			waypoint.transform.localScale = new Vector3 (0.5f, 0.5f, 0.5f);
			waypoint.transform.localPosition = new Vector3 (Random.Range (-10f, 10f), go.transform.localScale.y, Random.Range (-10f, 10f));
			waypoint.name = waypointName;
			waypoint.GetComponent<Renderer> ().material = mMat;
			waypoint.GetComponent <Collider> ().isTrigger = true;
			waypoint.AddComponent <WaypointIdentifier> ();
		}
	}
}