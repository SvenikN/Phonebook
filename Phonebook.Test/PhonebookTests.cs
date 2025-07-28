using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace Phonebook.Test
{
  public class PhonebookTests
  {
    private Phonebook phonebook;

    [OneTimeSetUp]
    public void OneTimeSetup()
    {
      phonebook = new Phonebook();
    }

    [OneTimeTearDown]
    public void OneTimeTearDown()
    {
      phonebook = null;
    }

    [Test]
    public void GetSubscriber_ExistingSubscriber()
    {
      //Arrange
      Subscriber subscriber = new Subscriber(Guid.NewGuid(), "Test", new List<PhoneNumber>());
      phonebook.AddSubscriber(subscriber);
      //Act
      var expectedSubscriber = phonebook.GetSubscriber(subscriber.Id);
      //Assert
      Assert.That(expectedSubscriber, Is.EqualTo(subscriber));
    }

    [Test]
    public void GetSubscriber_NotExistingSubscriber()
    {
      //Arrange
      Guid id = Guid.NewGuid();
      Subscriber existingSubscriber = new Subscriber(Guid.NewGuid(), "Test", new List<PhoneNumber>());
      phonebook.AddSubscriber(existingSubscriber);
      //Assert
      Assert.Throws<ArgumentException>(() => phonebook.GetSubscriber(id));
    }

    [Test]
    public void GetAll_ExistingSubscriber()
    {
      //Arrange
      var subscribers = new List<Subscriber>
      {
        new Subscriber(Guid.NewGuid(), "TestA", new List<PhoneNumber>()),
        new Subscriber(Guid.NewGuid(), "TestB", new List<PhoneNumber>()),
        new Subscriber(Guid.NewGuid(), "TestC", new List<PhoneNumber>())
      };
      foreach (Subscriber subscriber in subscribers)
      {
        phonebook.AddSubscriber(subscriber);
      }
      //Act
      var allSubscribers = phonebook.GetAll();
      //Assert
      Assert.That(allSubscribers, Is.EquivalentTo(subscribers));
    }

    [Test]
    public void CreateSubscriber_NewSubscriber()
    {
      //Arrange
      Guid id = Guid.NewGuid();
      var expectedSubscriber = new Subscriber(id, "Test", new List<PhoneNumber>());
      //Act
      phonebook.AddSubscriber(expectedSubscriber);
      //Assert
      Assert.That(phonebook.GetSubscriber(id), Is.EqualTo(expectedSubscriber));
    }

    [TestCase("EAB77320-EC98-453A-87EC-454C34DBDA1C", "Test")]
    public void CreateSubscriber_ExistingSubscriber_ThrowsException(string guid, string name)
    {
      //Arrange
      Guid id = Guid.Parse(guid);
      var expectedSubscriber = new Subscriber(id, name, new List<PhoneNumber>());
      //Act
      phonebook.AddSubscriber(expectedSubscriber);
      //Assert
      Assert.Throws<InvalidOperationException>(() => phonebook.AddSubscriber(expectedSubscriber));
    }

    [Test]
    public void RenameSubscriber_ExistingSubscriber()
    {
      //Arrange
      Guid id = Guid.NewGuid();
      var expectedSubscriber = new Subscriber(id, "Test", new List<PhoneNumber>());
      string newName = "TestTest";
      phonebook.AddSubscriber(expectedSubscriber);
      //Act
      phonebook.RenameSubscriber(expectedSubscriber, newName);
      //Assert
      expectedSubscriber = phonebook.GetSubscriber(id);
      Assert.That(expectedSubscriber.Name, Is.EqualTo(newName));
    }

    [Test]
    public void DeleteSubscriber_ExistingSubscriber()
    {
      //Arrange
      var expectedSubscriber = new Subscriber(Guid.NewGuid(), "Test", new List<PhoneNumber>());
      phonebook.AddSubscriber(expectedSubscriber);
      //Act
      phonebook.DeleteSubscriber(expectedSubscriber);
      //Assert
      Assert.That(phonebook.GetAll, Does.Not.Contain(expectedSubscriber));
    }

    [Test]
    public void AddNumberToSubscriber_ExistingSubscriber()
    {
      //Arrange
      Guid id = Guid.NewGuid();
      var subscriber = new Subscriber(id, "Test", new List<PhoneNumber>());
      phonebook.AddSubscriber(subscriber);
      //Act
      var newNumber = new PhoneNumber("123-456-7890", PhoneNumberType.Work);
      phonebook.AddNumberToSubscriber(subscriber, newNumber);
      //Assert
      var updatedSubscriber = phonebook.GetSubscriber(id);
      Assert.That(updatedSubscriber.PhoneNumbers, Contains.Item(newNumber));
    }
  }
}
