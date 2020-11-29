using System.Threading.Tasks;
using System;

namespace Linker
{
    class Program
    {
        public static readonly string Domain = "https://demo.testit.software/";
        public static readonly string PrivateToken = "OGl6MnVTNzNXQXEyQm9RTUNo";
        public static readonly Guid ProjectId = new Guid("5236eb3f-7c05-46f9-a609-dc0278896464");

        static async Task Main()
        {
            ApiClient client = new ApiClient(Domain, PrivateToken, ProjectId);
            await AddAutoTestToTestCaseAsync(client);
            Console.WriteLine("Open https://demo.testit.software/projects/4/autotests to view new autotests.");
        }

        private static async Task AddAutoTestToTestCaseAsync(ApiClient client)
        {
            await client.CreateAndAddAutoTestToTestCase(16, "Login_CorrectСredentials_SuccessfulAuthorization");
            await client.CreateAndAddAutoTestToTestCase(16, "Login_IncorrectСredentials_AuthorisationError");
            await client.CreateAndAddAutoTestToTestCase(16, "Login_WithoutСredentials_LoginButtonDisabled");

            await client.CreateAndAddAutoTestToTestCase(15, "CreateConfiguration");
            await client.CreateAndAddAutoTestToTestCase(12, "CreateConfiguration");

            await client.CreateAndAddAutoTestToTestCase(19, "SearchProject");
        }
    }
}
