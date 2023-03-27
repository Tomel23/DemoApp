using Demo.Application.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using Demo.Infrastructure.Database;
using Demo.Infrastructure.Database.Entries;
using Demo.Infrastructure.Database.Repositories;

namespace Demo.Test.Unit
{
    public class DemoServiseTest
    {
        private readonly IConfiguration _configuration;
        public DemoServiseTest()
        {
            _configuration = new ConfigurationBuilder()
                                        .AddJsonFile("appsettings.json")
                                        .Build();
        }

        [Fact]
        public async void GetDemoPath_ShouldReturnNotEmptyListOfStrings()
        {
            //arrrange
            using (DemoContext context = new DemoContext())
            {
                if (context.DbTokens.Any())
                    context.DbTokens.First().AccessToken = "eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzUxMiJ9.eyJzYWx0IjoiRG9tT1ZRdXl2cVpkelBMeiIsImNsaWVudF9pZCI6IlBvcnRhbENsb3VkQVBJX0YxNjQ1RURGMEFCRTRDQkJBOUQ1MTM1MzE0MTE3NjE5IiwidHlwZSI6ImNsaWVudF90b2tlbiIsInNjb3BlIjoiVVNFUkFQSSIsImlhdCI6MTY3OTYyNTU3OSwiZXhwaXJlc19pbiI6MzYwMCwiZXhwIjoxNjc5NjI5MTc5LCJuYmYiOjE2Nzk2MjU1Nzl9.DWRvG-LUonlTxxat_oLroyvJQHfSdb7cmAAty3QXulQRriWgVr9EhVsYcoQddF8Rig1TKdSPtTx55pk5uBHXrTGabBKVMIqxCgr5Q6Ag46rmitFRQPaShz0epxFlUToJHoiP0PcwI5O20_U5BI8NcPTMMFuxYN9Uwi7hgh9i_0OtbKfb7avPWXY3XdmTfIgF0HIjYtGY7t17i17lIcaU09dzmgFXU3RNTnI93TDzlc9oIQXaV2xjEIdBQuNiplw_xesbWvau-I_0EASKNv3xNSCVct0RTESXgcDagjD9KBZyBtgwjhXEtvSOgfJGWM7GIwvRp8Ovj-SUsL2JHd0QpIzHC0lCRIaUE3Qq6idaPGzqceA3-2GDyWhXkzvsDDBJoa67agNSctj5_SeVQ2TqgXOrzPDqX131kU9hnGD_HIgGjbtKHZYrwWE1v9JQIgE2zO1AEIHbqrltb0SnailQWqv8tqH20Zcx785L2089n7oTiRSMU3Vjdpz2AnkEVBWxfR9_9lVyFyAsCjE2BTyhtK7cHxatdfyMQwqiJa640Cs-UF7_XBRpq-cR3dgDMyAdMB9iVdlxRrft_tXCHulzCe02lSi6iEf9irmcIxv-kZJYMtLnB_JLehxrIrExmfYSaxOC8fuhY_CY8rlN2Jxi09Ri3K7yJKRR__gS87QTOHk";
                else
                    context.DbTokens.Add(new AuthenticationToken()
                    {
                        Id = Guid.NewGuid(),
                        AccessToken = "eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzUxMiJ9.eyJzYWx0IjoiRG9tT1ZRdXl2cVpkelBMeiIsImNsaWVudF9pZCI6IlBvcnRhbENsb3VkQVBJX0YxNjQ1RURGMEFCRTRDQkJBOUQ1MTM1MzE0MTE3NjE5IiwidHlwZSI6ImNsaWVudF90b2tlbiIsInNjb3BlIjoiVVNFUkFQSSIsImlhdCI6MTY3OTYyNTU3OSwiZXhwaXJlc19pbiI6MzYwMCwiZXhwIjoxNjc5NjI5MTc5LCJuYmYiOjE2Nzk2MjU1Nzl9.DWRvG-LUonlTxxat_oLroyvJQHfSdb7cmAAty3QXulQRriWgVr9EhVsYcoQddF8Rig1TKdSPtTx55pk5uBHXrTGabBKVMIqxCgr5Q6Ag46rmitFRQPaShz0epxFlUToJHoiP0PcwI5O20_U5BI8NcPTMMFuxYN9Uwi7hgh9i_0OtbKfb7avPWXY3XdmTfIgF0HIjYtGY7t17i17lIcaU09dzmgFXU3RNTnI93TDzlc9oIQXaV2xjEIdBQuNiplw_xesbWvau-I_0EASKNv3xNSCVct0RTESXgcDagjD9KBZyBtgwjhXEtvSOgfJGWM7GIwvRp8Ovj-SUsL2JHd0QpIzHC0lCRIaUE3Qq6idaPGzqceA3-2GDyWhXkzvsDDBJoa67agNSctj5_SeVQ2TqgXOrzPDqX131kU9hnGD_HIgGjbtKHZYrwWE1v9JQIgE2zO1AEIHbqrltb0SnailQWqv8tqH20Zcx785L2089n7oTiRSMU3Vjdpz2AnkEVBWxfR9_9lVyFyAsCjE2BTyhtK7cHxatdfyMQwqiJa640Cs-UF7_XBRpq-cR3dgDMyAdMB9iVdlxRrft_tXCHulzCe02lSi6iEf9irmcIxv-kZJYMtLnB_JLehxrIrExmfYSaxOC8fuhY_CY8rlN2Jxi09Ri3K7yJKRR__gS87QTOHk",
                        TokenType = "bearer",
                        ExpiresIn = 3600,
                        Scope = "USERAPI",
                        CreationDate = DateTimeOffset.UtcNow
                    });
                await context.SaveChangesAsync();
                var _demoDataServiceLogger = new Mock<ILogger<DemoDataService>>();
                var _authenticationLogger = new Mock<ILogger<AuthenticationService>>();
                var _authenticationTokenRepositoryLogger = new Mock<ILogger<AuthenticationTokenRepository>>();
                var _authenticationTokenRepository = new AuthenticationTokenRepository(context, _authenticationTokenRepositoryLogger.Object);
                var _authenticatonService = new AuthenticationService(_authenticationLogger.Object, _authenticationTokenRepository);

                var _demoDataService = new DemoDataService(_configuration, _authenticatonService, _demoDataServiceLogger.Object);

                //acct
                var result = await _demoDataService.GetDemoPath();

                //assert

                Assert.NotNull(result);
                Assert.IsType<HashSet<string>>(result);
                Assert.NotEmpty(result);
            }
        }
    }
}
