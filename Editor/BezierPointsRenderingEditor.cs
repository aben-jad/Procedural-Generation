using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(BezierPointsRendering))]
public class BezierPointsRenderingEditor : Editor
{
	enum FunctionsDrawed
	{
		none,
		sphere,
		index
	}


	BezierPointsRendering myObject;
	int currentVertex;

	float radius;

	int segs, rings;

	int startLine, endLine;

	FunctionsDrawed currentFunction;


	Dictionary<FunctionsDrawed, Action> drawerFunction;
	
	void OnEnable()
	{
		myObject = target as BezierPointsRendering;
		currentFunction = FunctionsDrawed.none;
		drawerFunction = new();
		drawerFunction.Add(FunctionsDrawed.none, null);
		drawerFunction.Add(FunctionsDrawed.sphere, DrawVerticesAsSphere);
		drawerFunction.Add(FunctionsDrawed.index, DrawVerticesAsIndexes);
	}

	void OnDisable()
	{
		myObject = null;
	}

	public override void OnInspectorGUI()
	{
		currentVertex = EditorGUILayout.IntField("i : ", currentVertex);
		segs = EditorGUILayout.IntField("segs : ", segs);
		rings = EditorGUILayout.IntField("rings : ", rings);
		radius = EditorGUILayout.FloatField("radius : ", radius);
		startLine = EditorGUILayout.IntField("start Line", startLine);
		endLine = EditorGUILayout.IntField("end Line", endLine);

		currentFunction = (FunctionsDrawed)EditorGUILayout.EnumPopup("current Function", currentFunction);
	}

	void OnSceneGUI()
	{
		var arr = GetVertices();

		drawerFunction[currentFunction]?.Invoke();

		//DrawLine(arr);
	}

	void DrawLine(Vector3[] _vertices)
	{
		Handles.color = Color.blue;
		Handles.DrawLine(_vertices[startLine], _vertices[endLine]);
	}


	void DrawVerticesAsIndexes()
	{
		Vector3[] arr;
		if (myObject.mesh == null)
			arr = GetVertices();
		else
			arr = myObject.mesh.vertices;


		Handles.color = Color.red;
		for (int i = 0; i < arr.Length; i++)
			Handles.Label(arr[i], i.ToString());
			//Handles.SphereHandleCap(0, arr[i], Quaternion.identity, .01f, EventType.Repaint);

		//Handles.color = Color.red;
		//if (currentVertex < arr.Length)
		//	Handles.Label(arr[currentVertex], currentVertex.ToString());
			//Handles.SphereHandleCap(0, arr[currentVertex], Quaternion.identity, .01f, EventType.Repaint);
	}


	void DrawVerticesAsSphere()
	{
		Vector3[] arr;
		if (myObject.mesh == null)
			arr = GetVertices();
		else
			arr = myObject.mesh.vertices;


		Handles.color = Color.white;
		for (int i = 0; i < arr.Length; i++)
			Handles.SphereHandleCap(0, arr[i], Quaternion.identity, .01f, EventType.Repaint);

		Handles.color = Color.red;
		if (currentVertex < arr.Length)
			Handles.SphereHandleCap(0, arr[currentVertex], Quaternion.identity, .01f, EventType.Repaint);
	}


	Vector3[] GetVertices()
	{
		Vector3[] vertices = new Vector3[segs * (rings - 1) + 2];

		for (int i = 0; i <= rings; i++)
		{
			float pi = Mathf.PI;
			float deltaTheta = pi / (rings);
			float deltaAlpha = (2 * pi ) / segs;
			if (i == 0)
				vertices[0] = new Vector3(0, -1, 0) * radius;
			else if(i == rings)
				vertices[vertices.Length - 1] = new Vector3(0, 1, 0) * radius;
			else
			{
				for (int j = 1; j <= segs; j++)
					vertices[j + ((i - 1) * segs)] = 
						new Vector3(Mathf.Cos((deltaTheta * i) - pi / 2) * Mathf.Cos(deltaAlpha * (j - 1)),
						Mathf.Sin((deltaTheta * i) - pi / 2), 
						Mathf.Cos((deltaTheta * i) - pi / 2) * Mathf.Sin(deltaAlpha * (j - 1))) * radius;
			}
		}

		return vertices;
	}
}
