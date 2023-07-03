using Microsoft.AspNetCore.Identity;

namespace ChinookDataAccessLayer.Models;

// Add profile data for application users by adding properties to the ChinookUser class
public class ChinookUserData : IdentityUser
{
    public virtual ICollection<UserPlaylistData> UserPlaylists { get; set; }
}

