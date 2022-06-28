using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace PWoLi.Web.Data
{
    public class WeatherForecastService
    {
        private IHttpContextAccessor _httpContextAccessor;

        public WeatherForecastService(Microsoft.AspNetCore.Http.IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        private static string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        public Task<WeatherForecast[]> GetForecastAsync(DateTime startDate)
        {
            //  PrincipalContext ctx = new PrincipalContext(ContextType.Domain);

            //   GroupPrincipal group = GroupPrincipal.FindByIdentity(ctx, "GI-LDAT_R_NJROS1BD0471_DEV");

            var user = _httpContextAccessor.HttpContext.User;

            if (user.IsInRole("GI-LDAT_R_NJROS1BD0471_DEV"))
            {
                var user1 = _httpContextAccessor.HttpContext.User;
            }
            //foreach (var item in (_httpContextAccessor.HttpContext.User.Identity as WindowsIdentity).Groups)
            //{
            //    var g = item.Value;
            //}

            var rng = new Random();
            return Task.FromResult(Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = startDate.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                //Summary = Summaries[rng.Next(Summaries.Length)]
                Summary = _httpContextAccessor.HttpContext.User.Identity.Name
            }).ToArray());
            ;
        }
    }
}