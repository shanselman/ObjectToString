using System;
using Common;
using MbUnit.Framework;

namespace ObjectToStringTest
{
    [TestFixture]
	public class MyTest
	{
		public MyTest(){}

		[Test]
		public void MakePersonFormattedString()
		{
			Person p = new Person();
			string foo = p.ToString("{BirthDate} My name is {FirstName} {LastName} and I'm cool.");
			Assert.AreEqual("1/22/1974 12:00:00 AM My name is Scott Hanselman and I'm cool.", foo);
		}

		[Test]
		public void MakePersonFormattedStringWithBadData()
		{
			Person p = new Person();
			string foo = p.ToString("{bogus} My name is {FirstName} {LastName} and I'm cool.");
			Assert.AreEqual("{bogus} My name is Scott Hanselman and I'm cool.", foo);
		}


		[Test]
		public void MakePersonFormattedStringWithVeryBadData()
		{
			Person p = new Person();
			string foo = p.ToString("{bogus:} My name is {FirstName} {LastName} and I'm cool.");
			Assert.AreEqual("{bogus:} My name is Scott Hanselman and I'm cool.", foo);
		}

		[Test]
		public void MakePersonFormattedStringWithVeryBadFormattingData()
		{
			Person p = new Person();
			string foo = p.ToString("{BirthDate:poo} My name is {FirstName} {LastName} and I'm cool.");
			Assert.AreEqual("poo My name is Scott Hanselman and I'm cool.", foo);
		}


		[Test]
		public void MakePersonFormattedStringWithQuestionableData()
		{
			Person p = new Person();
			string foo = p.ToString("{BirthDate:} My name is {FirstName} {LastName} and I'm cool.");
			Assert.AreEqual("1/22/1974 12:00:00 AM My name is Scott Hanselman and I'm cool.", foo);
		}

		[Test]
		public void MakePersonFormattedStringWithSlightlyQuestionableData()
		{
			Person p = new Person();
			string foo = p.ToString("{{}} My name is {FirstName} {LastName} and I'm cool.");
			Assert.AreEqual("{{}} My name is Scott Hanselman and I'm cool.", foo);
		}


		/// <summary>
		/// This will likely pass only in the en-us culture
		/// </summary>
		[Test]
		public void MakePersonFormattedStringWithFormat()
		{
			Person p = new Person();
			string foo = p.ToString("{BirthDate:D} My name is {FirstName} {LastName} and I'm cool.");
			Assert.AreEqual("Tuesday, January 22, 1974 My name is Scott Hanselman and I'm cool.", foo);
		}

		[Test]
		public void MakePersonFormattedStringWithFormatAndCulture()
		{
			Person p = new Person();
			string foo = p.ToString("{BirthDate:D} My name is {FirstName} {LastName} and I'm cool.",System.Globalization.CultureInfo.InvariantCulture);
			Assert.AreEqual("Tuesday, 22 January 1974 My name is Scott Hanselman and I'm cool.", foo);
		}

		[Test]
		public void MakePersonComplexFormattedStringWithFormatAndCulture()
		{
			Person p = new Person();
			string foo = p.ToString("{BirthDate:MM/dd/yy HH:mm:ss} My name is {FirstName} {LastName} and I'm cool.",System.Globalization.CultureInfo.InvariantCulture);
			Assert.AreEqual("01/22/74 00:00:00 My name is Scott Hanselman and I'm cool.", foo);
		}

		[Test]
		public void MakePersonFormattedStringWithFormatAndChineseCulture()
		{
			Person p = new Person();
			string foo = p.ToString("{BirthDate:D} My name is {FirstName} {LastName} and I'm cool.",new System.Globalization.CultureInfo("zh-cn"));
			Assert.AreEqual("1974年1月22日 My name is Scott Hanselman and I'm cool.", foo);
		}

		[Test]
		public void MakeSimplePersonFormattedString()
		{
			Person p = new Person();
			string foo = p.ToString("{LastName}, {FirstName}");

			Assert.AreEqual("Hanselman, Scott", foo);
		}

		[Test]
		public void MakeSimplePersonFormattedStringWithDate()
		{
			Person p = new Person();
			string foo = p.ToString("{LastName}, {FirstName} {BirthDate}");
			Assert.AreEqual("Hanselman, Scott 1/22/1974 12:00:00 AM", foo);
		}

		[Test]
		public void MakeSimplePersonFormattedStringWithDateAndOneBadPropertyName()
		{
			Person p = new Person();
			string foo = p.ToString("{LastName}, {ScottName} {BirthDate}");
			Assert.AreEqual("Hanselman, {ScottName} 1/22/1974 12:00:00 AM", foo);
		}

		[Test]
		public void MakeSimplePersonFormattedStringWithDouble()
		{
			Person p = new Person();
			string foo = p.ToString("{Money} {LastName}, {ScottName} {BirthDate}");
			Assert.AreEqual("3.43 Hanselman, {ScottName} 1/22/1974 12:00:00 AM", foo);
		}

		/// <summary>
		/// This will likely pass only in the en-us culture
		/// </summary>
	[Test]
	public void MakeSimplePersonFormattedStringWithDoubleFormatted()
	{
		Person p = new Person();
        string foo = p.ToString("{Money:C} {LastName}, {ScottName} {BirthDate}");
		Assert.AreEqual("$3.43 Hanselman, {ScottName} 1/22/1974 12:00:00 AM", foo);
	}

	[Test]
	public void MakeSimplePersonFormattedStringWithDoubleFormattedInHongKong()
	{
		Person p = new Person();
		string foo = p.ToString("{Money:C} {LastName}, {ScottName} {BirthDate}",new System.Globalization.CultureInfo("zh-hk"));
		Assert.AreEqual("HK$3.43 Hanselman, {ScottName} 1/22/1974 12:00:00 AM", foo);
	}
	}

	public class Person
	{
		public Person(){}

		private double _money = 3.43;
		public double Money
		{
			get
			{
				return _money;
			}
			set
			{
				_money = value;
			}
		}

		private DateTime _birthDate = new DateTime(1974,1,22);
		public DateTime BirthDate
		{
			get
			{
				return _birthDate;
			}
			set
			{
				_birthDate = value;
			}
		}


		public string MiddleName = "David";

		public string LastName = "Hanselman";

		private string _firstName = "Scott";
		public string FirstName
		{
			get
			{
				return _firstName;
			}
			set
			{
				_firstName = value;
			}
		}
	}
}
