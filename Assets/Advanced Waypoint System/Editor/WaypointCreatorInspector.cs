using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Worq.UUtility
{
	[DisallowMultipleComponent]
	[CustomEditor (typeof(WaypointRoute))]
	public class WaypointCreatorInspector : Editor
	{
		#region Variables

		public int quantity = 0;

		#endregion

		public override void OnInspectorGUI ()
		{
			
			base.OnInspectorGUI ();

//			if (AWSPatrol.drawGizmos) {
				
			GameObject parent = null;
			WaypointRouteIdentifier[] allGroups = GameObject.FindObjectsOfType<WaypointRouteIdentifier> ();

			if (GUILayout.Button ("Add New Waypoint")) {
				SetupManager ();
				foreach (WaypointRouteIdentifier t in allGroups) {

					for (int i = 0; i < targets.Length; i++) {
						if (t.name.Equals (this.targets [i].name)) {
							parent = t.gameObject;
							new Waypoint ().createNewWaypoint (parent);
							return;
						}
					}
				}
			}

			GUILayout.BeginHorizontal ();
			{
				quantity = EditorGUILayout.IntField ("", quantity);

				if (GUILayout.Button ("Mass Add Waypoints")) {

					foreach (WaypointRouteIdentifier t in allGroups) {
						
						for (int i = 0; i < targets.Length; i++) {
							if (t.name.Equals (this.targets [i].name)) {
								parent = t.gameObject;
								for (int j = 0; j < quantity; j += 1) {
									new Waypoint ().createNewWaypoint (parent);
								}
								return;
							}
						}
					}

//					for (int i = 0; i < quantity; i += 1) {
//						new Waypoint ().createNewWaypoint (parent);
//					}
				}
			}
			GUILayout.EndHorizontal ();
		}
		
		//		}

		private static void SetupManager ()
		{
			if (GameObject.FindObjectOfType<AWSManager> ()) {
				return;
			} else {
				GameObject go = new GameObject ();
				go.AddComponent <AWSManager> ();
				go.name = "_AWSManager";
				go.transform.position = Vector3.zero;
			}
		}

	}
}