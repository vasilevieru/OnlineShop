using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Application.Files.Commands;
using OnlineShop.Application.Files.Queries;
using OnlineShop.Application.Interfaces;
using OnlineShop.WebApi.Extensions;

namespace OnlineShop.WebApi.Controllers
{
    [Route("api/files/images")]
    public class UploadFilesController : BaseController
    {
        private readonly IImageFileService _imageFileService;
        private readonly IMediator _mediator;

        public UploadFilesController(
            IImageFileService imageFileService,
            IMediator mediator)
        {
            _imageFileService = imageFileService;
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        //[RoleAuthorize(Role.Administrator)]
        public async Task<IActionResult> GetImageById(int id)
        {
            var file = await _mediator.Send(new GetFileQuery { Id = id });

            if (file == null || !file.MimeType.StartsWith("image"))
            {
                return NotFound();
            }

            return Ok(file);
        }

        [HttpGet("product/{productId}")]
        public async Task<IActionResult> GetAllProductImages(int productId)
        {
            var files = await Mediator.Send(new GetFilesQuery { ProductId = productId });

            return Ok(files);
        }


        // GET: api/files/images/{id}/blob
        // Used in: Employees profile edit to load current profile image
        //          Projects for loadind current logo on edit
        [HttpGet("{id}/blob")]
        [RoleAuthorize(Role.Administrator)]
        [ResponseCache(NoStore = true)]
        public Task<IActionResult> GetImageBlobById(int id)
        {
            return GetImageBlobById(id, false);
        }

        // GET: api/files/images/{id}/thumbnail/blob
        // Used in: Employees profile edit to load current profile image
        //          Projects for loadind current logo on edit
        [HttpGet("{id}/thumbnail/blob")]
        [RoleAuthorize(Role.Administrator)]
        [ResponseCache(NoStore = true)]
        public Task<IActionResult> GetImageThumbnailBlobById(int id)
        {
            return GetImageBlobById(id, true);
        }
        
        // POST: api/files/images
        // Used in: Employees add to load new profile image
        //          Projects add for loading new logo on edit
        [HttpPost]
        //[RoleAuthorize(Role.Administrator)]
        public async Task<IActionResult> UploadImage([FromForm]IFormCollection formCollection)
        {
            if (formCollection == null || formCollection.Files.Count < 1)
                return BadRequest();

            IFormFile formFile = formCollection.Files.First();

            string uploadedImagePath = await _imageFileService.UploadAsync(formFile);

            var result = await Mediator.Send(
                new CreateFileCommand
                {
                    Name = formFile.FileName,
                    MimeType = formFile.ContentType,
                    Path = uploadedImagePath
                });
            
            return CreatedAtAction(nameof(GetImageById), new { id = result.Id }, result);
        }

        // PUT: api/files/images/{id}
        // Used in: Employees edit to update profile image
        //          Projects for updating logo on edit
        [HttpPut("{id}")]
        [RoleAuthorize(Role.Administrator)]
        public async Task<IActionResult> UpdateImage(int id, [FromForm]IFormCollection formCollection)
        {
            if (formCollection == null || formCollection.Files.Count < 1)
                return BadRequest();

            var file = await _mediator.Send(new GetFileQuery { Id = id });

            if (file == null)
                return BadRequest();

            IFormFile formFile = formCollection.Files.First();

            await _imageFileService.DeleteAsync(file.Path);

            // throws exception if formFile is null
            string uploadedImagePath = await _imageFileService.UploadAsync(formFile);

            var result = await Mediator.Send(
                new UpdateFileCommand
                {
                    Id = file.Id,
                    Name = formFile.FileName,
                    MimeType = formFile.ContentType,
                });

            return Ok(result);
        }

        // DELETE: api/files/images/{id}
        // Used in: Employees on edit to delete profile image
        //          Projects for deleting logo on project edit
        [HttpDelete("{id}")]
        [RoleAuthorize(Role.Administrator)]
        public async Task<IActionResult> DeleteImage(int id)
        {
            var file = await _mediator.Send(new GetFileQuery { Id = id });

            if (file == null || !file.MimeType.StartsWith("image"))
            {
                return NotFound();
            }

            var command = new DeleteImageFileCommand { Id = id };

            await _mediator.Send(command);

            return NoContent();
        }

        private async Task<IActionResult> GetImageBlobById(int id, bool isThumbnail)
        {
            var file = await _mediator.Send(new GetFileQuery { Id = id });

            if (file == null || !file.MimeType.StartsWith("image"))
            {
                return NotFound();
            }

            var fs = isThumbnail
                ? await _imageFileService.OpenReadThumbnailAsync(file.Path)
                : await _imageFileService.OpenReadAsync(file.Path);

            return File(fs, file.MimeType, file.Name);
        }
    }
}