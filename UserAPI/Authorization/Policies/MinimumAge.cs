using Microsoft.AspNetCore.Authorization;

namespace UserAPI.Authorization.Policies
{
    public class MinimumAge : IAuthorizationRequirement
    {
        public MinimumAge(int age)
        {
            this.Age = age;
        }

        public int Age { get; set; }
    }
}
