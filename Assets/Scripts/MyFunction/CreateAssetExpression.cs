using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

[CreateAssetMenu(menuName = "MyFunction/CreateAsset/CreateExpression")]
public class CreateAssetExpression : ScriptableObject
{
	public List<Sprite> expression;
}
