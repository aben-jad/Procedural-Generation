using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class TerainManager : MonoBehaviour
{
	public int xLength, zLength;
	

	private List<int> currentHiddenTriIndices;


	//private int[] triIndices;
	//
	Mesh mesh;

	TerrainMesh terrainMesh;
	PlayerMesh playerMesh;



	void Print(int[] _arr)
	{
		foreach (int i in _arr)
			print(i);
	}

	void Awake()
	{

		mesh = new Mesh();
		mesh.name = "Terrain Mesh";

		terrainMesh = new TerrainMesh(xLength, zLength, (xLength - 1) / 2 + ((xLength - 1) * ((zLength - 1) / 2)));

		playerMesh = new PlayerMesh(terrainMesh.ConnectedVertices, terrainMesh.Vertices.Count);

		terrainMesh.Vertices.AddRange(terrainMesh.ConnectedVertices);
		mesh.SetVertices(terrainMesh.Vertices);

		mesh.subMeshCount = 2;

		mesh.SetTriangles(terrainMesh.TriIndices, 0);
		mesh.SetTriangles(playerMesh.triIndices, 1);

		mesh.RecalculateNormals();

		GetComponent<MeshFilter>().mesh = mesh;
	}

	void OnDrawGizmos()
	{
		//return ;

		for (int i = 0; i < terrainMesh.Vertices.Count; i++)
		{
			UnityEditor.Handles.Label(terrainMesh.Vertices[i], i.ToString());
	//		mesh.SetTriangles(triIndices.GetRange(0, i), 0);
	//		mesh.RecalculateNormals();
	//		yield return new WaitForSeconds(1f);
		}
	
		//UnityEditor.Handles.Label(playerPosition, currentHiddenFace.ToString());
	}

	private void UpdateTriIndices()
	{
		if (Input.GetKeyDown(KeyCode.UpArrow) && terrainMesh.HiddenFace + xLength - 1 < (xLength - 1) * (zLength - 1))
		{
			terrainMesh.HiddenFace += xLength - 1;
		}

		if (Input.GetKeyDown(KeyCode.DownArrow) && terrainMesh.HiddenFace >= xLength - 1)
		{
			terrainMesh.HiddenFace -= xLength - 1;
		}

		if (Input.GetKeyDown(KeyCode.LeftArrow) && terrainMesh.HiddenFace % (xLength - 1) != 0)
		{
			terrainMesh.HiddenFace -= 1;
		}

		if (Input.GetKeyDown(KeyCode.RightArrow) && terrainMesh.HiddenFace % (xLength - 1) != xLength - 2)
		{
			terrainMesh.HiddenFace += 1;
		}

		mesh.Clear();

		mesh.SetVertices(terrainMesh.Vertices);//.AddRange(terrainMesh.ConnectedVertices));

		mesh.SetTriangles(terrainMesh.TriIndices, 0);
		mesh.SetTriangles(playerMesh.triIndices, 1);

		mesh.RecalculateNormals();


	}



	void Update()
	{
		UpdatMesh();
	}

	void UpdatMesh()
	{
		UpdateTriIndices();
	}


}
