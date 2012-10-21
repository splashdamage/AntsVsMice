using UnityEngine;
using UnityEditor;
using System.Collections;

public class GenerateBoardMesh: ScriptableObject {
	[MenuItem("GameObject/Create Other/Create mesh")]
	public static void CreateMesh() {
		Mesh mesh = new Mesh();
		Vector3[] vertices = new Vector3[] {
			Vector3.zero, // lowerLeft
			new Vector3(0,0,1080), //upperLeft
			new Vector3(1920,0,1080), //upperRight
			new Vector3(1920,0,0), // lowerRight
		};
		Vector2[] uv = new Vector2[] {
			new Vector2(0,0),
			new Vector2(0,1),
			new Vector2(1,1),
			new Vector2(1,0)
		};
		mesh.vertices = vertices;
		mesh.uv = uv;
		mesh.triangles = new int[] {
			0,1,2,
			0,2,3,
		};
		mesh.RecalculateNormals();
		mesh.RecalculateBounds();
		AssetDatabase.CreateAsset(mesh, "Assets/AntsVsMice/Levels/boardMesh.asset");
		AssetDatabase.SaveAssets ();
	}
}
