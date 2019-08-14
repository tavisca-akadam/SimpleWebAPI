using System;
using Xunit;
using SimpleWebAPI;
using SimpleWebAPI.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace SimpleWebAPITest
{
    public class GetRequestFixture
    {
        private MessageController messageController = new MessageController();
        [Fact]
        public void Test_message_controller_object()
        {
            Assert.IsType<MessageController>(messageController);
        }
        [Fact]
        public void Test_get_valid_message()
        {
            ActionResult<string> actualResult = messageController.Get("Hello");
            Assert.Equal("hi", actualResult);
        }
        [Fact]
        public void Test_get_invalid_message()
        {
            ActionResult<string> actualResult = messageController.Get("anil");
            Assert.Equal("Invalid token", actualResult);
        }
    }
}
