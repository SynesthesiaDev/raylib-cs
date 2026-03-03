using System.Numerics;
using static Raylib_cs.OpenGl;

namespace Raylib_cs;

public static unsafe partial class Rlgl
{
    /// <summary>Set shader value matrices</summary>
    public static void SetUniformMatrices(int locIndex, Matrix4x4[] mat)
    {
        fixed (Matrix4x4* p = mat)
        {
            SetUniformMatrices(locIndex, p, mat.Length);
        }
    }

    /// <summary>
    /// Enable stencil test
    /// </summary>
    public static void BeginStencil()
    {
        DrawRenderBatchActive();
        glEnable(GL_STENCIL_TEST);
    }

    /// <summary>
    /// Begin stencil mask mode
    /// </summary>
    public static void BeginStencilMask()
    {
        glEnable(GL_MULTISAMPLE);
        glColorMask(false, false, false, false);
        glStencilFunc(GL_ALWAYS, 1, 0xFF);
        glStencilOp(GL_KEEP, GL_KEEP, GL_REPLACE);
        glEnable(GL_ALPHA_TEST);
        glAlphaFunc(GL_GREATER, 0.5f);
    }


    /// <summary>
    /// End stencil mask mode
    /// </summary>
    public static void EndStencilMask()
    {
        DrawRenderBatchActive();
        glDisable(GL_ALPHA_TEST);
        glStencilFunc(GL_EQUAL, 1, 0xFF);
        glStencilOp(GL_KEEP, GL_KEEP, GL_KEEP);
        glColorMask(true, true, true, true);
    }

    /// <summary>
    /// Disable stencil test
    /// </summary>
    public static void EndStencil()
    {
        DrawRenderBatchActive();
        glDisable(GL_STENCIL_TEST);
    }

}
