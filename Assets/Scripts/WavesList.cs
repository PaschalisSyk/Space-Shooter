using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Waves List")]

public class WavesList : ScriptableObject
{
    public List<WaveConfig> waveConfigs;
}
