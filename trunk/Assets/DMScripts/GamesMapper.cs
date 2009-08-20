using UnityEngine;
using System.Collections.Generic;


public class GamesMapper {

    private static Dictionary<int, string> namesMap = null;
    private static Dictionary<int, string> scenesMap = null;

    public GamesMapper()
    {
        namesMap = new Dictionary<int, string>();

        scenesMap = new Dictionary<int, string>();

        initializeNamesMap();

        initializeScenesMap();

    }

    public string getGameName( int gNumber )
    {
        if (gNumber > 0)
        {
            return namesMap[gNumber]; 
        }
        return "";
    }

    public int getGameNumber(string gName)
    {
        if (namesMap.ContainsValue(gName))
        {
            foreach (KeyValuePair<int, string> pair in namesMap)
            {
                if (pair.Value.Equals(gName))
                {
                    return pair.Key;
                }
            }
        }
        return -1;
    }
    
    public string getGameScene(int gNumber)
    {
        if (gNumber > 0)
        {
            return scenesMap[gNumber];
        }
        return "";
    }

    private static void initializeNamesMap()
    {
        namesMap.Add(1, "SumaCromatica");
        namesMap.Add(2, "IdentificacionCromatica");
        namesMap.Add(3, "CapacidadDeRespuesta");
        namesMap.Add(4, "CapacidadDeRespuestaAvanzada");
        namesMap.Add(5, "Cuenta");
        namesMap.Add(6, "Balanza");
        namesMap.Add(7, "BalanzaAvanzada");
        namesMap.Add(8, "CadenasEsferas");
    }
    private static void initializeScenesMap()
    {
        scenesMap.Add(1, "Problem1Task1");
        scenesMap.Add(2, "Problem2Task1");
        scenesMap.Add(3, "Problem3Task1");
        scenesMap.Add(4, "Problem4Task1");
        scenesMap.Add(5, "Problem6Task1");
        scenesMap.Add(6, "Problem6Task1");
        scenesMap.Add(7, "Problem1Task1");
        scenesMap.Add(8, "Problem8Task1");
    }


}
