using Linker.Infrastructure;
using Linker.Models;
using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Linker
{
    public class ApiClient : IDisposable
    {
        private readonly HttpClient client;
        private readonly Guid projectId;

        public ApiClient(string host, string privateToken, Guid projectId)
        {
            this.projectId = projectId;
            client = new HttpClient();
            client.BaseAddress = new Uri(host);
            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("PrivateToken", privateToken);
        }

        public async Task CreateAndAddAutoTestToTestCase(int testCaseGlobalId, string externalName)
        {
            AutoTestModel[] autoTests = (await GetAutotestsFromProject(projectId)).Value;
            var autotest = autoTests.FirstOrDefault(a => a.ExternalId == externalName);
            if (autotest == null)
            {
                var autotestModel = new AutoTestModel
                {
                    ExternalId = externalName,
                    Name = externalName,
                    ProjectId = this.projectId
                };
                var response = await client.PostAsync("api/v2/autoTests", new JsonContent(autotestModel));
                autotest = (await Response.CreateValueResponse<AutoTestModel>(response)).Value;
                Console.WriteLine($"Autotest '{externalName}' created.");
            }
            else
                Console.WriteLine($"Autotest '{externalName}' existed.");

            await LinkAutoTestToTestCase(testCaseGlobalId, autotest.GlobalId);
        }

        private async Task LinkAutoTestToTestCase(int testCaseGlobalId, long autotestGlobalId)
        {
            string url = $"api/v2/autoTests/{autotestGlobalId}/workItems";
            StringContent content = new JsonContent(new { Id = testCaseGlobalId });
            await client.PostAsync(url, content);

            Console.WriteLine($"Linking Case #{testCaseGlobalId} to autotest #{autotestGlobalId}" + Environment.NewLine);
        }

        private async Task<Response<AutoTestModel[]>> GetAutotestsFromProject(Guid projectId)
        {
            string url = "api/v2/autoTests?projectId=" + projectId;
            var response = await client.GetAsync(url);
            return await Response.CreateValueResponse<AutoTestModel[]>(response);
        }

        public void Dispose()
        {
            client?.Dispose();
        }
    }
}
