using NUnit.Framework;
using UnityEngine;

public class RodTests
{
    private Rod _rod;
    private Ring _smallRing;
    private Ring _largeRing;

    [SetUp]
    public void Setup()
    {
        _rod = new GameObject().AddComponent<Rod>();
        _smallRing = new GameObject().AddComponent<Ring>();
        _largeRing = new GameObject().AddComponent<Ring>();

        _smallRing.Init(1);
        _largeRing.Init(3);
    }

    [TearDown]
    public void Teardown()
    {
        Object.DestroyImmediate(_rod.gameObject);
        Object.DestroyImmediate(_smallRing.gameObject);
        Object.DestroyImmediate(_largeRing.gameObject);
    }

    [Test]
    public void TryAddRing_SmallOnLarge_ReturnsTrue()
    {
        _rod.TryAddRing(_largeRing);

        bool result = _rod.TryAddRing(_smallRing);

        Assert.IsTrue(result);
    }

    [Test]
    public void TryAddRing_LargeOnSmall_ReturnsFalse()
    {
        _rod.TryAddRing(_smallRing);

        bool result = _rod.TryAddRing(_largeRing);

        Assert.IsFalse(result);
    }
}