using bee_healthy_backend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace bee_healthy_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistryController : ControllerBase
    {
        [HttpPost]

        public async Task<IActionResult> Registry(User user)
        {
            using (var cx = new BeeHealthyContext())
            {
                try
                {
                    if (cx.Users.FirstOrDefault(f => f.LoginNev == user.LoginNev) != null)
                    {
                        return Ok("Már létezik ilyen felhasználónév!");
                    }
                    if (cx.Users.FirstOrDefault(f => f.Email == user.Email) != null)
                    {
                        return Ok("Ezzel az e-mail címmel már regisztráltak!");
                    }
                    user.PermissionId = 1;
                    user.Active = false;
                    user.Hash = Program.CreateSHA256(user.Hash);
                    await cx.Users.AddAsync(user);
                    await cx.SaveChangesAsync();

                    Program.SendEmail(user.Email, "Regisztráció", $"https://localhost:7280/api/Registry?loginNev={user.LoginNev}&email={user.Email}");

                    return Ok("Sikeres regisztráció. Fejezze be a regisztrációját az e-mail címére küldött link segítségével!");
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
        }

        [HttpGet]

        public async Task<IActionResult> EndOfTheRegistry(string loginNev, string email)
        {
            using (var cx = new BeeHealthyContext())
            {
                User user = await cx.Users.FirstOrDefaultAsync(f => f.LoginNev == loginNev && f.Email == email);
                if (user == null)
                {
                    return Ok("Sikertelen a regisztráció befejezése!");
                }
                else
                {
                    user.Active = true;
                    cx.Users.Update(user);
                    await cx.SaveChangesAsync();
                    return Ok("A regisztráció befejezése sikeresen megtörtént.");
                }
            }
            return null;
        }

    }
}
