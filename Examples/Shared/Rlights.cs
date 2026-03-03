using System.Numerics;
using static Raylib_cs.Raylib;

namespace Examples.Shared;

public struct Light
{
    public bool Enabled;
    public LightType Type;
    public Vector3 Position;
    public Vector3 Target;
    public Color Color;

    public int EnabledLoc;
    public int TypeLoc;
    public int PosLoc;
    public int TargetLoc;
    public int ColorLoc;
}

public enum LightType
{
    Directorional,
    Point
}

public static class Rlights
{
    public static Light CreateLight(
        int lightsCount,
        LightType type,
        Vector3 pos,
        Vector3 target,
        Color color,
        NativeShader nativeShader
    )
    {
        Light light = new();

        light.Enabled = true;
        light.Type = type;
        light.Position = pos;
        light.Target = target;
        light.Color = color;

        string enabledName = "lights[" + lightsCount + "].enabled";
        string typeName = "lights[" + lightsCount + "].type";
        string posName = "lights[" + lightsCount + "].position";
        string targetName = "lights[" + lightsCount + "].target";
        string colorName = "lights[" + lightsCount + "].color";

        light.EnabledLoc = GetShaderLocation(nativeShader, enabledName);
        light.TypeLoc = GetShaderLocation(nativeShader, typeName);
        light.PosLoc = GetShaderLocation(nativeShader, posName);
        light.TargetLoc = GetShaderLocation(nativeShader, targetName);
        light.ColorLoc = GetShaderLocation(nativeShader, colorName);

        UpdateLightValues(nativeShader, light);

        return light;
    }

    public static void UpdateLightValues(NativeShader nativeShader, Light light)
    {
        // Send to shader light enabled state and type
        Raylib.SetShaderValue(
            nativeShader,
            light.EnabledLoc,
            light.Enabled ? 1 : 0,
            ShaderUniformDataType.Int
        );
        Raylib.SetShaderValue(nativeShader, light.TypeLoc, (int)light.Type, ShaderUniformDataType.Int);

        // Send to shader light target position values
        Raylib.SetShaderValue(nativeShader, light.PosLoc, light.Position, ShaderUniformDataType.Vec3);

        // Send to shader light target position values
        Raylib.SetShaderValue(nativeShader, light.TargetLoc, light.Target, ShaderUniformDataType.Vec3);

        // Send to shader light color values
        float[] color = new[]
        {
                (float)light.Color.R / (float)255,
                (float)light.Color.G / (float)255,
                (float)light.Color.B / (float)255,
                (float)light.Color.A / (float)255
            };
        Raylib.SetShaderValue(nativeShader, light.ColorLoc, color, ShaderUniformDataType.Vec4);
    }
}
