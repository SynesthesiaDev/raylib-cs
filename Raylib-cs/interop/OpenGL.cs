using System.Runtime.InteropServices;

namespace Raylib_cs;

public static partial class OpenGl
{
    public const int GL_STENCIL_TEST = 0x0B90;
    public const int GL_STENCIL_BUFFER_BIT = 0x00000400;
    public const int GL_ALWAYS = 0x0207;
    public const int GL_EQUAL = 0x0202;
    public const int GL_KEEP = 0x1E00;
    public const int GL_REPLACE = 0x1E01;
    public const int GL_STENCIL_BITS = 0x0D57;
    public const int GL_ALPHA_TEST = 0x0BC0;
    public const int GL_GREATER = 0x0BC0;
    public const int GL_MULTISAMPLE = 0x809D;

    private const string Binary = "opengl32.dll";

    [DllImport(Binary)]
    public static extern void glGetIntegerv(int pname, out int data);

    [DllImport(Binary)]
    public static extern void glEnable(int cap);

    [DllImport(Binary)]
    public static extern void glDisable(int cap);

    [DllImport(Binary)]
    public static extern void glClear(int mask);

    [DllImport(Binary)]
    public static extern void glClearStencil(int s);

    [DllImport(Binary)]
    public static extern void glStencilFunc(int func, int @ref, uint mask);

    [DllImport(Binary)]
    public static extern void glStencilOp(int fail, int zfail, int zpass);

    [DllImport(Binary)]
    public static extern void glAlphaFunc(int flag, float value);

    [DllImport(Binary)]
    public static extern void glColorMask(bool red, bool green, bool blue, bool alpha);
}
