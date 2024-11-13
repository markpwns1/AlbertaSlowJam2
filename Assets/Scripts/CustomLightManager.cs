using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[ExecuteInEditMode]
public class CustomLightManager : MonoBehaviour
{
    public string property = "_LightProperties";
    public Material mat;
    public int lightCount = 8;

    public Matrix4x4[] lights;
    
    // Start is called before the first frame update
    void Start()
    {
        lights = new Matrix4x4[lightCount];
    }

    // Update is called once per frame
    void Update()
    {
        var foundLights = FindObjectsOfType<CustomLight>();
        
        int i = 0;
        foreach (var light in foundLights)
        {
            if(i >= lightCount) break;
            var pos = light.GetPosition();
            var dir = light.GetDirection();

            var mat = lights[i];
            mat.SetRow(0, new Vector4(pos.x, pos.y, pos.z, 0.0f));
            mat.SetRow(1, new Vector4(dir.x, dir.y, dir.z, 0.0f));
            mat.SetRow(2, new Vector4(
                Mathf.Deg2Rad * light.angle, 
                light.range, 
                light.isSpotLight? 1.0f : 0.0f, 
                light.enabled? 1.0f : 0.0f));
            lights[i] = mat;

            i++;
        }

        for(; i < lightCount; i++) {
            lights[i] = Matrix4x4.zero;
        }

        Shader.SetGlobalMatrixArray(property, lights);
    }
}
