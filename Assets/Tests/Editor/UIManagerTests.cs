using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;

public class UIManagerTests
{
    private UIManager _uiManager;
    private Text _movesText;

    [SetUp]
    public void Setup()
    {
        _uiManager = new GameObject().AddComponent<UIManager>();
        _movesText = new GameObject().AddComponent<Text>();
        _uiManager._movesText = _movesText;
    }

    [TearDown]
    public void Teardown()
    {
        Object.DestroyImmediate(_uiManager.gameObject);
        Object.DestroyImmediate(_movesText.gameObject);
    }

    [Test]
    public void UpdateMoves_ValidInput_UpdatesTextCorrectly()
    {
        _uiManager.UpdateMoves(2, 5);

        Assert.AreEqual("Ходы: 2/5", _movesText.text);
    }
}