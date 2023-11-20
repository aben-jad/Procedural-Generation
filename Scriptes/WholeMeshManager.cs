using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class WholeMeshManager : MonoBehaviour
{
	// this class is the manager of the Whole mesh
	
	List<Vector3> wholeVertices;

	Mesh wholeMesh;
	TerrainMeshManager terrain;
	PlayerMeshManager player;

	public int terrainWidthLength, terrainHeightLength;
	public Material firstMat, secondMat;

	void Awake()
	{
		wholeVertices = new();

		wholeMesh = new();

		terrain = new TerrainMeshManager(wholeVertices, 0, terrainWidthLength, terrainHeightLength);

		player = new PlayerMeshManager(wholeVertices, wholeVertices.Count, terrain.ConnectedVertices);

		terrain.AddNullUVs();

		wholeMesh.Clear();

		wholeMesh.SetVertices(wholeVertices);

		wholeMesh.subMeshCount = 2;

		wholeMesh.SetTriangles(terrain.TriIndices, 0);
		wholeMesh.SetTriangles(player.TriIndices, 1);


		wholeMesh.RecalculateNormals();

		GetComponent<MeshFilter>().mesh = wholeMesh;
		GetComponent<MeshRenderer>().materials = new Material[]{firstMat, secondMat};

		firstMat.SetFloat("_FracX", terrainWidthLength - 1);
		firstMat.SetFloat("_FracY", terrainHeightLength - 1);
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

		player.UpdateVertices();

		wholeMesh.Clear();

		wholeMesh.SetVertices(wholeVertices);

		wholeMesh.subMeshCount = 2;

		wholeMesh.SetTriangles(terrain.TriIndices, 0);

		wholeMesh.SetTriangles(player.TriIndices, 1);
		
		wholeMesh.SetUVs(0, terrain.UVs);

		wholeMesh.SetUVs(1, player.UVs);

		//print(wholeMesh.vertices.Length + " " + player.UVs.Count);

		wholeMesh.RecalculateNormals();


	}
}
