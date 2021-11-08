using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class ComponentsContainer<T>
{
    private Dictionary<ID, T> m_Components = new Dictionary<ID, T>();
    [SerializeField] private List<T> m_ComponentsList = new List<T>();

    public T this[ID id]
    {
        get => m_Components[id];
        set => m_Components[id] = value;
    }

    public void Add(ID id, T component)
    {
        m_Components.Add(id, component);

        m_ComponentsList.Add(component);
    }

    public List<T> GetList()
    {
        return m_ComponentsList;
    }

    public T GetComponentByID(ID id)
    {
        return m_Components[id];
    }
}
