using TEAM_ONE_AND_ZERO_BACKEND.Models;

namespace TEAM_ONE_AND_ZERO_BACKEND.Repositories;

public interface ICommentRepository
{
    IEnumerable<Comment> GetAllComments();
    Comment? GetComment(int commentId); 
    Comment CreateComment(Comment newComment);
    Comment? UpdateComment(Comment newComment);
    void DeleteComment(int commentId);
}