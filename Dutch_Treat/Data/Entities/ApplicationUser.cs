using Microsoft.AspNetCore.Identity;

namespace Dutch_Treat.Data.Entities
{
    public class ApplicationUser : IdentityUser<int>
    {
        public int Age { get; set; }
    }
}

/**
 * Examine Register
 * var result = await _userManager.CreateAsync(user, Input.Password);
 * Configure password requirements, lockout, username rules
 * builder.Services.Configure<IdentityOptions>(options => {
    options.Password.RequireDigit = true;
    ...
});
Configure identity cookies
builder.Services.ConfigureApplicationCookie(options => {
    options.LoginPath = "/Identity/Account/Login";
});

Add middleware
Order matters:
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

5. Register Workflow
When user submits registration:
RegisterModel.OnPostAsync() runs.
_userManager.CreateAsync() creates the user.
Sends an email confirmation link (default).
If confirmation is required, user is redirected to RegisterConfirmation.

Login Workflow
LoginModel.OnPostAsync():
Calls PasswordSignInAsync()

Logout Workflow
LogoutModel.OnPost runs when user submits the logout form:
await _signInManager.SignOutAsync();

Add Authorization /Test Identity
Protect a page using:
[Authorize]

Navigation properties
Example: Add navigation to UserClaims
public class ApplicationUser : IdentityUser
{
    public virtual ICollection<IdentityUserClaim<string>> Claims { get; set; }
builder.Entity<ApplicationUser>()
    .HasMany(u => u.Claims)
    .WithOne()
    .HasForeignKey(uc => uc.UserId)
    .IsRequired();
}
 */