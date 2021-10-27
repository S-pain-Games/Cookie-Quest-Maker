using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReputationSystem
{
    private PlayerReputation _res;

    public void Initialize(PlayerReputation res)
    {
        _res = res;
        LoadData();
    }

    private void LoadData()
    {
        // This is where we should load the player data
        _res.m_EvilCookieReputation += 66;
        _res.m_GoodCookieReputation += 52;
    }

    public void AddGoodCookieRep(int amount) => _res.m_GoodCookieReputation += amount;
    public bool RemoveGoodCookieRep(int amount)
    {
        if (_res.m_GoodCookieReputation - amount >= 0)
        {
            _res.m_GoodCookieReputation -= amount;
            return true;
        }
        else
            return false;
            
    }
    public void AddEvilCookieRep(int amount) => _res.m_EvilCookieReputation += amount;
    public bool RemoveEvilCookieRep(int amount)
    {
        if (_res.m_EvilCookieReputation - amount >= 0)
        {
            _res.m_EvilCookieReputation -= amount;
            return true;
        }
        else
            return false;
            
    }
}
