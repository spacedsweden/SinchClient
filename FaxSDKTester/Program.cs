using Sinch;
using Sinch.FaxApi;
using Sinch.FaxApi.Models;

namespace FaxSDKTester
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            /// using it in non asp.net project, like a worker or similar where you dont have the asp.net DI
            /// Unlike twilio we wont force a static implementation, but rather just give you a way to create a client and you choose how 
            /// you want to handle the life of it. 
            /// We should also either have a aspnetcore package where we support services.AddSinch(options=>{
            /// projectId="sadfasdfsadfsafaf", key="key", secret="secret"
            /// })
            var sinch = new SinchClient("sadfasdfsadfsafaf", "key", "secret");
            //create and send a fax, we handle file read etc
            // I am a bit conflicted on the API in the name here, but if we dont add api, it becomes a wholelot of Fax, faxes,fax
            //Also it is not meant to really reflect api structure here, its meant to be easy to navigate with no docs. 
            var fax = await sinch.FaxApi.Faxes.Send("+15612600684", "+15612600684", "c:\tempfiles\faxcontent.pdf");
            //got a file stream?
            Stream stream = new MemoryStream(); //pretend we have a file in this, 
            //var fax2 = await sinch.FaxApi.Faxes.Send("+15612600684", "+15612600684", stream);

            //more options
            var fax3 = await sinch.FaxApi.Faxes.Send(new FaxOptions { 
                To = "+15612600684", 
                From = "+15612600684",
                ImageConversionMethod = ImageConversionMethod.MONOCHROME
            }, stream);

            //otehr methods
            await sinch.FaxApi.Faxes.Delete("faxid");

            ////how would such pseudo feel with i.e sms
            //var sms = sinch.SmsApi.Messages.Send("+15612600684", "+15612600684", "Message from sinch");
            ////how would such pseudo feel with i.e voice
            //var call = sinch.VoiceApi.Calls.Call("+15612600684", "+15612600684", "Text to speech text here");

            /// so this is different from twilio in one significant way. 
            /// Twilio skips the whole API name space thing, what you do 
            /// you work on resource of whatever type, ie MessageResource 
            /// MessageResource.Create()
            /// MessageResource.Read() 
            /// all as static methods, that expect that you have initiated a twilio client before.
            /// TwilioClient.Init(accountSid, authToken);
            /// var messages = MessageResource.Read(limit: 20);
            /// To me that feels more error prone, it does look nicer with shorter code samples but a bit to easy make errors

        }
    }
}