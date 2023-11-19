using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class QuadRing : MonoBehaviour
{
	public float innerRad, thickRad;

	public int segs;

	void Awake()
	{
		List<Vector3> points = new List<Vector3>();
		int[] triIndices = new int[(segs) * 12];

		SetPoints(points);
		SetTriIndices(ref triIndices);

		Mesh mesh = new Mesh();
		mesh.name = "Test Mesh";

		mesh.SetVertices(points);
		//mesh.SetUVs(0, uvs);
		mesh.triangles = triIndices;

		mesh.RecalculateNormals();

		GetComponent<MeshFilter>().mesh = mesh;


	}

	void SetPoints(List<Vector3> _points)
	{
		float delta = 2 * Mathf.PI / (float)segs;

		for (int i = 0; i < segs; i++)
		{
			_points.Add(new Vector3(Mathf.Cos((float)i * delta), Mathf.Sin((float)i * delta)) * innerRad);
			_points.Add(new Vector3(Mathf.Cos((float)i * delta), Mathf.Sin((float)i * delta)) * (innerRad + thickRad));
		}
	}

	void SetTriIndices(ref int[] _triIndices)
	{
		for (int i = 0; i < segs; i++)
		{
			_triIndices[6 * i] = 2 * i;
			_triIndices[6 * i + 1] = (2 * i + 1) % (segs * 2);
			_triIndices[6 * i + 2] = (2 * i + 3) % (segs * 2);
			_triIndices[6 * i + 3] = (2 * i) % (segs * 2);
			_triIndices[6 * i + 4] = (2 * i + 3) % (segs * 2);
			_triIndices[6 * i + 5] = (2 * i + 2) % (segs * 2);
		}
	}
}
