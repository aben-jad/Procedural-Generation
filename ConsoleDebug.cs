using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;

public class ConsoleDebug
{
	public static void ClearLog()
	{
		var assembly = Assembly.GetAssembly(typeof(UnityEditor.Editor));
		var type = assembly.GetType("UnityEditor.LogEntries");
		var method = type.GetMethod("Clear");
		method.Invoke(new object(), null);
	}
}
