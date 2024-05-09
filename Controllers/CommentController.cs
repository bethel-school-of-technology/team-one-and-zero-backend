using TEAM_ONE_AND_ZERO_BACKEND.Models;
using TEAM_ONE_AND_ZERO_BACKEND.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
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

        [HttpPost]
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
        public ActionResult<Comment> UpdateComment(Comment comment){
            if(!ModelState.IsValid){
                return BadRequest();
            }

            return Ok(_commentRepository.UpdateComment(comment));
        }

        [HttpDelete]
        [Route("{commentId: int}")]
        public ActionResult DeleteComment(int commentId){
            _commentRepository.DeleteComment(commentId);
            return NoContent();
        }
    }
}

