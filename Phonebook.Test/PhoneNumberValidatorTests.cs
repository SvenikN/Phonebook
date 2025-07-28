using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace Phonebook.Test
{
  public class PhoneNumberValidatorTests
  {
    [Test]
    public void PhoneNumberValidator_ValidPhoneNumber()
    {
      //Arrage
      PhoneNumber validPhone = new PhoneNumber("+8 (800) 555-3535", PhoneNumberType.Work);
      //Assert
      Assert.DoesNotThrow(() => PhoneNumberValidator.Validate(validPhone));
    }

    [TestCase("88005553535")]
    [TestCase("8 800 555 3535")]
    [TestCase("8 (800) 555-3535")]
    [TestCase("+7-800-555-35-35")]
    [TestCase("+8 (8000) 555-3535")]
    [TestCase("")]
    public void PhoneNumberValidator_InvalidPhoneNumber(string phone)
    {
      //Arrage
      PhoneNumber invalidPhone = new PhoneNumber(phone, PhoneNumberType.Work);
      //Assert
      Assert.Throws<ArgumentException>(() => PhoneNumberValidator.Validate(invalidPhone));
    }

    [Test]
    public void ValidateList_ValidPhoneNumber()
    {
      //Arrage
      var phoneNumbers = new List<PhoneNumber>
      {
        new PhoneNumber("+8 (800) 555-3535", PhoneNumberType.Work),
        new PhoneNumber("+8 (800) 555-3535", PhoneNumberType.Personal)
      };
      //Assert
      Assert.DoesNotThrow(() => PhoneNumberValidator.ValidateList(phoneNumbers));
    }

    [Test]
    public void ValidateList_EmptyList()
    {
      //Arrage
      var phoneNumbers = new List<PhoneNumber> {};
      //Assert
      Assert.DoesNotThrow(() => PhoneNumberValidator.ValidateList(phoneNumbers));
    }

    [Test]
    public void ValidateList_ThrowArgumentException()
    {      
      //Arrage
      var phoneNumbers = new List<PhoneNumber>
      {
        new PhoneNumber("88005553535", PhoneNumberType.Work),
        new PhoneNumber("+8 (800) 555-3535", PhoneNumberType.Personal)
      };
      //Assert
      Assert.Throws<ArgumentException>(() => PhoneNumberValidator.ValidateList(phoneNumbers));
    }
  }
}
