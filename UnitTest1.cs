using Newtonsoft.Json;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using RestSharp;
using RestSharp.Serialization.Json;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using System;
using System.Net;

namespace CrawfordTest
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            //Browser driver
            IWebDriver webDriver = new ChromeDriver();

            //Navigate to site
            webDriver.Navigate().GoToUrl("http://www.crawco.co.uk");

            //Find Facebook Link
            IWebElement lnkFaceBook = webDriver.FindElement(By.XPath("//a[@title='Crawford on Facebook']"));

            //setting the URL value to a string
            string fBURL = lnkFaceBook.GetAttribute("href");
            string testURL = "https://www.facebook.com/crawfordandco";

            //Assertion
            Assert.AreEqual(fBURL, testURL);
           

        }
        [Test]
        public void Test2()
        {
            //Arrange
            RestClient client = new RestClient("https://reqres.in/api");
            RestRequest request = new RestRequest("users/4", Method.GET);

            //Action
            IRestResponse response = client.Execute(request);
          

            //Converting Iresponse to JSOn Object
            JObject joResponse = JObject.Parse(response.Content);
            JObject ojObject = (JObject)joResponse["data"];
            
            string userFirstName = Convert.ToString(ojObject["first_name"]);
            string testFirstName = "Eve";
            string userLastName = Convert.ToString(ojObject["last_name"]);
            string testLastName = "Holt";
            
            // Assertion
            Assert.That(userFirstName, Is.EqualTo(testFirstName));
            Assert.That(userLastName, Is.EqualTo(testLastName));
        }

        [Test]
        public void Test3()
        {
            //Arrange
            RestClient client = new RestClient("https://reqres.in/api");
            RestRequest request = new RestRequest("users/6", Method.GET);

            //Action
            IRestResponse response = client.Execute(request);
           

            //Converting Iresponse to JSOn Object
            JObject joResponse = JObject.Parse(response.Content);
            JObject ojObject = (JObject)joResponse["data"];

            string userFirstName = Convert.ToString(ojObject["first_name"]);
            string testFirstName = "Sergio";
            string userLastName = Convert.ToString(ojObject["last_name"]);
            string testLastName = "Ramos";

            // Assertion
            //Note this test will fail because the user's firstname in the API is Tracy and Sergio
            Assert.That(userFirstName, Is.EqualTo(testFirstName));
            Assert.That(userLastName, Is.EqualTo(testLastName));
        }

        [Test]
        public void Test4()
        {
            //Arrange
            RestClient client = new RestClient("https://reqres.in/api");
            RestRequest request = new RestRequest("users/23", Method.GET);

            //Action
            IRestResponse response = client.Execute(request);
           

            //Assertion
            //The Http Status Code.NotFound = 404
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
        }
       
       
    }
}
