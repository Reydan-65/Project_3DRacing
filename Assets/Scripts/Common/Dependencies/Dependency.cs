using UnityEngine;

public abstract class Dependency : MonoBehaviour
{
    protected virtual void BindAll(MonoBehaviour monoBehaviourInScene) { }

    protected void FindAllObjectsToBind()
    {
        MonoBehaviour[] allMBInScene = FindObjectsOfType<MonoBehaviour>();

        for (int i = 0; i < allMBInScene.Length; i++)
        {
            BindAll(allMBInScene[i]);
        }
    }

    protected void Bind<T>(MonoBehaviour bindObject, MonoBehaviour target) where T : class
    {
        if (target is IDependency<T>) (target as IDependency<T>).Construct(bindObject as T);
    }
}
