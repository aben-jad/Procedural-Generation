using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class BezierPointsRendering : MonoBehaviour
{
	//[SerializeField]
	public Mesh mesh;

	void Awake()
	{
		mesh = MyMesh.GenerateSphereMesh();
		GetComponent<MeshFilter>().mesh = mesh;

	}

	//void OnDrawGizmos()
	//{
	//	var arr = mesh.vertices;
	//	for (int i = 0; i < mesh.vertexCount; i++)
	//		Gizmos.DrawSphere(arr[i], .01f);
	//}
}
