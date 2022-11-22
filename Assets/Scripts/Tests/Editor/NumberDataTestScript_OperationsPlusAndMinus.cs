using NUnit.Framework;

public class NumberDataTestScript_OperationsPlusAndMinus
{
    [Test]
    public void NumberData_Sum()
    {
        // Arrange
        var a = NumberData.FromString("110");
        var b = NumberData.FromString("110");
        // Act
        var c = a + b;
        var outStr = c.ToString();

        // Assert
        Assert.AreEqual("220", outStr);
    }

    [Test]
    public void NumberData_Sum_WithZero()
    {
        // Arrange
        var a = NumberData.FromString("123");
        var b = NumberData.FromString("0");
        // Act
        var c = a + b;
        var outStr = c.ToString();

        // Assert
        Assert.AreEqual(("123"), outStr);
    }

    [Test]
    public void NumberData_Sum_WithNeg()
    {
        // Arrange
        var a = NumberData.FromString("30");
        var b = NumberData.FromString("-100");
        // Act
        var c = a + b;
        var outStr = c.ToString();

        // Assert
        Assert.AreEqual(("-70"), outStr);
    }

    [Test]
    public void NumberData_Sum_TwoNegs()
    {
        // Arrange
        var a = NumberData.FromString("-30");
        var b = NumberData.FromString("-100");
        // Act
        var c = a + b;
        var outStr = c.ToString();

        // Assert
        Assert.AreEqual(("-130"), outStr);
    }

    [Test]
    public void NumberData_Substract()
    {
        // Arrange
        var a = NumberData.FromString("110");
        var b = NumberData.FromString("10");
        // Act
        var c = a - b;
        var outStr = c.ToString();

        // Assert
        Assert.AreEqual("100", outStr);
    }

    [Test]
    public void NumberData_Substract_Zero()
    {
        // Arrange
        var a = NumberData.FromString("110");
        var b = NumberData.FromString("0");
        // Act
        var c = a - b;
        var outStr = c.ToString();

        // Assert
        Assert.AreEqual(("110"), outStr);
    }

    [Test]
    public void NumberData_Substract_Negative()
    {
        // Arrange
        var a = NumberData.FromString("30");
        var b = NumberData.FromString("100");
        // Act
        var c = a - b;
        var outStr = c.ToString();

        // Assert
        Assert.AreEqual(("-70"), outStr);
    }

    [Test]
    public void NumberData_Substract_TwoNegs()
    {
        // Arrange
        var a = NumberData.FromString("-30");
        var b = NumberData.FromString("100");
        // Act
        var c = a - b;
        var outStr = c.ToString();

        // Assert
        Assert.AreEqual("-130", outStr);
    }
}