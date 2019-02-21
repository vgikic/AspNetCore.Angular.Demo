using Portal.Models.Dto;
using System.Collections.Generic;

namespace Angular.Core.Models.Dto
{
    public class UserAuthDto
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string BearerToken { get; set; }
        public bool IsAuthenticated { get; set; }
        public IEnumerable<ClaimDto> Claims { get; set; }
        public bool IsInactive { get; set; }
        public bool IsLocked { get; set; }
        public bool InvalidCredentials { get; set; }
    }
}
