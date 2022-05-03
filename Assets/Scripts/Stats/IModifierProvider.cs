using System.Collections.Generic;

namespace RPG.Stats
{
    public interface IModifierProvider
    {
        IEnumerable<float> GetAdditiveModifiers(Stat stat); //IEnurable is the same as as IEnumerator except it used in foreach loop
        IEnumerable<float> GetPercentageModifiers(Stat stat);
    }
}