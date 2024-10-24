using Microsoft.AspNetCore.Http;

namespace Portfolio.Service.Helpers;

public static class HtppContextHelper
{
    private static IHttpContextAccessor _httpContextAccessor;

    public static void Configure(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public static long? GetUserId()
    {
        var userIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirst("Id")?.Value;
        return userIdClaim != null ? Convert.ToInt64(userIdClaim) : (long?)null;
    }
}
