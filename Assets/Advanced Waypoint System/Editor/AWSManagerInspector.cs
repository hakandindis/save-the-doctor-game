using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Worq
{
	[DisallowMultipleComponent]
	[CustomEditor (typeof(AWSManager))]
	public class AWSManagerInspector : Editor
	{
		public override void OnInspectorGUI ()
		{
			base.OnInspectorGUI ();

			if (GUILayout.Button ("Auto Assign Entities To Routes")) {
				
				AWSEntityIdentifier[] entities = FindObjectsOfType<AWSEntityIdentifier> ();
				WaypointRoute[] groups = FindObjectsOfType<WaypointRoute> ();

				//more groups than entities
				if (groups.Length >= entities.Length) {
					for (int i = 0; i < entities.Length; i += 1) {
						entities [i].gameObject.GetComponent <AWSPatrol> ().group = groups [i];
					}
				} else {
					//more entities than groups
					for (int i = 0; i < entities.Length; i += 1) {
						entities [i].gameObject.GetComponent <AWSPatrol> ().group = groups [i % groups.Length];
					}
				}

			}
		}

		public static void setupEntity ()
		{
			GameObject go = new GameObject ();	
			go.AddComponent <AWSEntityIdentifier> ();
			go.AddComponent <AWSPatrol> ();

		}
	}
}