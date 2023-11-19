using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class ConnectMeshes : MonoBehaviour
{
	private List<Vector3> firstMeshVertices, secondeMeshVertices;
	private List<int> firstTriIndices, secondeTriIndices;

	private Mesh firstFace, secondeFace;

	private int startIndex;

	void Awake()
	{
		SetFirstMeshVertices();
		SetSecondeMeshVertices();

		firstFace = new Mesh();
		firstFace.name = "First Mesh";

		secondeFace = new Mesh();
		secondeFace.name = "Seconde Mesh";


		SetFirstTriIndices();
		SetSecondeTriIndices();

		CombineMeshes();

		firstFace.SetVertices(firstMeshVertices);

		firstFace.subMeshCount = 2;

		//firstFace.SetSubMesh(0, new UnityEngine.Rendering.SubMeshDescriptor(0, firstMeshVertices.Count, MeshTopology.Triangles), UnityEngine.Rendering.MeshUpdateFlags.Default);
		//firstFace.SetSubMesh(1, new UnityEngine.Rendering.SubMeshDescriptor(startIndex, secondeMeshVertices.Count, MeshTopology.Triangles), UnityEngine.Rendering.MeshUpdateFlags.Default);

		firstFace.SetTriangles(firstTriIndices ,0);
		firstFace.SetTriangles(secondeTriIndices ,1);

		firstFace.RecalculateNormals();

		GetComponent<MeshFilter>().mesh = firstFace;
	}

	void CombineMeshes()
	{
		startIndex = firstMeshVertices.Count;

		firstMeshVertices.AddRange(secondeMeshVertices);
	}

	void SetFirstMeshVertices()
	{
		firstMeshVertices = new List<Vector3>();

		firstMeshVertices.Add(new Vector3(0, 0, 0));
		firstMeshVertices.Add(new Vector3(1, 0, 0));
		firstMeshVertices.Add(new Vector3(0, 0, 1));
		firstMeshVertices.Add(new Vector3(1, 0, 1));
	}

	void SetSecondeMeshVertices()
	{
		secondeMeshVertices = new List<Vector3>();

		secondeMeshVertices.Add(new Vector3(0, 0, 1));
		secondeMeshVertices.Add(new Vector3(1, 0, 1));
		secondeMeshVertices.Add(new Vector3(0, 1, 1));
		secondeMeshVertices.Add(new Vector3(1, 1, 1));
	}

	void SetFirstTriIndices()
	{
		firstTriIndices = new List<int>();

		firstTriIndices.Add(0);
		firstTriIndices.Add(2);
		firstTriIndices.Add(1);

		firstTriIndices.Add(2);
		firstTriIndices.Add(3);
		firstTriIndices.Add(1);
	}

	void SetSecondeTriIndices()
	{
		secondeTriIndices = new List<int>();

		secondeTriIndices.Add(0 + 4);
		secondeTriIndices.Add(2 + 4);
		secondeTriIndices.Add(1 + 4);

		secondeTriIndices.Add(2 + 4);
		secondeTriIndices.Add(3 + 4);
		secondeTriIndices.Add(1 + 4);
	}


}
