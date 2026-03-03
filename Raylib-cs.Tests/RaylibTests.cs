using Xunit;

namespace Raylib_cs.Tests;

public class RaylibTests
{
    private unsafe void CheckType<T>() where T : unmanaged
    {
        Assert.True(BlittableHelper.IsBlittable<T>());
    }

    [Fact]
    public void CheckTypes()
    {
        CheckType<Color>();
        CheckType<Rectangle>();
        CheckType<Image>();
        CheckType<Texture2D>();
        CheckType<RenderTexture2D>();
        CheckType<NPatchInfo>();
        CheckType<GlyphInfo>();
        CheckType<NativeFont>();
        CheckType<Camera2D>();
        CheckType<Camera3D>();
        CheckType<NativeMesh>();
        CheckType<NativeShader>();
        CheckType<MaterialMap>();
        CheckType<Material>();
        CheckType<NativeTransform>();
        CheckType<BoneInfo>();
        CheckType<NativeModel>();
        CheckType<ModelAnimation>();
        CheckType<Ray>();
        CheckType<RayCollision>();
        CheckType<BoundingBox>();
        CheckType<VrDeviceInfo>();
        CheckType<VrStereoConfig>();
        CheckType<RenderBatch>();
    }
}
