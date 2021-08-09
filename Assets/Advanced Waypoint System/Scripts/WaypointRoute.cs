using UnityEngine;

namespace Worq
{
	[RequireComponent (typeof(WaypointRouteIdentifier))]
	[DisallowMultipleComponent]
	public class WaypointRoute : MonoBehaviour
	{
		public Color groupColor = Color.yellow;
		[HideInInspector]
		public int groupID = 0;
		string groupName;
		private int uniqueID;
		private AWSManager manager;
		
		public void CreateRoute ()
		{
			manager = GameObject.FindObjectOfType<AWSManager> ();

			int len = GameObject.FindObjectsOfType <WaypointRoute> ().Length;
			groupName = "Route " + (len + 1);

			GameObject go = new GameObject ();
			go.name = groupName;
			go.AddComponent <WaypointRouteIdentifier> ();
			go.AddComponent <WaypointRoute> ();

//			Vector3 pos = Camera.main.transform.TransformPoint (Vector3.forward * 1);
//			go.transform.position = pos;

			go.transform.SetParent (manager.transform);

		}

		void OnDrawGizmos ()
		{
			
			int waypointCount = 0;
			Transform groupTransform = transform;
			Transform[] waypoints;

			int childrenCount = groupTransform.childCount;

			for (int i = 0; i < childrenCount; i++) {
				if (groupTransform.GetChild (i).GetComponent <WaypointIdentifier> ()) {
					waypointCount += 1;
				}
			}
			waypoints = new Transform[waypointCount];
			int curIndex = 0;
			for (int i = 0; i < childrenCount; i++) {
				if (groupTransform.GetChild (i).GetComponent <WaypointIdentifier> ()) {
					waypoints [curIndex] = groupTransform.GetChild (i);
					curIndex++;
				}
			}

			if (null == waypoints || waypoints.Length <= 1)
				return;
			Gizmos.color = groupColor;

			manager = transform.parent.GetComponent<AWSManager>();
			if (waypoints.Length > 1 && manager.drawGizmos) {
				for (int i = 0; i < waypoints.Length; i += 1) {
					Gizmos.DrawLine (waypoints [i % waypoints.Length].position, waypoints [(i + 1) % waypoints.Length].position);
//					Debug.Log ("Drawing Gizmo from " + i % waypoints.Length + " to " + (i + 1) % waypoints.Length);
				}
			}
		}
	}
}