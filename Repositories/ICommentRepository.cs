using TEAM_ONE_AND_ZERO_BACKEND.Models;

namespace TEAM_ONE_AND_ZERO_BACKEND.Repositories;

public interface ICommentRepository
{
    IEnumerable<Comment> GetAllComments();
    Task<IEnumerable<Comment?>> GetCommentsByUsername(string username);
    Task<IEnumerable<Comment?>> GetCommentsBySongID(string songId);
    Comment? GetComment(int commentId); 
    Comment CreateComment(Comment newComment);
    Comment? UpdateComment(Comment newComment);
    void DeleteComment(int commentId);
}