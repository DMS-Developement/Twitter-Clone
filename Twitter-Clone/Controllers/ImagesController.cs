using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;
using SixLabors.ImageSharp.Formats.Webp;
using Supabase;
using Twitter_Clone.Data;
using FileOptions = Supabase.Storage.FileOptions;

namespace Twitter_Clone.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class ImagesController : ControllerBase
{
    private readonly TwitterCloneDb _dbContext;
    private readonly IWebHostEnvironment _hostEnvironment;
    private readonly Client _supabaseClient;

    public ImagesController(TwitterCloneDb dbContext, IWebHostEnvironment hostEnvironment, Client supabaseClient)
    {
        _dbContext = dbContext;
        _hostEnvironment = hostEnvironment;
        _supabaseClient = supabaseClient;
    }

    [HttpGet("{username}/profile-photo")]
    public async Task<IActionResult> RetrieveProfilePic(string username)
    {
        var user = await _dbContext.Users.Where(u => u.Username == username).SingleAsync();

        if (user == null) return BadRequest("User not found");

        if (user.ProfileImagePath == null) return BadRequest("User has no profile photo");

        var bucketName = "twitter-clone-storage";
        var supabasePath = user.ProfileImagePath;

        var result = await _supabaseClient.Storage.From(bucketName)
            .Download(supabasePath, null);

        if (result == null) return BadRequest("Problem with the image, upload again.");

        var mimeType = GetContentType(supabasePath);

        return new FileContentResult(result, mimeType)
        {
            FileDownloadName = Path.GetFileName(supabasePath)
        };
    }

    [HttpPost("{username}/profile-photo")]
    public async Task<IActionResult> UploadProfilePic(string username, IFormFile media)
    {
        try
        {
            if (media == null || media.Length == 0) return BadRequest("Problem with the image, upload again.");

            var tempImageDir = Path.Combine(_hostEnvironment.WebRootPath, "temp-images");

            if (!Directory.Exists(tempImageDir)) Directory.CreateDirectory(tempImageDir);

            var newFileName = Guid.NewGuid() + ".webp";

            var tempImagePath = Path.Combine(tempImageDir, newFileName);

            using var image = await Image.LoadAsync(media.OpenReadStream());
            image.Mutate(img => img.Resize(new ResizeOptions
                {
                    Mode = ResizeMode.Max, Size = new Size(1920, 0)
                })
            );

            await image.SaveAsWebpAsync(tempImagePath, new WebpEncoder
            {
                Quality = 100
            });

            var bucketName = "twitter-clone-storage";
            var supabasePath = $"user/{username}/images/profile-photo/{newFileName}";

            var result = await _supabaseClient.Storage.From(bucketName)
                .Upload(tempImagePath, supabasePath, new FileOptions { CacheControl = "3600", Upsert = false });

            if (result == null) return BadRequest("Problem with the image, upload again.");

            var user = await _dbContext.Users.Where(u => u.Username == username).SingleAsync();

            if (user == null) return BadRequest("User not found");

            user.ProfileImagePath = supabasePath;
            await _dbContext.SaveChangesAsync();

            System.IO.File.Delete(tempImagePath);

            return Ok(new { message = "Image uploaded successfully!" });
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    private string GetContentType(string path)
    {
        var provider = new FileExtensionContentTypeProvider();
        string contentType;
        if (!provider.TryGetContentType(path, out contentType)) contentType = "application/octet-stream";

        return contentType;
    }
}