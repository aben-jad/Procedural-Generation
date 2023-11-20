using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class HoleMeshManager : MonoBehaviour
{
	// this class is the manager of the hole mesh
	
	List<Vector3> holeVertices;

	Mesh holeMesh;
	TerrainMeshManager terrain;
	PlayerMeshManager player;

	public int terrainWidthLength, terrainHeightLength;
	public Material firstMat, secondMat;

	void Awake()
	{
		holeVertices = new();

		holeMesh = new();

		terrain = new TerrainMeshManager(holeVertices, 0, terrainWidthLength, terrainHeightLength);

		player = new PlayerMeshManager(holeVertices, holeVertices.Count, terrain.ConnectedVertices);

		holeMesh.Clear();

		holeMesh.SetVertices(holeVertices);

		holeMesh.subMeshCount = 2;

		holeMesh.SetTriangles(terrain.TriIndices, 0);
		holeMesh.SetTriangles(player.TriIndices, 1);


		holeMesh.RecalculateNormals();

		GetComponent<MeshFilter>().mesh = holeMesh;
		GetComponent<MeshRenderer>().materials = new Material[]{firstMat, secondMat};
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.UpArrow) && terrain.HiddenFace + terrainWidthLength - 1 < (terrainWidthLength - 1) * (terrainHeightLength - 1))
		{
			terrain.HiddenFace += terrainWidthLength - 1;
		}

		if (Input.GetKeyDown(KeyCode.DownArrow) && terrain.HiddenFace >= terrainWidthLength - 1)
		{
			terrain.HiddenFace -= terrainWidthLength - 1;
		}

		if (Input.GetKeyDown(KeyCode.LeftArrow) && terrain.HiddenFace % (terrainWidthLength - 1) != 0)
		{
			terrain.HiddenFace -= 1;
		}

		if (Input.GetKeyDown(KeyCode.RightArrow) && terrain.HiddenFace % (terrainWidthLength - 1) != terrainWidthLength - 2)
		{
			terrain.HiddenFace += 1;
		}

		player.UpdateVertices();//terrain.ConnectedVertices);

		holeMesh.Clear();

		holeMesh.SetVertices(holeVertices);

		holeMesh.subMeshCount = 2;

		holeMesh.SetTriangles(terrain.TriIndices, 0);

		holeMesh.SetTriangles(player.TriIndices, 1);

		holeMesh.RecalculateNormals();


	}
}
