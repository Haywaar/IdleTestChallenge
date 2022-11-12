using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class NumberData_Multiply_Equal_Greater
{
    [Test]
    public void NumberData_Multiply_x1()
    {
        // Arrange
        var a = NumberData.FromString("123");
        var b = 1;
        // Act
        var c = a * b;
        var outStr = c.ToString();

        // Assert
        Assert.AreEqual("123", outStr);
    }

    [Test]
    public void NumberData_Multiply_x2()
    {
        // Arrange
        var a = NumberData.FromString("123");
        var b = 2;
        // Act
        var c = a * b;
        var outStr = c.ToString();

        // Assert
        Assert.AreEqual("246", outStr);
    }

    [Test]
    public void NumberData_Multiply_x10()
    {
        // Arrange
        var a = NumberData.FromString("123");
        var b = 10;
        // Act
        var c = a * b;
        var outStr = c.ToString();

        // Assert
        Assert.AreEqual("1230", outStr);
    }

    [Test]
    public void NumberData_Multiply_MinusOne()
    {
        // Arrange
        var a = NumberData.FromString("123");
        var b = -1;
        // Act
        var c = a * b;
        var outStr = c.ToString();

        // Assert
        Assert.AreEqual("-123", outStr);
    }

    [Test]
    public void NumberData_Greater()
    {
        // Arrange
        var a = NumberData.FromString("456");
        var b = NumberData.FromString("123");

        // Act
        bool rez = a > b;

        // Assert
        Assert.AreEqual(true, rez);
    }

    [Test]
    public void NumberData_Greater_Equals()
    {
        // Arrange
        var a = NumberData.FromString("456");
        var b = NumberData.FromString("456");

        // Act
        bool rez = a > b;

        // Assert
        Assert.AreEqual(false, rez);
    }

    [Test]
    public void NumberData_Greater_Neg()
    {
        // Arrange
        var a = NumberData.FromString("56");
        var b = NumberData.FromString("-456");

        // Act
        bool rez = a > b;

        // Assert
        Assert.AreEqual(true, rez);
    }

    [Test]
    public void NumberData_Lesser()
    {
        // Arrange
        var a = NumberData.FromString("123");
        var b = NumberData.FromString("456");

        // Act
        bool rez = a < b;

        // Assert
        Assert.AreEqual(true, rez);
    }

    [Test]
    public void NumberData_Lesser_Equals()
    {
        // Arrange
        var a = NumberData.FromString("456");
        var b = NumberData.FromString("456");

        // Act
        bool rez = a < b;

        // Assert
        Assert.AreEqual(false, rez);
    }

    [Test]
    public void NumberData_Lesser_Neg()
    {
        // Arrange
        var a = NumberData.FromString("-456");
        var b = NumberData.FromString("56");

        // Act
        bool rez = a < b;

        // Assert
        Assert.AreEqual(true, rez);
    }

    [Test]
    public void NumberData_Abs()
    {
        // Arrange
        var a = NumberData.FromString("-456");

        // Act
        var outStr = NumberData.Abs(a).ToString();

        // Assert
        Assert.AreEqual("456", outStr);
    }

    [Test]
    public void NumberData_Abs_Same()
    {
        // Arrange
        var a = NumberData.FromString("456");

        // Act
        var outStr = NumberData.Abs(a).ToString();

        // Assert
        Assert.AreEqual("456", outStr);
    }
}