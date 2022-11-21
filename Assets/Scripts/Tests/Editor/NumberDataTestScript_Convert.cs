using NUnit.Framework;

public class NumberDataTestScript_Convert
{
    // A Test behaves as an ordinary method
    [Test]
    public void NumberDataFromString()
    {
        // Arrange
        var numberData = NumberData.NumberData.FromString("110");
        // Act
        var outStr = numberData.ToString();
        
        // Assert
        Assert.True(outStr.Equals("110"));
    }
    
    [Test]
    public void NumberDataFromString_BigString()
    {
        // Arrange
        var numberData = NumberData.NumberData.FromString("1000000000000");
        // Act
        var outStr = numberData.ToString();
        
        // Assert
        Assert.True(outStr.Equals("1000000000000"));
    }
    
    [Test]
    public void NumberDataFromString_Zero()
    {
        // Arrange
        var numberData = NumberData.NumberData.FromString("0");
        // Act
        var outStr = numberData.ToString();
        
        // Assert
        Assert.True(outStr.Equals("0"));
    }
    
    [Test]
    public void NumberDataFromString_NegativeString()
    {
        // Arrange
        var numberData = NumberData.NumberData.FromString("-100");
        // Act
        var outStr = numberData.ToString();
        
        // Assert
        Assert.True(outStr.Equals("-100"));
    }
    
    [Test]
    public void NumberDataFromString_FromInt()
    {
        // Arrange
        var numberData = NumberData.NumberData.FromInt(12345);
        // Act
        var outStr = numberData.ToString();
        
        // Assert
        Assert.True(outStr.Equals("12345"));
    }
    
    [Test]
    public void NumberDataFromString_FromInt_Negative()
    {
        // Arrange
        var numberData = NumberData.NumberData.FromInt(-12345);
        // Act
        var outStr = numberData.ToString();
        
        // Assert
        Assert.True(outStr.Equals("-12345"));
    }
    
    [Test]
    public void NumberDataFromString_FromInt_Zero()
    {
        // Arrange
        var numberData = NumberData.NumberData.FromInt(0);
        // Act
        var outStr = numberData.ToString();
        
        // Assert
        Assert.True(outStr.Equals("0"));
    }
}
