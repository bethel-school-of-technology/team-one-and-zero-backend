using TEAM_ONE_AND_ZERO_BACKEND.Models;
using TEAM_ONE_AND_ZERO_BACKEND.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace TEAM_ONE_AND_ZERO_BACKEND.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CommentController : ControllerBase
    {
        private readonly ILogger<CommentController> _logger;
        private readonly ICommentRepository _commentRepository;
        public CommentController(ILogger<CommentController> logger, ICommentRepository repository)
        {
            _logger = logger;
            _commentRepository = repository;
        }
        [HttpGet]
        public ActionResult<IEnumerable<Comment>> GetComments()
        {
            return Ok(_commentRepository.GetAllComments());
        }
        [HttpGet]
        [Route("{commentId: int}")]
        public ActionResult<Comment> GetCommentById(int commentId){
            var comment = _commentRepository.GetComment(commentId);
            if(comment == null)
            {
                return NotFound();
            }
            return Ok(comment);
        }
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public ActionResult<Comment> CreateComment(Comment comment)
        {
            if(!ModelState.IsValid){
                return BadRequest();
            }
            var newComment = _commentRepository.CreateComment(comment);
            return Created(nameof(GetComments), newComment);
        }
        [HttpPut]
        [Route("{commentId: int}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public ActionResult<Comment> UpdateComment(Comment comment){
            if(!ModelState.IsValid){
                return BadRequest();
            }
            return Ok(_commentRepository.UpdateComment(comment));
        }
        [HttpDelete]
        [Route("{commentId: int}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public ActionResult DeleteComment(int commentId){
            _commentRepository.DeleteComment(commentId);
            return NoContent();
        }
    }
}