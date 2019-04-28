using System.Collections.Generic;
using UnityEngine;

public static class Symbol
{
    public static readonly Dictionary<SymbolType, SymbolMetadata> SymbolMetadata =
        new Dictionary<SymbolType, SymbolMetadata>()
        {
            {SymbolType.Bar1, new SymbolMetadata(0, new Reward(4, 10))},
            {SymbolType.Bar2, new SymbolMetadata(60, new Reward(8, 10))},
            {SymbolType.Bell, new SymbolMetadata(120, new Reward(3, 10))},
            {SymbolType.Cherry, new SymbolMetadata(180, new Reward(2, 10))},
            {SymbolType.Grape, new SymbolMetadata(240, new Reward(2, 10))},
            {SymbolType.Dollar, new SymbolMetadata(300, new Reward(10, 10))}
        };

    public static SymbolType PickRandomSymbol()
    {
        return (SymbolType)Random.Range(0, 5);
    }
}
