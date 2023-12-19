using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManagment : Singleton<SceneManagment>
{
    public string PortalToSpawn { get; private set; }

    public void SetPortalToSpawn(string portalName){
        this.PortalToSpawn = portalName;
    }
}
