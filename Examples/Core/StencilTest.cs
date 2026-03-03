
namespace Examples.Core;

using static Raylib;

public static class StencilTest
{
    public static unsafe int Main()
    {
        // Initialization
        //--------------------------------------------------------------------------------------
        const int screenWidth = 800;
        const int screenHeight = 450;

        SetConfigFlags(ConfigFlags.StencilBuffer8Bit | ConfigFlags.Msaa4xHint);
        InitWindow(screenWidth, screenHeight, "raylib [core] stencil test");

        while (!WindowShouldClose())
        {
            BeginDrawing();
            ClearBackground(Color.RayWhite);

            Rlgl.BeginStencil();
            Rlgl.BeginStencilMask();

            var rect = new Rectangle(100, 100, 200, 200);
            DrawRectangleRounded(rect, 0.2f, 8, Color.Black);

            Rlgl.EndStencilMask();
            DrawRectangle(0, 0, 800, 450, Color.Blue);

            Rlgl.EndStencil();

            EndDrawing();
        }

        return 0;
    }

}
