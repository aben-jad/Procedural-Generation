using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class ProcGeo : MonoBehaviour
{
	GameObject startPoint, endPoint, startTang, endTang;


	void Awake()
	{
		InitBezierPoints();

		Mesh mesh = new Mesh();
		mesh.name = "Test Mesh";

		List<Vector3> points = new List<Vector3>()
		{
			new Vector3(-1, 1),
			new Vector3(1, 1),
			new Vector3(-1, -1),
			new Vector3(1, -1)
			//new Vector3(-1, 1, 1),
			//new Vector3(1, 1, 1),
			//new Vector3(-1, -1, 1),
			//new Vector3(1, -1, 1)
		};

		List<Vector2> uvs = new List<Vector2>()
		{
			new Vector2(0, 1),
			new Vector2(1, 1),
			new Vector2(0, 0),
			new Vector2(1, 0)
			
		};

		int[] triIndices = new int[]
		{
			0, 2, 1,
			1, 2, 3
			
		};

		mesh.SetVertices(points);
		mesh.SetUVs(0, uvs);
		mesh.triangles = triIndices;

		mesh.RecalculateNormals();

		//GetComponent<MeshFilter>().mesh = mesh;
	}

	void InitBezierPoints()
	{
		startPoint = GameObject.CreatePrimitive(PrimitiveType.Sphere);
		startPoint.name = ("start Point");
		startPoint.transform.localScale = Vector3.one * .1f;
		startPoint.transform.parent = this.transform;

		endPoint = GameObject.CreatePrimitive(PrimitiveType.Sphere);
		endPoint.name = ("end Point");
		endPoint.transform.localScale = Vector3.one * .1f;
		endPoint.transform.parent = this.transform;

		startTang = GameObject.CreatePrimitive(PrimitiveType.Sphere);
		startTang.name = ("start Tang");
		startTang.transform.localScale = Vector3.one * .1f;
		startTang.transform.parent = this.transform;

		endTang = GameObject.CreatePrimitive(PrimitiveType.Sphere);
		endTang.name = ("end Tang");
		endTang.transform.localScale = Vector3.one * .1f;
		endTang.transform.parent = this.transform;
	}
}
