using CQM.Databases;
using System.Collections;
using UnityEngine;

public class InventorySystem : ISystemEvents
{
    private InventoryData _inventoryData;

    public void RegisterEvents(out int sysID, out EventSys commands, out EventSys callbacks)
    {
        commands = new EventSys();
        callbacks = new EventSys();
        sysID = "inventory_sys".GetHashCode();

        var evt = commands.AddEvent<ItemData>("add_cookie".GetHashCode());
        evt.OnInvoked += (args) => AddCookieToInventory(args.m_ItemID, args.m_Amount);
        evt = commands.AddEvent<ItemData>("remove_cookie".GetHashCode());
        evt.OnInvoked += (args) => RemoveCookieFromInventory(args.m_ItemID, args.m_Amount);
    }

    public void Initialize(InventoryData data)
    {
        _inventoryData = data;
    }

    private void AddCookieToInventory(int cookieID, int amount)
    {
        InventoryItem item = _inventoryData.m_Cookies.Find(i => i.m_ItemID == cookieID);
        if (item != null)
            item.m_Amount += amount;
        else
            _inventoryData.m_Cookies.Add(new InventoryItem(cookieID, amount));
    }

    private void RemoveCookieFromInventory(int cookieID, int amount)
    {
        InventoryItem item = _inventoryData.m_Cookies.Find(i => i.m_ItemID == cookieID);
        if (item != null)
        {
            item.m_Amount -= amount;
            item.m_Amount = Mathf.Min(item.m_Amount, 0);

            if (item.m_Amount == 0)
                _inventoryData.m_Cookies.Remove(item);
        }
    }


    public void AddGoodCookieRep(int amount) => _inventoryData.m_GoodCookieReputation += amount;
    public bool RemoveGoodCookieRep(int amount)
    {
        if (_inventoryData.m_GoodCookieReputation - amount >= 0)
        {
            _inventoryData.m_GoodCookieReputation -= amount;
            return true;
        }
        else
            return false;

    }

    public void AddEvilCookieRep(int amount) => _inventoryData.m_EvilCookieReputation += amount;
    public bool RemoveEvilCookieRep(int amount)
    {
        if (_inventoryData.m_EvilCookieReputation - amount >= 0)
        {
            _inventoryData.m_EvilCookieReputation -= amount;
            return true;
        }
        else
            return false;

    }
}