
#pragma strict
public var speed : float;
public var next : GameObject;

public var character : Transform;
public var points : GameObject[];
function Start() 
{
points= GameObject.FindGameObjectsWithTag("gopoint");
System.Array.Sort(points,function(a,b)
{
return a.name.CompareTo(b.name);
}
);
transform.position=points[00].transform.position;
}
function Update () 
{
if(next==null)
{
next = points[1];
transform.LookAt(next.transform);
}
var distance=(next.transform.position - transform.position).magnitude;
if(distance>0.01)
{
transform.LookAt(next.transform);
transform.Translate(Vector3.forward*speed*Time.deltaTime);
}
else if(next != points[points.Length-1])
{
next=points[System.Array.IndexOf(points,next)+1];
}
}