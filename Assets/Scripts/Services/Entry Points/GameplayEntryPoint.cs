using Scellecs.Morpeh;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VContainer;

public class GameplayEntryPoint : MonoBehaviour
{
    WorldInitializer _worldInit;

    private void Start()
    {
        _worldInit.Initialize();
    }
    
    [Inject]
    private void InjectDependencies(WorldInitializer worldInit)
    {
        _worldInit = worldInit;
    }
}
