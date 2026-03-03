using static Raylib_cs.Raylib;
using System.Numerics;

namespace Examples.Shared;

public struct PbrLight
{
    public PbrLightType Type;
    public bool Enabled;
    public Vector3 Position;
    public Vector3 Target;
    public Vector4 Color;
    public float Intensity;

    // Shader light parameters locations
    public int TypeLoc;
    public int EnabledLoc;
    public int PositionLoc;
    public int TargetLoc;
    public int ColorLoc;
    public int IntensityLoc;
}

public enum PbrLightType
{
    Directorional,
    Point,
    Spot
}

public class PbrLights
{
    public static PbrLight CreateLight(
        int lightsCount,
        PbrLightType type,
        Vector3 pos,
        Vector3 target,
        Color color,
        float intensity,
        NativeShader nativeShader
    )
    {
        PbrLight light = new();

        light.Enabled = true;
        light.Type = type;
        light.Position = pos;
        light.Target = target;
        light.Color = new Vector4(
            color.R / 255.0f,
            color.G / 255.0f,
            color.B / 255.0f,
            color.A / 255.0f
        );
        light.Intensity = intensity;

        string enabledName = "lights[" + lightsCount + "].enabled";
        string typeName = "lights[" + lightsCount + "].type";
        string posName = "lights[" + lightsCount + "].position";
        string targetName = "lights[" + lightsCount + "].target";
        string colorName = "lights[" + lightsCount + "].color";
        string intensityName = "lights[" + lightsCount + "].intensity";

        light.EnabledLoc = GetShaderLocation(nativeShader, enabledName);
        light.TypeLoc = GetShaderLocation(nativeShader, typeName);
        light.PositionLoc = GetShaderLocation(nativeShader, posName);
        light.TargetLoc = GetShaderLocation(nativeShader, targetName);
        light.ColorLoc = GetShaderLocation(nativeShader, colorName);
        light.IntensityLoc = GetShaderLocation(nativeShader, intensityName);

        UpdateLightValues(nativeShader, light);

        return light;
    }

    public static void UpdateLightValues(NativeShader nativeShader, PbrLight light)
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
        Raylib.SetShaderValue(nativeShader, light.PositionLoc, light.Position, ShaderUniformDataType.Vec3);

        // Send to shader light target position values
        Raylib.SetShaderValue(nativeShader, light.TargetLoc, light.Target, ShaderUniformDataType.Vec3);

        // Send to shader light color values
        Raylib.SetShaderValue(nativeShader, light.ColorLoc, light.Color, ShaderUniformDataType.Vec4);

        // Send to shader light intensity values
        Raylib.SetShaderValue(nativeShader, light.IntensityLoc, light.Intensity, ShaderUniformDataType.Float);
    }
}
