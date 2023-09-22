using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SixLabors.ImageSharp.Formats.Webp;
using Twitter_Clone.Data;

namespace Twitter_Clone.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class ImagesController : ControllerBase
{
    private readonly TwitterCloneDb _dbContext;
    private readonly IWebHostEnvironment _hostEnvironment;

    public ImagesController(TwitterCloneDb dbContext, IWebHostEnvironment hostEnvironment)
    {
        _dbContext = dbContext;
        _hostEnvironment = hostEnvironment;
    }

    [HttpPost("{username}/images")]
    public async Task<IActionResult> UploadImage(string username, IFormFile media)
    {
        try
        {
            if (media == null || media.Length == 0) return BadRequest("Problem with the image, upload again.");

            var imagesPath = Path.Combine(_hostEnvironment.WebRootPath, "user", username, "images", "profile-photo");

            if (!Directory.Exists(imagesPath)) Directory.CreateDirectory(imagesPath);

            var newFileName = Guid.NewGuid() + ".webp";

            var relativePath = Path.Combine("user", username, "images", "profile-photo", newFileName);

            var imagePath = Path.Combine(_hostEnvironment.WebRootPath, relativePath);

            using var image = await Image.LoadAsync(media.OpenReadStream());
            image.Mutate(img => img.Resize(new ResizeOptions
                {
                    Mode = ResizeMode.Max, Size = new Size(1920, 0)
                })
            );

            await image.SaveAsWebpAsync(imagePath, new WebpEncoder
            {
                Quality = 100
            });


            var user = await _dbContext.Users.Where(u => u.Username == username).SingleAsync();

            if (user.ProfileImagePath != null)
            {
                var oldImagePath = Path.Combine(_hostEnvironment.WebRootPath, user.ProfileImagePath);

                if (System.IO.File.Exists(oldImagePath)) System.IO.File.Delete(oldImagePath);
            }

            user.ProfileImagePath = relativePath;
            await _dbContext.SaveChangesAsync();

            return Ok(new { message = "Image uploaded successfully!" });
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}