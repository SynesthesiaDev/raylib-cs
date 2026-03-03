/*******************************************************************************************
*
*   raylib [shaders] example - Hot reloading
*
*   NOTE: This example requires raylib OpenGL 3.3 for shaders support and only #version 330
*         is currently supported. OpenGL ES 2.0 platforms are not supported at the moment.
*
*   This example has been created using raylib 3.0 (www.raylib.com)
*   raylib is licensed under an unmodified zlib/libpng license (View raylib.h for details)
*
*   Copyright (c) 2020 Ramon Santamaria (@raysan5)
*
********************************************************************************************/

using System.Numerics;
using static Raylib_cs.Raylib;

namespace Examples.Shaders;

public class HotReloading
{
    public static int Main()
    {
        // Initialization
        //--------------------------------------------------------------------------------------
        int screenWidth = 800;
        int screenHeight = 450;

        InitWindow(screenWidth, screenHeight, "raylib [shaders] example - hot reloading");

        string fragShaderFileName = "resources/shaders/glsl330/reload.fs";
        long fragShaderFileModTime = GetFileModTime(fragShaderFileName);

        // Load raymarching shader
        // NOTE: Defining 0 (NULL) for vertex shader forces usage of internal default vertex shader
        NativeShader nativeShader = LoadShader(null, fragShaderFileName);

        // Get shader locations for required uniforms
        int resolutionLoc = GetShaderLocation(nativeShader, "resolution");
        int mouseLoc = GetShaderLocation(nativeShader, "mouse");
        int timeLoc = GetShaderLocation(nativeShader, "time");

        float[] resolution = new[] { (float)screenWidth, (float)screenHeight };
        Raylib.SetShaderValue(nativeShader, resolutionLoc, resolution, ShaderUniformDataType.Vec2);

        float totalTime = 0.0f;
        bool shaderAutoReloading = false;

        SetTargetFPS(60);
        //--------------------------------------------------------------------------------------

        // Main game loop
        while (!WindowShouldClose())
        {
            // Update
            //----------------------------------------------------------------------------------
            totalTime += GetFrameTime();
            Vector2 mouse = GetMousePosition();
            float[] mousePos = new[] { mouse.X, mouse.Y };

            // Set shader required uniform values
            Raylib.SetShaderValue(nativeShader, timeLoc, totalTime, ShaderUniformDataType.Float);
            Raylib.SetShaderValue(nativeShader, mouseLoc, mousePos, ShaderUniformDataType.Vec2);

            // Hot shader reloading
            if (shaderAutoReloading || (IsMouseButtonPressed(MouseButton.Left)))
            {
                long currentFragShaderModTime = GetFileModTime(fragShaderFileName);

                // Check if shader file has been modified
                if (currentFragShaderModTime != fragShaderFileModTime)
                {
                    // Try reloading updated shader
                    NativeShader updatedNativeShader = LoadShader(null, fragShaderFileName);

                    // It was correctly loaded
                    if (updatedNativeShader.Id != 0) //rlGetShaderIdDefault())
                    {
                        UnloadShader(nativeShader);
                        nativeShader = updatedNativeShader;

                        // Get shader locations for required uniforms
                        resolutionLoc = GetShaderLocation(nativeShader, "resolution");
                        mouseLoc = GetShaderLocation(nativeShader, "mouse");
                        timeLoc = GetShaderLocation(nativeShader, "time");

                        // Reset required uniforms
                        Raylib.SetShaderValue(
                            nativeShader,
                            resolutionLoc,
                            resolution,
                            ShaderUniformDataType.Vec2
                        );
                    }

                    fragShaderFileModTime = currentFragShaderModTime;
                }
            }

            if (IsKeyPressed(KeyboardKey.A))
            {
                shaderAutoReloading = !shaderAutoReloading;
            }
            //----------------------------------------------------------------------------------

            // Draw
            //----------------------------------------------------------------------------------
            BeginDrawing();
            ClearBackground(Color.RayWhite);

            // We only draw a white full-screen rectangle, frame is generated in shader
            BeginShaderMode(nativeShader);
            DrawRectangle(0, 0, screenWidth, screenHeight, Color.White);
            EndShaderMode();

            string info = $"PRESS [A] to TOGGLE SHADER AUTOLOADING: {(shaderAutoReloading ? "AUTO" : "MANUAL")}";
            DrawText(info, 10, 10, 10, shaderAutoReloading ? Color.Red : Color.Black);
            if (!shaderAutoReloading)
            {
                DrawText("MOUSE CLICK to SHADER RE-LOADING", 10, 30, 10, Color.Black);
            }

            // DrawText($"Shader last modification: ", 10, 430, 10, Color.BLACK);

            EndDrawing();
            //----------------------------------------------------------------------------------
        }

        // De-Initialization
        //--------------------------------------------------------------------------------------
        UnloadShader(nativeShader);

        CloseWindow();
        //--------------------------------------------------------------------------------------

        return 0;
    }
}
