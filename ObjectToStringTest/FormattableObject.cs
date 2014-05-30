using System;
using Common;
using Xunit;

namespace ObjectToStringTest
{
	public class MyTest
	{
		public MyTest(){}

		[Fact]
		public void MakePersonFormattedString()
		{
			Person p = new Person();
			string foo = p.ToString("{BirthDate} My name is {FirstName} {LastName} and I'm cool.");
			Assert.Equal("1/22/1974 12:00:00 AM My name is Scott Hanselman and I'm cool.", foo);
		}

		[Fact]
		public void MakePersonFormattedStringWithBadData()
		{
			Person p = new Person();
			string foo = p.ToString("{bogus} My name is {FirstName} {LastName} and I'm cool.");
			Assert.Equal("{bogus} My name is Scott Hanselman and I'm cool.", foo);
		}


		[Fact]
		public void MakePersonFormattedStringWithVeryBadData()
		{
			Person p = new Person();
			string foo = p.ToString("{bogus:} My name is {FirstName} {LastName} and I'm cool.");
			Assert.Equal("{bogus:} My name is Scott Hanselman and I'm cool.", foo);
		}

		[Fact]
		public void MakePersonFormattedStringWithVeryBadFormattingData()
		{
			Person p = new Person();
			string foo = p.ToString("{BirthDate:poo} My name is {FirstName} {LastName} and I'm cool.");
			Assert.Equal("poo My name is Scott Hanselman and I'm cool.", foo);
		}


		[Fact]
		public void MakePersonFormattedStringWithQuestionableData()
		{
			Person p = new Person();
			string foo = p.ToString("{BirthDate:} My name is {FirstName} {LastName} and I'm cool.");
			Assert.Equal("1/22/1974 12:00:00 AM My name is Scott Hanselman and I'm cool.", foo);
		}

		[Fact]
		public void MakePersonFormattedStringWithSlightlyQuestionableData()
		{
			Person p = new Person();
			string foo = p.ToString("{{}} My name is {FirstName} {LastName} and I'm cool.");
			Assert.Equal("{{}} My name is Scott Hanselman and I'm cool.", foo);
		}


		/// <summary>
		/// This will likely pass only in the en-us culture
		/// </summary>
		[Fact]
		public void MakePersonFormattedStringWithFormat()
		{
			Person p = new Person();
			string foo = p.ToString("{BirthDate:D} My name is {FirstName} {LastName} and I'm cool.");
			Assert.Equal("Tuesday, January 22, 1974 My name is Scott Hanselman and I'm cool.", foo);
		}

		[Fact]
		public void MakePersonFormattedStringWithFormatAndCulture()
		{
			Person p = new Person();
			string foo = p.ToString("{BirthDate:D} My name is {FirstName} {LastName} and I'm cool.",System.Globalization.CultureInfo.InvariantCulture);
			Assert.Equal("Tuesday, 22 January 1974 My name is Scott Hanselman and I'm cool.", foo);
		}

		[Fact]
		public void MakePersonComplexFormattedStringWithFormatAndCulture()
		{
			Person p = new Person();
			string foo = p.ToString("{BirthDate:MM/dd/yy HH:mm:ss} My name is {FirstName} {LastName} and I'm cool.",System.Globalization.CultureInfo.InvariantCulture);
			Assert.Equal("01/22/74 00:00:00 My name is Scott Hanselman and I'm cool.", foo);
		}

		[Fact]
		public void MakePersonFormattedStringWithFormatAndChineseCulture()
		{
			Person p = new Person();
			string foo = p.ToString("{BirthDate:D} My name is {FirstName} {LastName} and I'm cool.",new System.Globalization.CultureInfo("zh-cn"));
			Assert.Equal("1974年1月22日 My name is Scott Hanselman and I'm cool.", foo);
		}

		[Fact]
		public void MakeSimplePersonFormattedString()
		{
			Person p = new Person();
			string foo = p.ToString("{LastName}, {FirstName}");

			Assert.Equal("Hanselman, Scott", foo);
		}

		[Fact]
		public void MakeSimplePersonFormattedStringWithDate()
		{
			Person p = new Person();
			string foo = p.ToString("{LastName}, {FirstName} {BirthDate}");
			Assert.Equal("Hanselman, Scott 1/22/1974 12:00:00 AM", foo);
		}

		[Fact]
		public void MakeSimplePersonFormattedStringWithDateAndOneBadPropertyName()
		{
			Person p = new Person();
			string foo = p.ToString("{LastName}, {ScottName} {BirthDate}");
			Assert.Equal("Hanselman, {ScottName} 1/22/1974 12:00:00 AM", foo);
		}

		[Fact]
		public void MakeSimplePersonFormattedStringWithDouble()
		{
			Person p = new Person();
			string foo = p.ToString("{Money} {LastName}, {ScottName} {BirthDate}");
			Assert.Equal("3.43 Hanselman, {ScottName} 1/22/1974 12:00:00 AM", foo);
		}

		/// <summary>
		/// This will likely pass only in the en-us culture
		/// </summary>
	[Fact]
	public void MakeSimplePersonFormattedStringWithDoubleFormatted()
	{
		Person p = new Person();
        string foo = p.ToString("{Money:C} {LastName}, {ScottName} {BirthDate}");
		Assert.Equal("$3.43 Hanselman, {ScottName} 1/22/1974 12:00:00 AM", foo);
	}

	[Fact]
	public void MakeSimplePersonFormattedStringWithDoubleFormattedInHongKong()
	{
		Person p = new Person();
		string foo = p.ToString("{Money:C} {LastName}, {ScottName} {BirthDate}",new System.Globalization.CultureInfo("zh-hk"));
		Assert.Equal("HK$3.43 Hanselman, {ScottName} 1/22/1974 12:00:00 AM", foo);
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
